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
    public class BranchMaster
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Branch_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Language_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Language_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string GST_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SST_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Is_Plant { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        // public string Is_Depot { get; set; }
        public string Is_Warehouse { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        public int InsertBranchMaster(BranchMaster objBranchMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_branch", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Branch_Code", objBranchMaster.Branch_Code);
            cmd.Parameters.AddWithValue("@Branch_Name", objBranchMaster.Branch_Name);
            cmd.Parameters.AddWithValue("@Currency_Id", objBranchMaster.Currency_Id);
            cmd.Parameters.AddWithValue("@Language_Id", objBranchMaster.Language_Id);
            cmd.Parameters.AddWithValue("@GST_Number", objBranchMaster.GST_Number);
            cmd.Parameters.AddWithValue("@SST_No", objBranchMaster.SST_Number);
            cmd.Parameters.AddWithValue("@Is_Plant", objBranchMaster.Is_Plant);
            cmd.Parameters.AddWithValue("@Is_WareHouse", objBranchMaster.Is_Warehouse);
            cmd.Parameters.AddWithValue("@User_Id", objBranchMaster.User_Id);
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
        public int InsertBranchMasterIForBulk(BranchMaster objBranchMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_branchforbulk", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Branch_Name", objBranchMaster.Branch_Name);
           
            cmd.Parameters.AddWithValue("@User_Id", objBranchMaster.User_Id);
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
        public DataSet GetBranchMaster(int BranchMasterid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_t_branch_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_Id", BranchMasterid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdateBranchMaster(BranchMaster objBranchMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_branch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", objBranchMaster.Branch_Id);
            cmd.Parameters.AddWithValue("@Branch_Code", objBranchMaster.Branch_Code);
            cmd.Parameters.AddWithValue("@Branch_Name", objBranchMaster.Branch_Name);
            cmd.Parameters.AddWithValue("@Currency_Id", objBranchMaster.Currency_Id);
            cmd.Parameters.AddWithValue("@Language_Id", objBranchMaster.Language_Id);
            cmd.Parameters.AddWithValue("@GST_Number", objBranchMaster.GST_Number);
            cmd.Parameters.AddWithValue("@SST_No", objBranchMaster.SST_Number);
            cmd.Parameters.AddWithValue("@Is_Plant", objBranchMaster.Is_Plant);
            cmd.Parameters.AddWithValue("@Is_WareHouse", objBranchMaster.Is_Warehouse);
            cmd.Parameters.AddWithValue("@User_Id", objBranchMaster.User_Id);
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
        public int DeleteBranchMaster(BranchMaster objBranchMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_branch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", objBranchMaster.Branch_Id);
            cmd.Parameters.AddWithValue("@User_Id", objBranchMaster.User_Id);
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

        public DataSet GetBranchName()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_t_branch_name", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int GetBranchMasterApproveStatus(BranchMaster objBranchMaster)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_branch_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", objBranchMaster.Branch_Id);
            cmd.Parameters.AddWithValue("@User_Id", objBranchMaster.User_Id);
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
    public class BranchAddress
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Branch_Add_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Branch_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Name { get; set; }
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
        public string State_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        // public string Area_Name { get; set; }
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
        public string Alt_Landline_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Website { get; set; }
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
        public int InsertBranchAddress(BranchAddress objBranchAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_branchaddress", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Branch_Id", objBranchAddress.Branch_Id);
            cmd.Parameters.AddWithValue("@Branch_contactPerson", objBranchAddress.Contact_Person);
            cmd.Parameters.AddWithValue("@Branch_houseBuildingNo", objBranchAddress.House_No);
            cmd.Parameters.AddWithValue("@Branch_street1", objBranchAddress.Street_1);
            cmd.Parameters.AddWithValue("@Branch_Street2", objBranchAddress.Street_2);
            cmd.Parameters.AddWithValue("@Branch_Landmark", objBranchAddress.Landmark);
            cmd.Parameters.AddWithValue("@Country_Id", objBranchAddress.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", objBranchAddress.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", objBranchAddress.City_Id);
            cmd.Parameters.AddWithValue("@Branch_pincode", objBranchAddress.Pin_code);
            cmd.Parameters.AddWithValue("@Branch_mobileNumber", objBranchAddress.Mobile_Number);
            cmd.Parameters.AddWithValue("@Branch_altMobileNumber", objBranchAddress.Alt_Mobile_Number);
            cmd.Parameters.AddWithValue("@Branch_telephone_Extension", objBranchAddress.Telephone_Extension);
            cmd.Parameters.AddWithValue("@Branch_landLineNumber", objBranchAddress.Landline_Number);
            cmd.Parameters.AddWithValue("@Branch_altLandLineNumber", objBranchAddress.Alt_Landline_Number);
            cmd.Parameters.AddWithValue("@Branch_Website", objBranchAddress.Website);
            cmd.Parameters.AddWithValue("@User_Id", objBranchAddress.User_Id);
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
        public DataSet GetBranchAddress(int BranchAddressid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_t_branch_address_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_Address_Id", BranchAddressid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetBranchAddressForEdit(BranchAddress objBranchAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_t_branch_address_for_edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_Address_Id", objBranchAddress.Branch_Add_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdateBranchAddress(BranchAddress objBranchAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_branchaddress", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@branch_Address_Id", objBranchAddress.Branch_Add_Id);
            cmd.Parameters.AddWithValue("@Branch_Id", objBranchAddress.Branch_Id);
            cmd.Parameters.AddWithValue("@Branch_contactPerson", objBranchAddress.Contact_Person);
            cmd.Parameters.AddWithValue("@Branch_houseBuildingNo", objBranchAddress.House_No);
            cmd.Parameters.AddWithValue("@Branch_street1", objBranchAddress.Street_1);
            cmd.Parameters.AddWithValue("@Branch_Street2", objBranchAddress.Street_2);
            cmd.Parameters.AddWithValue("@Branch_Landmark", objBranchAddress.Landmark);
            cmd.Parameters.AddWithValue("@Country_Id", objBranchAddress.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", objBranchAddress.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", objBranchAddress.City_Id);
            cmd.Parameters.AddWithValue("@Branch_pincode", objBranchAddress.Pin_code);
            cmd.Parameters.AddWithValue("@Branch_mobileNumber", objBranchAddress.Mobile_Number);
            cmd.Parameters.AddWithValue("@Branch_altMobileNumber", objBranchAddress.Alt_Mobile_Number);
            cmd.Parameters.AddWithValue("@Branch_telephone_Extension", objBranchAddress.Telephone_Extension);
            cmd.Parameters.AddWithValue("@Branch_landLineNumber", objBranchAddress.Landline_Number);
            cmd.Parameters.AddWithValue("@Branch_altLandLineNumber", objBranchAddress.Alt_Landline_Number);
            cmd.Parameters.AddWithValue("@Branch_Website", objBranchAddress.Website);
            //  cmd.Parameters.AddWithValue("@State_Id", objBranchAddress.Is_Warehouse);
            cmd.Parameters.AddWithValue("@User_Id", objBranchAddress.User_Id);
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
        public int DeleteBranchAddress(BranchAddress objBranchAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_branch_address1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@branch_Address_Id", objBranchAddress.Branch_Add_Id);
            cmd.Parameters.AddWithValue("@User_Id", objBranchAddress.User_Id);
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

        public int GetBranchAddressApproveStatus(BranchAddress objBranchAddress)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_branchaddress_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Address_Id", objBranchAddress.Branch_Add_Id);
            cmd.Parameters.AddWithValue("@User_Id", objBranchAddress.User_Id);
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
        public DataSet GetBranchAddressForListForPopup(BranchAddress objBranchAddress)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_t_branch_address_for_list_for_popup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_Address_Id", objBranchAddress.Branch_Add_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }
}