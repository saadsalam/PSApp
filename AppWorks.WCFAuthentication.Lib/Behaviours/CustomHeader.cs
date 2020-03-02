using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;

namespace AppWorks.WCFAuthentication.Lib.Behaviours
{
    [DataContract]
    public class ServiceHeader
    {
        [DataMember]
        public string UserToken { get; set; }
    }

    public class CustomHeader : MessageHeader
    {
        private const string CUSTOM_HEADER_NAME = "AuthorizationHeader";
        private const string CUSTOM_HEADER_NAMESPACE = "TokenNameSpace";

        private ServiceHeader _customData;

        public ServiceHeader CustomData
        {
            get
            {
                return _customData;
            }
        }

        public CustomHeader()
        {
        }

        public CustomHeader(ServiceHeader customData)
        {
            _customData = customData;
        }

        public override string Name
        {
            get { return (CUSTOM_HEADER_NAME); }
        }

        public override string Namespace
        {
            get { return (CUSTOM_HEADER_NAMESPACE); }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ServiceHeader));
            StringWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, _customData);
            textWriter.Close();

            string text = textWriter.ToString();

            writer.WriteElementString(CUSTOM_HEADER_NAME, CUSTOM_HEADER_NAMESPACE, text.Trim());
        }

        public static ServiceHeader ReadHeader(Message request)
        {
            int headerPosition = request.Headers.FindHeader(CUSTOM_HEADER_NAME, CUSTOM_HEADER_NAMESPACE);

            if (headerPosition == -1)
            { return null; }

            MessageHeaderInfo headerInfo = request.Headers[headerPosition];

            XmlNode[] content = request.Headers.GetHeader<XmlNode[]>(headerPosition);

            string text = content[0].InnerText;

            XmlSerializer deserializer = new XmlSerializer(typeof(ServiceHeader));
            TextReader textReader = new StringReader(text);
            ServiceHeader customData = (ServiceHeader)deserializer.Deserialize(textReader);
            textReader.Close();

            return customData;
        }
    }
}
