using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Deployment.Application;

namespace Starbound.RealmFactory
{
    public class Constants
    {
        public static string VersionString
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                else
                {
                    return "Undeployed Development Version";
                }
            }
        }

        public static string VersionType
        {
            get
            {
                //return "Beta";
                //return "RC";
                return "";
            }
        }
    }
}
