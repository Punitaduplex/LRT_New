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
    public class Payment
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Payment_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Payment_Term_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Payment_Term_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Credit_Days { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tolerance_Days { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Relate_To { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertPayment(Payment objPayment)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_payment_term", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Payment_Term_Code", objPayment.Payment_Term_Code);
            cmd.Parameters.AddWithValue("@Payment_Term_Name", objPayment.Payment_Term_Name);
            cmd.Parameters.AddWithValue("@Credit_Days", objPayment.Credit_Days);
            cmd.Parameters.AddWithValue("@Tolerance_Days", objPayment.Tolerance_Days);
            cmd.Parameters.AddWithValue("@Relate_To", objPayment.Relate_To);
            cmd.Parameters.AddWithValue("@User_Id", objPayment.User_Id);
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
        public DataSet GetPayment(int Paymentid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_payment_term_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Payment_Term_Id", Paymentid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdatePayment(Payment objPayment)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_payment_term", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Payment_Term_Id", objPayment.Payment_Id);
            cmd.Parameters.AddWithValue("@Payment_Term_Code", objPayment.Payment_Term_Code);
            cmd.Parameters.AddWithValue("@Payment_Term_Name", objPayment.Payment_Term_Name);
            cmd.Parameters.AddWithValue("@Credit_Days", objPayment.Credit_Days);
            cmd.Parameters.AddWithValue("@Tolerance_Days", objPayment.Tolerance_Days);
            cmd.Parameters.AddWithValue("@Relate_To", objPayment.Relate_To);
            cmd.Parameters.AddWithValue("@User_Id", objPayment.User_Id);
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
        public int DeletePayment(Payment objPayment)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_payment_term", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Payment_Term_Id", objPayment.Payment_Id);
            cmd.Parameters.AddWithValue("@User_Id", objPayment.User_Id);
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
        public int GetPaymentApproveStatus(Payment objPayment)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_paymentterm_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Payment_Term_Id", objPayment.Payment_Id);
            cmd.Parameters.AddWithValue("@User_Id", objPayment.User_Id);
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