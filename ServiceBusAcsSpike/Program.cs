using System;
using log4net;
using log4net.Config;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace ServiceBusAcsSpike
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Program));

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            try
            {
                var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString.Sender");
                Log.DebugFormat("Connection string: {0}", connectionString);

                var client = TopicClient.CreateFromConnectionString(connectionString, "foo/bar");
                client.Send(new BrokeredMessage("This is a test"));

                Log.DebugFormat("Message sent");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}