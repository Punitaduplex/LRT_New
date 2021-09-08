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
    public class Vendor
    {
        public string Vendor_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Company_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string Vendor_Contact_Person { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Email_ID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        
        public int Vendor_Invitation_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        
        public string Vendor_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
      
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Access_Permission { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Password { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<VendorType> Type_List { get; set; }
        public string InsertVendorInvitation(Vendor objVendor)
        {
            string i = "";

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendor_Invitation", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Vendor_Company", Regex.Replace(objVendor.Vendor_Company_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Contact_Person", Regex.Replace(objVendor.Vendor_Contact_Person.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Email", Regex.Replace(objVendor.Vendor_Email_ID.Trim(), @"\s+", " "));
            // cmd.Parameters.AddWithValue("@Access_Permission", Regex.Replace(objVendor.Access_Permission.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Id", objVendor.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.VarChar, 50));
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
        public string InsertVendorInvitationDetail(Vendor objVendor)
        {
            string i = 0.ToString();

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendor_invitation_type", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Invitation_Id", objVendor.Vendor_Invitation_Id);
            cmd.Parameters.AddWithValue("@Type_Id", objVendor.Vendor_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendor.User_Id);
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
        public DataSet GetVendorInvitation(int VendorInvitationId)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_Invitation_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Vendor_Invitation_Id", VendorInvitationId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetVendorTypeAccVendorInvitationId(Vendor objVendor)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_type_acc_invitation_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Vendor_Invitation_Id", objVendor.Vendor_Invitation_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdateVendorInvitation(Vendor objVendor)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_v_vendor_Invitation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Invitation_Id", objVendor.Vendor_Invitation_Id);
            cmd.Parameters.AddWithValue("@Vendor_Company", Regex.Replace(objVendor.Vendor_Company_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Contact_Person", Regex.Replace(objVendor.Vendor_Contact_Person.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Email", Regex.Replace(objVendor.Vendor_Email_ID.Trim(), @"\s+", " "));
            //cmd.Parameters.AddWithValue("@Access_Permission", Regex.Replace(objVendor.Access_Permission.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Id", objVendor.User_Id);
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
        public int DeleteVendorInvitation(Vendor objVendor)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_v_vendor_Invitation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Invitation_Id", objVendor.Vendor_Invitation_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendor.User_Id);
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
        public int DeleteVendorInvitationType(Vendor objVendor)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Delete_v_vendor_invitation_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Invitation_Id", objVendor.Vendor_Invitation_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendor.User_Id);
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
        public int GetVendorInvitationApproveStatus(Vendor objVendor)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_v_vendor_Invitation_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Invitation_Id", objVendor.Vendor_Invitation_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendor.User_Id);
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

        public DataSet GetVendorCompanyAccEmail(Vendor objVendor)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_email_check", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email_Id", Regex.Replace(objVendor.Vendor_Email_ID.Trim(), @"\s+", " "));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetVendorListeddtls()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendorlisted_Dtls ", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }


        public DataSet GetVendorListed_dtls_BySearch(Vendor objlistedvendor)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendorlisted_Dtls_by_Search ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vendor_firstName", objlistedvendor.Vendor_Name);
            cmd.Parameters.AddWithValue("@company_Name", objlistedvendor.Vendor_Company_Name);
            cmd.Parameters.AddWithValue("@vendor_Email", objlistedvendor.Vendor_Email_ID);
            cmd.Parameters.AddWithValue("@vendor_teli_phoneNo1", objlistedvendor.Vendor_Contact_Person);
         

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }


        //----------------------------------pending-------------------------------------------
        public DataSet GetpendingVendorListed_dtls_BySearch(Vendor objlistedvendor)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_pending_vendorlisted_Dtls_by_Search ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vendor_firstName", objlistedvendor.Vendor_Name);
            cmd.Parameters.AddWithValue("@company_Name", objlistedvendor.Vendor_Company_Name);
            cmd.Parameters.AddWithValue("@vendor_Email", objlistedvendor.Vendor_Email_ID);
            cmd.Parameters.AddWithValue("@vendor_teli_phoneNo1", objlistedvendor.Vendor_Contact_Person);


            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }


        public DataSet GetPendingVendorListdtls()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_Pending_vendorlisted_Dtls ", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
    public class VendorRegistation
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Company_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contact_Person{ get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Email_ID { get; set; }
       
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Job_Description { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Company_Address { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Country_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Country_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int State_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int City_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City_Name { get; set; }
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public int Area_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Pin_Code { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_company_Telephone { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_company_FaxNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_company_MobileNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_company_Attn { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_company_GST { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_company_SST { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Payment_Term_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Category_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Category_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Payment_Term_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Region { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Work_Sample_Url{ get; set; }
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string Vendor_System_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Vendor_Name1 { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Vendor_Email1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Vendor_Name2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Vendor_Email2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Telephone_1 { get; set; }
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string Telephone_2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Address { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_City_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Country_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Pin_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Longitude { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Latitude { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Bank_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Bank_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Swift_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Account_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Receiver_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Bank_Address { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Bank_Postal_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Bank_City_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Bank_Country_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Bank_State_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bank_Contact_Number_1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bank_Contact_Number_2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Registration_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Eng_Level { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Telephone_Number_1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Telephone_Number_2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Paypal_Email_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NDA_Form_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Machine_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Machine_Function { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Machine_Capability{ get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Request_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Bank_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Permission_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Swift_Code_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Country_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_City_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bank_Country_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bank_City_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bank_State_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool Is_View { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Permission_Id_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Is_View_Str { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_LogIn_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_LogIn_Password { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Permission_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Form_24 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Form_48 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Form_9 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Other_Form { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Label1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Label2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Label3 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Count1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Approved_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Pending_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Registration_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Credit_Limit_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Website_Link { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Twitter_link { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Facebook_link { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string From_Currency { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string From_Currency_Amt { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string To_Currency { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string To_Currency_Amt { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Profile_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Profile_Url { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Catalog_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Catelog_Link { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Primary_Contact { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Other_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_First_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendor_Last_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Save_Data_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Vendor_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Machine_Addition { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<VendorType> Type_List { get; set; }



        public string InsertVendorRegistation(VendorRegistation objVendorRegistation)
        {
            string i = 0.ToString();

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendorregistration", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Registration_Status", Regex.Replace(objVendorRegistation.Registration_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Registation_No", Regex.Replace(objVendorRegistation.Company_Registration_No.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Sort_Name", Regex.Replace(objVendorRegistation.Contact_Person.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Name", Regex.Replace(objVendorRegistation.Vendor_Company_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Address", Regex.Replace(objVendorRegistation.Vendor_Company_Address.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_countryId", objVendorRegistation.Country_Id);
            cmd.Parameters.AddWithValue("@Company_stateId", objVendorRegistation.State_Id);
            cmd.Parameters.AddWithValue("@Company_cityId", objVendorRegistation.City_Id);
            cmd.Parameters.AddWithValue("@Company_pinCode", objVendorRegistation.Pin_Code);
            cmd.Parameters.AddWithValue("@Company_Teliphone", Regex.Replace(objVendorRegistation.Vendor_company_Telephone.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_FaxNo", Regex.Replace(objVendorRegistation.Vendor_company_FaxNo.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_MobileNo", Regex.Replace(objVendorRegistation.Vendor_company_MobileNo.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_alternanteNo", Regex.Replace(objVendorRegistation.Vendor_company_Attn.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_GST", Regex.Replace(objVendorRegistation.Vendor_company_GST.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_SST", Regex.Replace(objVendorRegistation.Vendor_company_SST.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Payment_Term_Id", objVendorRegistation.Payment_Term_Id);
            cmd.Parameters.AddWithValue("@Currency_Id", objVendorRegistation.Currency_Id);
            cmd.Parameters.AddWithValue("@Company_Region", Regex.Replace(objVendorRegistation.Vendor_Region.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Credit_Limit_Type", Regex.Replace(objVendorRegistation.Credit_Limit_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Website_Link", Regex.Replace(objVendorRegistation.Website_Link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Facebook_Link", Regex.Replace(objVendorRegistation.Facebook_link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Twitter_Link", Regex.Replace(objVendorRegistation.Twitter_link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@From_Currency", Regex.Replace(objVendorRegistation.From_Currency.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@To_Currency", Regex.Replace(objVendorRegistation.To_Currency.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@From_Currency_Amount", Regex.Replace(objVendorRegistation.From_Currency_Amt.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@To_Currency_Amount", Regex.Replace(objVendorRegistation.To_Currency_Amt.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Profile_Type", Regex.Replace(objVendorRegistation.Company_Profile_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Profile_link", objVendorRegistation.Company_Profile_Url);
            cmd.Parameters.AddWithValue("@Catalog_Type", objVendorRegistation.Catalog_Type);
            cmd.Parameters.AddWithValue("@Catelog_link", objVendorRegistation.Catelog_Link);
            cmd.Parameters.AddWithValue("@Company_WorksampleUrl", objVendorRegistation.Vendor_Work_Sample_Url);
            cmd.Parameters.AddWithValue("@NDA_Form_Url", Regex.Replace(objVendorRegistation.NDA_Form_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Form_24", objVendorRegistation.Form_24);
            cmd.Parameters.AddWithValue("@Form_48", Regex.Replace(objVendorRegistation.Form_48.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Form_9", objVendorRegistation.Form_9);
            cmd.Parameters.AddWithValue("@Form_Other", Regex.Replace(objVendorRegistation.Other_Form.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Location_Longitude", Regex.Replace(objVendorRegistation.Vendor_Longitude.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Location_Latitude", Regex.Replace(objVendorRegistation.Vendor_Latitude.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Primary_Contact", objVendorRegistation.Primary_Contact);
            cmd.Parameters.AddWithValue("@Vendor_First_Name", Regex.Replace(objVendorRegistation.Vendor_First_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Last_Name", Regex.Replace(objVendorRegistation.Vendor_Last_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Email", Regex.Replace(objVendorRegistation.Vendor_Email_ID.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Alt_Vendor_Name1", objVendorRegistation.Alt_Vendor_Name1);
            cmd.Parameters.AddWithValue("@Alt_Vendor_Email1", Regex.Replace(objVendorRegistation.Alt_Vendor_Email1.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Alt_Vendor_Name2", Regex.Replace(objVendorRegistation.Alt_Vendor_Name2.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Alt_Vendor_Email2", Regex.Replace(objVendorRegistation.Alt_Vendor_Email2.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Teliphone_No1", objVendorRegistation.Vendor_Telephone_Number_1);
            cmd.Parameters.AddWithValue("@Vendor_Address", Regex.Replace(objVendorRegistation.Vendor_Address.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_CityId", objVendorRegistation.Vendor_City_Id);
            cmd.Parameters.AddWithValue("@Vendor_PinCode", Regex.Replace(objVendorRegistation.Vendor_Pin_Code.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_CountryId", objVendorRegistation.Vendor_Country_Id);
            cmd.Parameters.AddWithValue("@Bank_Detail_Type", objVendorRegistation.Vendor_Bank_Type);
            cmd.Parameters.AddWithValue("@Bank_Id", objVendorRegistation.Bank_Id);
            cmd.Parameters.AddWithValue("@Swift_Code", objVendorRegistation.Vendor_Swift_Code);
            //cmd.Parameters.AddWithValue("@Other_Code", objVendorRegistation.Other_Code);
            cmd.Parameters.AddWithValue("@Account_No", Regex.Replace(objVendorRegistation.Vendor_Account_Number.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Reciver_Name", Regex.Replace(objVendorRegistation.Vendor_Receiver_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bank_Address", Regex.Replace(objVendorRegistation.Vendor_Bank_Address.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Postal_Code", Regex.Replace(objVendorRegistation.Vendor_Bank_Postal_Code.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bank_Country_Id", objVendorRegistation.Bank_Country_Id);
            cmd.Parameters.AddWithValue("@Bank_State_Id", objVendorRegistation.Bank_State_Id);
            cmd.Parameters.AddWithValue("@Bank_City_Id", objVendorRegistation.Bank_City_Id);
            cmd.Parameters.AddWithValue("@Bank_ContactNo1", objVendorRegistation.Bank_Contact_Number_1);
            cmd.Parameters.AddWithValue("@Bank_ContactNo2", objVendorRegistation.Bank_Contact_Number_2);
            cmd.Parameters.AddWithValue("@Paypal_Email_Id", objVendorRegistation.Paypal_Email_Id);
            cmd.Parameters.AddWithValue("@Company_Status", Regex.Replace(objVendorRegistation.Vendor_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Request_Status", Regex.Replace(objVendorRegistation.Request_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Data_Save_Type", Regex.Replace(objVendorRegistation.Save_Data_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
                i = 1.ToString();
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
        public int InsertVendorRegistationType(VendorRegistation objVendorRegistation)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendor_registration_Type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@Vendor_Type_Id", objVendorRegistation.Vendor_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public int InsertVendorRegistationMachine(VendorRegistation objVendorRegistation)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_vendor_registration_machine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@Machine_Name", objVendorRegistation.Machine_Name);
            cmd.Parameters.AddWithValue("@Machine_Function", objVendorRegistation.Machine_Function);
            cmd.Parameters.AddWithValue("@Machine_Capability", objVendorRegistation.Machine_Capability);
            cmd.Parameters.AddWithValue("@Machine_Addition", objVendorRegistation.Machine_Addition);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public int UpdateVendorRegistation(VendorRegistation objVendorRegistation)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_v_vendor_registration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@Registration_Status", Regex.Replace(objVendorRegistation.Registration_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Registation_No", Regex.Replace(objVendorRegistation.Company_Registration_No.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Sort_Name", Regex.Replace(objVendorRegistation.Contact_Person.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Name", Regex.Replace(objVendorRegistation.Vendor_Company_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Address", Regex.Replace(objVendorRegistation.Vendor_Company_Address.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_countryId", objVendorRegistation.Country_Id);
            cmd.Parameters.AddWithValue("@Company_stateId", objVendorRegistation.State_Id);
            cmd.Parameters.AddWithValue("@Company_cityId", objVendorRegistation.City_Id);
            cmd.Parameters.AddWithValue("@Company_pinCode", objVendorRegistation.Pin_Code);
            cmd.Parameters.AddWithValue("@Company_Teliphone", Regex.Replace(objVendorRegistation.Vendor_company_Telephone.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_FaxNo", Regex.Replace(objVendorRegistation.Vendor_company_FaxNo.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_MobileNo", Regex.Replace(objVendorRegistation.Vendor_company_MobileNo.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_alternanteNo", Regex.Replace(objVendorRegistation.Vendor_company_Attn.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_GST", Regex.Replace(objVendorRegistation.Vendor_company_GST.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_SST", Regex.Replace(objVendorRegistation.Vendor_company_SST.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Payment_Term_Id", objVendorRegistation.Payment_Term_Id);
            cmd.Parameters.AddWithValue("@Currency_Id", objVendorRegistation.Currency_Id);
            cmd.Parameters.AddWithValue("@Company_Region", Regex.Replace(objVendorRegistation.Vendor_Region.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Credit_Limit_Type", Regex.Replace(objVendorRegistation.Credit_Limit_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Website_Link", Regex.Replace(objVendorRegistation.Website_Link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Facebook_Link", Regex.Replace(objVendorRegistation.Facebook_link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Twitter_Link", Regex.Replace(objVendorRegistation.Twitter_link.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@From_Currency", Regex.Replace(objVendorRegistation.From_Currency.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@To_Currency", Regex.Replace(objVendorRegistation.To_Currency.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@From_Currency_Amount", Regex.Replace(objVendorRegistation.From_Currency_Amt.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@To_Currency_Amount", Regex.Replace(objVendorRegistation.To_Currency_Amt.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Profile_Type", Regex.Replace(objVendorRegistation.Company_Profile_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Company_Profile_link", objVendorRegistation.Company_Profile_Url);
            cmd.Parameters.AddWithValue("@Catalog_Type", objVendorRegistation.Catalog_Type);
            cmd.Parameters.AddWithValue("@Catelog_link", objVendorRegistation.Catelog_Link);
            cmd.Parameters.AddWithValue("@Company_WorksampleUrl", objVendorRegistation.Vendor_Work_Sample_Url);
            cmd.Parameters.AddWithValue("@NDA_Form_Url", Regex.Replace(objVendorRegistation.NDA_Form_Url.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Form_24", objVendorRegistation.Form_24);
            cmd.Parameters.AddWithValue("@Form_48", Regex.Replace(objVendorRegistation.Form_48.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Form_9", objVendorRegistation.Form_9);
            cmd.Parameters.AddWithValue("@Form_Other", Regex.Replace(objVendorRegistation.Other_Form.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Location_Longitude", Regex.Replace(objVendorRegistation.Vendor_Longitude.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Location_Latitude", Regex.Replace(objVendorRegistation.Vendor_Latitude.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Primary_Contact", objVendorRegistation.Primary_Contact);
            cmd.Parameters.AddWithValue("@Vendor_First_Name", Regex.Replace(objVendorRegistation.Vendor_First_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Last_Name", Regex.Replace(objVendorRegistation.Vendor_Last_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Email", Regex.Replace(objVendorRegistation.Vendor_Email_ID.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Alt_Vendor_Name1", objVendorRegistation.Alt_Vendor_Name1);
            cmd.Parameters.AddWithValue("@Alt_Vendor_Email1", Regex.Replace(objVendorRegistation.Alt_Vendor_Email1.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Alt_Vendor_Name2", Regex.Replace(objVendorRegistation.Alt_Vendor_Name2.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Alt_Vendor_Email2", Regex.Replace(objVendorRegistation.Alt_Vendor_Email2.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_Teliphone_No1", objVendorRegistation.Vendor_Telephone_Number_1);
            cmd.Parameters.AddWithValue("@Vendor_Address", Regex.Replace(objVendorRegistation.Vendor_Address.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_CityId", objVendorRegistation.Vendor_City_Id);
            cmd.Parameters.AddWithValue("@Vendor_PinCode", Regex.Replace(objVendorRegistation.Vendor_Pin_Code.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Vendor_CountryId", objVendorRegistation.Vendor_Country_Id);
            cmd.Parameters.AddWithValue("@Bank_Detail_Type", objVendorRegistation.Vendor_Bank_Type);
            cmd.Parameters.AddWithValue("@Bank_Id", objVendorRegistation.Bank_Id);
            cmd.Parameters.AddWithValue("@Swift_Code", objVendorRegistation.Vendor_Swift_Code);
            //cmd.Parameters.AddWithValue("@Other_Code", objVendorRegistation.Other_Code);
            cmd.Parameters.AddWithValue("@Account_No", Regex.Replace(objVendorRegistation.Vendor_Account_Number.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Reciver_Name", Regex.Replace(objVendorRegistation.Vendor_Receiver_Name.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bank_Address", Regex.Replace(objVendorRegistation.Vendor_Bank_Address.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Postal_Code", Regex.Replace(objVendorRegistation.Vendor_Bank_Postal_Code.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Bank_Country_Id", objVendorRegistation.Bank_Country_Id);
            cmd.Parameters.AddWithValue("@Bank_State_Id", objVendorRegistation.Bank_State_Id);
            cmd.Parameters.AddWithValue("@Bank_City_Id", objVendorRegistation.Bank_City_Id);
            cmd.Parameters.AddWithValue("@Bank_ContactNo1", objVendorRegistation.Bank_Contact_Number_1);
            cmd.Parameters.AddWithValue("@Bank_ContactNo2", objVendorRegistation.Bank_Contact_Number_2);
            cmd.Parameters.AddWithValue("@Paypal_Email_Id", objVendorRegistation.Paypal_Email_Id);
            cmd.Parameters.AddWithValue("@Company_Status", Regex.Replace(objVendorRegistation.Vendor_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Request_Status", Regex.Replace(objVendorRegistation.Request_Status.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@Data_Save_Type", Regex.Replace(objVendorRegistation.Save_Data_Type.Trim(), @"\s+", " "));
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public DataSet GetVendorRequestDetail(int VendorId)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_request_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Vendor_Id", VendorId);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetVendorRegistrationListForPopup(VendorRegistation objVendorRegistation)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_registration_list_for_popup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public string GetVendorRegistrationApproveStatus(VendorRegistation objVendorRegistation)
        {
            string i = "0";
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_v_vendor_Request_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
           
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
            cmd.Parameters.Add(new MySqlParameter("@error1", MySqlDbType.VarChar));
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
        public int GetVendorRegistrationStandByStatus(VendorRegistation objVendorRegistation)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_stand_by_v_vendor_Request_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public DataSet GetVendorInvitationDetailAccCompanyName(VendorRegistation objVendorRegistation)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_check_invitation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Company_Name", objVendorRegistation.Vendor_Company_Name);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetVenderDetailAccSearchValues(VendorRegistation objVendorRegistation)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_search_acc_searchValues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Sort_Name", objVendorRegistation.Contact_Person);
            cmd.Parameters.AddWithValue("@Vendor_Company_Name", objVendorRegistation.Vendor_Company_Name);
            cmd.Parameters.AddWithValue("@Vendor_Category", objVendorRegistation.Vendor_Category_Name);
            cmd.Parameters.AddWithValue("@Vendor_Status", objVendorRegistation.Vendor_Status);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetRegisteredVenderDetailAccSearchValues(VendorRegistation objVendorRegistation)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_registered_vendor_acc_searchValues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Sort_Name", objVendorRegistation.Contact_Person);
            cmd.Parameters.AddWithValue("@Vendor_Company_Name", objVendorRegistation.Vendor_Company_Name);
            cmd.Parameters.AddWithValue("@Vendor_Category", objVendorRegistation.Vendor_Category_Name);
            cmd.Parameters.AddWithValue("@Vendor_Status", objVendorRegistation.Vendor_Status);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetVenderTopTenDetailForSearch()
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_search_topten", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Vendor_Sort_Name", objVendorRegistation.Contact_Person);
            //cmd.Parameters.AddWithValue("@Vendor_Company_Name", objVendorRegistation.Vendor_Company_Name);
            //cmd.Parameters.AddWithValue("@Vendor_Category", objVendorRegistation.Vendor_Category_Name);
            //cmd.Parameters.AddWithValue("@Vendor_Status", objVendorRegistation.Vendor_Status);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetSearchVenderAccKeywordsForCompanyName(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_Vendor_wildcard_for_company_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Search_Keywords", keyword);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }

        public DataSet GetSearchVenderAccKeywordsForSortName(string keyword)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_Vendor_wildcard_for_sort_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Search_Keywords", keyword);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public int GetChangeVendorCompanyStatus(VendorRegistation objVendorRegistation)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_change_v_vendor_Company_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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

        public int InsertActionPermission(VendorRegistation objVendorRegistation)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_v_action_permission", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Permission_Id", objVendorRegistation.Permission_Id);
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);

            cmd.Parameters.AddWithValue("@Is_View", objVendorRegistation.Is_View);
            cmd.Parameters.AddWithValue("@Vendor_Login_Id", objVendorRegistation.Vendor_LogIn_Id);
            cmd.Parameters.AddWithValue("@Vendor_Login_PWD", objVendorRegistation.Vendor_LogIn_Password);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public DataSet GetVendorDetailForGenratePassword(VendorRegistation objVendorRegistation)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_detail_for_genrate_pwd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetActionPermissionAccVendorId(VendorRegistation objVendorRegistation)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_action_permission_acc_vendor_id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_id", objVendorRegistation.Vendor_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { 
            }
            return ds;

        }
        public DataSet GetActionPermission()
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_action_permission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Vendor_id", objVendorRegistation.Vendor_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public int DeleteVendorActionPermission(VendorRegistation objVendorRegistation)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_v_vendor_action_permission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public int DeleteVendorRegistrationType(VendorRegistation objVendorRegistation)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_vendor_registration_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
        public int DeleteVendorRegistrationMachine(VendorRegistation objVendorRegistation)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_vendor_registration_machine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            cmd.Parameters.AddWithValue("@User_Id", objVendorRegistation.User_Id);
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
       
        public DataSet GetVendorApprovalStatusChart()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_count_For_approval_status_chart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetStandbyVenderDetailAccSearchValues(VendorRegistation objVendorRegistation)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_standby_acc_searchValues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Sort_Name", objVendorRegistation.Contact_Person);
            cmd.Parameters.AddWithValue("@Vendor_Company_Name", objVendorRegistation.Vendor_Company_Name);
            cmd.Parameters.AddWithValue("@Vendor_Category", objVendorRegistation.Vendor_Category_Name);
            //cmd.Parameters.AddWithValue("@Vendor_Status", objVendorRegistation.Vendor_Status);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetVendorDetailAccVendorId(VendorRegistation objVendorRegistation)
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_detail_acc_vendor_id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vendor_Id", objVendorRegistation.Vendor_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetStandbyVenderTopTenDetailForSearch()
        {

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_standby_topten", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;

        }
        public DataSet GetVendorRequestStatusChart()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_count_For_request_status_chart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetVendorName()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_v_vendor_name", con);
                cmd.CommandType = CommandType.StoredProcedure;


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
    }


}