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
    public class Project
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Project_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Path { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Drawing_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Part_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Drawing_URL { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Design { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sales { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_By { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Project_Detail_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Detail { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Project_Count { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Keyword { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Search_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Engineering_Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Programer { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime Due_Date { get; set; }

        public string InsertProject(ProjectDetail objProjectDetail)
        {
            string i = "";

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_e_project", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Project_Name", objProjectDetail.Project_Name);
            cmd.Parameters.AddWithValue("@Project_Number", objProjectDetail.Project_Number);
            cmd.Parameters.AddWithValue("@Project_Type", objProjectDetail.Project_Type);
            cmd.Parameters.AddWithValue("@Project_By", objProjectDetail.Project_By);
            cmd.Parameters.AddWithValue("@User_Id", objProjectDetail.User_Id);
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


        public int InsertEngineering(Project objProject)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_e_engineering", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Project_Detail_Id", objProject.Project_Detail_Id);
            cmd.Parameters.AddWithValue("@Due_Date", objProject.Due_Date);
            cmd.Parameters.AddWithValue("@Project_Programer", objProject.Project_Programer);
            cmd.Parameters.AddWithValue("@User_Id", objProject.User_Id);
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


        public DataSet GetProjectCount()
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetProjectDetail(Project objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_Detail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_By", objProject.Project_By);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetProjectDetailForSorting(Project objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_Detail_sorting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_Id", objProject.Project_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public int ProjectSorting(Project objProject)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_sort_e_project_drawing", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Project_Detail_Id", objProject.Project_Detail_Id);
            cmd.Parameters.AddWithValue("@Part_No", objProject.Part_No);
            cmd.Parameters.AddWithValue("@Drawing_No", objProject.Drawing_No);
            cmd.Parameters.AddWithValue("@Drawing_URL", objProject.Drawing_URL);
            cmd.Parameters.AddWithValue("@User_Id", objProject.User_Id);
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
        public DataSet GetProjectNameAccProjectNo(Project objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_project_name_acc_project_no", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_No", objProject.Project_Number);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            { }
            return ds;
        }
        public DataSet GetProjectCADDetailAccProjectId(Project objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_project_CAD_acc_project_Id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_Id", objProject.Project_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) 
            { }
            return ds;
        }
        public DataSet GetProgramerProjectList(Project objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_detail_for_programer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Programer", objProject.Project_Programer);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) 
            { }
            return ds;
        }
    }

    public class ProjectDetail
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_By { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Project_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Project_Detail_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Part_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Drawing_No { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Drawing_URL { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int User_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Engineering_Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Due_Date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Upload_Date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Verified_By { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Done_By { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Elapsed_Time { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Project_Link { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Finished_Time { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Thickness { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Material_Finishing { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CAD_File_Type { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CAD_File_Format { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Old_part_Number { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Revision_Number { get; set; }
        public int InsertProjectDetail(ProjectDetail objProjectDetail)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_e_project_detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Project_Id", objProjectDetail.Project_Id);
            cmd.Parameters.AddWithValue("@Part_No", objProjectDetail.Project_Part_No);
            cmd.Parameters.AddWithValue("@CAD_File_Name", objProjectDetail.Project_Drawing_No);
            cmd.Parameters.AddWithValue("@CAD_File_URL", objProjectDetail.Project_Drawing_URL);
            cmd.Parameters.AddWithValue("@Material_Type", objProjectDetail.Material_Type);
            cmd.Parameters.AddWithValue("@Material_Thickness", objProjectDetail.Material_Thickness);
            cmd.Parameters.AddWithValue("@Material_Finishing", objProjectDetail.Material_Finishing);
            cmd.Parameters.AddWithValue("@CAD_File_Type", objProjectDetail.CAD_File_Type);
            cmd.Parameters.AddWithValue("@CAD_File_Format", objProjectDetail.CAD_File_Format);
            cmd.Parameters.AddWithValue("@User_Id", objProjectDetail.User_Id);
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

        public DataSet GetProjectListForSearch(ProjectDetail objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_Detail_for_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_no", objProject.Project_Number);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }

        public DataSet GetProjectListForEdit(ProjectDetail objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_Detail_for_Edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_no", objProject.Project_Number);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public DataSet GetProjectListForEditAction(ProjectDetail objProject)
        {
            DataSet ds = new DataSet();
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
                MySqlConnection con = new MySqlConnection(strcon);
                MySqlCommand cmd = new MySqlCommand("proc_get_e_project_Detail_for_edit_action", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_Detail_Id", objProject.Project_Detail_Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex) { }
            return ds;
        }
        public int RemoveDrawingNumber(ProjectDetail objProjectDetail)
        {
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_remove_e_drawing_number", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Project_Detail_Id", objProjectDetail.Project_Detail_Id);
            cmd.Parameters.AddWithValue("@User_Id", objProjectDetail.User_Id);
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

        public int InsertProjectRevision(ProjectDetail objProjectDetail)
        {
            int i = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strcon);
            MySqlCommand cmd = new MySqlCommand("proc_Insert_e_project_revision", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Project_Detail_Id", objProjectDetail.Project_Detail_Id);
            cmd.Parameters.AddWithValue("@Part_No", objProjectDetail.Project_Part_No);
            cmd.Parameters.AddWithValue("@CAD_File_Name", objProjectDetail.Project_Drawing_No);
            cmd.Parameters.AddWithValue("@CAD_File_URL", objProjectDetail.Project_Drawing_URL);
            cmd.Parameters.AddWithValue("@User_Id", objProjectDetail.User_Id);
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