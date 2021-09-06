using LRT_MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRT_MVC_Project.Controllers
{
    public class EngineeringController : Controller
    {
        // GET: Engineering
        public ActionResult Index()
        {
            return View();
        }

        #region //---------------dashboard------------------------------------
        public ActionResult EngineeringDashboard()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }
        #endregion


        #region//--------------------Project Master-------------------------
        public ActionResult ProjectMaster()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }

        public ActionResult ProjectList()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }
        [HttpPost]
        public JsonResult UploadProjectFile()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    //string path = "";
                    string zpath = "";
                    string cadfilename = "";
                    string FileType = "";
                    string FileFormate = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if (fileex == "3DXML" || fileex == "CGF" || fileex == "EPRT" || fileex == "IGS" || fileex == "SLDFTP" || fileex == "STEP" || fileex == "STEP" || fileex == "X_T_File" || fileex == "SOLIDWORKS Part")
                        {
                            if (i == 0)
                            {
                                FileFormate = "3D";
                            }
                            else
                            {
                                FileFormate = FileFormate + ",3D";
                            }

                        }
                        else if (fileex == "DWG" || fileex == "DXF")
                        {
                            if (i == 0)
                            {
                                FileFormate = "2D";
                            }
                            else
                            {
                                FileFormate = FileFormate + ",2D";
                            }

                        }
                        else if (fileex == "PDF" || fileex == "pdf")
                        {
                            if (i == 0)
                            {
                                FileFormate = "PDF";
                            }
                            else
                            {
                                FileFormate = FileFormate + ",PDF";
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                FileFormate = "CAD";
                            }
                            else
                            {
                                FileFormate = FileFormate + ",CAD";
                            }
                        }
                        if ((fileex == "dxf") || (fileex == "DWG") || (fileex == "swp") || (fileex == "STEP") || (fileex == "IGES") || (fileex == "3DS") || (fileex == "COLLADA") || (fileex == "SD Object") || (fileex == "FBX") || (fileex == "OBJ") || (fileex == "STL") || (fileex == "VRML/X3D") || (fileex == "3DXML") || (fileex == "CGR") || (fileex == "CGR") || (fileex == "3DXML") || (fileex == "DWG") || (fileex == "DXF") || (fileex == "EPRT") || (fileex == "IGS") || (fileex == "SLDFTP") || (fileex == "SLDPRT") || (fileex == "WRL") || (fileex == "X_T") || (fileex == "1") || (fileex == "SLDDRW") || (fileex == "pdf") || (fileex == "PDF") || fileex == "SOLIDWORKS Part")
                        {

                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                                if (i == 0)
                                {
                                    cadfilename = fname;
                                    FileType = "CAD";
                                }
                                else
                                {
                                    cadfilename = cadfilename + "," + fname;
                                    FileType = FileType + "," + "CAD";
                                }
                            }
                            else
                            {
                                fname = file.FileName;
                                if (i == 0)
                                {
                                    cadfilename = fname;
                                    FileType = "CAD";
                                }
                                else// Exists = false
                                {
                                    cadfilename = cadfilename + "," + fname;
                                    FileType = FileType + "," + "CAD";
                                }
                                //Path.GetFileName(file.FileName)
                            }
                            #region
                            string namewithoutEXt = Path.GetFileNameWithoutExtension(fname);
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/project_design/"), namewithoutEXt + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            // file.SaveAs(fname);
                          //  path = "/uploaded_image/project_design/" + name;
                            //sms = s[0] + " file uploaded successfully!+" + path;
                            string filenameWitoutextension = Path.GetFileNameWithoutExtension(name);
                            System.IO.Directory.CreateDirectory(Server.MapPath(@"\uploaded_image\project_design\") + filenameWitoutextension);
                            string filname = Path.Combine(Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension), filenameWitoutextension + "." + fileex);
                            file.SaveAs(filname);
                            var archive = Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension + fileex + ".Zip");
                            var temp = Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension);
                            ZipFile.CreateFromDirectory(temp, archive);
                           // string zpath = "/uploaded_image/project_design/" + filenameWitoutextension + ".Zip";

                            try
                            {
                                DirectoryInfo directory = new DirectoryInfo(temp);

                                foreach (FileInfo f in directory.GetFiles())
                                {
                                    f.Delete();
                                }

                                directory.Delete(true);
                            }
                            catch (Exception ex) {

                                try
                                {
                                    DirectoryInfo directory = new DirectoryInfo(temp);
                                    foreach (FileInfo f in directory.GetFiles())
                                    {
                                        f.Delete();
                                    }

                                    directory.Delete(true);
                                }
                                catch (Exception ex1) { }
                            }

                             if (i == 0)
                            {
                                zpath = "/uploaded_image/project_design/" + filenameWitoutextension + ".Zip";
                                // path = "/uploaded_image/project_design/" + name;
                            }
                            else
                            {
                                zpath = zpath + ",/uploaded_image/project_design/" + filenameWitoutextension + ".Zip";
                            }

                             sms = "File Uploaded Successfully!+" + zpath + "+" + cadfilename + "+" + FileType + "+" + FileFormate;


                            #endregion



                            #region//----------------old-------------------------------------
                           // string namewithoutEXt = Path.GetFileNameWithoutExtension(fname);
                           // fname = Path.Combine(Server.MapPath("~/uploaded_image/project_design/"), namewithoutEXt + "." + fileex);
                           // string[] splitpath = fname.Split('\\');
                           // string name = (splitpath[splitpath.Length - 1]);
                            // file.SaveAs(fname);
                            //string filenameWitoutextension = Path.GetFileNameWithoutExtension(name);
                           // System.IO.Directory.CreateDirectory(Server.MapPath(@"\uploaded_image\project_design\") + filenameWitoutextension);
                            //string filname = Path.Combine(Server.MapPath("~/uploaded_image/project_design/"), namewithoutEXt + "." + fileex);
                           // file.SaveAs(filname);
                           // var archive = Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension + ".Zip");
                           // var temp = Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension);
                           // System.IO.DirectoryInfo di = new DirectoryInfo(archive);
                            //try{
                            //foreach (DirectoryInfo dir in di.GetDirectories())
                            //{
                            //    dir.Delete(true);
                            //}
                            //}
                            //catch (Exception ex) { }

                            //DirectoryInfo directory = new DirectoryInfo(temp);
                            //try
                            //{
                            //    ZipFile.CreateFromDirectory(temp, archive);
                           
                            //foreach (FileInfo f in directory.GetFiles())
                            //{
                            //    f.Delete();
                            //}
                            //Directory.Delete(temp);
                            //if (i == 0)
                            //{
                            //    zpath = "/uploaded_image/project_design/" + filenameWitoutextension + ".Zip";
                            //    // path = "/uploaded_image/project_design/" + name;
                            //}
                            //else
                            //{
                            //    zpath = zpath + ",/uploaded_image/project_design/" + filenameWitoutextension + ".Zip";
                            //}
                            //sms = "File Uploaded Successfully!+" + zpath + "+" + cadfilename + "+" + FileType + "+" + FileFormate;
                            //}
                            //catch (Exception ex)
                            //{
                            //    foreach (FileInfo f in directory.GetFiles())
                            //    {
                            //        f.Delete();
                            //    } 
                            //    Directory.Delete(temp);
                            //    sms =di.Name+ " File alredy exist";
                            //}
                            #endregion
                        }
                       
                    }
                   
                    return Json(sms);
                }
                catch (Exception ex)
                {
                    return Json("This file alredy exist");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        [HttpPost]
        public JsonResult AddProject(ProjectDetail objProjectDetail)
        {
            string sms = "";
            Project project = new Project();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objProjectDetail.User_Id = Convert.ToInt32(cookie.Value);
            if (objProjectDetail.Project_Id == 0)
            {
                string[] s = project.InsertProject(objProjectDetail).Split(',');
                int i = Convert.ToInt32(s[0]);
                if (i == 0)
                {
                    project.Project_Id = Convert.ToInt32(s[1]);
                    string[] part = objProjectDetail.Project_Part_No.Split(',');
                    string[] drawing = objProjectDetail.Project_Drawing_No.Split(',');
                    string[] drawingUrl = objProjectDetail.Project_Drawing_URL.Split(',');
                    string[] mType = objProjectDetail.Material_Type.Split(',');
                    string[] thickness = objProjectDetail.Material_Thickness.Split(',');
                    string[] finishing = objProjectDetail.Material_Finishing.Split(',');
                    string[] cadFile = objProjectDetail.CAD_File_Type.Split(',');
                    string[] FileFormat = objProjectDetail.CAD_File_Format.Split(',');
                    for (int j = 0; j < part.Length; j++)
                    {
                        ProjectDetail pd = new ProjectDetail();
                        pd.Project_Id = Convert.ToInt32(s[1]);
                        pd.Project_Part_No = part[j];
                        pd.Project_Drawing_No = drawing[j];
                        pd.Project_Drawing_URL = drawingUrl[j];
                        pd.Material_Type = mType[j];
                        pd.Material_Thickness = thickness[j];
                        pd.Material_Finishing = finishing[j];
                        pd.CAD_File_Type = cadFile[j];
                        pd.CAD_File_Format = FileFormat[j];
                        pd.InsertProjectDetail(pd);
                    }

                    sms = "**Data inserted successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }
            //else
            //{

            //    int i = country.UpdateCountry(objcountry);
            //    if (i == 0)
            //    {
            //        sms = "**Data updated successfully**";
            //    }
            //    else
            //    {
            //        sms = "**Data already exist**";
            //    }
            //}
            return Json(sms);
        }

        [HttpGet]
        public JsonResult ProjectCount()
        {
            Project project = new Project();
            List<Project> listproject = new List<Project>();
            DataSet ds = project.GetProjectCount();
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Project project1 = new Project();
                    project1.Project_By = Convert.ToString(ds.Tables[0].Rows[i]["project_by"]);
                    project1.Project_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["count"]);
                    listproject.Add(project1);
                }
            }
            catch (Exception ex) { }

            return Json(listproject, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ProjectDesignDetail(Project objProject)
        {
            Project project = new Project();
            List<Project> projectList = new List<Project>();
            DataSet ds = project.GetProjectDetail(objProject);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Project project1 = new Project();
                project1.Project_Name = Convert.ToString(dr["projectName"]);
                project1.Project_Number = Convert.ToString(dr["projectNumber"]);
                project1.Project_Type = Convert.ToString(dr["projectType"]);
                project1.Project_Count = Convert.ToInt32(dr["count"]);
                project1.Project_Id = Convert.ToInt32(dr["projectId"]);
                projectList.Add(project1);

            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProjectDesignDetailForSorting(Project objProject)
        {
            Project project = new Project();
            List<Project> projectList = new List<Project>();
            DataSet ds = project.GetProjectDetailForSorting(objProject);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Project project1 = new Project();
                project1.Part_No = Convert.ToString(dr["PartNo"]);
                project1.Drawing_No = Convert.ToString(dr["cadfileName"]);
                project1.Drawing_URL = Convert.ToString(dr["catfileUrl"]);
                project1.Project_Detail_Id = Convert.ToInt32(dr["projectDetailId"]);
                projectList.Add(project1);

            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProjectDesignSorting(Project objProject)
        {
            string[] part = objProject.Part_No.Split(',');
            string[] drawing = objProject.Drawing_No.Split(',');
            string[] drawingUrl = objProject.Drawing_URL.Split(',');
            string[] DetailId = objProject.Project_Detail.Split(',');
            List<Project> newlist = new List<Project>();
            for (int i = 0; i < part.Length; i++)
            {
                string PartNo = part[i];
                try
                {
                    for (int j = 0; j < part.Length; j++)
                    {
                        string[] drawing1 = drawing[j].Split('.');
                        if (PartNo == drawing1[0])
                        {
                            Project project = new Project();
                            Project p = new Project();
                            p.Part_No = PartNo;
                            p.Drawing_No = drawing[j];
                            p.Drawing_URL = drawingUrl[j];
                            p.Project_Detail_Id = Convert.ToInt32(DetailId[i]);
                            newlist.Add(p);
                            project.ProjectSorting(p);
                            break;
                        }
                    }
                }
                catch (Exception ex) { }
            }


            return Json(newlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProjectNumberSearch(string prefix="")
        {

            SearchItem searchitem = new SearchItem();
            List<SearchItem> searchitemList = new List<SearchItem>();
            DataSet ds = searchitem.GetSearchProjectNoAccKeywords(prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                searchitemList.Add(new SearchItem
                {

                    Search_Name = dr["SearchItem"].ToString(),

                    // Item_Id = dr["itemId"].ToString()

                });

            }

            return Json(searchitemList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ProjectNameAccProjectNo(Project objProject)
            {
            Project project = new Project();
            // List<Project> projectList = new List<Project>();
            DataSet ds = project.GetProjectNameAccProjectNo(objProject);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                project.Project_Name = Convert.ToString(dr["projectName"]);
                project.Project_Id = Convert.ToInt32(dr["projectId"]);


            }
            return Json(project, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProjectCADDetailAccProjectId(Project objProject)
        {
            Project project = new Project();
            List<ProjectDetail> projectList = new List<ProjectDetail>();
            DataSet ds = project.GetProjectCADDetailAccProjectId(objProject);
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProjectDetail p = new ProjectDetail();
                    p.Project_Part_No = Convert.ToString(dr["PartNo"]);
                    p.Project_Drawing_No = Convert.ToString(dr["cadfileName"]);
                    p.Project_Drawing_URL = Convert.ToString(dr["catfileUrl"]);
                    p.Project_Detail_Id = Convert.ToInt32(dr["projectDetailId"]);
                    projectList.Add(p);

                }
            }
            catch (Exception ex) { }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region//--------------------engineering------------------------------
        public ActionResult ProjectLoad()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }
        [HttpPost]
        public JsonResult AddEngineering(Project objProject)
        {
            string sms = "";
            Project project = new Project();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objProject.User_Id = Convert.ToInt32(cookie.Value);
            if (objProject.Engineering_Id == 0)
            {

                int i = project.InsertEngineering(objProject);
                if (i == 0)
                {

                    sms = "**Data inserted successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }

            return Json(sms);
        }
        #endregion
        #region//----------------------------Programer Project ------------------------
        public ActionResult ProgramerProjectList()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }

        [HttpPost]
        public JsonResult ProgramerProject(Project objProject)
        {
            Project project = new Project();
            List<ProjectDetail> projectList = new List<ProjectDetail>();
            DataSet ds = project.GetProgramerProjectList(objProject);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ProjectDetail p = new ProjectDetail();
                p.Project_Part_No = Convert.ToString(dr["PartNo"]);
                p.Project_Drawing_No = Convert.ToString(dr["cadfileName"]);
                p.Due_Date = Convert.ToString(dr["duteDate"]);
                p.Project_Drawing_URL = Convert.ToString(dr["catfileUrl"]);
                p.Project_Detail_Id = Convert.ToInt32(dr["projectDetailId"]);
                p.Engineering_Id = Convert.ToInt32(dr["engineeringId"]);
                projectList.Add(p);

            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region//-----------------------edit project-----------------
        public ActionResult EditProject()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return View();
            }
            else
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return RedirectToAction("LogIn", "User");
            }
        }
        public ActionResult EditAction()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }

        [HttpPost]
        public ActionResult UploadRevisionFileMethod()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string filename = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if ((fileex == "dxf") || (fileex == "dwg") || (fileex == "swp") || (fileex == "STEP") || (fileex == "IGES") || (fileex == "3DS") || (fileex == "COLLADA") || (fileex == "FBX") || (fileex == "OBJ") || (fileex == "STL") || (fileex == "VRML/X3D") || (fileex == "3DXML") || (fileex == "CGR") || (fileex == "3DXML") || (fileex == "DWG") || (fileex == "DXF") || (fileex == "EPRT") || (fileex == "IGS") || (fileex == "SLDFTP") || (fileex == "SLDPRT") || (fileex == "WRL") || (fileex == "X_T") || (fileex == "1") || (fileex == "PDF") || (fileex == "pdf"))
                        {
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;                                                                           //Path.GetFileName(file.FileName)
                            }

                            string namewithoutEXt = Path.GetFileNameWithoutExtension(fname);
                            filename = namewithoutEXt + " " + s[0] + s[1] + "." + fileex;
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/project_design/"), namewithoutEXt + " " + s[0] + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            //file.SaveAs(fname);
                            path = "/uploaded_image/project_design/" + name;
                            //sms = s[0] + " file uploaded successfully!+" + path;

                            string filenameWitoutextension = Path.GetFileNameWithoutExtension(name);
                            System.IO.Directory.CreateDirectory(Server.MapPath(@"\uploaded_image\project_design\") + filenameWitoutextension);
                            string filname = Path.Combine(Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension), namewithoutEXt + " " + s[0] + s[1] + "." + fileex);
                            file.SaveAs(filname);
                            var archive = Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension + ".Zip");
                            try
                            {
                                FileInfo f1 = new FileInfo(archive);
                                f1.Delete();
                            }
                            catch (Exception ex) { }
                            var temp = Server.MapPath("~/uploaded_image/project_design/" + filenameWitoutextension);

                            ZipFile.CreateFromDirectory(temp, archive);
                            DirectoryInfo directory = new DirectoryInfo(temp);

                            foreach (FileInfo f in directory.GetFiles())
                            {
                                f.Delete();
                            }
                            Directory.Delete(temp);
                            string zpath = "/uploaded_image/project_design/" + filenameWitoutextension + ".Zip";
                            sms = "Revision file uploaded successfully!+" + zpath + "+" + filename;
                        }
                        else
                        { sms = "Please select only STL, OBJ, FBX, COLLADA, 3DS, IGES; STEP, VRML/X3D , dxf , dwg ,and swp,.3DXML,CGR,DWG,DXF,EPRT,IGS,SLDFTP,SLDPRT,WRL,X_T,1 CAD file+" + path; }
                    }
                    return Json(sms);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpPost]
        public JsonResult ProjectListForEdit(ProjectDetail objProjectDetail)
        {
            ProjectDetail project = new ProjectDetail();
            List<ProjectDetail> projectList = new List<ProjectDetail>();
            DataSet ds = project.GetProjectListForEdit(objProjectDetail);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ProjectDetail p = new ProjectDetail();
                p.Project_Part_No = Convert.ToString(dr["PartNo"]);
                p.Project_Drawing_No = Convert.ToString(dr["cadfileName"]);
                p.Project_Detail_Id = Convert.ToInt32(dr["projectDetailId"]);
                p.Project_Id = Convert.ToInt32(dr["projectId"]);

                projectList.Add(p);

            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProjectDetailForEditAction(ProjectDetail objProjectDetail)
        {
            ProjectDetail project = new ProjectDetail();
            List<ProjectDetail> projectList = new List<ProjectDetail>();
            DataSet ds = project.GetProjectListForEditAction(objProjectDetail);
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProjectDetail p = new ProjectDetail();
                    p.Project_Part_No = Convert.ToString(dr["PartNo"]);
                    p.Project_Drawing_No = Convert.ToString(dr["cadfileName"]);
                    p.Project_Number = Convert.ToString(dr["projectNumber"]);
                    p.Project_Name = Convert.ToString(dr["projectName"]);
                    p.Project_By = Convert.ToString(dr["project_by"]);
                    p.Project_Detail_Id = Convert.ToInt32(dr["projectDetailId"]);

                    p.Old_part_Number = Convert.ToString(dr["oldPartNo"]);
                    p.Revision_Number = Convert.ToInt32(dr["revisionNo"]) + 1;
                    projectList.Add(p);

                }
            }
            catch (Exception ex) { }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemovedDrawingNumber(ProjectDetail objProjectDetail)
        {
            string sms = "";
            ProjectDetail project = new ProjectDetail();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objProjectDetail.User_Id = Convert.ToInt32(cookie.Value);

            int i = project.RemoveDrawingNumber(objProjectDetail);
            if (i == 0)
            {
                sms = "This this drawing number has Removed successfully";
            }
            else
            {
                sms = "**This drawing number has not removed**";
            }

            return Json(sms);
        }

        [HttpPost]
        public JsonResult AddProjectRevision(ProjectDetail objProjectDetail)
        {
            string sms = "";
            ProjectDetail project = new ProjectDetail();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objProjectDetail.User_Id = Convert.ToInt32(cookie.Value);
            if (objProjectDetail.Project_Id == 0)
            {
                int i = project.InsertProjectRevision(objProjectDetail);
                if (i == 0)
                {
                    sms = "**Data inserted successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }

            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddProjectDetail(ProjectDetail objProjectDetail)
        {
            string sms = "";
            ProjectDetail project = new ProjectDetail();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objProjectDetail.User_Id = Convert.ToInt32(cookie.Value);
            //if (objProjectDetail.Project_Id == 0)
            //{
          //  project.Project_Id = Convert.ToInt32(s[1]);
            int i=0;
            string[] part = objProjectDetail.Project_Part_No.Split(',');
            string[] drawing = objProjectDetail.Project_Drawing_No.Split(',');
            string[] drawingUrl = objProjectDetail.Project_Drawing_URL.Split(',');
            string[] mType = objProjectDetail.Material_Type.Split(',');
            string[] thickness = objProjectDetail.Material_Thickness.Split(',');
            string[] finishing = objProjectDetail.Material_Finishing.Split(',');
            string[] cadFile = objProjectDetail.CAD_File_Type.Split(',');
            string[] FileFormat = objProjectDetail.CAD_File_Format.Split(',');
            for (int j = 0; j < part.Length; j++)
            {
                ProjectDetail pd = new ProjectDetail();
                pd.Project_Id = objProjectDetail.Project_Id;
                pd.Project_Part_No = part[j];
                pd.Project_Drawing_No = drawing[j];
                pd.Project_Drawing_URL = drawingUrl[j];
                pd.Material_Type = mType[j];
                pd.Material_Thickness = thickness[j];
                pd.Material_Finishing = finishing[j];
                pd.CAD_File_Type = cadFile[j];
                pd.CAD_File_Format = FileFormat[j];
                i=pd.InsertProjectDetail(pd);
            }
           // }
            sms = "**Data inserted successfully**";
            return Json(sms);
        }
        #endregion

        #region//------------------------Search Project------------------------------
        public ActionResult ProjectSearch()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                Common.CommonSetting.name = cookie1.Value;
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "User");
            }
        }

        [HttpPost]
        public JsonResult ProjectListForSearch(ProjectDetail objProjectDetail)
        {
            ProjectDetail project = new ProjectDetail();
            List<ProjectDetail> projectList = new List<ProjectDetail>();
            DataSet ds = project.GetProjectListForSearch(objProjectDetail);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ProjectDetail p = new ProjectDetail();
                p.Project_Part_No = Convert.ToString(dr["PartNo"]);
                p.Project_Drawing_No = Convert.ToString(dr["cadfileName"]);
                p.Due_Date = Convert.ToString(dr["duteDate"]);
                p.Upload_Date = Convert.ToString(dr["UploadDate"]);
                p.Done_By = Convert.ToString(dr["doneBy"]);
                p.Project_Detail_Id = Convert.ToInt32(dr["projectDetailId"]);
                p.Engineering_Id = Convert.ToInt32(dr["engineeringId"]);
                p.Project_Id = Convert.ToInt32(dr["projectId"]);
                string v = Convert.ToString(dr["verifiedBy"]);

                p.Verified_By = Convert.ToString(dr["verifiedBy"]);

                p.Project_Status = Convert.ToString(dr["projectStatus"]);
                p.Elapsed_Time = "";
                p.Finished_Time = "";
                p.Project_Link = "";
                projectList.Add(p);

            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}