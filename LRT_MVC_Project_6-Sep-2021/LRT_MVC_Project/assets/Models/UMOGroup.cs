using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class UMOGroup
    {
        public int UMO_Group_Id { get; set; }
        public string Group_Code { get; set; }
        public string Group_Name { get; set; }
        public int User_Id { get; set; }
    }
    public class UMOGroupQty
    {
        public int UMO_Group_Qty_Id { get; set; }
        public int UMO_Group_Id { get; set; }
        public string Group_Name { get; set; }
        public string Base_Quantity { get; set; }
        public string Base_UOM  { get; set; }
        public string Alt_Quantity { get; set; }
        public string Alt_UOM  { get; set; }
        public int User_Id { get; set; }
    }
}