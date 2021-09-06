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
    public class ManageItem
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Manage_Item_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Manage_Item_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Manage_Item_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }


        public int InsertManageItem(ManageItem objManageItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_ManageItem", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Manage_Item_Code", objManageItem.Manage_Item_Code);
            cmd.Parameters.AddWithValue("@Manage_Item_Name", objManageItem.Manage_Item_Name);
            cmd.Parameters.AddWithValue("@User_Id", objManageItem.User_Id);
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
        public DataSet GetManageItem(int ManageItemid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_Manage_item_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Manage_Item_Id", ManageItemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdateManageItem(ManageItem objManageItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_ManageItem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ManageItem_Id", objManageItem.Manage_Item_Id);
            cmd.Parameters.AddWithValue("@ManageItem_Code", objManageItem.Manage_Item_Code);
            cmd.Parameters.AddWithValue("@ManageItem_Name", objManageItem.Manage_Item_Name);
            cmd.Parameters.AddWithValue("@User_Id", objManageItem.User_Id);
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
        public int DeleteManageItem(ManageItem objManageItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_manage_item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Manage_Item_Id", objManageItem.Manage_Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objManageItem.User_Id);
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

        public DataSet GetMaqnageItemName()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_Manage_item_name", con);
                cmd.CommandType = CommandType.StoredProcedure;


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int GetManageItemApproveStatus(ManageItem objManageItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_manageitem_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Manage_Item_Id", objManageItem.Manage_Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objManageItem.User_Id);
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