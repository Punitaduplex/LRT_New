using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LRT_MVC_Project.Models
{
    public class HSCategory
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int HS_Category_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HS_Category_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HS_Category_Code { get; set; }
        public DataSet GetHSCategoryName()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_hscategory_name", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }
}