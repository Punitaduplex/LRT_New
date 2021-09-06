using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Common
{
    public static class CommonSetting
    {
        public static string name{ get; set; }
        public static Int32 User_Id { get; set; }
        public static string User_Email { get; set; }
        public static Int32 User_Session_Id { get; set; }
    }
}