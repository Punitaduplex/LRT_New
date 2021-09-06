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
    public class HSNAndSAC
    {
        public int HSN_SAC_Id { get; set; }
        public int Company_Id { get; set; }
        public string HSN_SAC_Code { get; set; }
        public string Company_Name { get; set; }
        public string Chapter_ID { get; set; }
        public string Category_Name { get; set; }
        public string Sub_Category_Name { get; set; }
        public string Sub_Sub_Category_Name { get; set; }
        public string HSN_Remark { get; set; }
        public int User_Id { get; set; }
    }
    public class HSN
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int HSN_Id { get; set; }
        // public int Company_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HSN_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        public int InsertHSN(HSN objHSN)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_hsn", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@HSN_Name", objHSN.HSN_Code);
            cmd.Parameters.AddWithValue("@User_Id", objHSN.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public DataSet GetHSN(int hsnid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_hsn_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HSN_Id", hsnid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateHSN(HSN objHSN)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_hsn", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HSN_Id", objHSN.HSN_Id);
            cmd.Parameters.AddWithValue("@HSN_Name", objHSN.HSN_Code);
            cmd.Parameters.AddWithValue("@User_Id", objHSN.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public int DeleteHSN(HSN objHSN)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_hsn", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HSN_Id", objHSN.HSN_Id);
            cmd.Parameters.AddWithValue("@User_Id", objHSN.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }

        public int GetHSNApproveStatus(HSN objHSN)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_hsn_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HSN_Id", objHSN.HSN_Id);
            cmd.Parameters.AddWithValue("@User_Id", objHSN.User_Id);
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