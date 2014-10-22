using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalEngine.Infrastructure
{
    public class ApplicationSettings
    {
        public static string ApplicationName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"];
        //public static string themesFolderPath = System.Configuration.ConfigurationManager.AppSettings["themesFolderPath"];

        public static string Message(string MessageTitle,string MessageText, string MessageType)
        {
            string alertType = "";
           
            switch (MessageType)
            { 
                case "Information":
                alertType = "alert-info";
                break;

                case "Critical":
                alertType = "alert-danger";

                break;

                case "Warnning":
                alertType = "alert-warning";
                break;

                case "Success":
                alertType = "alert-success";
                break;

                default:
                alertType = "";
                break;
            }

            string msgHead = "<div class='alert " + alertType;
            msgHead += " fade in'><button class='close' data-dismiss='alert'>×</button> <i class='fa-fw fa fa-check'></i><strong>" + MessageTitle + "! </strong>";
            msgHead += MessageText + "</div>";
            return msgHead;
        }
    }
}