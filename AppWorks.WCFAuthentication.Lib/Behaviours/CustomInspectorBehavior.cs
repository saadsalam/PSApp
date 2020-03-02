using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace AppWorks.WCFAuthentication.Lib.Behaviours
{
    public class CustomInspectorBehavior : Attribute, IDispatchMessageInspector, IClientMessageInspector, IEndpointBehavior, IServiceBehavior, IErrorHandler, IContractBehavior
    {
        /// <summary>
        /// This method check whether ther is a need to append the auth header in the current request
        /// </summary>
        /// <returns></returns>
        private bool IsNeedToAppendHeader(Message request)
        {
            //Get action name and if it is not a login then add the Auth Header
            var actionName = request.Headers.Action.Substring(request.Headers.Action.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1);

            //Don't need to append the header as this is the first time client is logging in
            if (actionName.Equals("validatelogin", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region IDispatchMessageInspector

        /// <summary>
        /// Just after the request has arrives on the server
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (IsNeedToAppendHeader(request))
            {
                // Read the custom context data from the headers
                ServiceHeader header = CustomHeader.ReadHeader(request);

                if (header == null)
                { throw new FaultException("The request is invalid. Authorization header could not be found."); }

                if (string.IsNullOrWhiteSpace(header.UserToken))
                { throw new FaultException("Authorization token value could not be found in service header."); }

                //check if supplied header and current service session ID matches
                //if not then throw the exception
                if (!OperationContext.Current.SessionId.Equals(header.UserToken, StringComparison.OrdinalIgnoreCase))
                { throw new FaultException("Invalid User token."); }
            }

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            OperationContext.Current.Extensions.Remove(ServerContext.Current);
        }
        #endregion

        #region IClientMessageInspector
        /// <summary>
        ///  Just before the response leaves the server
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var actionName = request.Headers.Action.Substring(request.Headers.Action.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1);

            //if action name is login then allow the request to the service
            if (actionName.Equals("validatelogin", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            ServiceHeader customData = new ServiceHeader();

            customData.UserToken = ClientContext.UserToken;

            CustomHeader header = new CustomHeader(customData);

            request.Headers.Add(header);

            return request;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (reply.IsFault)
            {
                // Create a copy of the original reply to allow default processing of the message
                MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
                Message copy = buffer.CreateMessage();
                reply = buffer.CreateMessage();

                var exception = ReadExceptionFromFaultDetail(copy) as Exception;
                if (exception != null)
                {

                    //if (exception.InnerException != null && exception.InnerException.InnerException != null && exception.InnerException.InnerException.Message != null)
                    //{
                    //    if (exception.InnerException.Message.Equals("A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 0 - The wait operation timed out.)") && exception.InnerException.InnerException.Message.Equals("The wait operation timed out"))
                    //        throw exception;
                    //}
                }
            }
        }

        private static object ReadExceptionFromFaultDetail(Message reply)
        {
            const string detailElementName = "detail";

            using (XmlDictionaryReader reader = reply.GetReaderAtBodyContents())
            {
                // Find <soap:Detail>
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element &&
                        detailElementName.Equals(reader.LocalName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return ReadExceptionFromDetailNode(reader);
                    }
                }
                // Couldn't find it!
                return null;
            }
        }

        private static object ReadExceptionFromDetailNode(XmlDictionaryReader reader)
        {
            // Move to the contents of <soap:Detail>
            if (!reader.Read())
            {
                return null;
            }

            // Return the deserialized fault
            try
            {
                NetDataContractSerializer serializer = new NetDataContractSerializer();
                return serializer.ReadObject(reader);
            }
            catch (SerializationException)
            {
                return null;
            }
        }

        #endregion

        #region IEndpointBehavior

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            var inspector = new CustomInspectorBehavior();
            clientRuntime.MessageInspectors.Add(inspector);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            var channelDispatcher = endpointDispatcher.ChannelDispatcher;
            if (channelDispatcher == null) return;
            foreach (var ed in channelDispatcher.Endpoints)
            {
                var inspector = new CustomInspectorBehavior();
                ed.DispatchRuntime.MessageInspectors.Add(inspector);
            }
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {

        }


        #endregion

        #region IServiceBehavior

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var eDispatcher in cDispatcher.Endpoints)
                {
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomInspectorBehavior());
                }
            }
        }
        #endregion

        #region IErrorHandler

        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is FaultException)
            {
                // Let WCF do normal processing
            }
            else
            {
                // Generate fault message manually including the exception as the fault detail
                MessageFault messageFault = MessageFault.CreateFault(new FaultCode("Sender"), new FaultReason(error.Message), error, new NetDataContractSerializer());
                fault = Message.CreateMessage(version, messageFault, null);
            }
        }

        #endregion

        #region IContractBehavior

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ApplyClientBehavior(clientRuntime);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            ApplyDispatchBehavior(dispatchRuntime.ChannelDispatcher);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region Behavior helpers

        private static void ApplyClientBehavior(System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            foreach (IClientMessageInspector messageInspector in clientRuntime.MessageInspectors)
            {
                if (messageInspector is CustomInspectorBehavior)
                {
                    return;
                }
            }

            clientRuntime.MessageInspectors.Add(new CustomInspectorBehavior());
        }

        private static void ApplyDispatchBehavior(System.ServiceModel.Dispatcher.ChannelDispatcher dispatcher)
        {
            // Don't add an error handler if it already exists
            foreach (IErrorHandler errorHandler in dispatcher.ErrorHandlers)
            {
                if (errorHandler is CustomInspectorBehavior)
                {
                    return;
                }
            }

            dispatcher.ErrorHandlers.Add(new CustomInspectorBehavior());
        }

        #endregion

    }
}
