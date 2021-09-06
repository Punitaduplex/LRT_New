using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class BOMType
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int BOM_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BOM_Type_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
         public string BOM_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
    }
}