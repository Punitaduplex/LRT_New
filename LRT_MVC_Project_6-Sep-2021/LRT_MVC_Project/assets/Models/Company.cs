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
    public class Company
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Company_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Language_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Image { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertCompanyMaster(Company objCompanyMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_company", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Company_Code", objCompanyMaster.Company_Code);
            cmd.Parameters.AddWithValue("@Company_Name", objCompanyMaster.Company_Name);
            cmd.Parameters.AddWithValue("@Currency_Id", objCompanyMaster.Currency_Id);

            cmd.Parameters.AddWithValue("@Langauge", objCompanyMaster.Language_Name);
            cmd.Parameters.AddWithValue("@Company_Logo_Url", objCompanyMaster.Company_Image);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyMaster.User_Id);
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
        public DataSet GetCompanyMaster(int CompanyMasterid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_company_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Id", CompanyMasterid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateCompanyMaster(Company objCompanyMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_company", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Id", objCompanyMaster.Company_Id);
            cmd.Parameters.AddWithValue("@Company_Code", objCompanyMaster.Company_Code);
            cmd.Parameters.AddWithValue("@Company_Name", objCompanyMaster.Company_Name);
            cmd.Parameters.AddWithValue("@Currency_Id", objCompanyMaster.Currency_Id);
            cmd.Parameters.AddWithValue("@Langauge", objCompanyMaster.Language_Name);
            cmd.Parameters.AddWithValue("@Company_Logo_Url", objCompanyMaster.Company_Image);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyMaster.User_Id);
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
        public int DeleteCompanyMaster(Company objCompanyMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_company", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Id", objCompanyMaster.Company_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyMaster.User_Id);
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
        public DataSet GetCompanyName()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_company_name", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int GetCompanyApproveStatus(Company objCompany)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_company_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Id", objCompany.Company_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCompany.User_Id);
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
    public class CompanyAddress
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Company_Add_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Company_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contact_Person { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string House_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Street_1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Street_2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Landmark { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Country_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Pin_code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Mobile_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Mobile_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Telephone_Extension { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Landline_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fax_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Website { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Email { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Category { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Country_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int State_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int City_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertCompanyAddress(CompanyAddress objCompanyAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_companyaddress", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Company_Id", objCompanyAddress.Company_Id);
            cmd.Parameters.AddWithValue("@Company_contactPerson", objCompanyAddress.Contact_Person);
            cmd.Parameters.AddWithValue("@Company_houseBuildingNo", objCompanyAddress.House_No);
            cmd.Parameters.AddWithValue("@Company_street1", objCompanyAddress.Street_1);
            cmd.Parameters.AddWithValue("@Company_Street2", objCompanyAddress.Street_2);
            cmd.Parameters.AddWithValue("@Company_Landmark", objCompanyAddress.Landmark);
            cmd.Parameters.AddWithValue("@Country_Id", objCompanyAddress.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", objCompanyAddress.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", objCompanyAddress.City_Id);
            cmd.Parameters.AddWithValue("@Company_pincode", objCompanyAddress.Pin_code);
            cmd.Parameters.AddWithValue("@Company_mobileNumber", objCompanyAddress.Mobile_Number);
            cmd.Parameters.AddWithValue("@Company_altMobileNumber", objCompanyAddress.Alt_Mobile_Number);
            cmd.Parameters.AddWithValue("@Company_TelephoneNumber", objCompanyAddress.Telephone_Extension);
            cmd.Parameters.AddWithValue("@Company_landLineNumber", objCompanyAddress.Landline_Number);
            cmd.Parameters.AddWithValue("@Company_FaxNumber", objCompanyAddress.Fax_Number);
            cmd.Parameters.AddWithValue("@Company_Website", objCompanyAddress.Website);
            cmd.Parameters.AddWithValue("@Company_Email", objCompanyAddress.Company_Email);
            cmd.Parameters.AddWithValue("@Company_category", objCompanyAddress.Company_Category);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyAddress.User_Id);
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
        public DataSet GetCompanyAddress(int CompanyAddressid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_company_address_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Address_Id", CompanyAddressid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public DataSet GetCommpanyAddressForEdit(CompanyAddress objcompanymasteradd)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_company_address_for_edit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Address_Id", objcompanymasteradd.Company_Add_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateCompanyAddress(CompanyAddress objCompanyAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_companyaddress", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Address_Id", objCompanyAddress.Company_Add_Id);
            cmd.Parameters.AddWithValue("@Company_Id", objCompanyAddress.Company_Id);
            cmd.Parameters.AddWithValue("@Company_contactPerson", objCompanyAddress.Contact_Person);
            cmd.Parameters.AddWithValue("@Company_houseBuildingNo", objCompanyAddress.House_No);
            cmd.Parameters.AddWithValue("@Company_street1", objCompanyAddress.Street_1);
            cmd.Parameters.AddWithValue("@Company_Street2", objCompanyAddress.Street_2);
            cmd.Parameters.AddWithValue("@Company_Landmark", objCompanyAddress.Landmark);
            cmd.Parameters.AddWithValue("@Country_Id", objCompanyAddress.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", objCompanyAddress.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", objCompanyAddress.City_Id);
            cmd.Parameters.AddWithValue("@Company_pincode", objCompanyAddress.Pin_code);
            cmd.Parameters.AddWithValue("@Company_mobileNumber", objCompanyAddress.Mobile_Number);
            cmd.Parameters.AddWithValue("@Company_altMobileNumber", objCompanyAddress.Alt_Mobile_Number);
            cmd.Parameters.AddWithValue("@Company_TelephoneNumber", objCompanyAddress.Telephone_Extension);
            cmd.Parameters.AddWithValue("@Company_landLineNumber", objCompanyAddress.Landline_Number);
            cmd.Parameters.AddWithValue("@Company_FaxNumber", objCompanyAddress.Fax_Number);
            cmd.Parameters.AddWithValue("@Company_Website", objCompanyAddress.Website);
            cmd.Parameters.AddWithValue("@Company_Email", objCompanyAddress.Company_Email);
            cmd.Parameters.AddWithValue("@Company_category", objCompanyAddress.Company_Category);
            //  cmd.Parameters.AddWithValue("@State_Id", objBranchAddress.Is_Warehouse);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyAddress.User_Id);
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
        public int DeleteCompanyAddress(CompanyAddress objCompanyMasterAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_companyaddress", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Address_Id", objCompanyMasterAddress.Company_Add_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyMasterAddress.User_Id);
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

        public int GetCompanyAddressApproveStatus(CompanyAddress objCompanyAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_companyaddress_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Address_Id", objCompanyAddress.Company_Add_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyAddress.User_Id);
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