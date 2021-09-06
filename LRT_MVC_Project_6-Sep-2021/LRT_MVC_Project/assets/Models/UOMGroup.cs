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
    public class UOMGroup
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Group_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Group_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Group_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }

        public int InsertUOMGroup(UOMGroup objuomgrp)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_umo_group", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UMO_Group_Code", objuomgrp.Group_Code);
            cmd.Parameters.AddWithValue("@UMO_Group_Name", objuomgrp.Group_Name);
            cmd.Parameters.AddWithValue("@User_Id", objuomgrp.User_Id);
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
        public DataSet GetUOMGroup(int UomGrpid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_umo_group_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_group_Id", UomGrpid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateUOMGroup(UOMGroup objuomgrp)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_umo_group", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Group_Id", objuomgrp.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@UMO_Group_Code", objuomgrp.Group_Code);
            cmd.Parameters.AddWithValue("@UMO_Group_Name", objuomgrp.Group_Name);
            cmd.Parameters.AddWithValue("@User_Id", objuomgrp.User_Id);
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
        public int DeleteUOMGroup(UOMGroup objuomgrp)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_uom_group", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Group_Id", objuomgrp.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@User_Id", objuomgrp.User_Id);
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
        public DataSet GetUOMGroupName()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_umo_group_name", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public int GetUOMGroupApproveStatus(UOMGroup objUOMGroup)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_uomgroup_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UOM_Group_Id", objUOMGroup.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUOMGroup.User_Id);
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

    public class UOMGroupQty
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Group_Qty_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Group_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Group_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Base_Quantity { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Base_UOM  { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Base_UOM_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_Quantity { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Alt_UOM  { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Alt_UOM_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        
        public int User_Id { get; set; }

        public int InsertUOMGroupQty(UOMGroupQty objUOMGroupMap)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_t_uomgroupmapping", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UMO_Group_Id", objUOMGroupMap.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@Base_Qty", objUOMGroupMap.Base_Quantity);
            cmd.Parameters.AddWithValue("@Base_UMO_Id", objUOMGroupMap.Base_UOM_Id);
            cmd.Parameters.AddWithValue("@Alt_Qty", objUOMGroupMap.Alt_Quantity);
            cmd.Parameters.AddWithValue("@Alt_UOM_Id", objUOMGroupMap.Alt_UOM_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUOMGroupMap.User_Id);
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
        public DataSet GetUOMGroupQty(int UOMGroupQtyid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_t_uom_group_mapping_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Mapping_Id", UOMGroupQtyid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateUOMGroupQty(UOMGroupQty objUOMGroupQty)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_t_uom_group_mapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Mapping_Id", objUOMGroupQty.UOM_Group_Qty_Id);
            cmd.Parameters.AddWithValue("@UMO_Group_Id", objUOMGroupQty.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@Base_Qty", objUOMGroupQty.Base_Quantity);
            cmd.Parameters.AddWithValue("@Base_UMO_Id", objUOMGroupQty.Base_UOM_Id);
            cmd.Parameters.AddWithValue("@Alt_Qty", objUOMGroupQty.Alt_Quantity);
            cmd.Parameters.AddWithValue("@Alt_UOM_Id", objUOMGroupQty.Alt_UOM_Id);
            //  cmd.Parameters.AddWithValue("@State_Id", objBranchAddress.Is_Warehouse);
            cmd.Parameters.AddWithValue("@User_Id", objUOMGroupQty.User_Id);
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
        public int DeleteUOMGroupQty(UOMGroupQty objUOMGroupMap)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_t_uom_group_mapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Mapping_Id", objUOMGroupMap.UOM_Group_Qty_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUOMGroupMap.User_Id);
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
        public int GetUOMGroupQtyApproveStatus(UOMGroupQty objUOMGroupQty)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_t_uomgroupmapping_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UOM_Group_Mapping_Id", objUOMGroupQty.UOM_Group_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUOMGroupQty.User_Id);
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