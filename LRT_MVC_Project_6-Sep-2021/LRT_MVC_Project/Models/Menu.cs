using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LRT_MVC_Project.Models
{
    public class Menu
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public List<SubMenu> Sub_Menu_lsit { get; set; }
        public DataSet GetMenu(int Menuid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_manu_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@menu_Id", Menuid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetMenuAccUserId(int UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_role_permission_nenulist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Id", UserId);
                //cmd.Parameters.AddWithValue("@sub_menu_Id", Sub_Menu_Id);
               
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                
            }
            catch (Exception ex) { }
            return ds;
        }
    }
    public class SubMenu
    {
        public int Menu_Id { get; set; }
        public int Sub_Menu_Id { get; set; }
        public string Sub_Menu_Name { get; set; }
        public List<SubToSubMenu> Sub_To_Sub_Menu_List { get; set; }
        public DataSet GetSubMenu(int Menu_id, int Sub_Menu_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_menu_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@menu_Id", Menu_id);
                cmd.Parameters.AddWithValue("@sub_menu_Id", Sub_Menu_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetSubMenuAccUserId(int UserId, int Menu_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_role_permission_subnenulist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Id", UserId);
                cmd.Parameters.AddWithValue("@Menu_Id", Menu_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }
    public class SubToSubMenu
    {
        public int Sub_To_Sub_Menu_Id { get; set; }
        public int Sub_Menu_Id { get; set; }
        public string Sub_To_Sub_Menu_Name { get; set; }
        public string Page_Url { get; set; }
        public DataSet GetSubToSubMenu(int Sub_Menu_Id, int Sub_To_Sub_Menu_id)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_subtosub_menu_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sub_to_sub_menu_Id", Sub_To_Sub_Menu_id);
                cmd.Parameters.AddWithValue("@sub_menu_Id", Sub_Menu_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetSubToSubMenuAccUserId(int UserId, int Sub_Menu_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_role_permission_subtosubnenulist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Id", UserId);
                cmd.Parameters.AddWithValue("@Sub_Menu_Id", Sub_Menu_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }
}