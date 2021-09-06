using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class MaterialBrand
    {
        public int Material_Brand_Id { get; set; }

        public string Material_Brand_Code { get; set; }
        public string Material_Brand_Name { get; set; }

        public int User_Id { get; set; }
    }
}