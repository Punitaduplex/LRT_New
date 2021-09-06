using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace LRTProject.Models
{
    public class RolePermission
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Permission_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Menu_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Menu_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Sub_To_Sub_Menu_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_To_Sub_Menu_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Role_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Role_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pageUrl { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool Is_Add { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool Is_Edit { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool Is_Delete { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool Is_View { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool Is_Approve { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Add_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Edit_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string View_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Delete_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_To_Sub_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        public int InsertRolePermission(RolePermission objRolePermission)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_u_role_permission", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Role_Id", objRolePermission.Role_Id);
            cmd.Parameters.AddWithValue("@Sub_To_Sub_Menu_Id", objRolePermission.Sub_To_Sub_Menu_Id);
            cmd.Parameters.AddWithValue("@Is_Add", objRolePermission.Is_Add);
            cmd.Parameters.AddWithValue("@Is_Edit", objRolePermission.Is_Edit);
            cmd.Parameters.AddWithValue("@Is_View", objRolePermission.Is_View);
            cmd.Parameters.AddWithValue("@Is_Approve", objRolePermission.Is_Approve);
            cmd.Parameters.AddWithValue("@Is_Delete", objRolePermission.Is_Delete);
            //cmd.Parameters.AddWithValue("@Role_Name", objRolePermission.Sub_To_Sub_Menu_Id);
            cmd.Parameters.AddWithValue("@User_Id", objRolePermission.User_Id);
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
        public DataSet GetSubToSubMenuListForPermission()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_subtosubmenu_List_for_permission", con);
            cmd.CommandType = CommandType.StoredProcedure;
          //  cmd.Parameters.AddWithValue("@Role_id", objRolePermission.Role_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public DataSet GetRolePermissionAccRoleId(RolePermission objRolePermission)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_u_role_permission_acc_rolewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Role_id", objRolePermission.Role_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public DataSet GetRolePermission(RolePermission objRolePermission)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_u_user_role_permission_Name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_Id", Common.CommonSetting.User_Id);
            cmd.Parameters.AddWithValue("@Sub_To_Sub_Menu_Id", objRolePermission.Sub_To_Sub_Menu_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

    }
}