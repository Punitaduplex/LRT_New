using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class ItemTypeCode
    {
        public int Item_Type_Code_Id { get; set; }

        public string Item_Type_Code { get; set; }
        public string Item_Type_Code_Name { get; set; }
       
        public int User_Id { get; set; }
    }
}