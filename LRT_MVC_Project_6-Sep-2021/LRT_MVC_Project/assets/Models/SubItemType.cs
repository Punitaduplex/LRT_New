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
    public class SubItemType
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Sub_item_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
       
        public int User_Id { get; set; }

        public int InsertSubItemType(SubItemType objsubitemtype)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_sub_item_type", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Item_Type_Id", objsubitemtype.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Name", objsubitemtype.Sub_Item_Type_Name);
            cmd.Parameters.AddWithValue("@User_Id", objsubitemtype.User_Id);
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
        public DataSet GetSubItemType(int subitemtypeid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_type_for_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Id", subitemtypeid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateSubItemType(SubItemType objsubitemtype)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_sub_item_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Id", objsubitemtype.Sub_item_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Id", objsubitemtype.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Name", objsubitemtype.Sub_Item_Type_Name);
            cmd.Parameters.AddWithValue("@User_Id", objsubitemtype.User_Id);
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
        public int DeleteSubItemType(SubItemType objsubitemtype)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_sub_item_type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Id", objsubitemtype.Sub_item_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objsubitemtype.User_Id);
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

        //public DataSet GetSubItemTypeName(SubItemType objSubItemType)
        //{
        //    string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
        //    MySqlConnection con = new MySqlConnection(strcon);
        //    MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_type_name", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Item_Type_Id", objSubItemType.Item_Type_Id);
        //    DataSet ds = new DataSet();
        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //    da.Fill(ds);
        //    return ds;
        //}
        public DataSet GetSubItemTypeName(SubItemType objSubItemType)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_type_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", objSubItemType.Item_Type_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public int GetSubItemTypeApproveStatus(SubItemType objSubItemType)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_subitemtype_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Id", objSubItemType.Sub_item_Type_Id);
            cmd.Parameters.AddWithValue("@User_Id", objSubItemType.User_Id);
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

    public class SubItemTypeField
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Sub_item_Type_Field_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Sub_item_Type_Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Type_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Name { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_Optional { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_Optional_Value { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_Input_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sub_Item_Type_Field_Input_Value { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Sub_Item_Type_Field_Priority { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Sub_Item_Type_Field_Status { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int User_Id { get; set; }

        public int InsertSubItemTypeField(SubItemTypeField objSubItemTypeField)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_subitemtypefield", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Type_Id", objSubItemTypeField.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Id", objSubItemTypeField.Sub_item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Field_Name", objSubItemTypeField.Sub_Item_Type_Field_Name);
            cmd.Parameters.AddWithValue("@Optional_Type", objSubItemTypeField.Sub_Item_Type_Field_Optional);
            cmd.Parameters.AddWithValue("@Optional_Value", objSubItemTypeField.Sub_Item_Type_Field_Optional_Value);
            cmd.Parameters.AddWithValue("@Text_Type", objSubItemTypeField.Sub_Item_Type_Field_Input_Type);
            // cmd.Parameters.AddWithValue("@Text_Value", objSubItemTypeField.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Field_Priority", objSubItemTypeField.Sub_Item_Type_Field_Priority);
            cmd.Parameters.AddWithValue("@Is_Active", objSubItemTypeField.Sub_Item_Type_Field_Status);
            cmd.Parameters.AddWithValue("@User_Id", objSubItemTypeField.User_Id);
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
        public DataSet GetSubItemTypeField(int subitemtypefieldid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_subitemtypefield_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Id", subitemtypefieldid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateSubItemTypeField(SubItemTypeField objSubItemTypeField)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_subitemtypefield", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Field_Id", objSubItemTypeField.Sub_item_Type_Field_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Id", objSubItemTypeField.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Id", objSubItemTypeField.Sub_item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Field_Name", objSubItemTypeField.Sub_Item_Type_Field_Name);
            cmd.Parameters.AddWithValue("@Optional_Type", objSubItemTypeField.Sub_Item_Type_Field_Optional);
            cmd.Parameters.AddWithValue("@Optional_Value", objSubItemTypeField.Sub_Item_Type_Field_Optional_Value);
            cmd.Parameters.AddWithValue("@Text_Type", objSubItemTypeField.Sub_Item_Type_Field_Input_Type);
            // cmd.Parameters.AddWithValue("@Text_Value", objSubItemTypeField.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Sub_Item_Field_Priority", objSubItemTypeField.Sub_Item_Type_Field_Priority);
            cmd.Parameters.AddWithValue("@Is_Active", objSubItemTypeField.Sub_Item_Type_Field_Status);
            cmd.Parameters.AddWithValue("@User_Id", objSubItemTypeField.User_Id);
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
        public int DeleteSubItemTypeField(SubItemTypeField objSubItemTypeField)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_subitemtypefield", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Id", objSubItemTypeField.Sub_item_Type_Field_Id);
            cmd.Parameters.AddWithValue("@User_Id", objSubItemTypeField.User_Id);
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


        //public DataSet GetSubItemTypeName(SubItemType objSubItemType)
        //{
        //    string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
        //    MySqlConnection con = new MySqlConnection(strcon);
        //    MySqlCommand cmd = new MySqlCommand("proc_get_m_sub_item_type_name", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Item_Type_Id", objSubItemType.Item_Type_Id);
        //    DataSet ds = new DataSet();
        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //    da.Fill(ds);
        //    return ds;
        //}

        public int GetSubItemTypeFieldApproveStatus(SubItemTypeField objSubItemTypeField)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_subitemtypefield_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sub_Item_Type_Field_Id", objSubItemTypeField.Sub_item_Type_Field_Id);
            cmd.Parameters.AddWithValue("@User_Id", objSubItemTypeField.User_Id);
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