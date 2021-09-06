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
    public class ExchangeRate
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Exchange_Rate_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Company_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime Exchange_Rate_Date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Date_String { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Exchange_Rate_Amount { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertExchangeRate(ExchangeRate objExchangeRate)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_exchange", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Company_Id", objExchangeRate.Company_Id);
            cmd.Parameters.AddWithValue("@Exchange_Date", objExchangeRate.Exchange_Rate_Date);
            cmd.Parameters.AddWithValue("@Currency_Id", objExchangeRate.Currency_Id);
            cmd.Parameters.AddWithValue("@Exchange_Amount", objExchangeRate.Exchange_Rate_Amount);
            cmd.Parameters.AddWithValue("@User_Id", objExchangeRate.User_Id);
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
        public DataSet GetExchangeRate(int ExchangeRateid)
        {
            DataSet ds = new DataSet();
            try{
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_exchange_rate_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exchange_Rate_Id", ExchangeRateid);
          
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            }
            catch(Exception ex){}
            return ds;
        }
        public int UpdateExchangeRate(ExchangeRate objExchangeRate)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_exchange", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exchange_Rate_Id", objExchangeRate.Exchange_Rate_Id);
            cmd.Parameters.AddWithValue("@Company_Id", objExchangeRate.Company_Id);
            cmd.Parameters.AddWithValue("@Exchange_Date", objExchangeRate.Exchange_Rate_Date);
            cmd.Parameters.AddWithValue("@Currency_Id", objExchangeRate.Currency_Id);
            cmd.Parameters.AddWithValue("@Exchange_Amount", objExchangeRate.Exchange_Rate_Amount);
            cmd.Parameters.AddWithValue("@User_Id", objExchangeRate.User_Id);
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
        public int DeleteExchangeRate(ExchangeRate objExchangeRate)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_exchange", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exchange_Rate_Id", objExchangeRate.Exchange_Rate_Id);
            cmd.Parameters.AddWithValue("@User_Id", objExchangeRate.User_Id);
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
        public int GetExchangeRateApproveStatus(ExchangeRate objExchangeRate)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_exchange_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Exchange_Id", objExchangeRate.Exchange_Rate_Id);
            cmd.Parameters.AddWithValue("@User_Id", objExchangeRate.User_Id);
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