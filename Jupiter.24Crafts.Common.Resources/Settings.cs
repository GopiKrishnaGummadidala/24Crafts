using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Common.Resources
{
    public static class Settings
    {
        public static string AuthTokenExpiryTime
        {
            get { return ConfigurationManager.AppSettings["AuthTokenExpiry"]; }
        }

        public static string SenderEmailId
        {
            get { return ConfigurationManager.AppSettings["SenderEmailId"]; }
        }

        public static string SenderPassword
        {
            get { return ConfigurationManager.AppSettings["SenderPassword"]; }
        }

        public static bool EnableMailSending
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableMailSending"]); }
        }

        public static string AdminEmailId
        {
            get { return ConfigurationManager.AppSettings["AdminEmailId"]; }
        }

        public static string ContentApproverEmailId
        {
            get { return ConfigurationManager.AppSettings["ContentApproverEmailId"]; }
        }

        public static bool EnableOTPSending
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableOTPSending"]); }
        }
    }
}
