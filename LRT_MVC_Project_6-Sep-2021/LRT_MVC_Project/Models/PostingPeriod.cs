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
    public class PostingPeriod
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Posting_Period_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Company_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Period_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Period_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Period { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string datestring { get; set; }
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Fiscal_Year_Start { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        public int InsertPostingPeriod(PostingPeriod objPostingPeriod)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_postingperiod", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Id", objPostingPeriod.Company_Id);
            cmd.Parameters.AddWithValue("@Period_Code", objPostingPeriod.Period_Code);
            cmd.Parameters.AddWithValue("@Period_Name", objPostingPeriod.Period_Name);
            cmd.Parameters.AddWithValue("@Sub_Period", objPostingPeriod.Sub_Period);
            cmd.Parameters.AddWithValue("@fiscal_Year_Start", objPostingPeriod.Fiscal_Year_Start);
            //  cmd.Parameters.AddWithValue("@Priority_Name", objPostingPeriod.Priority_Name);
            cmd.Parameters.AddWithValue("@User_Id", objPostingPeriod.User_Id);
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
        public DataSet GetPostingPeriod(int PostingPeriodid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_t_postingperiod_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Posting_Period_Id", PostingPeriodid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdatePostingPeriod(PostingPeriod objPostingPeriod)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_postingperiod", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Post_Period_Id", objPostingPeriod.Posting_Period_Id);
            cmd.Parameters.AddWithValue("@Company_Id", objPostingPeriod.Company_Id);
            cmd.Parameters.AddWithValue("@Period_Code", objPostingPeriod.Period_Code);
            cmd.Parameters.AddWithValue("@Period_Name", objPostingPeriod.Period_Name);
            cmd.Parameters.AddWithValue("@Sub_Period", objPostingPeriod.Sub_Period);
            cmd.Parameters.AddWithValue("@fiscal_Year_Start", objPostingPeriod.Fiscal_Year_Start);
            cmd.Parameters.AddWithValue("@User_Id", objPostingPeriod.User_Id);
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
        public int DeletePostingPeriod(PostingPeriod objPostingPeriod)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_postingperiod", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Post_Period_Id", objPostingPeriod.Posting_Period_Id);
            cmd.Parameters.AddWithValue("@User_Id", objPostingPeriod.User_Id);
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

        public int GetPostingPeriodApproveStatus(PostingPeriod objPostingPeriod)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_postingperiod_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Posting_Period_Id", objPostingPeriod.Posting_Period_Id);
            cmd.Parameters.AddWithValue("@User_Id", objPostingPeriod.User_Id);
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