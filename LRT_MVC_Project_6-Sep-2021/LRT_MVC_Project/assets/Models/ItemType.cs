using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LRTProject.Models
{
    public class ItemType
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Type_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

       
        public int InsertItemType(ItemType objItemType)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_Item_Type", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Item_Typ_Code", objItemType.Item_Type_Code);
            cmd.Parameters.AddWithValue("@Item_Typ_Name", objItemType.Item_Type_Name);
            cmd.Parameters.AddWithValue("@User_Id", objItemType.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public DataSet GetItemType(int ItemTypeid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_type_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", ItemTypeid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public DataSet GetItemTypeForItem(int ItemTypeid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_type_for_item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", ItemTypeid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateItemType(ItemType objItemType)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_Item_Type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItemType.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Code", objItemType.Item_Type_Code);
            cmd.Parameters.AddWithValue("@Item_Type_Name", objItemType.Item_Type_Name);
            cmd.Parameters.AddWithValue("@User_Id", objItemType.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public int DeleteItemType(ItemType objItemType)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_item_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItemType.Item_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItemType.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public DataSet GetItemTypeName()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_type_Name", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int GetItemTypeApproveStatus(ItemType objItemType)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_itemtype_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItemType.Item_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItemType.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            }
            catch (Exception ex)
            {
                i = 1;
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return i;
        }
    }
}