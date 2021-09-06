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
    public class MaterialCategory
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Material_Category_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Category_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Category_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        public int InsertMaterialCategory(MaterialCategory objMaterialCategory)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_material_category", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Material_Category_Code", objMaterialCategory.Material_Category_Code);
            cmd.Parameters.AddWithValue("@Material_Category_Name", objMaterialCategory.Material_Category_Name);
            cmd.Parameters.AddWithValue("@User_Id", objMaterialCategory.User_Id);
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
        public DataSet GetMaterialCategory(int MaterialCategoryid)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_material_category_for_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@material_category_Id", MaterialCategoryid);
               
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int UpdateMaterialCategory(MaterialCategory objMaterialCategory)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_material_Category", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Material_Category_Id", objMaterialCategory.Material_Category_Id);
            cmd.Parameters.AddWithValue("@Material_Category_Code", objMaterialCategory.Material_Category_Code);
            cmd.Parameters.AddWithValue("@Material_Category_Name", objMaterialCategory.Material_Category_Name);
            cmd.Parameters.AddWithValue("@User_Id", objMaterialCategory.User_Id);
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
        public int DeleteMaterialCategory(MaterialCategory objMaterialCategory)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_material_category", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Material_Category_Id", objMaterialCategory.Material_Category_Id);
            cmd.Parameters.AddWithValue("@User_Id", objMaterialCategory.User_Id);
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
        public DataSet GetMaterialCategoryName()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_m_material_category_Name", con);
                cmd.CommandType = CommandType.StoredProcedure;


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int GetMaterialCategoryApproveStatus(MaterialCategory objMaterialCategory)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_materialcategory_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Material_Category_Id", objMaterialCategory.Material_Category_Id);
            cmd.Parameters.AddWithValue("@User_Id", objMaterialCategory.User_Id);
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