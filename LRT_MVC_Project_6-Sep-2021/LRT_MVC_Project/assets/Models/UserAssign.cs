using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LRTProject.Models
{
    public class UserAssign
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UserAssign_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Reg_ID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Branch_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Department_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Designation_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Role_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Reg_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Department_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Designation_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Role_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertUserAssign(UserAssign objuserassign)
        {
            int i = 0;
            
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_u_userassign", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserReg_Id", objuserassign.User_Reg_ID);
            cmd.Parameters.AddWithValue("@Branch_Id", objuserassign.Branch_Id);
            cmd.Parameters.AddWithValue("@Department_Id", objuserassign.Department_Id);
            cmd.Parameters.AddWithValue("@Designation_Id", objuserassign.Designation_Id);
            cmd.Parameters.AddWithValue("@Role_Id", objuserassign.Role_Id);
            cmd.Parameters.AddWithValue("@User_Id", objuserassign.User_Id);
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
        public DataSet GetUserAssign(int userassignid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_u_userassign_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserAssign_Id", userassignid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateUserAssign(UserAssign objuserassign)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_u_userassign", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserAssign_Id", objuserassign.UserAssign_Id);
            cmd.Parameters.AddWithValue("@UserReg_Id", objuserassign.User_Reg_ID);
            cmd.Parameters.AddWithValue("@Branch_Id", objuserassign.Branch_Id);
            cmd.Parameters.AddWithValue("@Department_Id", objuserassign.Department_Id);
            cmd.Parameters.AddWithValue("@Designation_Id", objuserassign.Designation_Id);
            cmd.Parameters.AddWithValue("@Role_Id", objuserassign.Role_Id);
            cmd.Parameters.AddWithValue("@User_Id", objuserassign.User_Id);
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
        public int DeleteUserAssign(UserAssign objuserassign)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_u_userassign", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserAssign_Id", objuserassign.UserAssign_Id);
            cmd.Parameters.AddWithValue("@User_Id", objuserassign.User_Id);
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

        public int GetUserAssignApproveStatus(UserAssign objUserAssign)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_u_userassign_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_Assign_Id", objUserAssign.UserAssign_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUserAssign.User_Id);
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