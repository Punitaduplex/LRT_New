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
    public class Currency
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string International_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string International_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Decimal_Unit { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Symbol { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        public DataSet GetCurencyName()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_currency_Name", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int InsertCurrency(Currency objCurrency)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_currency", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Currency_Code", objCurrency.Currency_Code);
            cmd.Parameters.AddWithValue("@Currency_Name", objCurrency.Currency_Name);
            cmd.Parameters.AddWithValue("@InterNational_Code", objCurrency.International_Code);
            cmd.Parameters.AddWithValue("@InterNational_Name", objCurrency.International_Name);
            cmd.Parameters.AddWithValue("@Decimal_Unit", objCurrency.Decimal_Unit);
            cmd.Parameters.AddWithValue("@Currency_Symbol", objCurrency.Currency_Symbol);
            cmd.Parameters.AddWithValue("@User_Id", objCurrency.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public DataSet GetCurrency(int Currencyid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_currency_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Currency_Id", Currencyid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateCurrency(Currency objcurrency)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_Currency", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Currency_Id", objcurrency.Currency_Id);
            cmd.Parameters.AddWithValue("@Currency_Code", objcurrency.Currency_Code);
            cmd.Parameters.AddWithValue("@Currency_Name", objcurrency.Currency_Name);
            cmd.Parameters.AddWithValue("@InterNational_Code", objcurrency.International_Code);
            cmd.Parameters.AddWithValue("@InterNational_Name", objcurrency.International_Name);
            cmd.Parameters.AddWithValue("@Decimal_Unit", objcurrency.Decimal_Unit);
            cmd.Parameters.AddWithValue("@Currency_Symbol", objcurrency.Currency_Symbol);
            cmd.Parameters.AddWithValue("@User_Id", objcurrency.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public int DeleteCurrency(Currency objcurrency)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_Currency", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Currency_Id", objcurrency.Currency_Id);
            cmd.Parameters.AddWithValue("@User_Id", objcurrency.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            int i = Convert.ToInt32(cmd.Parameters["@error1"].Value);
            return i;
        }
        public int GetCurrencyApproveStatus(Currency objCurrency)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_currency_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Currency_Id", objCurrency.Currency_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCurrency.User_Id);
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