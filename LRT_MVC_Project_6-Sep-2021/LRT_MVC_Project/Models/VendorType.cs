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
    public class VendorType
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Sub_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Type_Sub_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Sub_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<VendorSubType> sublist { get; set; }
        public int User_Id { get; set; }


        public int UpdateVendorType(VendorType objVendorType)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_v_vendor_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Vendor_Type_Id", objVendorType.Vendor_Type_Id);
            cmd.Parameters.AddWithValue("@Vendor_Type_Name", objVendorType.Vendor_Type);
            cmd.Parameters.AddWithValue("@User_Id", objVendorType.User_Id);
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
        public string InsertVendorType(VendorType objVendorType)
        {
            string i = 0.ToString();

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendor_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@Vendor_Type_Name", objVendorType.Vendor_Type);
            cmd.Parameters.AddWithValue("@User_Id", objVendorType.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.VarChar,50));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToString(cmd.Parameters["@error1"].Value);
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

       

        public int DeleteVendorType(VendorType objVendorType)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_v_vendor_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Type_Id", objVendorType.Vendor_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorType.User_Id);
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
        public int GetVendorTypeApproveStatus(VendorType objVendorType)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_v_vendor_type_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Type_Id", objVendorType.Vendor_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorType.User_Id);
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
        public string InsertVendorTypeDetail(VendorType objVendorType)
        {
            string i = 0.ToString();

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendor_type_detail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Vendor_Type_Id", objVendorType.Vendor_Type_Id);
            cmd.Parameters.AddWithValue("@Vendor_Sub_Type_Id", objVendorType.Vendor_Sub_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorType.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.Int32));
            cmd.Parameters["@error1"].Direction = ParameterDirection.Output;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                i = Convert.ToString(cmd.Parameters["@error1"].Value);
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
        public DataSet GetVendorType(VendorType objVendorType)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_type_for_list ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type_Id", objVendorType.Vendor_Type_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetVendorSubTypeAccTypeId(VendorType objVendorType)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_sub_type_for_acc_type_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type_Id", objVendorType.Vendor_Type_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }
}