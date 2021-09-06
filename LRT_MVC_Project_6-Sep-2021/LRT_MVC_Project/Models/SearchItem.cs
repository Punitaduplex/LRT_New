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
    public class SearchItem
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Keyword { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Description { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_HS_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_CAD_File_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Internal_Item_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Manufacturing_Part_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Images_Url { get; set; }
        public DataSet GetSearchItemAccKeywords(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_item_wildcard_for_search2", con);
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

        public DataSet GetSearchItemAccKeywords1(SearchItem objsearchitem)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_item_wildcard_for_search2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Search_Keywords", objsearchitem.Search_Keyword);
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

        public DataSet GetSearchItemCodeAccKeywords(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_item_code_for_search", con);
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
        public DataSet GetSearchProjectNoAccKeywords(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_project_number_for_search", con);
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
        public DataSet GetItemDetailAccKeywords(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_Detail_acc_searchValue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchValue", keyword);
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