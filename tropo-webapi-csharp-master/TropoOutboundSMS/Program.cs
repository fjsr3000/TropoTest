using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using TropoCSharp.Structs;
using TropoCSharp.Tropo;

namespace OutboundTest
{
    /// <summary>
    /// A simple console appplication used to launch a Tropo Session and send an outbound SMS.
    /// Note - use in conjnction withe the OutboundSMS.aspx example in the TropoSamples project.
    /// For further information, see http://blog.tropo.com/2011/04/14/sending-outbound-sms-with-c/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // The voice and messaging tokens provisioned when your Tropo application is set up.
            string voiceToken = "21bca0ff7694344bbf17a92a296fe6f7b35b8ac89bc83ce8054a1f5fc9f637ce62df71cb42a6513c86852212";
            string messagingToken = "21bcbbb17c255b47a247bc1280e747ec16661fe702e56efe0f25ebef9df7c65e94a72dc0a9e788520eebc2a3";

            // A collection to hold the parameters we want to send to the Tropo Session API.
            IDictionary<string, string> parameters = new Dictionary<String, String>();

            // Enter a phone number to send a call or SMS message to here.
            parameters.Add("sendToNumber", "14803599974");

            // Enter a phone number to use as the caller ID.
            parameters.Add("sendFromNumber", "14804471106");

            // Select the channel you want to use via the Channel struct.
            string channel = Channel.Text;
            parameters.Add("channel", channel);

            string network = Network.SMS;
            parameters.Add("network", network);

            // Message is sent as a query string parameter, make sure it is properly encoded.
            parameters.Add("msg", HttpUtility.UrlEncode("This is a test message from C#."));

            // Instantiate a new instance of the Tropo object.
            Tropo tropo = new Tropo();

            // Create an XML doc to hold the response from the Tropo Session API.
            XmlDocument doc = new XmlDocument();

            // Set the token to use.
            string token = channel == Channel.Text ? messagingToken : voiceToken;

            // Load the XML document with the return value of the CreateSession() method call.
            doc.Load(tropo.CreateSession(token, parameters));

            // Display the results in the console.
            Console.WriteLine("Result: " + doc.SelectSingleNode("session/success").InnerText.ToUpper());
            Console.WriteLine("Token: " + doc.SelectSingleNode("session/token").InnerText);
            Console.Read();
        }
    }
}
