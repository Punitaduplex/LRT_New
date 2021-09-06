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
    public class Area
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Area_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Country_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int State_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Country_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int City_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Area_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Approve_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }



        public int InsertArea(Area objArea)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_m_area", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Country_Id", objArea.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", objArea.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", objArea.City_Id);
            cmd.Parameters.AddWithValue("@Area_Name", objArea.Area_Name);
            cmd.Parameters.AddWithValue("@User_Id", objArea.User_Id);
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
        public DataSet GetArea(int Areaid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_area_for_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Area_Id", Areaid);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int UpdateArea(Area objArea)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Update_m_area", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Area_Id", objArea.Area_Id);
            cmd.Parameters.AddWithValue("@Country_Id", objArea.Country_Id);
            cmd.Parameters.AddWithValue("@State_Id", objArea.State_Id);
            cmd.Parameters.AddWithValue("@City_Id", objArea.City_Id);
            cmd.Parameters.AddWithValue("@Area_Name", objArea.Area_Name);
            cmd.Parameters.AddWithValue("@User_Id", objArea.User_Id);
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
        public int DeleteArea(Area objArea)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_delete_m_area", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Area_Id", objArea.Area_Id);
            cmd.Parameters.AddWithValue("@User_Id", objArea.User_Id);
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
        public DataSet GetAreaName()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_get_m_area_name", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public int GetAreaApproveStatus(Area objArea)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_approve_m_area_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Area_Id", objArea.Area_Id);
            cmd.Parameters.AddWithValue("@User_Id", objArea.User_Id);
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