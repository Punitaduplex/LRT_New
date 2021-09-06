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
    public class SearchItem
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Keyword { get; set; }

        public DataSet GetSearchItemAccKeywords(string keyword)
        {
           
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_item_wildcard_for_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Search_Keywords", keyword);
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;
           
        }
    }
}