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
    public class UserSession
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Login_Date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_ID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Source_IP { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Browser { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Plateform { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Session_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Contact_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Email { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool User_Is_Active { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Login_date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Source_IP { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string User_Browser { get; set; }
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_PlateForm { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Logout_Date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Temp_Logout_Date { get; set; }

        public int InsertUserSession(DateTime logindate, int userid, string sourseip, string userstatus, string userbrowser, string userplateform)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_u_usersession", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Login_Date", logindate);
            cmd.Parameters.AddWithValue("@User_Id", userid);
            cmd.Parameters.AddWithValue("@Source_IP", sourseip);
            cmd.Parameters.AddWithValue("@User_Status", userstatus);
            cmd.Parameters.AddWithValue("@User_Browser", userbrowser);
            cmd.Parameters.AddWithValue("@User_Plateform", userplateform);
            cmd.Parameters.Add(new MySqlParameter("@User_Session_ID", MySqlDbType.Int32));
            cmd.Parameters["@User_Session_ID"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Common.CommonSetting.User_Session_Id = Convert.ToInt32(cmd.Parameters["@User_Session_ID"].Value);
            }
            catch (Exception ex)
            {
                i = 0;
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


        public int UpdateUserSession(int sessionid, int userid, string sourceip, string userstatus, string userbrowser, string userplateform, DateTime logoutdate, DateTime templogout)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_u_usersession", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Session_Id", sessionid);
            cmd.Parameters.AddWithValue("@User_Id", userid);
            cmd.Parameters.AddWithValue("@Source_IP", sourceip);
            cmd.Parameters.AddWithValue("@User_Status", userstatus);
            cmd.Parameters.AddWithValue("@User_Browser", userbrowser);
            cmd.Parameters.AddWithValue("@User_Plateform", userplateform);
            cmd.Parameters.AddWithValue("@Logout_Date", logoutdate);
            cmd.Parameters.AddWithValue("@Temp_Logout_Date", templogout);
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

        public DataSet GetUserSessionDetail()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_u_user_session_Detail", con);
                cmd.CommandType = CommandType.StoredProcedure;


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }
}