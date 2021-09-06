using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LRT_MVC_Project.Models
{
    public class UserRegistration
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int New_User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_First_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Last_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Contact_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Email { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Pssword { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Current_Password { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string New_Password { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email_ID { get; set; }
        public string InsertUserRegistration(UserRegistration objuserregistration)
        {
            string i = "";

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_u_user", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@User_First_Name", Regex.Replace(objuserregistration.User_First_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Last_Name", Regex.Replace(objuserregistration.User_Last_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Contact_No", Regex.Replace(objuserregistration.User_Contact_No.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Email", Regex.Replace(objuserregistration.User_Email.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Id", objuserregistration.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.VarChar, 50));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = (cmd.Parameters["@error1"].Value).ToString();
            }
            catch (Exception ex)
            {
                i = "1";
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
        public DataSet GetUserRegistration(int userregistrationid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@New_User_Id", userregistrationid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdateUserRegistration(UserRegistration objuserregistration)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_u_user", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@New_User_Id", objuserregistration.New_User_Id);
            cmd.Parameters.AddWithValue("@User_First_Name", Regex.Replace(objuserregistration.User_First_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Last_Name", Regex.Replace(objuserregistration.User_Last_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Contact_No", Regex.Replace(objuserregistration.User_Contact_No.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Email", Regex.Replace(objuserregistration.User_Email.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Id", objuserregistration.User_Id);
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
        public int DeleteUserRegistration(UserRegistration objuserregistration)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_u_user", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@New_User_Id", objuserregistration.New_User_Id);
            cmd.Parameters.AddWithValue("@User_Id", objuserregistration.User_Id);
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

        public int CheckUserLogIn(UserRegistration objUserRegistration)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_check_u_user_login", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@User_Email", Regex.Replace(objUserRegistration.User_Email.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_pwd", Regex.Replace(objUserRegistration.User_Pssword.Trim(), @"\s+", " "));
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;
            try
            {
            con.Open();
           
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
        public DataSet GetUserDetailForSession(UserRegistration objUserRegistration)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_detail_for_session", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Email", objUserRegistration.User_Email);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetUserName()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_name", con);
                cmd.CommandType = CommandType.StoredProcedure;


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int GetUserRegistrationApproveStatus(UserRegistration objUserRegistration)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_u_user_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@New_User_Id", objUserRegistration.New_User_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUserRegistration.User_Id);
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

        public int UpdateUserPassword(UserRegistration objUserRegistration)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_u_reset_password", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@New_User_Id", Common.CommonSetting.User_Id);
            cmd.Parameters.AddWithValue("@Current_Password", objUserRegistration.Current_Password);
            cmd.Parameters.AddWithValue("@New_Password", objUserRegistration.New_Password);
            cmd.Parameters.AddWithValue("@User_Id", objUserRegistration.User_Id);
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
        public string GetForgetPassword(string emailID)
        {
            string userpass = "";
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_u_User_Password", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email_ID", emailID);
            cmd.Parameters.Add(new MySqlParameter("@User_Password", MySqlDbType.VarChar, 15));
            cmd.Parameters["@User_Password"].Direction = ParameterDirection.Output;
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                userpass = cmd.Parameters["@User_Password"].Value.ToString();
            }
            catch (Exception ex)
            {
                userpass = "";
            }
            finally
            {
                con.Close();
                con.Dispose();
                con = null;
                cmd.Dispose();
                cmd = null;
            }
            return userpass;
        }
    }
}