using AppWorks.UI.AppService;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Common
{
    public class LogHelper : WcfSingleInstance
    {
        public static void LogErrorToDb(Exception ex)
        {
            try
            {
                ErrorLogProp errorProp = new ErrorLogProp();
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                //string message = ex.GetType().ToString()+ex.StackTrace.ToString();
                errorProp.Message = string.Format("{0}: {1} \n{2}", ex.GetType().ToString(), ex.Message, ex.StackTrace.ToString());
                errorProp.Logger = trace.GetFrame(0).GetMethod().ReflectedType.FullName;
                errorProp.Thread = trace.FrameCount.ToString();
                errorProp.SystemMac = string.Format("PC Name: {0}   Mac Address: {1}", Environment.MachineName, GetMACAddress());
                errorProp.BuildVersion = ConfigurationManager.AppSettings["CurrentApplicationVersion"].ToString();
                _serviceInstance.LogErrorToDb(errorProp);
            }
            catch (Exception e)
            {

            }
        }

        public static string GetMACAddress()
        {
            string macAddress = String.Empty;
            try
            {
                macAddress=NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                macAddress = string.Empty;
            }
            return macAddress;
        }
    }
}
