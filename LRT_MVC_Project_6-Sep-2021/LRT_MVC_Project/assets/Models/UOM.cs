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
    public class UOM
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UOM_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UOM_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UOM_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        public int InsertUOM(UOM objUOM)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_umo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UMO_Code", objUOM.UOM_Code);
            cmd.Parameters.AddWithValue("@UMO_Name", objUOM.UOM_Name);
            cmd.Parameters.AddWithValue("@User_Id", objUOM.User_Id);
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
        public DataSet GetUOM(int UOMid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_umo_for_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Id", UOMid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateUOM(UOM objUOM)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_umo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Id", objUOM.UOM_Id);
            cmd.Parameters.AddWithValue("@UMO_Code", objUOM.UOM_Code);
            cmd.Parameters.AddWithValue("@UMO_Name", objUOM.UOM_Name);
            cmd.Parameters.AddWithValue("@User_Id", objUOM.User_Id);
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
        public int DeleteUOM(UOM objUOM)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_uom", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UMO_Id", objUOM.UOM_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUOM.User_Id);
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
        public DataSet GetUOMBaseAltName()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_umo_name", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public int GetUOMApproveStatus(UOM objUOM)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_uom_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UOM_Id", objUOM.UOM_Id);
            cmd.Parameters.AddWithValue("@User_Id", objUOM.User_Id);
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