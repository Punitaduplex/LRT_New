using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class ShippingType
    {
        public int Shipping_Type_Id { get; set; }

        public string Shipping_Type_Code { get; set; }
        public string Shipping_Type_Name { get; set; }
        public int User_Id { get; set; }
    }
}