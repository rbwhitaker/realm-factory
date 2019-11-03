using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using Starbound.RealmFactory;

namespace RealmEngine
{
    public static class WebServicesCommunicator
    {
        //private static readonly string DomainName = "localhost";
        private static readonly string DomainName = "StarboundSoftware.com";
        
        private static long softwareId;

        static WebServicesCommunicator()
        {
            softwareId = Starbound.RealmFactory.Properties.Settings.Default.SoftwareId;
            if (softwareId == -1)
            {
                softwareId = CreateId();
                Starbound.RealmFactory.Properties.Settings.Default.SoftwareId = softwareId;
                Starbound.RealmFactory.Properties.Settings.Default.Save();
            }
        }

        public static void Initialize()
        {
            // Nothing to do here... it is all done in the static constructor, which 
            // will get called before this code actually runs... pretty sneaky, huh?
        }

        private static int CreateId()
        {
            string url = "http://" + DomainName + "/files/software/realm-factory/api/GetId.php";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["version"] = Constants.VersionString;

            string results = PostRequest(url, parameters);
            XmlDocument document = new XmlDocument();
            document.LoadXml(results);
            XmlNode documentElement = document.DocumentElement;

            if (documentElement.Name == "id")
            {
                return Convert.ToInt32(documentElement.InnerText.Trim());
            }

            return -1;
        }

        public static void SubmitBugReport(BugReport bugReport)
        {
            string url = "http://" + DomainName + "/files/software/realm-factory/api/SubmitBug.php";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["id"] = softwareId.ToString();
            parameters["version"] = Constants.VersionString;
            parameters["severity"] = bugReport.Severity.ToString();
            parameters["description"] = bugReport.Description;
            parameters["reproduction-steps"] = bugReport.ReproductionSteps;

            PostRequest(url, parameters);
        }

        public static void SubmitFeedback(FeedbackReport feedbackReport)
        {
            string url = "http://" + DomainName + "/files/software/realm-factory/api/SubmitFeedback.php";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["id"] = softwareId.ToString();
            parameters["version"] = Constants.VersionString;
            parameters["feedback"] = feedbackReport.FeedbackText;

            PostRequest(url, parameters);
        }

        public static void SubmitFeatureRequest(FeatureRequestReport featureRequestReport)
        {
            string url = "http://" + DomainName + "/files/software/realm-factory/api/RequestFeature.php";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["id"] = softwareId.ToString();
            parameters["version"] = Constants.VersionString;
            parameters["description"] = featureRequestReport.Description;
            parameters["reason"] = featureRequestReport.Reason;

            PostRequest(url, parameters);
        }

        private static string PostRequest(string url, Dictionary<string, string> parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string postData = "";
            for (int index = 0; index < parameters.Keys.Count; index++)
            {
                if (index != 0)
                {
                    postData += "&";
                }

                postData += parameters.Keys.ElementAt(index);
                postData += "=";
                postData += parameters[parameters.Keys.ElementAt(index)];
            }

            Encoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            WebResponse response = request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string results = reader.ReadToEnd();
            return results;
        }
    }
}
