using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LRTProject.Models
{
    public class Item
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public  int  Item_Id {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Group_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Valuation_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Type_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Sub_Type_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Item_Manage_Code_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Valuation_Method_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Material_Category_Id  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Description  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Manufacture  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Brand  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Supplier_Name  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Price {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Min_Order_Qty {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Threshold  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_HS_Code  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_URL_Link {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_CoCountry_of_Origin  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Manufacturing_Part_Number  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public decimal Item_Shipping_Cost {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Contact_Details  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_EMail  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_We_Chat  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_WhatsApp  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_Other_Contact_Type  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Data_Sheet_Url  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Compliance_Url  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_CAD_File_Url  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Certification_Url  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Scrapping_from_WebUrl  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Images_Url  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string item_Status  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Internal_Item_Code  {get;set;}
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UOM_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UOM_Group_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Valuation_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Sub_Type_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Manage_Item_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Category_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Item_Search_Value { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Label1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Count { get; set; }
        public int InsertItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_item", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UOM_Id", objItem.UOM_Id);
            cmd.Parameters.AddWithValue("@UOM_Group_Id", objItem.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@Valuation_Id", objItem.Valuation_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItem.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", objItem.Item_Sub_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Manage_Code_Id", objItem.Item_Manage_Code_Id);
            cmd.Parameters.AddWithValue("@Valuation_Method_Id", objItem.Valuation_Method_Id);
            cmd.Parameters.AddWithValue("@Material_Category_Id", objItem.Material_Category_Id);
            cmd.Parameters.AddWithValue("@Item_Description", objItem.Item_Description);
            cmd.Parameters.AddWithValue("@Item_Manufacture", objItem.Item_Manufacture);
            cmd.Parameters.AddWithValue("@Item_Brand", objItem.Item_Brand);
            cmd.Parameters.AddWithValue("@Supplier_Name", objItem.Supplier_Name);
            cmd.Parameters.AddWithValue("@Item_Price", objItem.Item_Price);
            cmd.Parameters.AddWithValue("@Item_Min_Order_Qty", objItem.Item_Min_Order_Qty);
            cmd.Parameters.AddWithValue("@Item_Thres_hold", objItem.Item_Threshold);
            cmd.Parameters.AddWithValue("@item_HS_Code", objItem.Item_HS_Code);
            cmd.Parameters.AddWithValue("@item_URL_Link", objItem.Item_URL_Link);
            cmd.Parameters.AddWithValue("@Item_CoCountryof_Origin", objItem.Item_CoCountry_of_Origin);
            cmd.Parameters.AddWithValue("@Item_Manufacturing_Part_Number", objItem.Item_Manufacturing_Part_Number);
            cmd.Parameters.AddWithValue("@Item_Shipping_Cost", objItem.Item_Shipping_Cost);
            cmd.Parameters.AddWithValue("@Item_Contact_Details", objItem.Item_Contact_Details);
            cmd.Parameters.AddWithValue("@Item_EMail", objItem.Item_EMail);
            cmd.Parameters.AddWithValue("@Item_WeChat", objItem.Item_We_Chat);
            cmd.Parameters.AddWithValue("@item_Whats_App", objItem.Item_WhatsApp);
            cmd.Parameters.AddWithValue("@Item_Other_Contact_Type", objItem.item_Other_Contact_Type);
            cmd.Parameters.AddWithValue("@Item_Data_Sheet_Url", objItem.Item_Data_Sheet_Url);
            cmd.Parameters.AddWithValue("@Item_Compliance_Url", objItem.Item_Compliance_Url);
            cmd.Parameters.AddWithValue("@item_CADFile_Url", objItem.item_CAD_File_Url);
            cmd.Parameters.AddWithValue("@Item_Certification_Url", objItem.Item_Certification_Url);
            cmd.Parameters.AddWithValue("@Item_Scrapping_fromWeb_Url", objItem.Item_Scrapping_from_WebUrl);
            cmd.Parameters.AddWithValue("@Item_Images_Url", objItem.Item_Images_Url);
            cmd.Parameters.AddWithValue("@Item_Status", objItem.item_Status);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
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

        public int UpdateItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@UOM_Id", objItem.UOM_Id);
            cmd.Parameters.AddWithValue("@UOM_Group_Id", objItem.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@Valuation_Id", objItem.Valuation_Id);
            cmd.Parameters.AddWithValue("@Item_Type_Id", objItem.Item_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Sub_Type_Id", objItem.Item_Sub_Type_Id);
            cmd.Parameters.AddWithValue("@Item_Manage_Code_Id", objItem.Item_Manage_Code_Id);
            cmd.Parameters.AddWithValue("@Valuation_Method_Id", objItem.Valuation_Method_Id);
            cmd.Parameters.AddWithValue("@Material_Category_Id", objItem.Material_Category_Id);
            cmd.Parameters.AddWithValue("@Item_Description", objItem.Item_Description);
            cmd.Parameters.AddWithValue("@Item_Manufacture", objItem.Item_Manufacture);
            cmd.Parameters.AddWithValue("@Item_Brand", objItem.Item_Brand);
            cmd.Parameters.AddWithValue("@Supplier_Name", objItem.Supplier_Name);
            cmd.Parameters.AddWithValue("@Item_Price", objItem.Item_Price);
            cmd.Parameters.AddWithValue("@Item_Min_Order_Qty", objItem.Item_Min_Order_Qty);
            cmd.Parameters.AddWithValue("@Item_Thres_hold", objItem.Item_Threshold);
            cmd.Parameters.AddWithValue("@item_HS_Code", objItem.Item_HS_Code);
            cmd.Parameters.AddWithValue("@item_URL_Link", objItem.Item_URL_Link);
            cmd.Parameters.AddWithValue("@Item_CoCountryof_Origin", objItem.Item_CoCountry_of_Origin);
            cmd.Parameters.AddWithValue("@Item_Manufacturing_Part_Number", objItem.Item_Manufacturing_Part_Number);
            cmd.Parameters.AddWithValue("@Item_Shipping_Cost", objItem.Item_Shipping_Cost);
            cmd.Parameters.AddWithValue("@Item_Contact_Details", objItem.Item_Contact_Details);
            cmd.Parameters.AddWithValue("@Item_EMail", objItem.Item_EMail);
            cmd.Parameters.AddWithValue("@Item_WeChat", objItem.Item_We_Chat);
            cmd.Parameters.AddWithValue("@item_Whats_App", objItem.Item_WhatsApp);
            cmd.Parameters.AddWithValue("@Item_Other_Contact_Type", objItem.item_Other_Contact_Type);
            cmd.Parameters.AddWithValue("@Item_Data_Sheet_Url", objItem.Item_Data_Sheet_Url);
            cmd.Parameters.AddWithValue("@Item_Compliance_Url", objItem.Item_Compliance_Url);
            cmd.Parameters.AddWithValue("@item_CADFile_Url", objItem.item_CAD_File_Url);
            cmd.Parameters.AddWithValue("@Item_Certification_Url", objItem.Item_Certification_Url);
            cmd.Parameters.AddWithValue("@Item_Scrapping_fromWeb_Url", objItem.Item_Scrapping_from_WebUrl);
            cmd.Parameters.AddWithValue("@Item_Images_Url", objItem.Item_Images_Url);
            cmd.Parameters.AddWithValue("@Item_Status", objItem.item_Status);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
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
        public DataSet GetItem(int Itemid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_edit_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int DeleteItem(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
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
        public int ItemStatusChange(Item objItem)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_m_item_Inactive_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item_Id", objItem.Item_Id);
            cmd.Parameters.AddWithValue("@User_Id", objItem.User_Id);
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
        public DataSet GetItemForSearchList(Item ObjItem)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_search_list_Acc_Type_Id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item_Type_Id", ObjItem.Item_Type_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public DataSet GetItemForSearchListAccSearchValue(Item ObjItem)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_search_acc_searchValue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchValue", ObjItem.Item_Search_Value);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public DataSet GetItemForList(int Itemid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public DataSet GetItemForListForPopup(Item objitemlistview)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_list_for_popup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item_Id", objitemlistview.Item_Id);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public DataSet GetItemForcolumnBarChart()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_item_count_For_Columan_bar_chart", con);
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.AddWithValue("@item_Type_Id", Itemid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
}