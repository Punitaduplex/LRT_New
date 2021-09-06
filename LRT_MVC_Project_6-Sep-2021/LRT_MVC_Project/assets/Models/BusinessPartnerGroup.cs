using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class BusinessPartnerGroup
    {
        public int Business_Partner_Group_Id { get; set; }
      
        public string Business_Partner_Type { get; set; }
        public string Business_Partner_Group_Code { get; set; }
       
        public string Business_Partner_Group_Name { get; set; }
       // public string Area_Name { get; set; }
        public int User_Id { get; set; }
    }
}