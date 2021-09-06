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
    public class CompanyGroup
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Company_Group_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Currency_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Language_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Group_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Group_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Currency { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Language_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Company_Image { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertCompanyGroup(CompanyGroup objCompanyGroup)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_Company_Group", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Company_Group_Code", objCompanyGroup.Company_Group_Code);
            cmd.Parameters.AddWithValue("@Company_Group_Name", objCompanyGroup.Company_Group_Name);
            cmd.Parameters.AddWithValue("@Currency_Id", objCompanyGroup.Currency_Id);
            cmd.Parameters.AddWithValue("@Language_Id", objCompanyGroup.Language_Id);
            cmd.Parameters.AddWithValue("@Company_Logo_Url", objCompanyGroup.Company_Image);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyGroup.User_Id);
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
        public int InsertCompanyGroupForBulk(CompanyGroup objCompanyGroup)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_Company_Groupforbulk", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Company_Group_Name", objCompanyGroup.Company_Group_Name);
           
            cmd.Parameters.AddWithValue("@User_Id", objCompanyGroup.User_Id);
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

        public DataSet GetCompanyGroup(int CompanyGroupid)
        {
              DataSet ds = new DataSet();
              try
              {
                  string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                  MySqlConnection con = new MySqlConnection(strcon);
                  MySqlCommand cmd = new MySqlCommand("proc_get_m_company_group_for_list", con);
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@company_group_Id", CompanyGroupid);
                  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                  da.Fill(ds);
              }
              catch (Exception ex) { }
            return ds;
        }
        public int UpdateCompanyGroup(CompanyGroup objCompanyGroup)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_company_group", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Group_Id", objCompanyGroup.Company_Group_Id);
            cmd.Parameters.AddWithValue("@Company_Group_Code", objCompanyGroup.Company_Group_Code);
            cmd.Parameters.AddWithValue("@Company_Group_Name", objCompanyGroup.Company_Group_Name);
            cmd.Parameters.AddWithValue("@Currency_Id", objCompanyGroup.Currency_Id);
            cmd.Parameters.AddWithValue("@Language_Id", objCompanyGroup.Language_Id);
            cmd.Parameters.AddWithValue("@Company_Logo_Url", objCompanyGroup.Company_Image);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyGroup.User_Id);
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
        public int DeleteCompanyGroup(CompanyGroup objCompanyGroup)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_company_group", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@company_Group_Id", objCompanyGroup.Company_Group_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyGroup.User_Id);
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
        public DataSet GetCompanyGroupName()
        {

            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_company_group_Name", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int GetCompanyGroupApproveStatus(CompanyGroup objCompanyGroup)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_companygroup_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Company_Group_Id", objCompanyGroup.Company_Group_Id);
            cmd.Parameters.AddWithValue("@User_Id", objCompanyGroup.User_Id);
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