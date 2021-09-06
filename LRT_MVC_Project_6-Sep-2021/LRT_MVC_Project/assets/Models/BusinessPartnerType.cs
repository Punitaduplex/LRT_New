using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class BusinessPartnerType
    {
        public int Business_Partner_Type_Id { get; set; }
        public string Business_Partner_Type_Code { get; set; }
        public string Business_Partner_Type_Name { get; set; }
        public int User_Id { get; set; }
    }
}