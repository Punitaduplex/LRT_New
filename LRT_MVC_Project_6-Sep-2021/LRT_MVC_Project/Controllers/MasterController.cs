using LRT_MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Net.Mail;
using System.Text;





namespace LRT_MVC_Project.Controllers
{
     public class CustomPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;
        MemoryStream stream;
        string contentType;
        string fileName;
        public CustomPostedFile (byte[] fileBytes, string filename = null)
        {
            this.fileBytes = fileBytes;
          
           this.fileName = filename;
           this.contentType = "image/png";
            this.stream = new MemoryStream(fileBytes);
        }

       // public override int ContentLength => fileBytes.Length;
        public override string FileName { get { return fileName; } }
        public override Stream InputStream
        {
            get { return stream; }
        }
        public override string ContentType { get { return contentType; } }

        public override void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.WriteTo(file);
        }
    }
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Index()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            {
                 HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
                 Common.CommonSetting.name = cookie1.Value;
                return View();
               
            }
            else
            {
                //if(this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
                //{
                //    HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                //    string s = cookie.Value;
                //}
               // 
                string[] results = BarcodeLib.BarcodeReader.BarcodeReader.read("c:/qrcode-aspnet.gif", BarcodeLib.BarcodeReader.BarcodeReader.QRCODE);

                return RedirectToAction("LogIn", "User"); 
            }
        }
        public JsonResult GetScan()
        {
             BarcodeLib.BarcodeReader.OptimizeSetting setting = new BarcodeLib.BarcodeReader.OptimizeSetting();

                setting.setMaxOneBarcodePerPage(true);

                BarcodeLib.BarcodeReader.ScanArea top20 = new BarcodeLib.BarcodeReader.ScanArea(new PointF(0.0F, 0.0F), new PointF(100.0F, 20.0F));

                BarcodeLib.BarcodeReader.ScanArea bottom20 = new BarcodeLib.BarcodeReader.ScanArea(new PointF(0.0F, 80.0F), new PointF(100.0F, 100.0F));

                List<BarcodeLib.BarcodeReader.ScanArea> areas = new List<BarcodeLib.BarcodeReader.ScanArea>();
                areas.Add(top20);
                areas.Add(bottom20);

                setting.setAreas(areas);
                string[] results = BarcodeLib.BarcodeReader.BarcodeReader.read("c:/qrcode-aspnet.gif", BarcodeLib.BarcodeReader.BarcodeReader.QRCODE);
                return Json(results, JsonRequestBehavior.AllowGet);
        }
        
        #region//--------------------------------------Menu--------------------------
        public ActionResult SideMenu()
        {
            List<Menu> list = new List<Menu>();
            try
            {
               
                Menu mn = new Menu();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                int userid = Convert.ToInt32(cookie.Value);// Common.CommonSetting.User_Id;
                DataSet ds = mn.GetMenuAccUserId(userid);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Menu menu = new Menu();
                    menu.Menu_Id = Convert.ToInt32(dr["menuId"]);
                    menu.Menu_Name = dr["menuName"].ToString();

                    List<SubMenu> submenulist = new List<SubMenu>();
                    SubMenu sb = new SubMenu();
                    DataSet ds1 = sb.GetSubMenuAccUserId(userid, menu.Menu_Id);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        SubMenu submenu = new SubMenu();
                        submenu.Sub_Menu_Id = Convert.ToInt32(dr1["submenuId"]);
                        submenu.Sub_Menu_Name = dr1["submenuName"].ToString();
                        submenulist.Add(submenu);
                        // ---------------------------------------------------------------------

                        List<SubToSubMenu> subsubmenulist = new List<SubToSubMenu>();
                        SubToSubMenu ssb = new SubToSubMenu();
                        DataSet ds2 = ssb.GetSubToSubMenuAccUserId(userid, submenu.Sub_Menu_Id);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            SubToSubMenu subsubmenu = new SubToSubMenu();
                            subsubmenu.Sub_To_Sub_Menu_Id = Convert.ToInt32(dr2["subtosubMenuId"]);
                            subsubmenu.Sub_To_Sub_Menu_Name = dr2["subtosubmenuName"].ToString();
                            subsubmenu.Page_Url = dr2["pageURL"].ToString();
                            subsubmenulist.Add(subsubmenu);
                        }
                        submenu.Sub_To_Sub_Menu_List = subsubmenulist;
                        //---------------------------------------------------------------------------------------------
                    }
                    menu.Sub_Menu_lsit = submenulist;
                    list.Add(menu);
                }

                //List<Menu> list = new List<Menu>();
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    list.Add(new Menu { Menu_Id = Convert.ToInt32(dr["menuId"]), Menu_Name = dr["menuName"].ToString() });
                //}

            }
            catch (Exception ex) { }
            return PartialView("SideMenu", list);
        }
        //public ActionResult SideMenu()
        //{
        //    List<Menu> list = new List<Menu>();
        //    Menu mn = new Menu();
        //    DataSet ds = mn.GetMenu(0);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Menu menu = new Menu();
        //        menu.Menu_Id = Convert.ToInt32(dr["menuId"]);
        //        menu.Menu_Name = dr["menuName"].ToString();

        //        List<SubMenu> submenulist = new List<SubMenu>();
        //        SubMenu sb = new SubMenu();
        //        DataSet ds1 = sb.GetSubMenu(menu.Menu_Id, 0);
        //        foreach (DataRow dr1 in ds1.Tables[0].Rows)
        //        {
        //            SubMenu submenu = new SubMenu();
        //            submenu.Sub_Menu_Id = Convert.ToInt32(dr1["submenuId"]);
        //            submenu.Sub_Menu_Name = dr1["submenuName"].ToString();
        //            submenulist.Add(submenu);
        //            // ---------------------------------------------------------------------

        //            List<SubToSubMenu> subsubmenulist = new List<SubToSubMenu>();
        //            SubToSubMenu ssb = new SubToSubMenu();
        //            DataSet ds2 = ssb.GetSubToSubMenu(submenu.Sub_Menu_Id, 0);
        //            foreach (DataRow dr2 in ds2.Tables[0].Rows)
        //            {
        //                SubToSubMenu subsubmenu = new SubToSubMenu();
        //                subsubmenu.Sub_To_Sub_Menu_Id = Convert.ToInt32(dr2["subtosubMenuId"]);
        //                subsubmenu.Sub_To_Sub_Menu_Name = dr2["subtosubmenuName"].ToString();
        //                subsubmenu.Page_Url = dr2["pageURL"].ToString();
        //                subsubmenulist.Add(subsubmenu);
        //            }
        //            submenu.Sub_To_Sub_Menu_List = subsubmenulist;
        //            //---------------------------------------------------------------------------------------------
        //        }
        //        menu.Sub_Menu_lsit = submenulist;
        //        list.Add(menu);
        //    }

        //    //List<Menu> list = new List<Menu>();
        //    //foreach (DataRow dr in ds.Tables[0].Rows)
        //    //{
        //    //    list.Add(new Menu { Menu_Id = Convert.ToInt32(dr["menuId"]), Menu_Name = dr["menuName"].ToString() });
        //    //}


        //    return PartialView("SideMenu", list);
        //}
        //[HttpPost]
        //public ActionResult SideMenu()
        //{
        //    List<Menu> menulist = new List<Menu>();
        //    Menu mn = new Menu();
        //    DataSet ds = mn.GetMenu(0);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Menu menu = new Menu();
        //        menu.Menu_Id = Convert.ToInt32(dr["menuId"]);
        //        menu.Menu_Name = dr["menuName"].ToString();

        //        List<SubMenu> submenulist = new List<SubMenu>();
        //        SubMenu sb = new SubMenu();
        //        DataSet ds1 = sb.GetSubMenu(menu.Menu_Id, 0);
        //        foreach (DataRow dr1 in ds1.Tables[0].Rows)
        //        {
        //            SubMenu submenu = new SubMenu();
        //            submenu.Sub_Menu_Id = Convert.ToInt32(dr1["submenuId"]);
        //            submenu.Sub_Menu_Name = dr1["submenuName"].ToString();
        //            submenulist.Add(submenu);
        //            // ---------------------------------------------------------------------

        //            //List<SubToSubMenu> subsubmenulist = new List<SubToSubMenu>();
        //            //SubToSubMenu ssb = new SubToSubMenu();
        //            //DataSet ds2 = ssb.GetSubToSubMenu(submenu.Sub_Menu_Id, 0);
        //            //foreach (DataRow dr2 in ds2.Tables[0].Rows)
        //            //{
        //            //    SubToSubMenu subsubmenu = new SubToSubMenu();
        //            //    subsubmenu.Sub_To_Sub_Menu_Id = Convert.ToInt32(dr2["subtosubMenuId"]);
        //            //    subsubmenu.Sub_To_Sub_Menu_Name = dr2["subtosubmenuName"].ToString();
        //            //    subsubmenu.Page_Url = dr2["pageURL"].ToString();
        //            //    subsubmenulist.Add(subsubmenu);
        //            //}
        //            //submenu.Sub_To_Sub_Menu_List = subsubmenulist;
        //            //---------------------------------------------------------------------------------------------
        //        }
        //        menu.Sub_Menu_lsit = submenulist;
        //        menulist.Add(menu);
        //    }

        //    Session["menu"] = menulist;
        //    return PartialView("MenuBar");
        //}

        #endregion
        #region//------------------Role Permission--------------------------------
        [HttpPost]
        public JsonResult AddRolePermission(RolePermission objRolePermission)
        {
            string sms = "";
            RolePermission rp = new RolePermission();
            try
            { 
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
           // int userid = Convert.ToInt32(cookie.Value);
            objRolePermission.User_Id = Convert.ToInt32(cookie.Value);// Common.CommonSetting.User_Id;
            DataSet ds = rp.GetRolePermission(objRolePermission);
          
                rp.Is_Add = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAdd"]);
                rp.Is_Edit = Convert.ToBoolean(ds.Tables[0].Rows[0]["isEdit"]);
                rp.Is_Delete = Convert.ToBoolean(ds.Tables[0].Rows[0]["isDelete"]);
                rp.Is_View = Convert.ToBoolean(ds.Tables[0].Rows[0]["isView"]);
                rp.Is_Approve = Convert.ToBoolean(ds.Tables[0].Rows[0]["isApprove"]);
            }
            catch (Exception ex)
            { }
            return Json(rp, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public JsonResult SubItemTypeFieldOPtionalValue(SubItemTypeField objSubItemTypeField)
        {
            SubItemTypeField subitemtypefield = new SubItemTypeField();
            try
            {
                DataSet ds = subitemtypefield.GetSubItemTypeFieldOptionalValueAccId(objSubItemTypeField);



                subitemtypefield.Sub_Item_Type_Field_Optional_Value = ds.Tables[0].Rows[0]["optionalValue"].ToString();
            }
            catch (Exception ex) { }
            return Json(subitemtypefield, JsonRequestBehavior.AllowGet);
        }

        #region//----------------------company---------------------------
        #region//-----bank master-----------------------
        public ActionResult BankMaster()
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
        public JsonResult AddBank(Bank objbank)
        {
            string sms = "";
            Bank bank = new Bank();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                //int userid = Convert.ToInt32(cookie.Value);
                objbank.User_Id = Convert.ToInt32(cookie.Value); //Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
                if (objbank.Bank_Id == 0)
                {
                    int i = bank.InsertBank(objbank);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = bank.UpdateBank(objbank);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkBank(Bank objbank)
        {
            string sms = "";
            Bank bank = new Bank();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objbank.User_Id = Convert.ToInt32(cookie.Value);
                string[] bankcode = objbank.Bank_Code.Split(',');
                string[] bankname = objbank.Bank_Name.Split(',');
                string[] swiftcode = objbank.Swift_Code.Split(',');
                for (int i = 0; i < bankcode.Length; i++)
                {
                    Bank bank1 = new Bank();
                   // bank1.Amount = 0.ToString();
                    bank1.Bank_Code = bankcode[i].ToString();
                    bank1.Bank_Name = bankname[i].ToString();
                    bank1.Swift_Code = swiftcode[i].ToString();
                    int j = bank.InsertBank1(bank1);
                }
                sms = "**Bank bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteBank(Bank objbank)
        {
            string sms = "";
            Bank bank = new Bank();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                //int userid = Convert.ToInt32(cookie.Value);
                objbank.User_Id = Convert.ToInt32(cookie.Value);
                // objbank.User_Id = Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);


                int i = bank.DeleteBank(objbank);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult BankDetails()
        {
            Bank bank = new Bank();
            List<Bank> ObjBank = new List<Bank>();
            try
            {
                DataSet ds = bank.GetBank(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ObjBank.Add(new Bank
                    {
                        Bank_Id = Convert.ToInt32(dr["bankId"]),
                        Bank_Code = dr["bankCode"].ToString(),
                        Bank_Name = dr["bankName"].ToString(),
                       // Amount = dr["BankAmount"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            var jsonResult = Json(ObjBank, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
          //  return Json(ObjBank, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BankApproveStatus(Bank objBank)
        {
            string sms = "";
            Bank bank = new Bank();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                //int userid = Convert.ToInt32(cookie.Value);
                objBank.User_Id = Convert.ToInt32(cookie.Value);
                // objBank.User_Id = Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);


                int i = bank.GetBankApproveStatus(objBank);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/BankMaster";
                    MailConst.senderMail("Bank", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult BankName()
        {
            List<SelectListItem> banklist = new List<SelectListItem>();
            Bank b = new Bank();
            try
            {
                DataSet ds = b.GetBankName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    banklist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["bankId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["bankName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            var jsonResult = Json(banklist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           // return Json(banklist);
        }
        [HttpPost]
        public JsonResult SwiftCodeAccBankId(Bank objBank)
        {
            List<SelectListItem> swiftlist = new List<SelectListItem>();
            Bank b = new Bank();
            try
            {
                DataSet ds = b.GetWiftCodeAccBankId(objBank);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    swiftlist.Add(new SelectListItem
                    {
                        Value = dr["swiftCodeId"].ToString(),
                        Text = dr["swiftCode"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(swiftlist);
        }
        #endregion

        #region//--------------------Company Group Master-------------------------
        public ActionResult CompanyGroupMaster()
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
        public ActionResult UploadCompanyGroupImage()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if ((fileex == "jpeg") || (fileex == "JPEG") || (fileex == "jpg") || (fileex == "JPG") || (fileex == "png") || (fileex == "PNG") || (fileex == "gif") || (fileex == "GIF"))
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/company_group/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1]) + "." + fileex;
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/company_group/" + name;
                            sms = "File Uploaded Successfully!+" + path;
                        }
                        else
                        { sms = "Please select only jpeg of jpg file_" + path; }
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
        public JsonResult AddCompanyGroup(CompanyGroup objcompanygroup)
        {
            string sms = "";
            CompanyGroup companygroup = new CompanyGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanygroup.User_Id = Convert.ToInt32(cookie.Value);// Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
                if (objcompanygroup.Company_Group_Id == 0)
                {
                    int i = companygroup.InsertCompanyGroup(objcompanygroup);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = companygroup.UpdateCompanyGroup(objcompanygroup);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkCompanyGroup(CompanyGroup objcompanygroup)
        {
            string sms = "";
            CompanyGroup companygroup = new CompanyGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanygroup.User_Id = Convert.ToInt32(cookie.Value);
                string[] groupname = objcompanygroup.Company_Group_Name.Split(',');
                string[] groupcode = objcompanygroup.Company_Group_Code.Split(',');
                string[] currency = objcompanygroup.Currency.Split(',');
                string[] language = objcompanygroup.Language_Name.Split(',');
                for (int i = 0; i < groupname.Length; i++)
                {
                    Currency c = new Currency();
                    c.Currency_Name = currency[i].ToString();
                    int id = c.InsertCurrencyBulk(c);
                    Language l = new Language();
                    l.Language_Name = language[i].ToString();
                    int id1 = l.InsertLanguageBulk(l);
                    CompanyGroup companygroup1 = new CompanyGroup();
                    companygroup1.Currency_Id = id;
                    companygroup1.Company_Group_Name = groupname[i].ToString();
                    companygroup1.Company_Group_Code = groupcode[i].ToString();
                    companygroup1.Language_Id = id1;
                    companygroup1.Company_Image = "";
                    int j = companygroup.InsertCompanyGroup(companygroup1);
                }
                sms = "**Company group bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteCompanyGroup(CompanyGroup objcompanygroup)
        {
            string sms = "";
            CompanyGroup companygroup = new CompanyGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanygroup.User_Id = Convert.ToInt32(cookie.Value);


                int i = companygroup.DeleteCompanyGroup(objcompanygroup);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CompanyGroupDetails()
        {
            CompanyGroup companygroup = new CompanyGroup();
            List<CompanyGroup> Objcompanygroup = new List<CompanyGroup>();
            try
            {
                DataSet ds = companygroup.GetCompanyGroup(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Objcompanygroup.Add(new CompanyGroup
                    {
                        Company_Group_Id = Convert.ToInt32(dr["companyGroupId"]),
                        Currency_Id = Convert.ToInt32(dr["currencyId"]),
                        Language_Id = Convert.ToInt32(dr["language_Id"]),
                        Company_Group_Code = dr["companyGroupCode"].ToString(),
                        Company_Group_Name = dr["companyGroupName"].ToString(),
                        Language_Name = dr["languageName"].ToString(),
                        Company_Image = dr["companyLogo_Url"].ToString(),
                        Currency = dr["currencyName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objcompanygroup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CompanyGroupApproveStatus(CompanyGroup objCompanyGroup)
        {
            string sms = "";
            CompanyGroup companygroup = new CompanyGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCompanyGroup.User_Id = Convert.ToInt32(cookie.Value);


                int i = companygroup.GetCompanyGroupApproveStatus(objCompanyGroup);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CompanyGroupMaster";
                    MailConst.senderMail("Company Group", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        public JsonResult CompanyGroupName()
        {
            List<SelectListItem> companygrouplist = new List<SelectListItem>();
            CompanyGroup b = new CompanyGroup();
            try
            {
                DataSet ds = b.GetCompanyGroupName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    companygrouplist.Add(new SelectListItem
                    {
                        Value = dr["companyGroupId"].ToString(),
                        Text = dr["companyGroupName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(companygrouplist);
        }

        #endregion

        #region//--------------------company Master-------------------------
        public ActionResult CompanyMaster()
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
        public ActionResult UploadCompanyImage()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if ((fileex == "jpeg") || (fileex == "JPEG") || (fileex == "jpg") || (fileex == "JPG") || (fileex == "png") || (fileex == "PNG") || (fileex == "GIF") || (fileex == "gif"))
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/company/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1]) + "." + fileex;
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/company/" + name;
                            sms = "File Uploaded Successfully!+" + path;
                        }
                        else
                        { sms = "Please select only jpeg of jpg file_" + path; }
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
        public JsonResult AddCompanyMaster(Company objcompanymaster)
        {
            string sms = "";
            Company companymaster = new Company();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanymaster.User_Id = Convert.ToInt32(cookie.Value);
                if (objcompanymaster.Company_Id == 0)
                {
                    int i = companymaster.InsertCompanyMaster(objcompanymaster);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data Already Exist**";
                    }
                }
                else
                {
                    int i = companymaster.UpdateCompanyMaster(objcompanymaster);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkCompany(Company objcompanymaster)
        {
            string sms = "";
            Company company = new Company();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanymaster.User_Id = Convert.ToInt32(cookie.Value);
                string[] companyname = objcompanymaster.Company_Name.Split(',');
                string[] companycode = objcompanymaster.Company_Code.Split(',');
                string[] currency = objcompanymaster.Currency_Name.Split(',');
                string[] language = objcompanymaster.Language_Name.Split(',');
                for (int i = 0; i < companyname.Length; i++)
                {
                    Currency c = new Currency();
                    c.Currency_Name = currency[i].ToString();
                    int id = c.InsertCurrencyBulk(c);
                    Language l = new Language();
                    l.Language_Name = language[i].ToString();
                    int id1 = l.InsertLanguageBulk(l);
                    Company company1 = new Company();
                    company1.Currency_Id = id;
                    company1.Company_Name = companyname[i].ToString();
                    company1.Company_Code = companycode[i].ToString();
                    company1.Language_Id = id1;
                    company1.Company_Image = "";
                    int j = company.InsertCompanyMaster(company1);
                }
                sms = "**Company bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteCompanyMaster(Company objcompanymaster)
        {
            string sms = "";
            Company companymaster = new Company();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanymaster.User_Id = Convert.ToInt32(cookie.Value);

                int i = companymaster.DeleteCompanyMaster(objcompanymaster);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CompanyMasterDetails()
        {
            Company companymaster = new Company();
            List<Company> objcompanymaster = new List<Company>();
            try
            {
                DataSet ds = companymaster.GetCompanyMaster(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objcompanymaster.Add(new Company
                    {
                        Company_Id = Convert.ToInt32(dr["companyId"]),
                        Currency_Id = Convert.ToInt32(dr["currencyId"]),
                        Language_Id = Convert.ToInt32(dr["language_Id"]),
                        Company_Code = dr["companyCode"].ToString(),
                        Company_Name = dr["companyName"].ToString(),
                        Language_Name = dr["languageName"].ToString(),
                        Company_Image = dr["companyLogo_Url"].ToString(),
                        Currency_Name = dr["currencyName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objcompanymaster, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CompanyApproveStatus(Company objCompany)
        {
            string sms = "";
            Company company = new Company();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCompany.User_Id = Convert.ToInt32(cookie.Value);

                int i = company.GetCompanyApproveStatus(objCompany);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CompanyMaster";
                    MailConst.senderMail("Company", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
       
        [HttpPost]
        public JsonResult CompanyMasterName()
        {
            List<SelectListItem> companylist = new List<SelectListItem>();
            Company b = new Company();
            try
            {
                DataSet ds = b.GetCompanyName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    companylist.Add(new SelectListItem
                    {
                        Value = dr["companyId"].ToString(),
                        Text = dr["companyName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(companylist);
        }
        #endregion

        #region//--------------------company address Master-------------------------
        [HttpPost]
        public JsonResult AddCompanyMasterAdd(CompanyAddress objCompanyMasterAddress)
        {
            string sms = "";
            CompanyAddress companymasteradd = new CompanyAddress();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCompanyMasterAddress.User_Id = Convert.ToInt32(cookie.Value);
                if (objCompanyMasterAddress.Company_Add_Id == 0)
                {
                    int i = companymasteradd.InsertCompanyAddress(objCompanyMasterAddress);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = companymasteradd.UpdateCompanyAddress(objCompanyMasterAddress);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteCompanyAddress(CompanyAddress objCompanyMasterAddress)
        {
            string sms = "";
            CompanyAddress companymasteradd = new CompanyAddress();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCompanyMasterAddress.User_Id = Convert.ToInt32(cookie.Value);

                int i = companymasteradd.DeleteCompanyAddress(objCompanyMasterAddress);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CompanyAddressDetails()
        {
            CompanyAddress companyaddress = new CompanyAddress();
            List<CompanyAddress> objcompanyaddress = new List<CompanyAddress>();
            try
            {
                DataSet ds = companyaddress.GetCompanyAddress(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objcompanyaddress.Add(new CompanyAddress
                    {
                        Company_Add_Id = Convert.ToInt32(dr["companyAddressId"]),
                        Company_Id = Convert.ToInt32(dr["companyId"]),
                        Country_Id = Convert.ToInt32(dr["countryId"]),
                        State_Id = Convert.ToInt32(dr["stateId"]),
                        City_Id = Convert.ToInt32(dr["cityId"]),
                        Contact_Person = dr["contactPerson"].ToString(),
                        House_No = dr["houseBuildingNo"].ToString(),
                        Pin_code = dr["pincode"].ToString(),
                        Mobile_Number = dr["mobileNumber"].ToString(),
                        Company_Name = dr["companyName"].ToString(),
                        Country_Name = dr["countryName"].ToString(),
                        State_Name = dr["stateName"].ToString(),
                        City_Name = dr["cityName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objcompanyaddress, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CompanyAddressApproveStatus(CompanyAddress objCompanyAddress)
        {
            string sms = "";
            CompanyAddress companyaddress = new CompanyAddress();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCompanyAddress.User_Id = Convert.ToInt32(cookie.Value);

                int i = companyaddress.GetCompanyAddressApproveStatus(objCompanyAddress);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CompanyMaster";
                    MailConst.senderMail("Company Address", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult CompanyAddressDetailsForEdit(CompanyAddress objcompanymasteradd)
        {
            CompanyAddress companyaddress = new CompanyAddress();
            List<CompanyAddress> companyaddressList = new List<CompanyAddress>();
            try
            {
                DataSet ds = companyaddress.GetCommpanyAddressForEdit(objcompanymasteradd);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    companyaddress.Company_Add_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["companyAddressId"]);
                    companyaddress.Company_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["companyId"]);
                    companyaddress.Contact_Person = Convert.ToString(ds.Tables[0].Rows[0]["contactPerson"]);
                    companyaddress.House_No = Convert.ToString(ds.Tables[0].Rows[0]["houseBuildingNo"]);
                    companyaddress.Street_1 = Convert.ToString(ds.Tables[0].Rows[0]["street1"]);
                    companyaddress.Street_2 = Convert.ToString(ds.Tables[0].Rows[0]["street2"]);
                    companyaddress.Landmark = Convert.ToString(ds.Tables[0].Rows[0]["landmark"]);
                    companyaddress.Country_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["countryId"]);
                    companyaddress.State_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["stateId"]);
                    companyaddress.City_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["cityId"]);
                    companyaddress.Pin_code = Convert.ToString(ds.Tables[0].Rows[0]["pincode"]);
                    companyaddress.Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["mobileNumber"]);
                    companyaddress.Alt_Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["altMobileNumber"]);
                    companyaddress.Telephone_Extension = Convert.ToString(ds.Tables[0].Rows[0]["telephoneNumber"]);
                    companyaddress.Landline_Number = Convert.ToString(ds.Tables[0].Rows[0]["landLineNumber"]);
                    companyaddress.Fax_Number = Convert.ToString(ds.Tables[0].Rows[0]["faxNumber"]);
                    companyaddress.Website = Convert.ToString(ds.Tables[0].Rows[0]["website"]);
                    companyaddress.Company_Email = Convert.ToString(ds.Tables[0].Rows[0]["website"]);
                    companyaddress.Company_Category = Convert.ToString(ds.Tables[0].Rows[0]["category"]);
                }
            }
            catch (Exception ex) { }
            return Json(companyaddress, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CompanyAddressView(CompanyAddress objCompanyAddress)
        {
            CompanyAddress company = new CompanyAddress();
            try
            {
                DataSet ds = company.GetCompanyAddressForListForPopup(objCompanyAddress);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    company.Company_Add_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["companyAddressId"]);
                    company.Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                    company.Contact_Person = ds.Tables[0].Rows[0]["contactPerson"].ToString();
                    company.House_No = ds.Tables[0].Rows[0]["houseBuildingNo"].ToString();
                    company.Street_1 = Convert.ToString(ds.Tables[0].Rows[0]["street1"]);
                    company.Street_2 = ds.Tables[0].Rows[0]["street2"].ToString();
                    company.Landmark = ds.Tables[0].Rows[0]["landmark"].ToString();
                    company.Country_Name = Convert.ToString(ds.Tables[0].Rows[0]["countryName"]);
                    company.State_Name = Convert.ToString(ds.Tables[0].Rows[0]["stateName"]);
                    company.City_Name = Convert.ToString(ds.Tables[0].Rows[0]["cityName"]);
                    company.Pin_code = Convert.ToString(ds.Tables[0].Rows[0]["pincode"]);
                    company.Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["mobileNumber"]);
                    company.Alt_Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["altMobileNumber"]);
                    company.Telephone_Extension = Convert.ToString(ds.Tables[0].Rows[0]["telephoneNumber"]);
                    company.Landline_Number = Convert.ToString(ds.Tables[0].Rows[0]["landLineNumber"]);
                    company.Fax_Number = Convert.ToString(ds.Tables[0].Rows[0]["faxNumber"]);
                    company.Website = Convert.ToString(ds.Tables[0].Rows[0]["website"]);
                    company.Company_Email = Convert.ToString(ds.Tables[0].Rows[0]["emailid"]);
                    company.Company_Category = Convert.ToString(ds.Tables[0].Rows[0]["category"]);
                }
            }
            catch (Exception ex) { }
            return Json(company, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region//--------------------company assignment Master-------------------------
        public ActionResult CompanyAssignmentMaster()
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
        public JsonResult AddCompanyAssignment(CompanyAssignment objcompanyassignment)
        {
            string sms = "";
            CompanyAssignment companyassignment = new CompanyAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanyassignment.User_Id = Convert.ToInt32(cookie.Value);
                if (objcompanyassignment.Company_Assignment_Id == 0)
                {
                    int i = companyassignment.InsertCompanyAssignment(objcompanyassignment);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = companyassignment.UpdateCompanyAssignment(objcompanyassignment);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult AddBulkCompanyAssignment(CompanyAssignment objcompanyassignment)
        {
            string sms = "";
            CompanyAssignment companyassignment = new CompanyAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanyassignment.User_Id = Convert.ToInt32(cookie.Value);
                string[] companyname = objcompanyassignment.Company_Name.Split(',');
                string[] companygroupname = objcompanyassignment.Company_Group_Name.Split(',');

                for (int i = 0; i < companyname.Length; i++)
                {
                    Company c = new Company();
                    c.Company_Name = companyname[i].ToString();
                    int id = c.InsertCompanyMasterForBulk(c);

                    CompanyGroup cg = new CompanyGroup();
                    cg.Company_Group_Name = companygroupname[i].ToString();
                    int id1 = cg.InsertCompanyGroupForBulk(cg);

                    CompanyAssignment companyassignment1 = new CompanyAssignment();
                    companyassignment1.Company_Group_Id = id1;
                    companyassignment1.Company_Id = id;

                    int j = companyassignment.InsertCompanyAssignment(companyassignment1);
                }
                sms = "**Company assignment bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteCompanyAssignment(CompanyAssignment objcompanyassignment)
        {
            string sms = "";
            CompanyAssignment companyassignment = new CompanyAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcompanyassignment.User_Id = Convert.ToInt32(cookie.Value);

                int i = companyassignment.DeleteCompanyAssignment(objcompanyassignment);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CompanyAssignmentDetails()
        {
            CompanyAssignment companyassignment = new CompanyAssignment();
            List<CompanyAssignment> objcompanyassignment = new List<CompanyAssignment>();
            try
            {
                DataSet ds = companyassignment.GetCompanyAssignment(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objcompanyassignment.Add(new CompanyAssignment
                    {
                        Company_Assignment_Id = Convert.ToInt32(dr["companyAssignmentId"]),
                        Company_Id = Convert.ToInt32(dr["companyId"]),
                        Company_Group_Id = Convert.ToInt32(dr["companyGroupId"]),
                        Company_Name = dr["companyName"].ToString(),
                        Company_Group_Name = dr["companyGroupName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objcompanyassignment, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CompanyAssignmentApproveStatus(CompanyAssignment objCompanyAssignment)
        {
            string sms = "";
            CompanyAssignment companyassignment = new CompanyAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCompanyAssignment.User_Id = Convert.ToInt32(cookie.Value);

                int i = companyassignment.GetCompanyAssignmentApproveStatus(objCompanyAssignment);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CompanyAssignmentMaster";
                    MailConst.senderMail("Company Assignment", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion

        #region//--------------------branch Master-------------------------
        public ActionResult BranchMaster()
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
        public JsonResult AddBranchMaster(BranchMaster objBranch)
        {
            string sms = "";
            BranchMaster branch = new BranchMaster();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranch.User_Id = Convert.ToInt32(cookie.Value);
                if (objBranch.Branch_Id == 0)
                {
                    int i = branch.InsertBranchMaster(objBranch);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {

                    int i = branch.UpdateBranchMaster(objBranch);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkBranch(BranchMaster objBranch)
        {
            string sms = "";
            BranchMaster branch = new BranchMaster();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranch.User_Id = Convert.ToInt32(cookie.Value);
                string[] companyname = objBranch.Branch_Name.Split(',');
                string[] companycode = objBranch.Branch_Code.Split(',');
                string[] currency = objBranch.Currency_Name.Split(',');
                string[] language = objBranch.Language_Name.Split(',');
                string[] gstno = objBranch.GST_Number.Split(',');
                string[] sst = objBranch.SST_Number.Split(',');
                string[] isplant = objBranch.Is_Plant.Split(',');
                string[] iswarehouse = objBranch.Is_Warehouse.Split(',');
                for (int i = 0; i < companyname.Length; i++)
                {
                    Currency c = new Currency();
                    c.Currency_Name = currency[i].ToString();
                    int id = c.InsertCurrencyBulk(c);
                    Language l = new Language();
                    l.Language_Name = language[i].ToString();
                    int id1 = l.InsertLanguageBulk(l);
                    BranchMaster branch1 = new BranchMaster();
                    branch1.Currency_Id = id;
                    branch1.Branch_Name = companyname[i].ToString();
                    branch1.Branch_Code = companycode[i].ToString();
                    branch1.Language_Id = id1;
                    branch1.SST_Number = sst[i].ToString();
                    branch1.GST_Number = gstno[i].ToString();
                    branch1.Is_Plant = isplant[i].ToString();
                    branch1.Is_Warehouse = iswarehouse[i].ToString();
                    int j = branch.InsertBranchMaster(branch1);
                }
                sms = "**Branch bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteBranchMaster(BranchMaster objBranch)
        {
            string sms = "";
            BranchMaster branch = new BranchMaster();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranch.User_Id = Convert.ToInt32(cookie.Value);

                int i = branch.DeleteBranchMaster(objBranch);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult BranchMasterDetails()
        {
            BranchMaster branch = new BranchMaster();
            List<BranchMaster> BranchList = new List<BranchMaster>();
            try
            {
                DataSet ds = branch.GetBranchMaster(0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        BranchList.Add(new BranchMaster
                        {
                            Branch_Id = Convert.ToInt32(dr["branchId"]),
                            Branch_Code = dr["branchCode"].ToString(),
                            Branch_Name = dr["branchName"].ToString(),
                            Currency_Id = Convert.ToInt32(dr["currencyId"]),
                            Language_Id = Convert.ToInt32(dr["language_Id"]),
                            Currency_Name = dr["currencyName"].ToString(),
                            Language_Name = dr["languageName"].ToString(),
                            GST_Number = dr["gstNumber"].ToString(),
                            SST_Number = (dr["sstNo"]).ToString(),
                            Is_Plant = dr["isPlant"].ToString(),
                            Is_Warehouse = dr["isWareHouse"].ToString(),
                            Approve_Status = dr["isApproved"].ToString()
                        });

                    }
                }
            }
            catch (Exception ex) { }
            return Json(BranchList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BranchMasterApproveStatus(BranchMaster objBranch)
        {
            string sms = "";
            BranchMaster area = new BranchMaster();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranch.User_Id = Convert.ToInt32(cookie.Value);

                int i = area.GetBranchMasterApproveStatus(objBranch);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/BranchMaster";
                    MailConst.senderMail("Branch", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult BranchName()
        {
            List<SelectListItem> Branchlist = new List<SelectListItem>();
            BranchMaster b = new BranchMaster();
            try
            {
                DataSet ds = b.GetBranchName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Branchlist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["branchId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["branchName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Branchlist);
        }
        #endregion

        #region//--------------------branch address Master-------------------------
        public JsonResult AddBranchAddressMaster(BranchAddress objBranchAddress)
        {
            string sms = "";
            BranchAddress branchAddress = new BranchAddress();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranchAddress.User_Id = Convert.ToInt32(cookie.Value);
                if (objBranchAddress.Branch_Add_Id == 0)
                {
                    int i = branchAddress.InsertBranchAddress(objBranchAddress);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {

                    int i = branchAddress.UpdateBranchAddress(objBranchAddress);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteBranchAddressMaster(BranchAddress objBranchAddress)
        {
            string sms = "";
            BranchAddress branchAddress = new BranchAddress();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranchAddress.User_Id = Convert.ToInt32(cookie.Value);
                int i = branchAddress.DeleteBranchAddress(objBranchAddress);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult BranchAddressDetails()
        {
            BranchAddress branchaddress = new BranchAddress();
            List<BranchAddress> BranchAddList = new List<BranchAddress>();
            try
            {
                DataSet ds = branchaddress.GetBranchAddress(0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        BranchAddList.Add(new BranchAddress
                        {
                            Branch_Add_Id = Convert.ToInt32(dr["branchAddressId"]),
                            Branch_Name = dr["branchName"].ToString(),
                            Contact_Person = dr["contactPerson"].ToString(),
                            House_No = dr["houseBuildingNo"].ToString(),
                            Country_Name = (dr["countryName"]).ToString(),
                            State_Name = dr["stateName"].ToString(),
                            City_Name = dr["cityName"].ToString(),
                            Pin_code = dr["pincode"].ToString(),
                            Mobile_Number = (dr["mobileNumber"]).ToString(),
                            Approve_Status = dr["isApproved"].ToString()

                        });

                    }
                }
            }
            catch (Exception ex) { }
            return Json(BranchAddList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BranchAddressApproveStatus(BranchAddress objBranchAddress)
        {
            string sms = "";
            BranchAddress branchaddress = new BranchAddress();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranchAddress.User_Id = Convert.ToInt32(cookie.Value);
                int i = branchaddress.GetBranchAddressApproveStatus(objBranchAddress);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/BranchMaster";
                    MailConst.senderMail("Branch Address", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult BranchAddressDetailsForEdit(BranchAddress objBranchAddress)
        {
            BranchAddress branchaddress = new BranchAddress();
            List<BranchAddress> BranchAddList = new List<BranchAddress>();
            try
            {
                DataSet ds = branchaddress.GetBranchAddressForEdit(objBranchAddress);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    branchaddress.Branch_Add_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["branchAddressId"]);
                    branchaddress.Branch_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["branchId"]);
                    branchaddress.Contact_Person = Convert.ToString(ds.Tables[0].Rows[0]["contactPerson"]);
                    branchaddress.House_No = Convert.ToString(ds.Tables[0].Rows[0]["houseBuildingNo"]);
                    branchaddress.Street_1 = Convert.ToString(ds.Tables[0].Rows[0]["street1"]);
                    branchaddress.Street_2 = Convert.ToString(ds.Tables[0].Rows[0]["street2"]);
                    branchaddress.Landmark = Convert.ToString(ds.Tables[0].Rows[0]["landmark"]);
                    branchaddress.Country_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["countryId"]);
                    branchaddress.State_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["stateId"]);
                    branchaddress.City_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["cityId"]);
                    branchaddress.Pin_code = Convert.ToString(ds.Tables[0].Rows[0]["pincode"]);
                    branchaddress.Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["mobileNumber"]);
                    branchaddress.Alt_Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["altMobileNumber"]);
                    branchaddress.Telephone_Extension = Convert.ToString(ds.Tables[0].Rows[0]["telephoneExtension"]);
                    branchaddress.Landline_Number = Convert.ToString(ds.Tables[0].Rows[0]["landLineNumber"]);
                    branchaddress.Alt_Landline_Number = Convert.ToString(ds.Tables[0].Rows[0]["altLandLineNumber"]);
                    branchaddress.Website = Convert.ToString(ds.Tables[0].Rows[0]["website"]);
                }
            }
            catch (Exception ex) { }
            return Json(branchaddress, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BranchMasterAddressView(BranchAddress objBranchAddress)
        {
            BranchAddress branch = new BranchAddress();
            try
            {
                DataSet ds = branch.GetBranchAddressForListForPopup(objBranchAddress);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    branch.Branch_Add_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["branchAddressId"]);
                    branch.Branch_Name = ds.Tables[0].Rows[0]["branchName"].ToString();
                    branch.Contact_Person = ds.Tables[0].Rows[0]["contactPerson"].ToString();
                    branch.House_No = ds.Tables[0].Rows[0]["houseBuildingNo"].ToString();
                    branch.Street_1 = Convert.ToString(ds.Tables[0].Rows[0]["street1"]);
                    branch.Street_2 = ds.Tables[0].Rows[0]["street2"].ToString();
                    branch.Landmark = ds.Tables[0].Rows[0]["landmark"].ToString();
                    branch.Country_Name = Convert.ToString(ds.Tables[0].Rows[0]["countryName"]);
                    branch.State_Name = Convert.ToString(ds.Tables[0].Rows[0]["stateName"]);
                    branch.City_Name = Convert.ToString(ds.Tables[0].Rows[0]["cityName"]);
                    branch.Pin_code = Convert.ToString(ds.Tables[0].Rows[0]["pincode"]);
                    branch.Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["mobileNumber"]);
                    branch.Alt_Mobile_Number = Convert.ToString(ds.Tables[0].Rows[0]["altMobileNumber"]);
                    branch.Telephone_Extension = Convert.ToString(ds.Tables[0].Rows[0]["telephoneExtension"]);
                    branch.Landline_Number = Convert.ToString(ds.Tables[0].Rows[0]["landLineNumber"]);
                    branch.Alt_Landline_Number = Convert.ToString(ds.Tables[0].Rows[0]["altLandLineNumber"]);
                    branch.Website = Convert.ToString(ds.Tables[0].Rows[0]["website"]);
                }
            }
            catch (Exception ex) { }
            return Json(branch, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region//--------------------branch assignment Master-------------------------
        public ActionResult BranchAssignmentMaster()
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
        public JsonResult AddBranchAssignment(BranchAssignment objbranchassignment)
        {
            string sms = "";
            BranchAssignment branchassignment = new BranchAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objbranchassignment.User_Id = Convert.ToInt32(cookie.Value);
                if (objbranchassignment.Branch_Assignment_Id == 0)
                {
                    int i = branchassignment.InsertBranchAssignment(objbranchassignment);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = branchassignment.UpdateBranchAssignment(objbranchassignment);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkBranchAssignment(BranchAssignment objbranchassignment)
        {
            string sms = "";
            BranchAssignment branchassignment = new BranchAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objbranchassignment.User_Id = Convert.ToInt32(cookie.Value);
                string[] companyname = objbranchassignment.Company_Name.Split(',');
                string[] branchname = objbranchassignment.Branch_Name.Split(',');

                for (int i = 0; i < companyname.Length; i++)
                {
                    Company c = new Company();
                    c.Company_Name = companyname[i].ToString();
                    int id = c.InsertCompanyMasterForBulk(c);

                    BranchMaster cg = new BranchMaster();
                    cg.Branch_Name = branchname[i].ToString();
                    int id1 = cg.InsertBranchMasterIForBulk(cg);

                    BranchAssignment branchassignment1 = new BranchAssignment();
                    branchassignment1.Branch_Id = id1;
                    branchassignment1.Company_Id = id;

                    int j = branchassignment.InsertBranchAssignment(branchassignment1);
                }
                sms = "**Branch assignment bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteBranchAssignment(BranchAssignment objbranchassignment)
        {
            string sms = "";
            BranchAssignment branchassignment = new BranchAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objbranchassignment.User_Id = Convert.ToInt32(cookie.Value);

                int i = branchassignment.DeleteBranchAssignment(objbranchassignment);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult BranchAssignmentDetails()
        {
            BranchAssignment branchassignment = new BranchAssignment();
            List<BranchAssignment> objbranchassignment = new List<BranchAssignment>();
            try
            {
                DataSet ds = branchassignment.GetBranchAssignment(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objbranchassignment.Add(new BranchAssignment
                    {
                        Branch_Assignment_Id = Convert.ToInt32(dr["branchAssignmentId"]),
                        Company_Id = Convert.ToInt32(dr["companyId"]),
                        Branch_Id = Convert.ToInt32(dr["branchId"]),
                        Company_Name = dr["companyName"].ToString(),
                        Branch_Name = dr["branchName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objbranchassignment, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BranchAssignmentApproveStatus(BranchAssignment objBranchAssignment)
        {
            string sms = "";
            BranchAssignment branch = new BranchAssignment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objBranchAssignment.User_Id = Convert.ToInt32(cookie.Value);

                int i = branch.GetBranchAssignmentApproveStatus(objBranchAssignment);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/BranchAssignmentMaster";
                    MailConst.senderMail("Branch Assignment", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }


        #endregion

        #region//-----payment master-----------------------
        public ActionResult PaymentMaster()
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

        public JsonResult AddPayment(Payment objPayment)
        {
            string sms = "";
            Payment payment = new Payment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPayment.User_Id = Convert.ToInt32(cookie.Value);
                if (objPayment.Payment_Id == 0)
                {
                    int i = payment.InsertPayment(objPayment);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = payment.UpdatePayment(objPayment);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkPayment(Payment objPayment)
        {
            string sms = "";
            Payment payment = new Payment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPayment.User_Id = Convert.ToInt32(cookie.Value);
                string[] paymentcode = objPayment.Payment_Term_Code.Split(',');
                string[] paymentName = objPayment.Payment_Term_Name.Split(',');
                string[] creditdays = objPayment.Credit_Days.Split(',');
                string[] tolrancedays = objPayment.Tolerance_Days.Split(',');
                string[] relateto = objPayment.Relate_To.Split(',');

                for (int i = 0; i < paymentName.Length; i++)
                {
                    Payment payment1 = new Payment();
                    payment1.Payment_Term_Code = paymentcode[i].ToString();
                    payment1.Payment_Term_Name = paymentName[i].ToString();
                    payment1.Credit_Days = creditdays[i].ToString();
                    payment1.Tolerance_Days = tolrancedays[i].ToString();
                    payment1.Relate_To = relateto[i].ToString();

                    int j = payment.InsertPayment(payment1);
                }
                sms = "**Payment bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeletePayment(Payment objPayment)
        {
            string sms = "";
            Payment payment = new Payment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPayment.User_Id = Convert.ToInt32(cookie.Value);

                int i = payment.DeletePayment(objPayment);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult PaymentDetails()
        {
            Payment payment = new Payment();
            List<Payment> ObjPayment = new List<Payment>();
            try
            {
                DataSet ds = payment.GetPayment(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ObjPayment.Add(new Payment
                    {
                        Payment_Id = Convert.ToInt32(dr["paymentTermId"]),
                        Payment_Term_Code = dr["paymentTermCode"].ToString(),
                        Payment_Term_Name = dr["paymentTermName"].ToString(),
                        Credit_Days = dr["creditDays"].ToString(),
                        Tolerance_Days = Convert.ToString(dr["toleranceDays"]),
                        Relate_To = Convert.ToString(dr["relateTo"]),
                        Approve_Status = dr["isApproved"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(ObjPayment, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PaymentApproveStatus(Payment objPayment)
        {
            string sms = "";
            Payment payment = new Payment();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPayment.User_Id = Convert.ToInt32(cookie.Value);

                int i = payment.GetPaymentApproveStatus(objPayment);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/PaymentMaster";
                    MailConst.senderMail("Payment", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult PaymentTermName()
        {
            List<SelectListItem> banklist = new List<SelectListItem>();
            Payment b = new Payment();
            try
            {
                DataSet ds = b.GetPaymentName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    banklist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["paymentTermId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["paymentTermName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(banklist);
        }
        #endregion

        #region//-----posting period master-----------------------
        public ActionResult PostingPeriodMaster()
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
        public JsonResult AddPostingPeriod(PostingPeriod objPostingPeriod)
        {
            string sms = "";
            try
            {
                IFormatProvider format = new CultureInfo("fr-FR");
                PostingPeriod postingperiod = new PostingPeriod();
                DateTime datestr = DateTime.ParseExact(objPostingPeriod.datestring, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime date =Convert.ToDateTime(datestr.Day + "/" + datestr.Month + "/" + datestr.Year ) ;
                // DateTime date1= new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
                objPostingPeriod.Fiscal_Year_Start = datestr;// DateTime.ParseExact(objPostingPeriod.Fiscal_Year_Start.Year.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPostingPeriod.User_Id = Convert.ToInt32(cookie.Value);
                if (objPostingPeriod.Posting_Period_Id == 0)
                {
                    int i = postingperiod.InsertPostingPeriod(objPostingPeriod);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = postingperiod.UpdatePostingPeriod(objPostingPeriod);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkPostingPeriod(PostingPeriod objPostingPeriod)
        {
            string sms = "";
            PostingPeriod postingperiod = new PostingPeriod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPostingPeriod.User_Id = Convert.ToInt32(cookie.Value);
                string[] companyname = objPostingPeriod.Company_Name.Split(',');
                string[] periodname = objPostingPeriod.Period_Name.Split(',');
                string[] periodcode = objPostingPeriod.Period_Code.Split(',');
                string[] subperiod = objPostingPeriod.Sub_Period.Split(',');
                string[] fdate = objPostingPeriod.datestring.Split(',');
                IFormatProvider format = new CultureInfo("fr-FR");


                for (int i = 0; i < periodname.Length; i++)
                {
                    Company c = new Company();
                    c.Company_Name = companyname[i].ToString();
                    int id = c.InsertCompanyMasterForBulk(c);

                    PostingPeriod postingperiod1 = new PostingPeriod();
                    postingperiod1.Company_Id = id;
                    postingperiod1.Period_Name = periodname[i].ToString();
                    postingperiod1.Period_Code = periodcode[i].ToString();
                    postingperiod1.Sub_Period = subperiod[i];
                    try
                    {
                        DateTime datestr = DateTime.ParseExact(fdate[i].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        postingperiod1.Fiscal_Year_Start = datestr;
                    }
                    catch (Exception ex)
                    {

                    }
                    int j = postingperiod.InsertPostingPeriod(postingperiod1);
                }
                sms = "**Posting period bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeletePostingPeriod(PostingPeriod objPostingPeriod)
        {
            string sms = "";
            PostingPeriod postingperiod = new PostingPeriod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPostingPeriod.User_Id = Convert.ToInt32(cookie.Value);

                int i = postingperiod.DeletePostingPeriod(objPostingPeriod);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult PostingPeriodDetails()
        {
            PostingPeriod postingperiod = new PostingPeriod();
            List<PostingPeriod> ObjPostingPeriod = new List<PostingPeriod>();
            try
            {
                DataSet ds = postingperiod.GetPostingPeriod(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ObjPostingPeriod.Add(new PostingPeriod
                    {
                        Posting_Period_Id = Convert.ToInt32(dr["postperiodId"]),
                        Company_Name = dr["companyName"].ToString(),
                        Period_Code = dr["periodCode"].ToString(),
                        Period_Name = dr["periodName"].ToString(),
                        Sub_Period = Convert.ToString(dr["subPeriod"]),
                        datestring = Convert.ToString(dr["fiscalYearStart"]),
                        Company_Id = Convert.ToInt32(dr["companyId"]),
                        Approve_Status = dr["isApproved"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(ObjPostingPeriod, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PostingPeriodApproveStatus(PostingPeriod objPostingPeriod)
        {
            string sms = "";
            PostingPeriod postingperiod = new PostingPeriod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPostingPeriod.User_Id = Convert.ToInt32(cookie.Value);

                int i = postingperiod.GetPostingPeriodApproveStatus(objPostingPeriod);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/PostingPeriodMaster";
                    MailConst.senderMail("Posting Period", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion

        #region //--------------- Department Master -----------------------
        public ActionResult DepartmentMaster()
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
        public JsonResult AddDepartment(Department objDepartment)
        {
            string sms = "";
            Department department = new Department();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDepartment.User_Id = Convert.ToInt32(cookie.Value);
                if (objDepartment.Department_Id == 0)
                {
                    int i = department.InsertDepartment(objDepartment);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = department.UpdateDepartment(objDepartment);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkDepartment(Department objDepartment)
        {
            string sms = "";
            Department department = new Department();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDepartment.User_Id = Convert.ToInt32(cookie.Value);
                string[] itcode = objDepartment.Department_Name.Split(',');
                string[] itName = objDepartment.Department_Description.Split(',');
                for (int i = 0; i < itName.Length; i++)
                {
                    Department department1 = new Department();
                    department1.Department_Name = itcode[i].ToString();
                    department1.Department_Description = itName[i].ToString();
                    int j = department.InsertDepartment(department1);
                }
                sms = "**Department bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteDepartment(Department objDepartment)
        {
            string sms = "";
            Department department = new Department();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDepartment.User_Id = Convert.ToInt32(cookie.Value);

                int i = department.DeleteDepartment(objDepartment);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult DepartmentDetails()
        {
            Department department = new Department();
            List<Department> objDepartment = new List<Department>();
            try
            {
                DataSet ds = department.GetDepartment(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objDepartment.Add(new Department
                    {
                        Department_Id = Convert.ToInt32(dr["departmentId"]),
                        Department_Name = dr["departmentName"].ToString(),
                        Department_Description = dr["departmentDescription"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objDepartment, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DepartmentApproveStatus(Department objDepartment)
        {
            string sms = "";
            Department department = new Department();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDepartment.User_Id = Convert.ToInt32(cookie.Value);

                int i = department.GetDepartmentApproveStatus(objDepartment);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/DepartmentMaster";
                    MailConst.senderMail("Department", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DepartmentName()
        {
            List<SelectListItem> departmentnamelist = new List<SelectListItem>();
            Department b = new Department();
            try
            {
                DataSet ds = b.GetDepartmentName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    departmentnamelist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["departmentId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["departmentname"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(departmentnamelist);
        }
        #endregion
        #region //--------------- Designation Master -----------------------
        public ActionResult DesignationMaster()
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
        public JsonResult AddDesignation(Designation objDesignation)
        {
            string sms = "";
            Designation designation = new Designation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDesignation.User_Id = Convert.ToInt32(cookie.Value);
                if (objDesignation.Designation_Id == 0)
                {
                    int i = designation.InsertDesignation(objDesignation);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = designation.UpdateDesignation(objDesignation);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkDesignation(Designation objDesignation)
        {
            string sms = "";
            Designation designation = new Designation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDesignation.User_Id = Convert.ToInt32(cookie.Value);
                string[] Dname = objDesignation.Designation_Name.Split(',');
                string[] DDescription = objDesignation.Designation_Description.Split(',');
                for (int i = 0; i < Dname.Length; i++)
                {
                    Designation designation1 = new Designation();
                    designation1.Designation_Name = Dname[i].ToString();
                    designation1.Designation_Description = DDescription[i].ToString();
                    int j = designation.InsertDesignation(designation1);
                }
                sms = "**Designation bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteDesignation(Designation objDesignation)
        {
            string sms = "";
            Designation designation = new Designation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDesignation.User_Id = Convert.ToInt32(cookie.Value);

                int i = designation.DeleteDesignation(objDesignation);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult DesignationDetails()
        {
            Designation designation = new Designation();
            List<Designation> objdesignation = new List<Designation>();
            try
            {
                DataSet ds = designation.GetDesignation(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objdesignation.Add(new Designation
                    {
                        Designation_Id = Convert.ToInt32(dr["designationId"]),
                        Designation_Name = dr["designationName"].ToString(),
                        Designation_Description = dr["designationDescription"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objdesignation, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DesignationApproveStatus(Designation objDesignation)
        {
            string sms = "";
            Designation designation = new Designation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objDesignation.User_Id = Convert.ToInt32(cookie.Value);

                int i = designation.GetDesignationApproveStatus(objDesignation);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/DesignationMaster";
                    MailConst.senderMail("Designation", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult DesignationName()
        {
            List<SelectListItem> designationnamelist = new List<SelectListItem>();
            Designation b = new Designation();
            try
            {
                DataSet ds = b.GetDesignationName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    designationnamelist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["designationId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["designationname"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(designationnamelist);
        }
        #endregion
        #endregion

        #region//--------------------------item master----------------------------------------------
        #region//-----priority master-----------------------
        public ActionResult PriorityMaster()
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
        public JsonResult AddPriority(Priority objpriority)
        {
            string sms = "";
            Priority Priority = new Priority();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objpriority.User_Id = Convert.ToInt32(cookie.Value);
                if (objpriority.Priority_Id == 0)
                {
                    int i = Priority.InsertPriority(objpriority);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {

                    int i = Priority.UpdatePriority(objpriority);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkPriority(Priority objpriority)
        {
            string sms = "";
            Priority priority = new Priority();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objpriority.User_Id = Convert.ToInt32(cookie.Value);
                string[] priorityname = objpriority.Priority_Name.Split(',');

                for (int i = 0; i < priorityname.Length; i++)
                {
                    Priority priority1 = new Priority();
                    priority1.Priority_Name = priorityname[i].ToString();

                    int j = priority.InsertPriority(priority1);
                }
                sms = "**Priority bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeletePriority(Priority objpriority)
        {
            string sms = "";
            Priority Priority = new Priority();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objpriority.User_Id = Convert.ToInt32(cookie.Value);
                int i = Priority.DeletePriority(objpriority);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult PriorityDetails()
        {
            Priority Priority = new Priority();
            List<Priority> ObjPriority = new List<Priority>();
            try
            {
                DataSet ds = Priority.GetPriority(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ObjPriority.Add(new Priority
                    {
                        Priority_Id = Convert.ToInt32(dr["priorityId"]),
                        Priority_Name = dr["priorityName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(ObjPriority, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PriorityApproveStatus(Priority objPriority)
        {
            string sms = "";
            Priority priority = new Priority();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objPriority.User_Id = Convert.ToInt32(cookie.Value);

                int i = priority.GetPriorityApproveStatus(objPriority);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/PriorityMaster";
                    MailConst.senderMail("Priority", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion

        #region//-----Valuation Method-----------------------
        public ActionResult ValuationMethodMaster()
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
        public JsonResult AddValuationMethod(ValuationMethod objvmethod)
        {
            string sms = "";
            ValuationMethod vmethod = new ValuationMethod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objvmethod.User_Id = Convert.ToInt32(cookie.Value);
                if (objvmethod.Valuation_Method_Id == 0)
                {
                    int i = vmethod.InsertValuationMethod(objvmethod);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {

                    int i = vmethod.UpdateValuationMethod(objvmethod);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkValuationMethod(ValuationMethod objvmethod)
        {
            string sms = "";
            ValuationMethod vm = new ValuationMethod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objvmethod.User_Id = Convert.ToInt32(cookie.Value);
                string[] vmcode = objvmethod.Valuation_Method_Code.Split(',');
                string[] vmName = objvmethod.Valuation_Method_Name.Split(',');
                for (int i = 0; i < vmName.Length; i++)
                {
                    ValuationMethod vm1 = new ValuationMethod();
                    vm1.Valuation_Method_Code = vmcode[i].ToString();
                    vm1.Valuation_Method_Name = vmName[i].ToString();
                    int j = vm.InsertValuationMethod(vm1);
                }
                sms = "**Valuation method bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteValuationMethod(ValuationMethod objvmethod)
        {
            string sms = "";
            ValuationMethod vmethod = new ValuationMethod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objvmethod.User_Id = Convert.ToInt32(cookie.Value);

                int i = vmethod.DeleteValuationMethod(objvmethod);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult ValuationMethodDetails()
        {
            ValuationMethod vmethod = new ValuationMethod();
            List<ValuationMethod> Objvmethod = new List<ValuationMethod>();
            try
            {
                DataSet ds = vmethod.GetValuationMethod(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objvmethod.Add(new ValuationMethod
                    {
                        Valuation_Method_Id = Convert.ToInt32(dr["valuationMethodId"]),
                        Valuation_Method_Code = dr["valuationMethodCode"].ToString(),
                        Valuation_Method_Name = dr["valuationMethodName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objvmethod, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ValuationMethodApproveStatus(ValuationMethod objValuationMethod)
        {
            string sms = "";
            ValuationMethod method = new ValuationMethod();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objValuationMethod.User_Id = Convert.ToInt32(cookie.Value);

                int i = method.GetValuationMethodApproveStatus(objValuationMethod);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/ValuationMethodMaster";
                    MailConst.senderMail("Valuation Method", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion

        #region//--------------------valuation Master-------------------------
        public ActionResult ValuationMaster()
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
        public JsonResult AddValuation(Valuation objvaluation)
        {
            string sms = "";
            Valuation valuation = new Valuation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objvaluation.User_Id = Convert.ToInt32(cookie.Value);
                if (objvaluation.Valuation_Id == 0)
                {
                    int i = valuation.InsertValuation(objvaluation);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = valuation.UpdateValuation(objvaluation);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkValuation(Valuation objvaluation)
        {
            string sms = "";
            Valuation valuation = new Valuation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objvaluation.User_Id = Convert.ToInt32(cookie.Value);
                string[] vmcode = objvaluation.Valuation_Code.Split(',');
                string[] vmName = objvaluation.Valuation_Name.Split(',');
                for (int i = 0; i < vmName.Length; i++)
                {
                    Valuation valuation1 = new Valuation();
                    valuation1.Valuation_Code = vmcode[i].ToString();
                    valuation1.Valuation_Name = vmName[i].ToString();
                    int j = valuation.InsertValuation(valuation1);
                }
                sms = "**Valuation bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteValuation(Valuation objvaluation)
        {
            string sms = "";
            Valuation valuation = new Valuation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objvaluation.User_Id = Convert.ToInt32(cookie.Value);

                int i = valuation.DeleteValuation(objvaluation);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult ValuationDetails()
        {
            Valuation valuation = new Valuation();
            List<Valuation> ObjValuation = new List<Valuation>();
            try
            {
                DataSet ds = valuation.GetValuation(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ObjValuation.Add(new Valuation
                    {
                        Valuation_Id = Convert.ToInt32(dr["valuationId"]),
                        Valuation_Code = dr["valuationCode"].ToString(),
                        Valuation_Name = dr["valuationName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(ObjValuation, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ValuationApproveStatus(Valuation objValuation)
        {
            string sms = "";
            Valuation valuation = new Valuation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objValuation.User_Id = Convert.ToInt32(cookie.Value);

                int i = valuation.GetValuationApproveStatus(objValuation);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/ValuationMaster";
                    MailConst.senderMail("Valuation", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult ValuationName()
        {
            List<SelectListItem> Valuationlist = new List<SelectListItem>();
            Valuation b = new Valuation();
            try
            {
                DataSet ds = b.GetValuationName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Valuationlist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["valuationId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["valuationName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Valuationlist);
        }
        #endregion

        #region//--------------------hsn Master-------------------------
  
        public ActionResult HSNMaster()
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
        public JsonResult AddHSN(HSN objhsn)
        {
            string sms = "";
            HSN hsn = new HSN();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objhsn.User_Id = Convert.ToInt32(cookie.Value);
                if (objhsn.HSN_Id == 0)
                {
                    int i = hsn.InsertHSN(objhsn);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = hsn.UpdateHSN(objhsn);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkHSN(HSN objhsn)
        {
            string sms = "";
            HSN hsn = new HSN();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objhsn.User_Id = Convert.ToInt32(cookie.Value);
                string[] hsncode = objhsn.HSN_Code.Split(',');

                for (int i = 0; i < hsncode.Length; i++)
                {
                    HSN hsn1 = new HSN();
                    hsn1.HSN_Code = hsncode[i].ToString();

                    int j = hsn.InsertHSN(hsn1);
                }
                sms = "**HSN bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteHSN(HSN objhsn)
        {
            string sms = "";
            HSN hsn = new HSN();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objhsn.User_Id = Convert.ToInt32(cookie.Value);

                int i = hsn.DeleteHSN(objhsn);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult HSNDetails()
        {
            HSN hsn = new HSN();
            List<HSN> Objhsn = new List<HSN>();
            try
            {
                DataSet ds = hsn.GetHSN(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objhsn.Add(new HSN
                    {
                        HSN_Id = Convert.ToInt32(dr["hsnId"]),
                        HSN_Code = dr["hsnName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objhsn, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult HSNApproveStatus(HSN objHSN)
        {
            string sms = "";
            HSN hsn = new HSN();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objHSN.User_Id = Convert.ToInt32(cookie.Value);

                int i = hsn.GetHSNApproveStatus(objHSN);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/HSNMaster";
                    MailConst.senderMail("HSN", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        #endregion

        #region//--------------------UOM Master-------------------------

        public ActionResult UOMMaster()
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
        public JsonResult AddUOM(UOM objuom)
        {
            string sms = "";
            UOM uom = new UOM();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objuom.User_Id = Convert.ToInt32(cookie.Value);
                if (objuom.UOM_Id == 0)
                {
                    int i = uom.InsertUOM(objuom);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data Already Exist**";
                    }
                }
                else
                {
                    int i = uom.UpdateUOM(objuom);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkuom(UOM objuom)
        {
            string sms = "";
            UOM uom = new UOM();
            try
            { 
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objuom.User_Id = Convert.ToInt32(cookie.Value);
            string[] vmcode = objuom.UOM_Code.Split(',');
            string[] vmName = objuom.UOM_Name.Split(',');
            for (int i = 0; i < vmName.Length; i++)
            {
                UOM uom1 = new UOM();
                uom1.UOM_Code = vmcode[i].ToString();
                uom1.UOM_Name = vmName[i].ToString();
                int j = uom.InsertUOM(uom1);
            }
            sms = "**UOM bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteUOM(UOM objuom)
        {
            string sms = "";
            UOM uom = new UOM();
            try
            { 
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objuom.User_Id = Convert.ToInt32(cookie.Value);


            int i = uom.DeleteUOM(objuom);
            if (i == 0)
            {
                sms = "**Data deleted successfully**";
            }
            else
            {
                sms = "**Data not deleted successfully**";
            }
          }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult UOMDetails()
        {
            UOM uom = new UOM();
            List<UOM> Objuom = new List<UOM>();
            try
            {
                DataSet ds = uom.GetUOM(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objuom.Add(new UOM
                    {
                        UOM_Id = Convert.ToInt32(dr["uomId"]),
                        UOM_Code = dr["uomCode"].ToString(),
                        UOM_Name = dr["uomName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objuom, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UOMApproveStatus(UOM objUOM)
        {
            string sms = "";
            UOM uom = new UOM();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOM.User_Id = Convert.ToInt32(cookie.Value);

                int i = uom.GetUOMApproveStatus(objUOM);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/UOMMaster";
                    MailConst.senderMail("UOM", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult UOMBaseAltName()
        {
            List<SelectListItem> umolist = new List<SelectListItem>();
            UOM b = new UOM();
            try
            {
                DataSet ds = b.GetUOMBaseAltName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    umolist.Add(new SelectListItem
                    {
                        Value = dr["uomId"].ToString(),
                        Text = dr["uomName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(umolist);
        }
        [HttpPost]
        public JsonResult UOMNameAccUOmGroupName(UOMGroup objUOMGroup)
        {
            List<SelectListItem> umolist = new List<SelectListItem>();
            UOMGroup b = new UOMGroup();
            try
            {
                DataSet ds = b.GetUOMNameAccUOMGroupName(objUOMGroup);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    umolist.Add(new SelectListItem
                    {
                        Value = dr["uomId"].ToString(),
                        Text = dr["uomName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(umolist);
        }

        [HttpPost]
        public JsonResult UOMListAccUOMGroupId(UOMGroup objUOMGroup)
        {
            List<SelectListItem> UOMlist = new List<SelectListItem>();
            UOMGroup b = new UOMGroup();
            try
            {
                DataSet ds = b.GetUOMListAccUOMGroupId(objUOMGroup);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UOMlist.Add(new SelectListItem
                    {
                        Value = dr["uomId"].ToString(),
                        Text = dr["uomName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(UOMlist);
        }

        [HttpPost]
        public JsonResult AltUOMListAccUOMGroupId(UOMGroup objUOMGroup)
        {
            List<SelectListItem> UOMlist = new List<SelectListItem>();
            UOMGroup b = new UOMGroup();
            try
            {
                DataSet ds = b.GeAlttUOMListAccUOMGroupId(objUOMGroup);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UOMlist.Add(new SelectListItem
                    {
                        Value = dr["uomId"].ToString(),
                        Text = dr["uomName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(UOMlist);
        }
        #endregion


        #region//--------------------uom group Master-------------------------

        public ActionResult UOMGroupMaster()
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
        public JsonResult AddUOMGroup(UOMGroup objUOMGroup)
        {
            string sms = "";
            UOMGroup umoGroup = new UOMGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroup.User_Id = Convert.ToInt32(cookie.Value);
                if (objUOMGroup.UOM_Group_Id == 0)
                {
                    int i = umoGroup.InsertUOMGroup(objUOMGroup);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = umoGroup.UpdateUOMGroup(objUOMGroup);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkuomGroup(UOMGroup objUOMGroup)
        {
            string sms = "";
            UOMGroup uom = new UOMGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroup.User_Id = Convert.ToInt32(cookie.Value);
                string[] vmcode = objUOMGroup.Group_Code.Split(',');
                string[] vmName = objUOMGroup.Group_Name.Split(',');
                for (int i = 0; i < vmName.Length; i++)
                {
                    UOMGroup uom1 = new UOMGroup();
                    uom1.Group_Code = vmcode[i].ToString();
                    uom1.Group_Name = vmName[i].ToString();
                    int j = uom.InsertUOMGroup(uom1);
                }
                sms = "**UOM group bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteUOMGroup(UOMGroup objUOMGroup)
        {
            string sms = "";
            UOMGroup uomgroup = new UOMGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroup.User_Id = Convert.ToInt32(cookie.Value);

                int i = uomgroup.DeleteUOMGroup(objUOMGroup);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult UOMGroupDetails()
        {
            UOMGroup uomgroup = new UOMGroup();
            List<UOMGroup> objUOMGroup = new List<UOMGroup>();
            try
            {
                DataSet ds = uomgroup.GetUOMGroup(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objUOMGroup.Add(new UOMGroup
                    {
                        UOM_Group_Id = Convert.ToInt32(dr["uomGroupId"]),
                        Group_Code = dr["uomGroupCode"].ToString(),
                        Group_Name = dr["uomGroupName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objUOMGroup, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UOMGroupApproveStatus(UOMGroup objUOMGroup)
        {
            string sms = "";
            UOMGroup uomGroup = new UOMGroup();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroup.User_Id = Convert.ToInt32(cookie.Value);

                int i = uomGroup.GetUOMGroupApproveStatus(objUOMGroup);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/UOMGroupMaster";
                    MailConst.senderMail("UOM Group", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult UOMGroupName()
        {
            List<SelectListItem> uomgrouplist = new List<SelectListItem>();
            UOMGroup b = new UOMGroup();
            try
            {
                DataSet ds = b.GetUOMGroupName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    uomgrouplist.Add(new SelectListItem
                    {
                        Value = dr["uomGroupId"].ToString(),
                        Text = dr["uomGroupName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(uomgrouplist);
        }
        [HttpPost]
        public JsonResult UOMGroupNameAccUOMId(UOMGroup ObjUOMGroup)
        {
            // List<SelectListItem> uomgrouplist = new List<SelectListItem>();
            UOMGroup b = new UOMGroup();
            UOMGroup uom = new UOMGroup();
            try
            {
                DataSet ds = b.GetUOMGroupNameAccUOMId(ObjUOMGroup);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    uom.UOM_Group_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["uomGroupId"]);
                    uom.Group_Name = ds.Tables[0].Rows[0]["uomGroupName"].ToString();
                }
            }
            catch (Exception ex) { }
            return Json(uom);
        }

        #endregion

        #region//--------------------umo mapping Master-------------------------

        [HttpPost]
        public JsonResult AddUOMGroupQty(UOMGroupQty objUOMGroupMap)
        {
            string sms = "";
            UOMGroupQty uomgroupqty = new UOMGroupQty();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroupMap.User_Id = Convert.ToInt32(cookie.Value);
                if (objUOMGroupMap.UOM_Group_Qty_Id == 0)
                {
                    int i = uomgroupqty.InsertUOMGroupQty(objUOMGroupMap);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = uomgroupqty.UpdateUOMGroupQty(objUOMGroupMap);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkuomGroupMapping(UOMGroupQty objUOMGroupMapping)
        {
            string sms = "";
            UOMGroupQty uomg = new UOMGroupQty();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroupMapping.User_Id = Convert.ToInt32(cookie.Value);
                string[] uomgroupname = objUOMGroupMapping.Group_Name.Split(',');
                string[] BaseQty = objUOMGroupMapping.Base_Quantity.Split(',');
                string[] BaseUOM = objUOMGroupMapping.Base_UOM.Split(',');
                string[] AltQty = objUOMGroupMapping.Alt_Quantity.Split(',');
                string[] AltUOM = objUOMGroupMapping.Alt_UOM.Split(',');
                for (int i = 0; i < BaseQty.Length; i++)
                {

                    UOMGroup uomGroup = new UOMGroup();
                    uomGroup.Group_Name = uomgroupname[i].ToString();
                    int id1 = uomGroup.InsertUOMGroupBulk(uomGroup);


                    UOM uom1 = new UOM();
                    uom1.UOM_Name = BaseUOM[i].ToString();
                    int id2 = uom1.InsertUOMbulk(uom1);


                    UOM uom2 = new UOM();
                    uom2.UOM_Name = AltUOM[i].ToString();
                    int id = uom2.InsertUOMbulk(uom2);



                    UOMGroupQty uomg1 = new UOMGroupQty();
                    uomg1.UOM_Group_Id = id1;
                    uomg1.Base_Quantity = BaseQty[i].ToString();
                    uomg1.Base_UOM_Id = id2;
                    uomg1.Alt_Quantity = AltQty[i].ToString();
                    uomg1.Alt_UOM_Id = id;
                    int j = uomg.InsertUOMGroupQty(uomg1);
                }
                sms = "**UOM Group Mapping bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteUOMGroupQty(UOMGroupQty objUOMGroupMap)
        {
            string sms = "";
            UOMGroupQty uomgroupqty = new UOMGroupQty();
            try
            {
                objUOMGroupMap.User_Id = Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);


                int i = uomgroupqty.DeleteUOMGroupQty(objUOMGroupMap);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult UOMGroupQtyDetails()
        {
            UOMGroupQty uomgroupqty = new UOMGroupQty();
            List<UOMGroupQty> objuomgroupqty = new List<UOMGroupQty>();
            try
            {
                DataSet ds = uomgroupqty.GetUOMGroupQty(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objuomgroupqty.Add(new UOMGroupQty
                    {
                        UOM_Group_Qty_Id = Convert.ToInt32(dr["uomMappingId"]),
                        UOM_Group_Id = Convert.ToInt32(dr["uomGroupId"]),
                        Base_UOM_Id = Convert.ToInt32(dr["baseUOMId"]),
                        Alt_UOM_Id = Convert.ToInt32(dr["altUOMId"]),
                        Base_Quantity = dr["baseqty"].ToString(),
                        Alt_Quantity = dr["altqty"].ToString(),
                        Group_Name = dr["umogroupName"].ToString(),
                        Base_UOM = dr["baseumoName"].ToString(),
                        Alt_UOM = dr["altumoName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()

                    });

                }
            }
            catch (Exception ex) { }
            return Json(objuomgroupqty, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UOMGroupQtyApproveStatus(UOMGroupQty objUOMGroupQty)
        {
            string sms = "";
            UOMGroupQty qty = new UOMGroupQty();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objUOMGroupQty.User_Id = Convert.ToInt32(cookie.Value);

                int i = qty.GetUOMGroupQtyApproveStatus(objUOMGroupQty);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/UOMGroupMaster";
                    MailConst.senderMail("UOM Mapping", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }


        #endregion

        #region//-----exchange rate master-----------------------
        public ActionResult ExchangeRateMaster()
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
        public JsonResult AddExchangeRate(ExchangeRate objExchangeRate)
        {
            string sms = "";
            ExchangeRate exchangerate = new ExchangeRate();
            try
            {
                DateTime datestr = DateTime.ParseExact(objExchangeRate.Date_String, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                // DateTime date = Convert.ToDateTime(datestr.Day + "/" + datestr.Month + "/" + datestr.Year);
                // DateTime date1 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
                objExchangeRate.Exchange_Rate_Date = datestr;
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objExchangeRate.User_Id = Convert.ToInt32(cookie.Value);
                if (objExchangeRate.Exchange_Rate_Id == 0)
                {
                    int i = exchangerate.InsertExchangeRate(objExchangeRate);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = exchangerate.UpdateExchangeRate(objExchangeRate);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
         [HttpPost]
        public JsonResult AddBulkExchangeRate(ExchangeRate objExchangeRate)
        {
            string sms = "";
            ExchangeRate ExchangeRate = new ExchangeRate();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objExchangeRate.User_Id = Convert.ToInt32(cookie.Value);
                string[] companyname = objExchangeRate.Company_Name.Split(',');
                string[] Exchange_Rate_Date = objExchangeRate.Date_String.Split(',');
                string[] Currency = objExchangeRate.Currency.Split(',');
                string[] Exchange_Rate_Amount = objExchangeRate.Exchange_Rate_Amount.Split(',');

                IFormatProvider format = new CultureInfo("fr-FR");


                for (int i = 0; i < Exchange_Rate_Amount.Length; i++)
                {
                    Company c = new Company();
                    c.Company_Name = companyname[i].ToString();
                    int id = c.InsertCompanyMasterForBulk(c);
                    Currency c1 = new Currency();
                    c1.Currency_Name = Currency[i].ToString();
                    int id1 = c1.InsertCurrencyBulk(c1);
                    ExchangeRate ExchangeRate1 = new ExchangeRate();
                    ExchangeRate1.Company_Id = id;
                    ExchangeRate1.Company_Name = companyname[i].ToString();
                    DateTime datestr = DateTime.ParseExact(Exchange_Rate_Date[i], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    // DateTime date = Convert.ToDateTime(datestr.Day + "/" + datestr.Month + "/" + datestr.Year);
                    // DateTime date1 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
                    ExchangeRate1.Exchange_Rate_Date = datestr;
                    //ExchangeRate1.Date_String = objExchangeRate.Date_String[i].ToString();
                    ExchangeRate1.Currency_Id = id1;
                    ExchangeRate1.Exchange_Rate_Amount = Exchange_Rate_Amount[i].ToString();

                    //try
                    //{
                    //    DateTime datestr = DateTime.ParseExact(Exchange_Rate_Date[i].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //    //DateTime datestr = DateTime.Parse(objExchangeRate.Date_String[i].ToString());
                    //    ExchangeRate1.Exchange_Rate_Date = datestr;
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    int j = ExchangeRate.InsertExchangeRate(ExchangeRate1);
                }
                sms = "**Exchange Rate  bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteExchangeRate(ExchangeRate objExchangeRate)
        {
            string sms = "";
            ExchangeRate exchangerate = new ExchangeRate();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objExchangeRate.User_Id = Convert.ToInt32(cookie.Value);

                int i = exchangerate.DeleteExchangeRate(objExchangeRate);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult ExchangeRateDetails()
        {
            ExchangeRate exchangerate = new ExchangeRate();
            List<ExchangeRate> Objexchangerate = new List<ExchangeRate>();
            try
            {
                DataSet ds = exchangerate.GetExchangeRate(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Objexchangerate.Add(new ExchangeRate
                    {
                        Exchange_Rate_Id = Convert.ToInt32(dr["exchangeRateId"]),
                        Company_Name = dr["companyName"].ToString(),
                        Date_String = dr["exchangeDate"].ToString(),
                        Currency = dr["currencyName"].ToString(),
                        Company_Id = Convert.ToInt32(dr["companyId"]),
                        Currency_Id = Convert.ToInt32(dr["currencyId"]),
                        Exchange_Rate_Amount = dr["exchageAmount"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Objexchangerate, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExchangeRateApproveStatus(ExchangeRate objExchangeRate)
        {
            string sms = "";
            ExchangeRate exchangerate = new ExchangeRate();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objExchangeRate.User_Id = Convert.ToInt32(cookie.Value);

                int i = exchangerate.GetExchangeRateApproveStatus(objExchangeRate);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/ExchangeRateMaster";
                    MailConst.senderMail("Exchange Rate", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        #endregion

        #region//----------------HSCategory-----------------------------
        public ActionResult HSCaregory()
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
        public JsonResult HSCaregoryName()
        {
            List<SelectListItem> hscategorynamelist = new List<SelectListItem>();
            HSCategory b = new HSCategory();
            try
            {
                DataSet ds = b.GetHSCategoryName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    hscategorynamelist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["hscategoryId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["hscategoryName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(hscategorynamelist);
        }

        #endregion

        #region //--------------- Rack Master -----------------------
        public ActionResult RackMaster()
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
        public JsonResult AddRack(Rack objRack)
        {
            string sms = "";
            Rack rack = new Rack();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRack.User_Id = Convert.ToInt32(cookie.Value);
                if (objRack.Rack_Id == 0)
                {
                    int i = rack.InsertRack(objRack);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = rack.UpdateRack(objRack);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult AddBulkRack(Rack objRack)
        {
            string sms = "";
            Rack rack = new Rack();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRack.User_Id = Convert.ToInt32(cookie.Value);
                string[] rackno = objRack.Rack_Number.Split(',');
                string[] rackName = objRack.Rack_Name.Split(',');
                for (int i = 0; i < rackName.Length; i++)
                {
                    Rack rack1 = new Rack();
                    rack1.Rack_Number = rackno[i].ToString();
                    rack1.Rack_Name = rackName[i].ToString();
                    int j = rack.InsertRack(rack1);
                }
                sms = "**Rack bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteRack(Rack objRack)
        {
            string sms = "";
            Rack rack = new Rack();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRack.User_Id = Convert.ToInt32(cookie.Value);

                int i = rack.DeleteRack(objRack);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult RackDetails()
        {
            Rack rack = new Rack();
            List<Rack> objRack = new List<Rack>();
            try
            {
                DataSet ds = rack.GetRackList(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objRack.Add(new Rack
                    {
                        Rack_Id = Convert.ToInt32(dr["rackId"]),
                        Rack_Name = dr["rackName"].ToString(),
                        Rack_Number = dr["rackNumber"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objRack, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RackApproveStatus(Rack objRack)
        {
            string sms = "";
            Rack rack = new Rack();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRack.User_Id = Convert.ToInt32(cookie.Value);

                int i = rack.GetRackApproveStatus(objRack);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/RackMaster";
                    MailConst.senderMail("Rack", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult RackName()
        {
            List<SelectListItem> racknamelist = new List<SelectListItem>();
            Rack b = new Rack();
            try
            {
                DataSet ds = b.GetRackList(0);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    racknamelist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["rackId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["rackName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(racknamelist);
        }
        #endregion
        #region //--------------- Bin Number Master -----------------------
        public ActionResult BinMaster()
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
        public JsonResult AddRackNumber(RackNumber objRackNumber)
        {
            string sms = "";
            RackNumber rack = new RackNumber();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRackNumber.User_Id = Convert.ToInt32(cookie.Value);
                if (objRackNumber.Rack_Number_Id == 0)
                {
                    int i = rack.InsertRackNumber(objRackNumber);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = rack.UpdateRackNumber(objRackNumber);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkBin(RackNumber objbin)
        {
            string sms = "";
            RackNumber bin = new RackNumber();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objbin.User_Id = Convert.ToInt32(cookie.Value);
                string[] binnumber = objbin.Rack_Number_Name.Split(',');

                for (int i = 0; i < binnumber.Length; i++)
                {
                    RackNumber bin1 = new RackNumber();
                    bin1.Rack_Number_Name = binnumber[i].ToString();

                    int j = bin.InsertRackNumber(bin1);
                }
                sms = "**bin bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteRackNumber(RackNumber objRackNumber)
        {
            string sms = "";
            RackNumber rack = new RackNumber();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRackNumber.User_Id = Convert.ToInt32(cookie.Value);

                int i = rack.DeleteRackNumber(objRackNumber);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult RackNumberDetails()
        {
            RackNumber rack = new RackNumber();
            List<RackNumber> objRackNumber = new List<RackNumber>();
            try
            {
                DataSet ds = rack.GetRackNumberList(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objRackNumber.Add(new RackNumber
                    {
                        Rack_Number_Id = Convert.ToInt32(dr["racknumberId"]),
                        Rack_Number_Name = dr["racknumberName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objRackNumber, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RackNumberApproveStatus(RackNumber objRackNumber)
        {
            string sms = "";
            RackNumber rack = new RackNumber();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objRackNumber.User_Id = Convert.ToInt32(cookie.Value);

                int i = rack.GetRackNumberApproveStatus(objRackNumber);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/BinMaster";
                    MailConst.senderMail("Bin", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult RackNumberName()
        {
            List<SelectListItem> rackNumberlist = new List<SelectListItem>();
            RackNumber b = new RackNumber();
            try
            {
                DataSet ds = b.GetRackNumberList(0);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rackNumberlist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["racknumberId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["racknumberName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(rackNumberlist);
        }
        #endregion

        #region //--------------- Store Location Master -----------------------
        public ActionResult StoreLocationMaster()
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
        public JsonResult AddStoreLocation(StoreLocation objStoreLocation)
        {
            string sms = "";
            StoreLocation store = new StoreLocation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objStoreLocation.User_Id = Convert.ToInt32(cookie.Value);
                if (objStoreLocation.Store_Location_Id == 0)
                {
                    int i = store.InsertStoreLocation(objStoreLocation);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = store.UpdateStoreLocation(objStoreLocation);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkstoreLocation(StoreLocation objStoreLocation)
        {
            string sms = "";
            StoreLocation sl = new StoreLocation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objStoreLocation.User_Id = Convert.ToInt32(cookie.Value);
                string[] slcode = objStoreLocation.Store_Location_Code.Split(',');
                string[] slName = objStoreLocation.Store_Location_Name.Split(',');
                for (int i = 0; i < slName.Length; i++)
                {
                    StoreLocation sl1 = new StoreLocation();
                    sl1.Store_Location_Code = slcode[i].ToString();
                    sl1.Store_Location_Name = slName[i].ToString();
                    int j = sl.InsertStoreLocation(sl1);
                }
                sms = "**store bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteStoreLocationr(StoreLocation objStoreLocation)
        {
            string sms = "";
            StoreLocation store = new StoreLocation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objStoreLocation.User_Id = Convert.ToInt32(cookie.Value);

                int i = store.DeleteStoreLocation(objStoreLocation);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult StoreLocationDetails()
        {
            StoreLocation store = new StoreLocation();
            List<StoreLocation> objStoreLocation = new List<StoreLocation>();
            try
            {
                DataSet ds = store.GetStoreLocationList(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objStoreLocation.Add(new StoreLocation
                    {
                        Store_Location_Id = Convert.ToInt32(dr["storelocationId"]),
                        Store_Location_Name = dr["storelocationName"].ToString(),
                        Store_Location_Code = dr["storelocationCode"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objStoreLocation, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult StoreLocationApproveStatus(StoreLocation objStoreLocation)
        {
            string sms = "";
            StoreLocation store = new StoreLocation();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objStoreLocation.User_Id = Convert.ToInt32(cookie.Value);

                int i = store.GetStoreLocationApproveStatus(objStoreLocation);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/StoreLocationMaster";
                    MailConst.senderMail("Store Location", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult StoreLocationName()
        {
            List<SelectListItem> storelist = new List<SelectListItem>();
            StoreLocation b = new StoreLocation();
            try
            {
                DataSet ds = b.GetStoreLocationName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    storelist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["storelocationId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["storelocationName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(storelist);
        }
        [HttpPost]
        public JsonResult StoreLocationCodeAccStoreId(StoreLocation  Objstorelocation)
        {
            // List<SelectListItem> uomgrouplist = new List<SelectListItem>();
            StoreLocation b = new StoreLocation();
            try
            {
                DataSet ds = b.GetStoreLocationCodeAccId(Objstorelocation);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    b.Store_Location_Code = ds.Tables[0].Rows[0]["storelocationCode"].ToString();
                }
            }
            catch (Exception ex) { }
            return Json(b);
        }
        #endregion

        #region //---------------Mererial Category-----------------------
        public ActionResult MaterialCategoryMaster()
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
        public JsonResult AddInsertMaterialCategory(MaterialCategory objMaterialCategory)
        {
            string sms = "";
            MaterialCategory mtrlgrp = new MaterialCategory();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objMaterialCategory.User_Id = Convert.ToInt32(cookie.Value);
                if (objMaterialCategory.Material_Category_Id == 0)
                {
                    int i = mtrlgrp.InsertMaterialCategory(objMaterialCategory);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = mtrlgrp.UpdateMaterialCategory(objMaterialCategory);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkMaterialCategory(MaterialCategory objMaterialCategory)
        {
            string sms = "";
            MaterialCategory materialcategory = new MaterialCategory();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objMaterialCategory.User_Id = Convert.ToInt32(cookie.Value);
                string[] mccode = objMaterialCategory.Material_Category_Code.Split(',');
                string[] mcName = objMaterialCategory.Material_Category_Name.Split(',');
                for (int i = 0; i < mcName.Length; i++)
                {
                    MaterialCategory materialcategory1 = new MaterialCategory();
                    materialcategory1.Material_Category_Code = mccode[i].ToString();
                    materialcategory1.Material_Category_Name = mcName[i].ToString();
                    int j = materialcategory.InsertMaterialCategory(materialcategory1);
                }
                sms = "**Material category bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteMaterialCategory(MaterialCategory objMaterialCategory)
        {
            string sms = "";
            MaterialCategory mtrlgrp = new MaterialCategory();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objMaterialCategory.User_Id = Convert.ToInt32(cookie.Value);

                int i = mtrlgrp.DeleteMaterialCategory(objMaterialCategory);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult MaterialCategoryDetails()
        {
            MaterialCategory mtrlgrp = new MaterialCategory();
            List<MaterialCategory> objMaterialCategory = new List<MaterialCategory>();
            try
            {
                DataSet ds = mtrlgrp.GetMaterialCategory(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objMaterialCategory.Add(new MaterialCategory
                    {
                        Material_Category_Id = Convert.ToInt32(dr["materialCategoryId"]),
                        Material_Category_Code = dr["materialCategoryCode"].ToString(),
                        Material_Category_Name = dr["materialCategoryName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objMaterialCategory, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MaterialCategoryApproveStatus(MaterialCategory objMaterialCategory)
        {
            string sms = "";
            MaterialCategory materialcategory = new MaterialCategory();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objMaterialCategory.User_Id = Convert.ToInt32(cookie.Value);

                int i = materialcategory.GetMaterialCategoryApproveStatus(objMaterialCategory);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/MaterialCategoryMaster";
                    MailConst.senderMail("Material Category", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult MaterialCategoryName()
        {
            List<SelectListItem> MaterialCategorylist = new List<SelectListItem>();
            MaterialCategory b = new MaterialCategory();
            try
            {
                DataSet ds = b.GetMaterialCategoryName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MaterialCategorylist.Add(new SelectListItem
                    {
                        Value = dr["materialCategoryId"].ToString(),
                        Text = dr["materialCategoryName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(MaterialCategorylist);
        }
        #endregion

        #region//--------------------item type Master-------------------------

        public ActionResult ItemTypeMaster()
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
        public ActionResult UploadItemTypeIcon()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if ((fileex == "jpeg") || (fileex == "JPEG") || (fileex == "jpg") || (fileex == "JPG") || (fileex == "png") || (fileex == "PNG"))
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
                            fname = Path.Combine(Server.MapPath("~/ItemTypeImages/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1]) + "." + fileex;
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/ItemTypeImages/" + name;
                            sms = "File Uploaded Successfully!+" + path;
                        }
                        else
                        { sms = "Please select only jpeg,png file_" + path; }
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
        public JsonResult AddItemType(ItemType objitemtype)
        {
            string sms = "";
            ItemType itemtype = new ItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objitemtype.User_Id = Convert.ToInt32(cookie.Value);
                if (objitemtype.Item_Type_Id == 0)
                {
                    int i = itemtype.InsertItemType(objitemtype);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = itemtype.UpdateItemType(objitemtype);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkItemType(ItemType objitemtype)
        {
            string sms = "";
            ItemType itemtype = new ItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objitemtype.User_Id = Convert.ToInt32(cookie.Value);
                string[] vmcode = objitemtype.Item_Type_Code.Split(',');
                string[] vmName = objitemtype.Item_Type_Name.Split(',');
                for (int i = 0; i < vmName.Length; i++)
                {
                    ItemType itemtype1 = new ItemType();
                    itemtype1.Item_Type_Code = vmcode[i].ToString();
                    itemtype1.Item_Type_Name = vmName[i].ToString();
                    itemtype1.Item_Type_Url = "";
                    int j = itemtype.InsertItemType(itemtype1);
                }
                sms = "**item type bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteItemType(ItemType objitemtype)
        {
            string sms = "";
            ItemType itemtype = new ItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objitemtype.User_Id = Convert.ToInt32(cookie.Value);

                int i = itemtype.DeleteItemType(objitemtype);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult ItemTypeDetails()
        {
            ItemType itemtype = new ItemType();
            List<ItemType> ObjItemType = new List<ItemType>();
            try
            {
                DataSet ds = itemtype.GetItemType(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ObjItemType.Add(new ItemType
                    {
                        Item_Type_Id = Convert.ToInt32(dr["itemTypeId"]),
                        Item_Type_Code = dr["itemTypeCode"].ToString(),
                        Item_Type_Name = dr["itemTypeName"].ToString(),
                        Item_Type_Url = dr["itemTypeUrl"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(ObjItemType, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ItemTypeDetailsFotItem()
        {
            ItemType itemtype = new ItemType();
            List<ItemType> ObjItemType = new List<ItemType>();
            try
            {
                DataSet ds = itemtype.GetItemTypeForItem(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ObjItemType.Add(new ItemType
                    {
                        Item_Type_Id = Convert.ToInt32(dr["itemTypeId"]),
                        Item_Type_Url = dr["itemTypeUrl"].ToString(),
                        Item_Type_Name = dr["itemTypeName"].ToString(),
                        Item_Count = Convert.ToInt32(dr["itemNumber"])
                    });

                }
            }
            catch (Exception ex) { }
            return Json(ObjItemType, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemTypeApproveStatus(ItemType objItemType)
        {
            string sms = "";
            ItemType itemtype = new ItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItemType.User_Id = Convert.ToInt32(cookie.Value);

                int i = itemtype.GetItemTypeApproveStatus(objItemType);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/ItemTypeMaster";
                    MailConst.senderMail("Item Type", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult ItemTypeName()
        {
            List<SelectListItem> itemTypelist = new List<SelectListItem>();
            ItemType b = new ItemType();
            try
            { 
            DataSet ds = b.GetItemTypeName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemTypelist.Add(new SelectListItem
                    {
                        Value = dr["itemTypeId"].ToString(),
                        Text = dr["itemTypeName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(itemTypelist);
        }
        #endregion

        #region//--------------------item sub type Master-------------------------

        public ActionResult SubItemTypeMaster()
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
        public JsonResult AddSubItemType(SubItemType objsubitemtype)
        {
            string sms = "";
            SubItemType subitemtype = new SubItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsubitemtype.User_Id = Convert.ToInt32(cookie.Value);
                if (objsubitemtype.Sub_item_Type_Id == 0)
                {
                    int i = subitemtype.InsertSubItemType(objsubitemtype);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = subitemtype.UpdateSubItemType(objsubitemtype);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkSubItemType(SubItemType objsubitemtype)
        {
            string sms = "";
            SubItemType subitmtype = new SubItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsubitemtype.User_Id = Convert.ToInt32(cookie.Value);
                string[] itemtypename = objsubitemtype.Item_Type_Name.Split(',');
                string[] subitemtypename = objsubitemtype.Sub_Item_Type_Name.Split(',');

                for (int i = 0; i < itemtypename.Length; i++)
                {
                    ItemType it = new ItemType();
                    it.Item_Type_Name = itemtypename[i].ToString();
                    int id = it.InsertBulkItemType(it);
                    SubItemType subitmtype1 = new SubItemType();
                    subitmtype1.Item_Type_Id = id;

                    subitmtype1.Sub_Item_Type_Name = subitemtypename[i].ToString();

                    int j = subitmtype.InsertSubItemType(subitmtype1);
                }
                sms = "**Sub item type bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteSubItemType(SubItemType objsubitemtype)
        {
            string sms = "";
            SubItemType subitemtype = new SubItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsubitemtype.User_Id = Convert.ToInt32(cookie.Value);

                int i = subitemtype.DeleteSubItemType(objsubitemtype);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult SubItemTypeDetails()
        {
            SubItemType subitemtype = new SubItemType();
            List<SubItemType> objsubitemtype = new List<SubItemType>();
            try
            {
                DataSet ds = subitemtype.GetSubItemType(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objsubitemtype.Add(new SubItemType
                    {
                        Sub_item_Type_Id = Convert.ToInt32(dr["subitemtypeId"]),
                        Item_Type_Id = Convert.ToInt32(dr["itemtypeId"]),
                        Sub_Item_Type_Name = dr["subitemtypeName"].ToString(),
                        Item_Type_Name = dr["ItemTypeName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objsubitemtype, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SubItemTypeApproveStatus(SubItemType objSubItemType)
        {
            string sms = "";
            SubItemType subitemtype = new SubItemType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objSubItemType.User_Id = Convert.ToInt32(cookie.Value);

                int i = subitemtype.GetSubItemTypeApproveStatus(objSubItemType);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/SubItemTypeMaster";
                    MailConst.senderMail("Sub Item Type", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult SubItemTypeNameAccItemTypeForul(SubItemType objSubItemType)
        {

            List<SubItemType> ObjSubItemTypevalue = new List<SubItemType>();
            SubItemType b = new SubItemType();
            try
            {
                DataSet ds = b.GetSubItemTypeName(objSubItemType);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ObjSubItemTypevalue.Add(new SubItemType
                    {
                        Sub_item_Type_Id = Convert.ToInt32(dr["subitemtypeId"].ToString()),
                        Sub_Item_Type_Name = dr["subitemtypeName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(ObjSubItemTypevalue);
        }
        public JsonResult SubItemTypeNameAccItemType(SubItemType objSubItemType)
        {
            List<SelectListItem> statelist = new List<SelectListItem>();
            SubItemType b = new SubItemType();
            try
            {
                DataSet ds = b.GetSubItemTypeName(objSubItemType);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    statelist.Add(new SelectListItem
                    {
                        Value = dr["subitemtypeId"].ToString(),
                        Text = dr["subitemtypeName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(statelist);
        }

        #endregion

        #region//-------------------- Sub Item Field Master-------------------------

        public ActionResult SubItemField()
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
        public JsonResult AddSubItemField(SubItemTypeField objsubitemfield)
        {
            string sms = "";
            try
            {
                SubItemTypeField subitemtypefield = new SubItemTypeField();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsubitemfield.User_Id = Convert.ToInt32(cookie.Value);
                if (objsubitemfield.Sub_item_Type_Field_Id == 0)
                {
                    int i = subitemtypefield.InsertSubItemTypeField(objsubitemfield);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data Already Exist**";
                    }
                }
                else
                {
                    int i = subitemtypefield.UpdateSubItemTypeField(objsubitemfield);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data Already Exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteSubItemTypeField(SubItemTypeField objsubitemtypefield)
        {
            string sms = "";
            try
            {
                SubItemTypeField subitemtypefield = new SubItemTypeField();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsubitemtypefield.User_Id = Convert.ToInt32(cookie.Value);
                int i = subitemtypefield.DeleteSubItemTypeField(objsubitemtypefield);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult SubItemTypeFieldDetails()
        {
            SubItemTypeField subitemtypefield = new SubItemTypeField();
            List<SubItemTypeField> objsubitemtypefield = new List<SubItemTypeField>();
            try
            {
                DataSet ds = subitemtypefield.GetSubItemTypeField(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objsubitemtypefield.Add(new SubItemTypeField
                    {
                        Sub_item_Type_Field_Id = Convert.ToInt32(dr["subitemtypefieldId"]),
                        Item_Type_Id = Convert.ToInt32(dr["itemtypeId"]),
                        Sub_item_Type_Id = Convert.ToInt32(dr["subitemtypeId"]),
                        Item_Type_Name = dr["itemType"].ToString(),
                        Sub_Item_Type_Name = dr["subitemType"].ToString(),
                        Sub_Item_Type_Field_Name = dr["subitemtypefieldName"].ToString(),
                        Sub_Item_Type_Field_Optional = dr["optionaltype"].ToString(),
                        Sub_Item_Type_Field_Optional_Value = dr["optionalValue"].ToString(),
                        Sub_Item_Type_Field_Priority = Convert.ToInt32(dr["subitemtypefieldPriority"]),
                        Sub_Item_Type_Field_Input_Type = dr["textType"].ToString(),
                        UOM_Group_Id = Convert.ToInt32(dr["uomGroupId"]),
                        UOM_Group_Name = dr["uomgroup"].ToString(),
                        Approve_Status = dr["isApproved"].ToString(),
                        Sub_Item_Type_Field_Optional_Full_Value = dr["fullOptionalValue"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objsubitemtypefield, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubItemTypeFieldApproveStatus(SubItemTypeField objSubItemTypeField)
        {
            string sms = "";
            SubItemTypeField subitemtypefield = new SubItemTypeField();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objSubItemTypeField.User_Id = Convert.ToInt32(cookie.Value);

                int i = subitemtypefield.GetSubItemTypeFieldApproveStatus(objSubItemTypeField);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/SubItemField";
                    MailConst.senderMail("Sub Item Field", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult SubItemTypeName()
        {
            List<SelectListItem> subitemtypelist = new List<SelectListItem>();
            SubItemType b = new SubItemType();
            try
            {
                DataSet ds = b.GetSubItemType(0);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    subitemtypelist.Add(new SelectListItem
                    {
                        Value = dr["subitemtypeId"].ToString(),
                        Text = dr["subitemtypeName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(subitemtypelist);
        }

        [HttpPost]
        public JsonResult SubItemFieldAccSubItemTypeId(SubItemTypeField objSubItemTypeField)
        {
            SubItemTypeField subitemfield = new SubItemTypeField();
            List<SubItemTypeField> objitemlist = new List<SubItemTypeField>();
            try
            { 
            DataSet ds = subitemfield.GetSubItemTypeFieldAccSubItemTypeId(objSubItemTypeField);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new SubItemTypeField
                    {
                        Sub_item_Type_Field_Id = Convert.ToInt32(dr["subitemtypefieldId"]),
                        Sub_Item_Type_Field_Name = dr["subitemtypefieldName"].ToString(),
                        Sub_Item_Type_Field_Optional = dr["optionaltype"].ToString(),
                        Sub_Item_Type_Field_Optional_Value = dr["optionalValue"].ToString(),
                        Sub_Item_Type_Field_Input_Type = dr["textType"].ToString(),
                        Sub_Item_Type_Field_Priority = Convert.ToInt32(dr["subitemtypefieldPriority"]),
                        UOM_Group_Id = Convert.ToInt32(dr["uomGroupId"])
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region//--------------------Manage Item Master-------------------------
        public ActionResult ManageItemMaster()
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
        public JsonResult AddManageItem(ManageItem objmanageitem)
        {
            string sms = "";
            try
            {
                ManageItem manageitem = new ManageItem();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objmanageitem.User_Id = Convert.ToInt32(cookie.Value);
                if (objmanageitem.Manage_Item_Id == 0)
                {
                    int i = manageitem.InsertManageItem(objmanageitem);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = manageitem.UpdateManageItem(objmanageitem);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkManageItem(ManageItem objManageItem)
        {
            string sms = "";
            try
            {
                ManageItem manageitem = new ManageItem();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objManageItem.User_Id = Convert.ToInt32(cookie.Value);
                string[] micode = objManageItem.Manage_Item_Code.Split(',');
                string[] miName = objManageItem.Manage_Item_Name.Split(',');
                for (int i = 0; i < miName.Length; i++)
                {
                    ManageItem manageitem1 = new ManageItem();
                    manageitem1.Manage_Item_Code = micode[i].ToString();
                    manageitem1.Manage_Item_Name = miName[i].ToString();
                    int j = manageitem.InsertManageItem(manageitem1);
                }
                sms = "**Manage item bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteManageItem(ManageItem objmanageitem)
        {
            string sms = "";
            try
            {
                ManageItem manageitem = new ManageItem();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objmanageitem.User_Id = Convert.ToInt32(cookie.Value);

                int i = manageitem.DeleteManageItem(objmanageitem);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult ManageItemDetails()
        {
            ManageItem manageitem = new ManageItem();
            List<ManageItem> objmanageitem = new List<ManageItem>();
            try
            {
                DataSet ds = manageitem.GetManageItem(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objmanageitem.Add(new ManageItem
                    {
                        Manage_Item_Id = Convert.ToInt32(dr["manageitemId"]),
                        Manage_Item_Code = dr["manageitemCode"].ToString(),
                        Manage_Item_Name = dr["manageitemName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objmanageitem, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ManageItemApproveStatus(ManageItem objManageItem)
        {
            string sms = "";
            try
            {
                ManageItem manageitem = new ManageItem();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objManageItem.User_Id = Convert.ToInt32(cookie.Value);

                int i = manageitem.GetManageItemApproveStatus(objManageItem);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/ManageItemMaster";
                    MailConst.senderMail("Manage Item", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult MamageItemName()
        {
            List<SelectListItem> MaqnageItemlist = new List<SelectListItem>();
            ManageItem b = new ManageItem();
            try
            {
                DataSet ds = b.GetMaqnageItemName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MaqnageItemlist.Add(new SelectListItem
                    {
                        Value = dr["manageitemId"].ToString(),
                        Text = dr["manageitemName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(MaqnageItemlist);
        }
        #endregion

        #region//------------------------Item Master-----------------------------
        public ActionResult ItemMaster()
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
        public ActionResult UploadCADFileMethod()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if ((fileex == "dxf") || (fileex == "dwg") || (fileex == "swp") || (fileex == "STEP") || (fileex == "IGES") || (fileex == "3DS") || (fileex == "COLLADA") || (fileex == "FBX") || (fileex == "OBJ") || (fileex == "STL") || (fileex == "VRML/X3D") || (fileex == "3DXML") || (fileex == "CGR") || (fileex == "3DXML") || (fileex == "DWG") || (fileex == "DXF") || (fileex == "EPRT") || (fileex == "IGS") || (fileex == "SLDFTP") || (fileex == "SLDPRT") || (fileex == "WRL") || (fileex == "X_T") || (fileex == "1"))
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/ItemsFile/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            // file.SaveAs(fname);
                            path = "/uploaded_image/ItemsFile/" + name;
                            //sms = s[0] + " file uploaded successfully!+" + path;
                            string filenameWitoutextension = Path.GetFileNameWithoutExtension(name);
                            System.IO.Directory.CreateDirectory(Server.MapPath(@"\uploaded_image\ItemsFile\") + filenameWitoutextension);
                            string filname = Path.Combine(Server.MapPath("~/uploaded_image/ItemsFile/" + filenameWitoutextension), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            file.SaveAs(filname);
                            var archive = Server.MapPath("~/uploaded_image/ItemsFile/" + filenameWitoutextension + ".Zip");
                            var temp = Server.MapPath("~/uploaded_image/ItemsFile/" + filenameWitoutextension);
                            ZipFile.CreateFromDirectory(temp, archive);
                            string zpath = "/uploaded_image/ItemsFile/" + filenameWitoutextension + ".Zip";
                            sms = s[0] + " file uploaded successfully!+" + zpath;
                        }
                        else
                        { sms = "Please select only STL, OBJ, FBX, COLLADA, 3DS, IGES; STEP, VRML/X3D , dxf , dwg ,and swp,.3DXML,CGR,DWG,DXF,EPRT,IGS,SLDFTP,SLDPRT,WRL,X_T,1 CAD file for " + s[0] + " file+" + path; }
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
        public ActionResult UploadFileMethod()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[1];
                        if ((fileex == "pdf") || (fileex == "docx") || (fileex == "doc") || (fileex == "xls") || (fileex == "xlsx") || (fileex == "xlsm"))
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/ItemsFile/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/ItemsFile/" + name;
                            sms = s[0] + " file uploaded successfully!+" + path;
                        }
                        else
                        { sms = "Please select only pdf, doc and xls file for "+ s[0]  +" file+" + path; }
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
        public ActionResult UploadItemImage()
        {
            //string path = Server.MapPath("~/Content/Upload/");
            //HttpFileCollectionBase files = Request.Files;
            //for (int i = 0; i < files.Count; i++)
            //{
            //    HttpPostedFileBase file = files[i];
            //    file.SaveAs(path + file.FileName);
            //}
            //return Json(files.Count + " Files Uploaded!");

            if (Request.Files.Count > 0)
            {
                try
                {
                    string sms = "";
                    string path = "";
                    string[] s = Request.Form.ToString().Split('=');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        //int idx = Path.GetFileName(Request.Files[i].FileName).LastIndexOf('.');
                       // string fileex = Path.GetFileName(Request.Files[i].FileName).Split('.')[i];
                        string fileex = Path.GetExtension(Request.Files[i].FileName);
                        if ((fileex == ".jpeg") || (fileex == ".JPEG") || (fileex == ".jpg") || (fileex == ".JPG") || (fileex == ".png") || (fileex == ".PNG"))
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
                        fname = Path.Combine(Server.MapPath("~/uploaded_image/Items/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1]) + i  + fileex;
                        string[] splitpath = fname.Split('\\');
                        string name = (splitpath[splitpath.Length - 1]);
                        file.SaveAs(fname);
                        if (i == 0)
                        {
                            path = "/uploaded_image/Items/" + name;
                        }
                        else
                        {
                            path = path + ",/uploaded_image/Items/" + name;
                        }
                        sms = "Image Uploaded Successfully!+" + path;
                        }
                        else
                        { sms = "Please select only jpeg of jpg or png file_" + path; }
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
        public JsonResult AddItem(Item objItem)
        {
            Item itemppara = new Item();
            //Item objitem1 = new Item();
            itemppara.Item_Type_Id = 0;
            int count = 0;

            string sms = "";
            Item item = new Item();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objItem.User_Id = Convert.ToInt32(cookie.Value);
            //-------------------------------------------------insert item-----------------------------------------------------------------
            if (objItem.Item_Id == 0)
            {

                DataSet ds11 = itemppara.GetItemForCheckDuplcate(objItem);
               string val="";
               
                    foreach (DataRow dr in ds11.Tables[0].Rows)
                    {
                         val=Convert.ToString(dr["itemCode"]);
                       
                    }
                 if (val != 0.ToString())
                {
                    sms = "**Duplicate manufacturing part number. Please insert another part number**" + "," + val;
                }
                else
                {
                    if (objItem.Sub_Item_Type_Field_values == "" || objItem.Sub_Item_Type_Field_values == null)
                    {
                        sms = item.InsertItem(objItem);
                        string[] code = sms.Split(',');
                        try
                        {
                            string[] part = objItem.Alt_Manufacturing_Part_Number.Split(',');
                            string[] brand = objItem.Alt_Item_Brand.Split(',');
                            string[] url = objItem.Alt_Item_Url.Split(',');
                            for (int j = 0; j < part.Length; j++)
                            {
                                if (part[j] == "undefined" || brand[j] == "undefined" || url[j] == "undefined")
                                {

                                }
                                else
                                {
                                    Item pd = new Item();
                                    pd.Internal_Item_Code = code[1];
                                    pd.Alt_Manufacturing_Part_Number = part[j];
                                    pd.Alt_Item_Brand = brand[j];
                                    pd.Alt_Item_Url = url[j];

                                    pd.InsertItemAlternate(pd);
                                }
                            }
                        }
                        catch (Exception ex)
                        { }

                        string path = "";
                        try
                        {
                            item.Internal_Item_Code = code[1];
                            DataSet ds1 = item.GetItemForListForExportAccInternalcode(item);
                            StringWriter sw = new StringWriter();
                            ds1.WriteXml(Server.MapPath("/uploaded_image/xmlfiles/" + ds1.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml"));
                            string s = sw.ToString();
                            if (ds1 != null)
                            {
                                path = "/uploaded_image/xmlfiles/" + ds1.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml";
                                
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        DataSet ds = itemppara.GetSubItemFieldValueForCheckDuplcate(objItem);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            count = Convert.ToInt32(dr["count"]);
                        }
                        if (count == 0)
                        {
                            sms = item.InsertItem(objItem);
                            string[] code = sms.Split(',');
                            try
                            {

                                string[] part = objItem.Alt_Manufacturing_Part_Number.Split(',');
                                string[] brand = objItem.Alt_Item_Brand.Split(',');
                                string[] url = objItem.Alt_Item_Url.Split(',');
                                for (int j = 0; j < part.Length; j++)
                                {
                                    if (part[j] == "undefined" || brand[j] == "undefined" || url[j] == "undefined")
                                    {

                                    }
                                    else
                                    {
                                        Item pd = new Item();
                                        pd.Internal_Item_Code = code[1];
                                        pd.Alt_Manufacturing_Part_Number = part[j];
                                        pd.Alt_Item_Brand = brand[j];
                                        pd.Alt_Item_Url = url[j];

                                        pd.InsertItemAlternate(pd);
                                    }
                                }
                            }
                            catch (Exception ex) { }
                            string path = "";
                            try
                            {
                                item.Internal_Item_Code = code[1];
                                DataSet ds1 = item.GetItemForListForExportAccInternalcode(item);
                                StringWriter sw = new StringWriter();
                                ds1.WriteXml(Server.MapPath("/uploaded_image/xmlfiles/" + ds1.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml"));
                                string s = sw.ToString();
                                if (ds1 != null)
                                {
                                    path = "/uploaded_image/xmlfiles/" + ds1.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml";  
                                }
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            sms = "**with this parameter already created sub category item. Please change parameter.**";
                        }
                       
                    }
                }


            }
            //-----------------------------------------------------------------update item------------
            else
            {
                DataSet ds12 = itemppara.GetItemForCheckDuplcateForUpdate(objItem);
                int val2 = 0;

                foreach (DataRow dr in ds12.Tables[0].Rows)
                {
                    val2 = Convert.ToInt32(dr["COUNT"]);

                }
                if (val2 != 0)
                {
                    sms = "**Duplicate manufacturing part number. Please insert another part number**,1";
                }
                else
                {
                    int i = item.UpdateItem(objItem);
                    int d = item.DeleteAltItem(objItem);
                    try
                    {
                        string[] part = objItem.Alt_Manufacturing_Part_Number.Split(',');
                        string[] brand = objItem.Alt_Item_Brand.Split(',');
                        string[] url = objItem.Alt_Item_Url.Split(',');
                        for (int j = 0; j < part.Length; j++)
                        {
                            if (part[j] == "undefined" || brand[j] == "undefined" || url[j] == "undefined")
                            {

                            }
                            else
                            {
                                Item pd = new Item();
                                pd.Internal_Item_Code = objItem.Internal_Item_Code;
                                pd.Alt_Manufacturing_Part_Number = part[j];
                                pd.Alt_Item_Brand = brand[j];
                                pd.Alt_Item_Url = url[j];

                                pd.InsertItemAlternate(pd);
                            }
                        }
                    }
                    catch (Exception ex) { }
                    if (i == 0)
                    {
                        string path = "";
                        item.Internal_Item_Code = objItem.Internal_Item_Code;
                        DataSet ds1 = item.GetItemForListForExportAccInternalcode(item);
                        StringWriter sw = new StringWriter();
                        ds1.WriteXml(Server.MapPath("/uploaded_image/xmlfiles/" + ds1.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml"));
                        string s = sw.ToString();
                        if (ds1 != null)
                        {


                            path = "/uploaded_image/xmlfiles/" + ds1.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml";
                            // WebClient wc = new WebClient();
                            //wc.DownloadFile("http://103.133.214.97:84" + path,"");
                            //var FileVirtualPath = path;
                            //File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
                            //using (System.Net.WebClient client = new System.Net.WebClient())
                            //{
                            //    client.DownloadFile("http://103.133.214.97:84/uploaded_image/xmlfiles/Fas0000004.xml", "some.xml");
                            //}
                            // sms = "**Data Exported successfully**+" + path;
                        }
                        sms = "**Data updated successfully**,0";
                    }
                    else
                    {
                        sms = "**Data already exist**,2";
                    }
              }

            }
            return Json(sms);
        }

        [HttpGet]
        public JsonResult ItemDetails()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItem(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),
                        Item_Sub_Type_Id = Convert.ToInt32(dr["itemSubTypeId"]),
                        Internal_Item_Code = dr["internalItemCode"].ToString(),
                        Item_Description = dr["itemDescription"].ToString(),
                        Sort_Item_Desctiption = dr["sortitemDescription"].ToString(),
                        Item_Price = Convert.ToDecimal(dr["itemPrice"]),
                        Item_Min_Order_Qty = Convert.ToDecimal(dr["itemMinOrderQty"]),
                        Item_Manufacturing_Part_Number = dr["itemManufacturingPartNumber"].ToString(),
                        Item_Type_Name = dr["itemType"].ToString(),
                        item_Status = dr["itemStatus"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubItemFieldValueForCheckDuplicate(Item objItem)
        {
            Item itemppara = new Item();
            Item objitem = new Item();
            itemppara.Item_Type_Id = 0;
            if (objItem.Sub_Item_Type_Field_values == "")
            {
                objitem.Count = 0;
            }
            else
            {
                try
                {
                    DataSet ds = itemppara.GetSubItemFieldValueForCheckDuplcate(objItem);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objitem.Count = Convert.ToInt32(dr["count"]);
                    }
                }
                catch (Exception ex) { }
            }
            return Json(objitem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StatusChangeItem(Item objItemList)
        {
            string sms = "";
            try
            {
                Item item = new Item();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItemList.User_Id = Convert.ToInt32(cookie.Value);
                int i = item.ItemStatusChange(objItemList);
                if (i == 0)
                {
                    sms = "**Data status changed successfully.**";
                }
                else
                {
                    sms = "**Data status not changed.**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult ListingItemImageDetails(Item objitemlistview)
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemImageForListForPopup(objitemlistview);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {

                        Item_Images_Url = dr["ItemImageUrl"].ToString(),

                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ItemDetailsForBarchart()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForcolumnBarChart();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Label1 = dr["itemTypename"].ToString(),
                        Count = Convert.ToInt32(dr["Count1"])
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ItemDetailsForPiechart()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
            DataSet ds = itemlist.GetItemStatusForPieChart();
            
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        itemlist.Approved_Count = Convert.ToInt32(dr["approved"]);
                        itemlist.Pending_Count = Convert.ToInt32(dr["pending"]);
                        itemlist.Reject_Count = Convert.ToInt32(dr["reject"]);

                    }
                }
            }
            catch (Exception ex) { }
            return Json(itemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRecordForManufactureName(string prefix)
        {

            Item searchitem = new Item();
            List<Item> searchmanufactureList = new List<Item>();
            try
            {
                DataSet ds = searchitem.GetSearchManufactureAccKeywords(prefix);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    searchmanufactureList.Add(new Item
                    {
                        Search_Name = dr["SearchItem"].ToString(),

                    });

                }
            }
            catch (Exception ex) { }
            return Json(searchmanufactureList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ItemForCheckDuplicate(Item objItem)
        {
            Item itemppara = new Item();
            Item objitem = new Item();
            try
            {
                DataSet ds = itemppara.GetItemForCheckDuplcate(objItem);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitem.Internal_Item_Code = Convert.ToString(dr["itemCode"]);
                }
            }
            catch (Exception ex) { }
            return Json(objitem, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SubItemTypeFieldNameForScrap(Item objItem)
        {
            List<SelectListItem> fieldlist = new List<SelectListItem>();
            Item b = new Item();
            try
            {
                DataSet ds = b.GetSubItemTypeFieldForScrap(objItem);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fieldlist.Add(new SelectListItem
                    {
                        Value = dr["subitemtypefieldId"].ToString(),
                        Text = dr["subitemtypefieldName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(fieldlist);
       }
        [HttpPost]
        public JsonResult ScrapDataShow(Item objscrap)
        {
            string sms = "";
            string Path = "";
            Item b = new Item();
            DataTable ds = null;
            List<Item> objitemscrap = new List<Item>();
            try
            {
                ds = b.GetScrapData(objscrap);
                if (ds.Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Rows)
                    {
                        objitemscrap.Add(new Item
                        {
                            Item_Manufacturing_Part_Number = dr["partno"].ToString(),
                            Item_Description = dr["description"].ToString(),
                            Item_Manufacture = dr["manufacturer"].ToString(),
                            Item_Price_Str = dr["price"].ToString(),
                            Item_Brand = dr["brand"].ToString(),
                            Item_Images_Url = dr["imagesrc"].ToString(),
                            Item_Scrap_Size = dr["itemsize"].ToString(),
                        });
                    }
                    try
                    {
                        if (objscrap.Item_URLS == "https://my.mouser.com/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                string[] img = ds.Rows[0]["imagesrc"].ToString().Split(',');
                                for (int i = 0; i < img.Length; i++)
                                {
                                    string[] url = (img[i].ToString()).Split('=');
                                    string[] imgsr = url[2].ToString().Replace("\"", "").Replace("/>", "").Split(' ');
                                    Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr[0].Replace("https:", "")).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    if (i == 0)
                                    {
                                        Path = "/uploaded_image/Items/" + name;
                                    }
                                    else
                                    {
                                        Path = Path + "," + "/uploaded_image/Items/" + name;
                                    }
                                    Thread.Sleep(1000);
                                }
                                objitemscrap.Add(new Item
                                {
                                    Image_Path = Path
                                });
                            }
                            else
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    string[] url = (ds.Rows[i]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = url[1].ToString().Replace("\"", "").Replace("/>", "");
                                    Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    //objitemscrap.Add(new Item
                                    //{
                                    Session["Path"] = "/uploaded_image/Items/" + name;

                                    //});
                                    Common.CommonSetting1.User_Item_Images_Url = "";
                                    Common.CommonSetting1.Item_Images_Url = "/uploaded_image/Items/" + name;
                                    objitemscrap[i].Image_Path = Path;
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        if (objscrap.Item_URLS == "https://my.rs-online.com/web/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                if (ds.Rows[0]["imagesrc"].ToString() == "")
                                {
                                    objitemscrap[0].Image_Path = "";
                                }
                                else
                                {
                                    string[] url = (ds.Rows[0]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = url[1].ToString().Replace("\"", "").Replace("/>", "");
                                    Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    Path = "/uploaded_image/Items/" + name;
                                    objitemscrap[0].Image_Path = Path;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    string[] url = (ds.Rows[i]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = url[1].ToString().Replace("\"", "").Replace("/>", "");
                                    string[] imgsr1 = imgsr.Split(' ');
                                    Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr1[0]).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    Path = "/uploaded_image/Items/" + name;
                                    objitemscrap[i].Image_Path = Path;
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        if (objscrap.Item_URLS == "https://www.pemnet.com/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                string[] img = ds.Rows[0]["imagesrc"].ToString().Split(',');
                                for (int i = 0; i < img.Length; i++)
                                {
                                    if (img[i].Contains("img"))
                                    {
                                        string[] url = (img[i].ToString()).ToString().Split('=');
                                        string imgsr = url[url.Length - 2].ToString().Replace("\"", "").Replace(" title", "");
                                        Common.CommonSetting1.User_Item_Images_Url = ((imgsr).Replace(" ", "")).ToString();
                                        WebClient wc1 = new WebClient();
                                        byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                        string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                        string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[fileex.Length - 1];
                                        HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                        string fname;
                                        fname = file.FileName;
                                        fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                        file.SaveAs(fname);
                                        string[] splitpath = fname.Split('\\');
                                        string name = (splitpath[splitpath.Length - 1]);
                                        if (i == 0)
                                        {
                                            Path = "/uploaded_image/Items/" + name;
                                        }
                                        else
                                        {
                                            Path = Path + "," + "/uploaded_image/Items/" + name;
                                        }
                                        Thread.Sleep(1000);
                                    }
                                }
                                objitemscrap.Add(new Item
                                {
                                    Image_Path = Path
                                });
                                // objitemscrap[0].Image_Path = Path;
                            }
                            else
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    string[] url = (ds.Rows[i]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = url[5].ToString().Replace("\"", "").Replace(" title", "");
                                    Common.CommonSetting1.User_Item_Images_Url = ((imgsr).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    Path = "/uploaded_image/Items/" + name;
                                    objitemscrap[i].Image_Path = Path;
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        if (objscrap.Item_URLS == "https://my.misumi-ec.com/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                string[] img = ds.Rows[0]["imagesrc"].ToString().Split(',');
                                for (int i = 0; i < img.Length; i++)
                                {
                                    if (img[i].Contains("img"))
                                    {
                                        string[] url = (img[i].ToString()).ToString().Split('=');
                                        string imgsr = url[3].ToString().Replace("\"", "").Replace("https:", "").Replace("?$product_main$ alt", "");
                                        Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr).Replace(" ", "")).ToString();
                                        WebClient wc1 = new WebClient();
                                        byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                        string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                        string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[fileex.Length - 1];
                                        HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                        string fname;
                                        fname = file.FileName;
                                        fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                        file.SaveAs(fname);
                                        string[] splitpath = fname.Split('\\');
                                        string name = (splitpath[splitpath.Length - 1]);
                                        if (i == 0)
                                        {
                                            Path = "/uploaded_image/Items/" + name;
                                        }
                                        else
                                        {
                                            Path = Path + "," + "/uploaded_image/Items/" + name;
                                        }
                                        Thread.Sleep(1000);
                                    }
                                }
                                objitemscrap.Add(new Item
                                {
                                    Image_Path = Path
                                });
                            }
                            else
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    string[] url = (ds.Rows[i]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = url[3].ToString().Replace("\"", "").Replace("?$product_view_b$ alt", "");
                                    Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    Path = "/uploaded_image/Items/" + name;
                                    objitemscrap[i].Image_Path = Path;
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        if (objscrap.Item_URLS == "https://www.alibaba.com/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                string[] img = ds.Rows[0]["imagesrc"].ToString().Split(',');
                                for (int i = 0; i < img.Length; i++)
                                {
                                    if (img[i].Contains("img"))
                                    {
                                        string[] url = (img[i]).ToString().Split('=');
                                        string imgsr = "";
                                        if (url.Length <= 5)
                                        {
                                            imgsr = url[2].ToString().Replace("\"", "").Replace("https:", "").Replace(" alt", "");
                                        }
                                        else if (url.Length <= 7)
                                        {
                                            imgsr = url[4].ToString().Replace("\"", "").Replace("https:", "").Replace(" alt", "");
                                        }
                                        else
                                        {
                                            imgsr = url[6].ToString().Replace("\"", "").Replace("https:", "").Replace(" alt", "");
                                        }
                                        Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr).Replace(" ", "")).ToString();

                                        WebClient wc1 = new WebClient();
                                        byte[] bytes;
                                        try
                                        {
                                            //bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                            bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                            string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');

                                            string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[fileex.Length - 1];
                                            HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                            string fname;
                                            fname = file.FileName;
                                            fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                            file.SaveAs(fname);
                                            string[] splitpath = fname.Split('\\');
                                            string name = (splitpath[splitpath.Length - 1]);
                                            if (i == 0)
                                            {
                                                Path = "/uploaded_image/Items/" + name;
                                            }
                                            else
                                            {
                                                Path = Path + "," + "/uploaded_image/Items/" + name;
                                            }
                                            Thread.Sleep(1000);
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }
                                objitemscrap.Add(new Item
                                {
                                    Image_Path = Path
                                });
                                // objitemscrap[0].Image_Path = Path;

                            }
                            else
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    string[] url = (ds.Rows[i]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = "";
                                    if (url.Length < 5)
                                    {
                                        imgsr = url[2].ToString().Replace("\"", "").Replace(" style", "");
                                    }
                                    else if (url.Length < 7)
                                    {
                                        imgsr = url[4].ToString().Replace("\"", "").Replace(" style", "");
                                    }
                                    else
                                    {
                                        imgsr = url[6].ToString().Replace("\"", "").Replace(" style", "");
                                    }
                                    string[] imgsr1 = imgsr.Split(' ');
                                    Common.CommonSetting1.User_Item_Images_Url = (("https:" + imgsr1[0]).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[4];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    Path = "/uploaded_image/Items/" + name;
                                    objitemscrap[i].Image_Path = Path;
                                    Thread.Sleep(1000);
                                }

                            }
                        }
                        if (objscrap.Item_URLS == "http://www.instarelectrical.com/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                string[] img = ds.Rows[0]["imagesrc"].ToString().Split(',');
                                for (int i = 0; i < img.Length; i++)
                                {
                                    if (img[i].Contains("img"))
                                    {
                                        string[] url = (img[i].ToString()).ToString().Split('=');
                                        string[] imgsr = url[2].ToString().Replace("\"", "").Replace(" alt", "").Split(' ');

                                        Common.CommonSetting1.User_Item_Images_Url = ("http:" + (imgsr[0].Replace("http:", "")).Replace(" ", "")).ToString();
                                        WebClient wc1 = new WebClient();
                                        byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                        string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                        string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[fileex.Length - 1];
                                        HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                        string fname;
                                        fname = file.FileName;
                                        fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                        file.SaveAs(fname);
                                        string[] splitpath = fname.Split('\\');
                                        string name = (splitpath[splitpath.Length - 1]);
                                        if (i == 0)
                                        {
                                            Path = "/uploaded_image/Items/" + name;
                                        }
                                        else
                                        {
                                            Path = Path + "," + "/uploaded_image/Items/" + name;
                                        }
                                        Thread.Sleep(1000);
                                    }
                                }
                                objitemscrap.Add(new Item
                                {
                                    Image_Path = Path
                                });
                                // objitemscrap[0].Image_Path = Path;
                            }
                            else
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    string[] url = (ds.Rows[i]["imagesrc"].ToString()).ToString().Split('=');
                                    string imgsr = url[2].ToString().Replace("\"", "").Replace(" alt", "");
                                    Common.CommonSetting1.User_Item_Images_Url = ((imgsr).Replace(" ", "")).ToString();
                                    WebClient wc1 = new WebClient();
                                    byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                    string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                    string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                    HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                    string fname;
                                    fname = file.FileName;
                                    fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + i + "." + fileext).Replace("\r\n", "").ToString();
                                    file.SaveAs(fname);
                                    string[] splitpath = fname.Split('\\');
                                    string name = (splitpath[splitpath.Length - 1]);
                                    Path = "/uploaded_image/Items/" + name;
                                    objitemscrap[i].Image_Path = Path;
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        if (objscrap.Item_URLS == "https://www.ccmrails.com/")
                        {
                            if (objscrap.Scrap_Type == "single")
                            {
                                string[] img = ds.Rows[0]["imagesrc"].ToString().Split('|');
                                for (int i = 0; i < img.Length; i++)
                                {
                                    if (img[i].Contains("img"))
                                    {
                                        string[] url = (img[i].ToString()).ToString().Split('=');
                                        string imgsr = (url[1] + url[2] + url[3] + url[4]).ToString().Replace("\"", "").Replace(" alt", "");
                                        Common.CommonSetting1.User_Item_Images_Url = ((imgsr).Replace(" ", "")).ToString();
                                        WebClient wc1 = new WebClient();
                                        byte[] bytes = wc1.DownloadData(Common.CommonSetting1.User_Item_Images_Url);
                                        string[] fileex = (Common.CommonSetting1.User_Item_Images_Url).Split('.');
                                        string fileext = (Common.CommonSetting1.User_Item_Images_Url).Split('.')[3];
                                        HttpPostedFileBase file = (HttpPostedFileBase)new CustomPostedFile(bytes, (DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString());
                                        string fname;
                                        fname = file.FileName;
                                        fname = (Server.MapPath("~/uploaded_image/Items/") + DateTime.Now.ToString("yyyyMMddmmss") + "." + fileext).Replace("\r\n", "").ToString();
                                        file.SaveAs(fname);
                                        string[] splitpath = fname.Split('\\');
                                        string name = (splitpath[splitpath.Length - 1]);

                                        if (i == 0)
                                        {
                                            Path = "/uploaded_image/Items/" + name;
                                        }
                                        else
                                        {
                                            Path = Path + "," + "/uploaded_image/Items/" + name;
                                        }
                                        Thread.Sleep(1000);
                                    }
                                }
                                objitemscrap.Add(new Item
                                {
                                    Image_Path = Path
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    { }
                }
            }
            catch (Exception ex) { }
            return Json(objitemscrap, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Generate(QRCodeModel qrcode)
        {
            try
            {
                qrcode.QRCodeImagePath = GenerateQRCode(qrcode.QRCodeText);
                ViewBag.Message = "QR Code Created successfully";
            }
            catch (Exception ex)
            {
                //catch exception if there is any
            }
            return Json(qrcode);
            // return View("Index", qrcode);
        }

        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "~/uploaded_image/item_qrcode/";
            string imagePath = "~/uploaded_image/item_qrcode/" + qrcodeText + ".jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            string path = "/uploaded_image/item_qrcode/" + qrcodeText + ".jpg";
            return path;
        }
        [HttpGet]
        public JsonResult PendingItemDetailsForListing()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForListPending();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),
                        Internal_Item_Code = dr["internalItemCode"].ToString(),
                        Item_Request_Description = dr["requestDescription"].ToString(),
                        Item_Manufacturing_Part_Number = dr["itemManufacturingPartNumber"].ToString(),
                        Request_Id = Convert.ToInt32(dr["requestId"]),
                        Approve_Status = Convert.ToString(dr["isApproved"])
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult RejectedItemDetailsForListing()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForListRejected();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),
                        Internal_Item_Code = dr["internalItemCode"].ToString(),
                        Item_Request_Description = dr["requestDescription"].ToString(),
                        Item_Manufacturing_Part_Number = dr["itemManufacturingPartNumber"].ToString(),
                        Request_Id = Convert.ToInt32(dr["requestId"]),
                        Approve_Status = Convert.ToString(dr["isApproved"])
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ApprovedItemDetailsForListing()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForListApproved();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),
                        Internal_Item_Code = dr["internalItemCode"].ToString(),
                        Item_Request_Description = dr["requestDescription"].ToString(),
                        Item_Manufacturing_Part_Number = dr["itemManufacturingPartNumber"].ToString(),
                        Request_Id = Convert.ToInt32(dr["requestId"]),
                        Approve_Status = Convert.ToString(dr["isApproved"])
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetItemCodeSearch(string prefix="")
        {

            SearchItem searchitem = new SearchItem();
            List<SearchItem> searchitemList = new List<SearchItem>();
            try
            { 
            DataSet ds = searchitem.GetSearchItemCodeAccKeywords(prefix);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    searchitemList.Add(new SearchItem
                    {

                        Search_Name = dr["SearchItem"].ToString(),

                        // Item_Id = dr["itemId"].ToString()

                    });

                }
            }
            catch (Exception ex) { }
            return Json(searchitemList, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ItemDetailsForRefrance(Item objItem)
        {
            Item item = new Item();
            try
            {
                DataSet ds = item.GetItemDetailForRefrance(objItem);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    item.Item_Description = ds.Tables[0].Rows[0]["itemDescription"].ToString();
                    item.Item_Manufacture = ds.Tables[0].Rows[0]["itemManufacture"].ToString();
                    item.Item_Brand = Convert.ToString(ds.Tables[0].Rows[0]["itemBrand"]);
                    item.Item_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemTypeId"]);
                    item.Item_Manufacturing_Part_Number = Convert.ToString(ds.Tables[0].Rows[0]["itemManufacturingPartNumber"]);
                    item.Item_Sub_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemSubTypeId"]);
                    item.Sub_Item_Type_Field_Options = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionType"]);
                    item.Sub_Item_Type_Field_values = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionValues"]);
                    item.Sub_Item_Type_Field_UOM = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldUOMName"]);
                    item.Internal_Item_Code = Convert.ToString(ds.Tables[0].Rows[0]["internalItemCode"]);
                }
            }
            catch (Exception ex) { }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SubItemTypeIdForRefrance(Item objItem)
        {
            Item item = new Item();
            try
            {
                DataSet ds = item.GetSubItemTypeIdForRefrance(objItem);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {


                    item.Item_Sub_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemSubTypeId"]);

                }
            }
            catch (Exception ex) { }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ItemDetailsForRefranceAccItemId(Item objItem)
        {
            Item item = new Item();
            try
            {
                DataSet ds = item.GetItemDetailForRefranceAccItemId(objItem);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    item.Item_Description = ds.Tables[0].Rows[0]["itemDescription"].ToString();
                    item.Item_Manufacture = ds.Tables[0].Rows[0]["itemManufacture"].ToString();
                    item.Item_Brand = Convert.ToString(ds.Tables[0].Rows[0]["itemBrand"]);
                    item.Item_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemTypeId"]);
                    item.Item_Manufacturing_Part_Number = Convert.ToString(ds.Tables[0].Rows[0]["itemManufacturingPartNumber"]);
                    item.Item_Sub_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemSubTypeId"]);
                    item.Sub_Item_Type_Field_Options = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionType"]);
                    item.Sub_Item_Type_Field_values = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionValues"]);
                    item.Sub_Item_Type_Field_UOM = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldUOMName"]);
                    item.Internal_Item_Code = Convert.ToString(ds.Tables[0].Rows[0]["internalItemCode"]);
                }
            }
            catch (Exception ex) { }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region//---------------------search functionality -------------------------
        public ActionResult SearchItem()
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
        public JsonResult GetRecord(string prefix="")
        {

            SearchItem searchitem = new SearchItem();
            List<SearchItem> searchitemList = new List<SearchItem>();
            List<string> result = new List<string>();
            try
            {
                DataSet ds = searchitem.GetSearchItemAccKeywords(prefix);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // result.Add(string.Format("{0}/{1}", dr["SearchItem"], dr["SearchItem"]));
                    searchitemList.Add(new SearchItem
                    {

                        Search_Name = dr["SearchItem"].ToString(),

                        // Item_Id = dr["itemId"].ToString()

                    });

                }
            }
            catch (Exception ex) { }
            return Json(searchitemList, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetRecord1(SearchItem ObjSearchItem)
        {

            SearchItem searchitem = new SearchItem();
            List<SearchItem> searchitemList = new List<SearchItem>();
            List<string> result = new List<string>();
            try
            {
                DataSet ds = searchitem.GetSearchItemAccKeywords1(ObjSearchItem);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // result.Add(string.Format("{0}/{1}", dr["SearchItem"], dr["SearchItem"]));
                    searchitemList.Add(new SearchItem
                    {

                        Search_Name = dr["SearchItem"].ToString(),

                        // Item_Id = dr["itemId"].ToString()

                    });

                }
            }
            catch (Exception ex) { }
            return Json(searchitemList, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ItemDetailsForSarch(Item objItem)
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForSearchList(objItem);

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item item = new Item();
                    item.Item_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["itemId"]);
                    item.Item_HS_Code = ds.Tables[0].Rows[i]["itemHSCode"].ToString();
                    item.Item_Description = ds.Tables[0].Rows[i]["itemDescription"].ToString();
                    item.Internal_Item_Code = ds.Tables[0].Rows[i]["internalItemCode"].ToString();
                    item.Item_Manufacturing_Part_Number = ds.Tables[0].Rows[i]["itemManufacturingPartNumber"].ToString();
                    item.item_CAD_File_Url = ds.Tables[0].Rows[i]["itemCADFileUrl"].ToString();
                    try
                    {
                        item.Item_Quantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["itemQuantity"].ToString());
                    }
                    catch (Exception ex)
                    {
                        item.Item_Quantity = 0;
                    }
                    string[] s = ds.Tables[0].Rows[i]["imageurl"].ToString().Split(',');
                    if (s.Length > 0)
                    {
                        item.Item_Images_Url = s[0].ToString();
                    }
                    else
                    {
                        item.Item_Images_Url = "";
                    }
                    objitemlist.Add(item);
                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemDetailsForSearchAccSearchValue(Item objItem)
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForSearchListAccSearchValue(objItem);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item item = new Item();
                    item.Item_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["itemId"]);
                    item.Item_HS_Code = ds.Tables[0].Rows[i]["itemHSCode"].ToString();
                    item.Item_Description = ds.Tables[0].Rows[i]["itemDescription"].ToString();
                    item.item_CAD_File_Url = ds.Tables[0].Rows[i]["itemCADFileUrl"].ToString();
                    item.Internal_Item_Code = ds.Tables[0].Rows[i]["internalItemCode"].ToString();
                    item.Item_Manufacturing_Part_Number = ds.Tables[0].Rows[i]["itemManufacturingPartNumber"].ToString();
                    item.Item_Quantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["itemQuantity"].ToString());
                    string[] s = ds.Tables[0].Rows[i]["imageurl"].ToString().Split(',');
                    if (s.Length > 0)
                    {
                        item.Item_Images_Url = s[0].ToString();
                    }
                    else
                    {
                        item.Item_Images_Url = "";
                    }
                    objitemlist.Add(item);
                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ViewDocItem(Item objsearchitemid)
        {
            Item itmemlist = new Item();
            List<Item> objitem = new List<Item>();
            try
            {
                DataSet ds = itmemlist.GetItemDoc(objsearchitemid);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitem.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"].ToString()),
                        Item_Certification_Url = dr["itemCertificationUrl"].ToString(),
                        Item_Compliance_Url = dr["itemComplianceUrl"].ToString(),
                        Item_Data_Sheet_Url = dr["itemDataSheetUrl"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objitem, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ItemNameWithIdDetail(Item objsearchitemid)
        {
            Item itmemlist = new Item();
            List<Item> objitem = new List<Item>();
            try
            {
                DataSet ds = itmemlist.GetItemWithID(objsearchitemid);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitem.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"].ToString()),
                        Internal_Item_Code = dr["intpartnumber"].ToString(),
                        Item_Type_Name = dr["itemcategory"].ToString(),
                        Item_Part_Number = dr["partnumber"].ToString(),
                        Item_Manufacturing_Part_Number = dr["manufacturingnumber"].ToString(),
                        Item_Description = dr["partname"].ToString(),
                        Item_Brand = dr["brand"].ToString(),
                        Item_Manufacture = dr["manufacturer"].ToString(),
                        Supplier_Name = dr["suppliername"].ToString(),
                        Item_URL_Link = dr["informationlink"].ToString(),
                        Item_CoCountry_of_Origin = dr["location"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objitem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemDetailsAccSarchValue(Item objItem)
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemDetailAccSearchValue(objItem);

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item item = new Item();
                    item.Item_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["itemId"]);
                    item.Item_HS_Code = ds.Tables[0].Rows[i]["itemHSCode"].ToString();
                    item.Item_Description = ds.Tables[0].Rows[i]["itemDescription"].ToString();
                    item.Internal_Item_Code = ds.Tables[0].Rows[i]["internalItemCode"].ToString();
                    item.Item_Manufacturing_Part_Number = ds.Tables[0].Rows[i]["itemManufacturingPartNumber"].ToString();
                    item.item_CAD_File_Url = ds.Tables[0].Rows[i]["itemCADFileUrl"].ToString();
                    try
                    {
                        item.Item_Quantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["itemQuantity"].ToString());
                    }
                    catch (Exception ex)
                    {
                        item.Item_Quantity = 0;
                    }
                    string[] s = ds.Tables[0].Rows[i]["imageurl"].ToString().Split(',');
                    if (s.Length > 0)
                    {
                        item.Item_Images_Url = s[0].ToString();
                    }
                    else
                    {
                        item.Item_Images_Url = "";
                    }
                    objitemlist.Add(item);
                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //public ActionResult Image1()
        //{
        //    return View();
        //}

        #region//------------------------listing Item Master-----------------------------
        public ActionResult ListingItem()
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

        [HttpGet]
        public JsonResult ItemDetailsForListing()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemForList(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),
                        Item_Type_Id = Convert.ToInt32(dr["itemTypeId"]),
                        Item_Sub_Type_Id = Convert.ToInt32(dr["itemSubTypeId"]),
                        Internal_Item_Code = dr["internalItemCode"].ToString(),
                        Item_Description = dr["itemDescription"].ToString(),
                        Sort_Item_Desctiption = dr["sortitemDescription"].ToString(),
                        Item_Manufacturing_Part_Number = dr["itemManufacturingPartNumber"].ToString(),
                        Item_Type_Name = dr["itemType"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListingItemDetails(Item objitemlistview)
        {
            Item item = new Item();
            try
            {
                DataSet ds = item.GetItemForListForPopup(objitemlistview);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item.Item_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemId"]);
                    item.Item_Name = ds.Tables[0].Rows[0]["itemName"].ToString();
                    item.Internal_Item_Code = ds.Tables[0].Rows[0]["internalItemCode"].ToString();
                    item.UOM_Name = Convert.ToString(ds.Tables[0].Rows[0]["UOMName"]);
                    item.Item_Sub_Type_Name = Convert.ToString(ds.Tables[0].Rows[0]["subitemName"]);
                    item.Item_Type_Name = Convert.ToString(ds.Tables[0].Rows[0]["itemType"]);
                    item.Item_Description = ds.Tables[0].Rows[0]["itemDescription"].ToString();
                    try
                    {
                        item.Item_Quantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemQuantity"]);
                    }
                    catch (Exception ex)
                    {
                        item.Item_Quantity = 0;
                    }
                    item.Item_QRCode_path = Convert.ToString(ds.Tables[0].Rows[0]["itemQRCodeImagePath"]);
                    item.Item_Manufacturing_Part_Number = Convert.ToString(ds.Tables[0].Rows[0]["itemManufacturingPartNumber"]);
                    item.Bar_Code_No = Convert.ToString(ds.Tables[0].Rows[0]["itemBarCodeNo"]);
                    item.Rack_Name = Convert.ToString(ds.Tables[0].Rows[0]["rackName"]);
                    item.Rack_Number_Name = Convert.ToString(ds.Tables[0].Rows[0]["racknumberName"]);
                    item.Item_Manufacture = ds.Tables[0].Rows[0]["itemManufacture"].ToString();
                    item.Item_Brand = Convert.ToString(ds.Tables[0].Rows[0]["itemBrand"]);
                    
                    item.Supplier_Name = Convert.ToString(ds.Tables[0].Rows[0]["supplierName"]);
                    item.Item_Price = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemPrice"]);
                    item.Item_Min_Order_Qty = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemMinOrderQty"]);
                    item.Item_Threshold = Convert.ToString(ds.Tables[0].Rows[0]["itemThreshold"]);
                    item.Item_HS_Code = Convert.ToString(ds.Tables[0].Rows[0]["itemHSCode"]);
                    item.Item_URL_Link = Convert.ToString(ds.Tables[0].Rows[0]["itemURLLink"]);
                    item.Item_CoCountry_of_Origin = Convert.ToString(ds.Tables[0].Rows[0]["itemCoCountryofOrigin"]);
                   
                    item.Item_Shipping_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemShippingCost"]);
                    item.Item_Contact_Details = Convert.ToString(ds.Tables[0].Rows[0]["itemContactDetails"]);
                    item.Item_EMail = Convert.ToString(ds.Tables[0].Rows[0]["itemEMail"]);
                    item.Item_We_Chat = Convert.ToString(ds.Tables[0].Rows[0]["itemWeChat"]);
                    item.Item_WhatsApp = Convert.ToString(ds.Tables[0].Rows[0]["itemWhatsApp"]);
                    item.item_Other_Contact_Type = Convert.ToString(ds.Tables[0].Rows[0]["itemOtherContactType"]);
                    item.Item_Data_Sheet_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemDataSheetUrl"]);
                    item.Item_Compliance_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemComplianceUrl"]);
                    item.item_CAD_File_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemCADFileUrl"]);
                    item.Item_Certification_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemCertificationUrl"]);
                    item.Item_Scrapping_from_WebUrl = Convert.ToString(ds.Tables[0].Rows[0]["itemScrappingfromWebUrl"]);
                  
                    item.UOM_Group_Name = Convert.ToString(ds.Tables[0].Rows[0]["uomGroupName"]);
                    item.Valuation_Name = Convert.ToString(ds.Tables[0].Rows[0]["valuationName"]);
                    item.Manage_Item_Name = Convert.ToString(ds.Tables[0].Rows[0]["manageitemName"]);
                   
                    //item.Material_Category_Name = Convert.ToString(ds.Tables[0].Rows[0]["materialCategoryName"]);
                    item.item_Status = Convert.ToString(ds.Tables[0].Rows[0]["itemStatus"]);
                    item.Item_Images_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemImagesUrl"]);
                    item.Sub_Item_Type_Field_values = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionValues"]);
                   
                    item.Sub_Item_Type_Field_UOM = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldUOMName"]);

                    item.Alt_UOM_Group_Name = Convert.ToString(ds.Tables[0].Rows[0]["altuomGroupName"]);
                    item.Alt_UOM_Name = Convert.ToString(ds.Tables[0].Rows[0]["altUOMName"]);
                    item.HS_Category_Name = Convert.ToString(ds.Tables[0].Rows[0]["hsCategoryName"]);
                    item.Alt_Manufacturing_Part_Number = Convert.ToString(ds.Tables[0].Rows[0]["altManufacturingPartNumber"]);
                    item.Alt_Item_Brand = Convert.ToString(ds.Tables[0].Rows[0]["altBrand"]);
                    item.Alt_Item_Url = Convert.ToString(ds.Tables[0].Rows[0]["altUrl"]);
                   
                   
                    item.Store_Location_Name = Convert.ToString(ds.Tables[0].Rows[0]["storelocationName"]);
                    item.Store_Location_Code = Convert.ToString(ds.Tables[0].Rows[0]["storelocationCode"]);
                    item.Designer_URL = Convert.ToString(ds.Tables[0].Rows[0]["designerURL"]);
                    item.Cell_Number = Convert.ToString(ds.Tables[0].Rows[0]["CellNo"]);
                }
            }
            catch (Exception ex) { }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListingItemDetailsExport(Item objitemlistview)
        {
            string sms = "";
            string path = "";
            Item item = new Item();
            try
            {
                DataSet ds = item.GetItemForListForExport(objitemlistview);
                StringWriter sw = new StringWriter();
                ds.WriteXml(Server.MapPath("/uploaded_image/xmlfiles/" + ds.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml"));
                string s = sw.ToString();
                if (ds != null)
                {


                    path = "/uploaded_image/xmlfiles/" + ds.Tables[0].Rows[0]["internalItemCode"].ToString() + ".xml";
                    // WebClient wc = new WebClient();
                    //wc.DownloadFile("http://103.133.214.97:84" + path,"");
                    //var FileVirtualPath = path;
                    //File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
                    //using (System.Net.WebClient client = new System.Net.WebClient())
                    //{
                    //    client.DownloadFile("http://103.133.214.97:84/uploaded_image/xmlfiles/Fas0000004.xml", "some.xml");
                    //}
                    sms = "**Data Exported successfully**+" + path;
                }
                else
                {
                    sms = "**Data Not Exported successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion
        #region//------------------------Edit Item Master-----------------------------
        public ActionResult EditItem()
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
        public ActionResult UpdateItem()
        {
            if (Common.CommonSetting.User_Id == 0 || Common.CommonSetting.User_Id == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            else
            {
                return View();
            }
        }
        public ActionResult RequestForEdit()
        {
            if (Common.CommonSetting.User_Id == 0 || Common.CommonSetting.User_Id == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult ItemDetailsForUpdate(Item objItem)
        {
            Item item = new Item();
            try
            {
                DataSet ds = item.GetItemDetailForUpdate(objItem);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item.Item_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemId"]);
                    item.Item_Name = dr["itemName"].ToString();
                    item.Item_Description = ds.Tables[0].Rows[0]["itemDescription"].ToString();
                    item.Item_Manufacture = ds.Tables[0].Rows[0]["itemManufacture"].ToString();
                    item.UOM_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["uomId"]);
                    item.Item_Sub_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemSubTypeId"]);
                    item.Item_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemTypeId"]);
                    item.Rack_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["rackId"]);
                    item.Internal_Item_Code = Convert.ToString(ds.Tables[0].Rows[0]["internalItemCode"]);
                    item.item_Status = Convert.ToString(ds.Tables[0].Rows[0]["itemStatus"]);
                    item.Rack_Number_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["racknumberId"]);
                    item.Bar_Code_No = Convert.ToString(ds.Tables[0].Rows[0]["itemBarCodeNo"]);
                    item.Item_QRCode_path = Convert.ToString(ds.Tables[0].Rows[0]["itemQRCodeImagePath"]);
                    try
                    {
                        item.Item_Quantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemQuantity"]);
                    }
                    catch (Exception ex)
                    {
                        item.Item_Quantity = 0;
                    }
                    item.Item_Brand = Convert.ToString(ds.Tables[0].Rows[0]["itemBrand"]);
                    item.Supplier_Name = Convert.ToString(ds.Tables[0].Rows[0]["supplierName"]);
                    item.Item_Price = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemPrice"]);
                    item.Item_Min_Order_Qty = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemMinOrderQty"]);
                    item.Item_Threshold = Convert.ToString(ds.Tables[0].Rows[0]["itemThreshold"]);
                    item.Item_HS_Code = Convert.ToString(ds.Tables[0].Rows[0]["itemHSCode"]);
                    item.Item_URL_Link = Convert.ToString(ds.Tables[0].Rows[0]["itemURLLink"]);
                    item.Item_CoCountry_of_Origin = Convert.ToString(ds.Tables[0].Rows[0]["itemCoCountryofOrigin"]);
                    item.Item_Manufacturing_Part_Number = Convert.ToString(ds.Tables[0].Rows[0]["itemManufacturingPartNumber"]);
                    item.Item_Shipping_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemShippingCost"]);
                    item.Item_Contact_Details = Convert.ToString(ds.Tables[0].Rows[0]["itemContactDetails"]);
                    item.Item_EMail = Convert.ToString(ds.Tables[0].Rows[0]["itemEMail"]);
                    item.Item_We_Chat = Convert.ToString(ds.Tables[0].Rows[0]["itemWeChat"]);
                    item.Item_WhatsApp = Convert.ToString(ds.Tables[0].Rows[0]["itemWhatsApp"]);
                    item.item_Other_Contact_Type = Convert.ToString(ds.Tables[0].Rows[0]["itemOtherContactType"]);
                    item.Item_Data_Sheet_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemDataSheetUrl"]);
                    item.Item_Compliance_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemComplianceUrl"]);
                    item.item_CAD_File_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemCADFileUrl"]);
                    item.Item_Certification_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemCertificationUrl"]);
                    item.Item_Scrapping_from_WebUrl = Convert.ToString(ds.Tables[0].Rows[0]["itemScrappingfromWebUrl"]);
                   
                    item.UOM_Group_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["uomGroupId"]);
                    item.Valuation_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["valuationId"]);
                    item.Item_Manage_Code_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemManageCodeId"]);
                   
                    item.Material_Category_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["materialCategoryId"]);
                  
                    item.Sub_Item_Type_Field_Options = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionType"]);
                    item.Sub_Item_Type_Field_values = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldOptionValues"]);
                    item.Item_Images_Url = Convert.ToString(ds.Tables[0].Rows[0]["itemImagesUrl"]);
                   
                    item.Currency_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["currencyId"]);
                  
                    item.Sub_Item_Type_Field_UOM = Convert.ToString(ds.Tables[0].Rows[0]["subitemtypefieldUOMName"]);
                    item.Alt_UOM_Group_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["altUOMGroupId"]);
                    item.Alt_UOM_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["altUOMId"]);
                    item.HS_Category_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["hscategoryId"]);
                   
                    item.Store_Location_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["storelocationId"]);
                    item.Store_Location_Code = Convert.ToString(ds.Tables[0].Rows[0]["storelocationCode"]);
                    item.Designer_URL = Convert.ToString(ds.Tables[0].Rows[0]["designerURL"]);
                   
                }
            }
            catch (Exception ex) { }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ItemAltDetailsForUpdate(Item objItem)
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemAltDetailForUpdate(objItem);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Alternate_Id = Convert.ToInt32(dr["alternateId"]),
                        Alt_Manufacturing_Part_Number = dr["altManufacturingPartNumber"].ToString(),
                        Alt_Item_Brand = dr["altBrand"].ToString(),

                        Alt_Item_Url = dr["altUrl"].ToString()

                    });

                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ItemApproveStatusChange(Item objItem)
        {
            string sms = "";
            Item item = new Item();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItem.User_Id = Convert.ToInt32(cookie.Value);
                int i = item.GetItemApproveStatus(objItem);
                if (i == 0)
                {
                    sms = "**Data status changed successfully.**";
                }
                else
                {
                    sms = "**Data status not changed.**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult SubItemFieldValueForCheckDuplicateUpdate(Item objItem)
        {
            Item itemppara = new Item();
            Item objitem = new Item();
            itemppara.Item_Type_Id = 0;
            if (objItem.Sub_Item_Type_Field_values == "") {
                objitem.Count =0;
            }
            else
            {
                try
                {
                    DataSet ds = itemppara.GetSubItemFieldValueForCheckDuplcateUpdate(objItem);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objitem.Count = Convert.ToInt32(dr["count1"]);
                    }
                }
                catch (Exception ex) { }
            }
            return Json(objitem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddItemRequest(Item objItem)
        {
            string sms = "";
            try
            {
                Item item = new Item();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItem.User_Id = Convert.ToInt32(cookie.Value);
                //if (objItem.Item_Id == 0)
                //{
                int i = item.InsertItemRequest(objItem);
                if (i == 0)
                {
                    sms = "**Data inserted successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
                // }
                //else
                //{
                //    int i = item.UpdateItem(objItem);
                //    if (i == 0)
                //    {
                //        sms = "**Data updated successfully**";
                //    }
                //    else
                //    {
                //        sms = "**Data already exist**";
                //    }
                //}
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult RequestForEditList()
        {
            Item itemlist = new Item();
            List<Item> objitemlist = new List<Item>();
            try
            {
                DataSet ds = itemlist.GetItemRequestForList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),
                        Internal_Item_Code = dr["internalItemCode"].ToString(),
                        Item_Request_Description = dr["requestDescription"].ToString(),
                        Item_Manufacturing_Part_Number = dr["itemManufacturingPartNumber"].ToString(),
                        Request_Id = Convert.ToInt32(dr["requestId"]),
                        Item_Sub_Type_Id = Convert.ToInt32(dr["itemSubTypeId"]),
                        Approve_Status = Convert.ToString(dr["isApproved"])
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objitemlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RequestApproveStatusChange(Item objItem)
        {
            string sms = "";
            Item item = new Item();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItem.User_Id = Convert.ToInt32(cookie.Value);
                int i = item.GetRequestApproveStatus(objItem);
                if (i == 0)
                {
                    sms = "**Data status changed successfully.**";
                    string Link = "https://testing2.leaderrange.co/Master/ItemMaster";
                    MailConst.senderMail("Item", Link);
                }
                else
                {
                    sms = "**Data status not changed.**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult ItemDetailForCompare(Item objItem)
        {
            Item item = new Item();
            List<Item> objItemlist = new List<Item>();
            try
            {
                DataSet ds = item.GetItemForListForCompare(objItem);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objItemlist.Add(new Item
                    {
                        Item_Id = Convert.ToInt32(dr["itemId"]),

                        Item_Description = dr["itemDescription"].ToString(),
                        Item_Manufacture = dr["itemManufacture"].ToString(),
                        Item_Brand = dr["itemBrand"].ToString(),
                        Supplier_Name = dr["supplierName"].ToString(),
                        Item_Price = Convert.ToDecimal(dr["itemPrice"]),
                        Item_Min_Order_Qty = Convert.ToDecimal(dr["itemMinOrderQty"]),
                        Item_Threshold = Convert.ToString(dr["itemThreshold"]),
                        Item_HS_Code = Convert.ToString(dr["itemHSCode"]),
                        Item_URL_Link = Convert.ToString(dr["itemURLLink"]),
                        Item_CoCountry_of_Origin = Convert.ToString(dr["itemCoCountryofOrigin"]),
                        Item_Manufacturing_Part_Number = Convert.ToString(dr["itemManufacturingPartNumber"]),
                        Item_Shipping_Cost = Convert.ToDecimal(dr["itemShippingCost"]),
                        Item_Contact_Details = Convert.ToString(dr["itemContactDetails"]),
                        Item_EMail = Convert.ToString(dr["itemEMail"]),
                        Item_We_Chat = Convert.ToString(dr["itemWeChat"]),
                        Item_WhatsApp = Convert.ToString(dr["itemWhatsApp"]),
                        item_Other_Contact_Type = Convert.ToString(dr["itemOtherContactType"]),
                        Item_Data_Sheet_Url = Convert.ToString(dr["itemDataSheetUrl"]),
                        Item_Compliance_Url = Convert.ToString(dr["itemComplianceUrl"]),
                        item_CAD_File_Url = Convert.ToString(dr["itemCADFileUrl"]),
                        Item_Certification_Url = Convert.ToString(dr["itemCertificationUrl"]),
                        Item_Scrapping_from_WebUrl = Convert.ToString(dr["itemScrappingfromWebUrl"]),
                        UOM_Name = Convert.ToString(dr["UOMName"]),
                        UOM_Group_Name = Convert.ToString(dr["uomGroupName"]),
                        Valuation_Name = Convert.ToString(dr["valuationName"]),
                        Manage_Item_Name = Convert.ToString(dr["manageitemName"]),
                        Item_Sub_Type_Name = Convert.ToString(dr["subitemName"]),
                        Item_Type_Name = Convert.ToString(dr["itemType"]),
                        Sub_Item_Type_Field_UOM = Convert.ToString(dr["subitemtypefieldUOMName"]),
                        Sub_Item_Type_Field_values = Convert.ToString(dr["subitemtypefieldOptionValues"]),
                        Item_Images_Url = Convert.ToString(dr["itemImagesUrl"]),
                        Currency_Name = Convert.ToString(dr["currencyName"]),
                        Internal_Item_Code = Convert.ToString(dr["internalItemCode"]),
                        Alt_UOM_Name = Convert.ToString(dr["altUOMName"]),
                        Alt_UOM_Group_Name = Convert.ToString(dr["altuomGroupName"]),
                        HS_Category_Name = Convert.ToString(dr["hscategoryName"]),
                        Rack_Name = Convert.ToString(dr["rackName"]),
                        Rack_Number_Name = Convert.ToString(dr["racknumberName"]),
                        Store_Location_Name = Convert.ToString(dr["storelocationName"]),
                        Store_Location_Code = Convert.ToString(dr["storelocationCode"]),
                        Item_Quantity = Convert.ToInt32(dr["itemQuantity"]),
                        Cell_Number = Convert.ToString(dr["cellNumber"]),
                        //Store_Location_Code = Convert.ToString(dr["itemDataSheetUrl"]),
                        //Store_Location_Code = Convert.ToString(dr["itemCADFileUrl"]),
                        //Store_Location_Code = Convert.ToString(dr["itemScrappingfromWebUrl"]),
                    });
                }
            }
            catch (Exception ex) { }
            return Json(objItemlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RejectUpdatedItem(Item objItem)
        {
            string sms = "";
            Item item = new Item();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItem.User_Id = Convert.ToInt32(cookie.Value);

                int i = item.RejectUpdatedItem(objItem);
                if (i == 0)
                {
                    sms = "**Reject updated data successfully**";
                }
                else
                {
                    sms = "*Not reject updated data successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion


        #region //--------------- Cell Master -----------------------
        public ActionResult CellMaster()
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
        public JsonResult AddCell(CellNumber objCellNumber)
        {
            string sms = "";
            try
            {
                CellNumber cell = new CellNumber();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCellNumber.User_Id = Convert.ToInt32(cookie.Value);
                if (objCellNumber.Cell_Id == 0)
                {
                    int i = cell.InsertCellNumber(objCellNumber);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = cell.UpdateCellNumber(objCellNumber);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkCellNumber(CellNumber objCellNumber)
        {
            string sms = "";
            try
            {
                CellNumber cell = new CellNumber();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCellNumber.User_Id = Convert.ToInt32(cookie.Value);
                string[] cellnumber = objCellNumber.Cell_Number.Split(',');

                for (int i = 0; i < cellnumber.Length; i++)
                {
                    CellNumber cell1 = new CellNumber();
                    cell1.Cell_Number = cellnumber[i].ToString();

                    int j = cell.InsertCellNumber(cell1);
                }
                sms = "**Language bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult DeleteCellNumber(CellNumber objCellNumber)
        {
            string sms = "";
            try
            {
                CellNumber cell = new CellNumber();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCellNumber.User_Id = Convert.ToInt32(cookie.Value);

                int i = cell.DeleteCellNumber(objCellNumber);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CellNumberDetails()
        {
            CellNumber cell = new CellNumber();
            List<CellNumber> objCellNumber = new List<CellNumber>();
            try
            {
                DataSet ds = cell.GetCellNumber(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objCellNumber.Add(new CellNumber
                    {
                        Cell_Id = Convert.ToInt32(dr["cellId"]),
                        Cell_Number = dr["cellNo"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objCellNumber, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CellNumberApproveStatus(CellNumber objCellNumber)
        {
            string sms = "";
            try
            {
                CellNumber cell = new CellNumber();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCellNumber.User_Id = Convert.ToInt32(cookie.Value);

                int i = cell.GetCellNumberApproveStatus(objCellNumber);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CellMaster";
                    MailConst.senderMail("Cell", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult CellNumberName()
        {
            List<SelectListItem> CellNumberlist = new List<SelectListItem>();
            CellNumber b = new CellNumber();
            try
            {
                DataSet ds = b.GetCellNoName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CellNumberlist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["cellId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["cellNo"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(CellNumberlist);
        }
        #endregion

        #region//--------------------------QRCode----------------------------------------------------
        public ActionResult ScanQRCode()
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
        public JsonResult ReadQRCode(Item ObjItem)
        {
            Item item = new Item();
            //string barcodeText = "";
            //string imagePath = ObjItem.Item_QRCode_path;
            //string barcodePath = Server.MapPath(imagePath);
            //var barcodeReader = new BarcodeReader();

            //var result = barcodeReader.Decode(new Bitmap(barcodePath));
            //if (result != null)
            //{
            //    barcodeText = result.Text;
            //}
            Item itemlist = new Item();
            try
            {
                item.Bar_Code_No = ObjItem.Bar_Code_No;
                DataSet ds = item.GetItemLocationDetailAccQRCode(item);
               
                if (ds.Tables[0].Rows.Count > 0)
                {
                    itemlist.Item_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["itemId"]);
                    itemlist.Rack_Name = ds.Tables[0].Rows[0]["rackName"].ToString();
                    itemlist.Rack_Number_Name = ds.Tables[0].Rows[0]["racknumberName"].ToString();
                    itemlist.Internal_Item_Code = ds.Tables[0].Rows[0]["internalItemCode"].ToString();
                    itemlist.Store_Location_Name = ds.Tables[0].Rows[0]["storelocationName"].ToString();
                    itemlist.Store_Location_Code = ds.Tables[0].Rows[0]["storelocationCode"].ToString();
                    itemlist.Cell_Number = ds.Tables[0].Rows[0]["cellNumber"].ToString();
                    try
                    {
                        itemlist.Item_Quantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["itemQuantity"]);
                    }
                    catch (Exception ex)
                    {
                        itemlist.Item_Quantity = 0;
                    }
                }
            }
            catch (Exception ex) { }
            return Json(itemlist, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #endregion
        #region//-------------------General Master-------------------

        #region//--------------------currency Master-------------------------
        public ActionResult CurrencyMaster()
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
        public JsonResult AddCurrency(Currency objcurrency)
        {
            string sms = "";
            try
            {
                Currency currency = new Currency();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcurrency.User_Id = Convert.ToInt32(cookie.Value);
                if (objcurrency.Currency_Id == 0)
                {
                    int i = currency.InsertCurrency(objcurrency);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = currency.UpdateCurrency(objcurrency);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkCurrency(Currency objcurrency)
        {
            string sms = "";
            try
            {
                Currency currency = new Currency();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcurrency.User_Id = Convert.ToInt32(cookie.Value);
                string[] countrycode = objcurrency.Currency_Code.Split(',');
                string[] countryName = objcurrency.Currency_Name.Split(',');
                string[] Iternationalcode = objcurrency.International_Code.Split(',');
                string[] IternationalName = objcurrency.International_Name.Split(',');
                string[] decimalunit = objcurrency.Decimal_Unit.Split(',');
                string[] symbol = objcurrency.Currency_Symbol.Split(',');
                for (int i = 0; i < countrycode.Length; i++)
                {
                    Currency currency1 = new Currency();
                    currency1.Currency_Code = countrycode[i].ToString();
                    currency1.Currency_Name = countryName[i].ToString();
                    currency1.International_Code = Iternationalcode[i].ToString();
                    currency1.International_Name = IternationalName[i].ToString();
                    currency1.Decimal_Unit = decimalunit[i].ToString();
                    currency1.Currency_Symbol = symbol[i].ToString();
                    int j = currency.InsertCurrency(currency1);
                }
                sms = "**Currency bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteCurrency(Currency objcurrency)
        {
            string sms = "";
            try
            {
                Currency currency = new Currency();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcurrency.User_Id = Convert.ToInt32(cookie.Value);

                int i = currency.DeleteCurrency(objcurrency);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CurrencyDetails()
        {
            Currency contury = new Currency();
            List<Currency> Objcurrency = new List<Currency>();
            try
            {
                DataSet ds = contury.GetCurrency(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objcurrency.Add(new Currency
                    {
                        Currency_Id = Convert.ToInt32(dr["currencyId"]),
                        Currency_Code = dr["currencyCode"].ToString(),
                        Currency_Name = dr["currencyName"].ToString(),
                        International_Code = dr["interNationalCode"].ToString(),
                        International_Name = dr["interNationalName"].ToString(),
                        Decimal_Unit = dr["decimalUnit"].ToString(),
                        Currency_Symbol = dr["currencySymbol"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objcurrency, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CurrencyApproveStatus(Currency objCurrency)
        {
            string sms = "";
            try
            {
                Currency currency = new Currency();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCurrency.User_Id = Convert.ToInt32(cookie.Value);

                int i = currency.GetCurrencyApproveStatus(objCurrency);
                if (i == 0)
                {
                    // senderMail();
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CurrencyMaster";
                    MailConst.senderMail("Currency", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
       
        [HttpPost]
        public JsonResult CurrencyName()
        {
            List<SelectListItem> Currencylist = new List<SelectListItem>();
            Currency b = new Currency();
            try
            {
                DataSet ds = b.GetCurencyName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Currencylist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["currencyId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["currencyName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Currencylist);
        }
        #endregion

        #region//--------------------country Master-------------------------
        public ActionResult CountryMaster()
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
        public JsonResult AddCountry(Country objcountry)
        {
            string sms = "";
            try
            {
                Country country = new Country();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcountry.User_Id = Convert.ToInt32(cookie.Value);
                if (objcountry.Country_Id == 0)
                {
                    int i = country.InsertCountry(objcountry);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {

                    int i = country.UpdateCountry(objcountry);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkCountry(Country objcountry)
        {
            string sms = "";
            try
            {
                Country country = new Country();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcountry.User_Id = Convert.ToInt32(cookie.Value);
                string[] countrycode = objcountry.Country_Code.Split(',');
                string[] countryName = objcountry.Country_Name.Split(',');
                for (int i = 0; i < countrycode.Length; i++)
                {
                    Country country1 = new Country();
                    country1.Country_Code = countrycode[i].ToString();
                    country1.Country_Name = countryName[i].ToString();
                    int j = country.InsertCountry(country1);
                }
                sms = "**Country bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult DeleteCountry(Country objcountry)
        {
            string sms = "";
            try
            {
                Country country = new Country();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcountry.User_Id = Convert.ToInt32(cookie.Value);

                int i = country.DeleteCountry(objcountry);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CountryDetails()
        {
            Country contury = new Country();
            List<Country> ObjCountry = new List<Country>();
            try
            {
                DataSet ds = contury.GetCountry(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ObjCountry.Add(new Country
                    {
                        Country_Id = Convert.ToInt32(dr["countryId"]),
                        Country_Code = dr["countryCode"].ToString(),
                        Country_Name = dr["countryName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(ObjCountry, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PhoneCountryCode()
        {
            List<SelectListItem> countrylist = new List<SelectListItem>();
            Country b = new Country();
            try
            {
                DataSet ds = b.GetPhoneCountryCode();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    countrylist.Add(new SelectListItem
                    {
                        Value = dr["countryCode"].ToString(),
                        Text = dr["countryCode"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(countrylist);
        }
        [HttpPost]
        public JsonResult CountryApproveStatus(Country objcountry)
        {
            string sms = "";
            try
            {
                Country country = new Country();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcountry.User_Id = Convert.ToInt32(cookie.Value);
                int i = country.GetCountryApproveStatus(objcountry);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CountryMaster";
                    MailConst.senderMail("Country", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult CountryName()
        {
            List<SelectListItem> countrylist = new List<SelectListItem>();
            Country b = new Country();
            try
            {
                DataSet ds = b.GetCountryName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    countrylist.Add(new SelectListItem
                    {
                        Value = dr["countryId"].ToString(),
                        Text = dr["countryName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(countrylist);
        }
   
        #endregion

        #region//--------------------state Master-------------------------
        public ActionResult StateMaster()
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
        public JsonResult AddState(State objstate)
        {
            string sms = "";
            try
            {
                State state = new State();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objstate.User_Id = Convert.ToInt32(cookie.Value);
                if (objstate.State_Id == 0)
                {
                    int i = state.InsertState(objstate);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = state.UpdateState(objstate);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkState(State objstate)
        {
            string sms = "";
            try
            {
                State state = new State();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objstate.User_Id = Convert.ToInt32(cookie.Value);
                string[] countryname = objstate.Country_Name.Split(',');
                string[] statecode = objstate.State_Code.Split(',');
                string[] statename = objstate.State_Name.Split(',');
                string[] gstcode = objstate.GST_Code.Split(',');
                for (int i = 0; i < countryname.Length; i++)
                {
                    Country c = new Country();
                    c.Country_Name = countryname[i].ToString();
                    int id = c.InsertCountryBulk(c);
                    State state1 = new State();
                    state1.Country_Id = id;
                    state1.State_Code = statecode[i].ToString();
                    state1.State_Name = statename[i].ToString();
                    state1.GST_Code = gstcode[i].ToString();
                    int j = state.InsertState(state1);
                }
                sms = "**state bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteState(State objstate)
        {
            string sms = "";
            try
            {
                State state = new State();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objstate.User_Id = Convert.ToInt32(cookie.Value);

                int i = state.DeleteState(objstate);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult StateDetails()
        {
            State state = new State();
            List<State> ObjState = new List<State>();
            try
            {
                DataSet ds = state.GetState(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ObjState.Add(new State
                    {
                        Country_Id = Convert.ToInt32(dr["countryId"]),
                        State_Id = Convert.ToInt32(dr["stateId"]),
                        Country_Name = dr["countryName"].ToString(),
                        State_Code = dr["stateCode"].ToString(),
                        State_Name = dr["stateName"].ToString(),
                        GST_Code = dr["gstcode"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(ObjState, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult StateApproveStatus(State objState)
        {
            string sms = "";
            try
            {
                State state = new State();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objState.User_Id = Convert.ToInt32(cookie.Value);

                int i = state.GetStateApproveStatus(objState);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/StateMaster";
                    MailConst.senderMail("State", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        public JsonResult StateName()
        {
            List<SelectListItem> statelist = new List<SelectListItem>();
            State b = new State();
            try
            {
                DataSet ds = b.GetStateName();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    statelist.Add(new SelectListItem
                    {
                        Value = dr["stateId"].ToString(),
                        Text = dr["stateName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(statelist);
        }
        [HttpPost]
        public JsonResult StateNameAccCountryId(State objState)
        {
            List<SelectListItem> statelist = new List<SelectListItem>();
            State b = new State();
            try
            {
                DataSet ds = b.GetStateNameAccCountryId(objState);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    statelist.Add(new SelectListItem
                    {
                        Value = dr["stateId"].ToString(),
                        Text = dr["stateName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(statelist);
        }
        [HttpPost]
        public JsonResult UpdateEditableStateTableData(State objstate, Country objcountry)
        {

            string sms = "";
            try
            {
                State state = new State();
                Country country = new Country();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objstate.User_Id = Convert.ToInt32(cookie.Value);
                objcountry.Country_Id = objstate.Country_Id;
                objcountry.Country_Code = objstate.State_Code;
                objcountry.Country_Name = objstate.Country_Name;
                int i1 = country.UpdateCountry(objcountry);

                int i = state.UpdateState(objstate);
                if (i == 0 || i1 == 0)
                {
                    sms = "**Data updated successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
       
        #endregion

        #region//--------------------city Master-------------------------
        public ActionResult CityMaster()
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
        public JsonResult AddCity(City objcity)
        {
            string sms = "";
            try
            {
                City city = new City();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcity.User_Id = Convert.ToInt32(cookie.Value);
                if (objcity.City_Id == 0)
                {
                    int i = city.InsertCity(objcity);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = city.UpdateCity(objcity);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data Already Exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkcity(City objcity)
        {
            string sms = "";
            try
            {
                City city = new City();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcity.User_Id = Convert.ToInt32(cookie.Value);
                string[] countryname = objcity.Country_Name.Split(',');
                string[] statename = objcity.State_Name.Split(',');
                string[] cityname = objcity.City_Name.Split(',');
                for (int i = 0; i < countryname.Length; i++)
                {
                    Country c = new Country();
                    c.Country_Name = countryname[i].ToString();
                    int id = c.InsertCountryBulk(c);

                    State s = new State();
                    s.State_Name = statename[i].ToString();
                    int id1 = s.InsertStateBulk(s);

                    City city1 = new City();
                    city1.Country_Id = id;
                    city1.State_Id = id1;
                    city1.City_Name = cityname[i].ToString();
                    int j = city.InsertCity(city1);
                }
                sms = "**City bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteCity(City objcity)
        {
            string sms = "";
            try
            {
                City city = new City();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objcity.User_Id = Convert.ToInt32(cookie.Value);

                int i = city.DeleteCity(objcity);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult CityDetails()
        {
            City city = new City();
            List<City> Objcity = new List<City>();
            try
            {
                DataSet ds = city.GetCity(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objcity.Add(new City
                    {
                        City_Id = Convert.ToInt32(dr["cityId"]),
                        Country_Id = Convert.ToInt32(dr["countryId"]),
                        State_Id = Convert.ToInt32(dr["stateId"]),
                        Country_Name = dr["countryName"].ToString(),
                        State_Name = dr["stateName"].ToString(),
                        City_Name = dr["cityName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objcity, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CityApproveStatus(City objCity)
        {
            string sms = "";
            try
            {
                City city = new City();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objCity.User_Id = Convert.ToInt32(cookie.Value);

                int i = city.GetCityApproveStatus(objCity);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/CityMaster";
                    MailConst.senderMail("City", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult CityName()
        {
            List<SelectListItem> Citylist = new List<SelectListItem>();
            City b = new City();
            try
            {
                DataSet ds = b.GetCityName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Citylist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["cityId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["cityName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Citylist);
        }

        [HttpPost]
        public JsonResult CityNameAccStateId(City objCity)
        {
            List<SelectListItem> citylist = new List<SelectListItem>();
            City b = new City();
            try
            {
                DataSet ds = b.GetCityNameAccStateId(objCity);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    citylist.Add(new SelectListItem
                    {
                        Value = dr["cityId"].ToString(),
                        Text = dr["cityName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(citylist);
        }
        #endregion

        #region//--------------------area Master-------------------------
        public ActionResult AreaMaster()
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
        public JsonResult AddArea(Area objarea)
        {
            string sms = "";
            try
            {
                Area area = new Area();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objarea.User_Id = Convert.ToInt32(cookie.Value);
                if (objarea.Area_Id == 0)
                {
                    int i = area.InsertArea(objarea);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = area.UpdateArea(objarea);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkarea(Area objarea)
        {
            string sms = "";
            try
            {
                Area area = new Area();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objarea.User_Id = Convert.ToInt32(cookie.Value);
                string[] countryname = objarea.Country_Name.Split(',');
                string[] statename = objarea.State_Name.Split(',');
                string[] cityname = objarea.City_Name.Split(',');
                string[] areaname = objarea.Area_Name.Split(',');
                for (int i = 0; i < countryname.Length; i++)
                {
                    Country c = new Country();
                    c.Country_Name = countryname[i].ToString();
                    int id = c.InsertCountryBulk(c);

                    State s = new State();
                    s.State_Name = statename[i].ToString();
                    int id1 = s.InsertStateBulk(s);

                    City c1 = new City();
                    c1.City_Name = cityname[i].ToString();
                    int id2 = c1.InsertCityBulk(c1);

                    Area area1 = new Area();
                    area1.Country_Id = id;
                    area1.State_Id = id1;
                    area1.City_Id = id2;
                    area1.Area_Name = areaname[i].ToString();
                    int j = area.InsertArea(area1);
                }
                sms = "**Area bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteArea(Area objarea)
        {
            string sms = "";
            try
            {
                Area area = new Area();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objarea.User_Id = Convert.ToInt32(cookie.Value);

                int i = area.DeleteArea(objarea);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult AreaDetails()
        {
            Area area = new Area();
            List<Area> Objarea = new List<Area>();
            try
            {
                DataSet ds = area.GetArea(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objarea.Add(new Area
                    {
                        Area_Id = Convert.ToInt32(dr["areaId"]),
                        City_Id = Convert.ToInt32(dr["cityId"]),
                        State_Id = Convert.ToInt32(dr["stateId"]),
                        Country_Id = Convert.ToInt32(dr["countryId"]),
                        Area_Name = dr["areaName"].ToString(),
                        City_Name = dr["cityName"].ToString(),
                        State_Name = dr["stateName"].ToString(),
                        Country_Name = dr["countryName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objarea, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AreaName()
        {
            List<SelectListItem> Arealist = new List<SelectListItem>();
            Area b = new Area();
            try
            {
                DataSet ds = b.GetAreaName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Arealist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["areaId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["areaName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Arealist);
        }

        [HttpPost]
        public JsonResult AreaApproveStatus(Area objarea)
        {
            string sms = "";
            try
            {
                Area area = new Area();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objarea.User_Id = Convert.ToInt32(cookie.Value);

                int i = area.GetAreaApproveStatus(objarea);
                if (i == 0)
                {

                    sms = "Status changed successfully";
                    string status = "";
                    if (objarea.Approve_Status == "1")
                    {
                        status = "Disapproved";
                    }
                    else
                    {
                        status = "Approved";
                    }
                    string Link = "https://testing2.leaderrange.co/Master/AreaMaster";
                    MailConst.senderMail("Area", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
    
        #endregion

        #region //--------------- Language Master -----------------------
        public ActionResult LanguageMaster()
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
        public JsonResult AddLanguage(Language objLanguage)
        {
            string sms = "";
            try
            {
                Language language = new Language();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objLanguage.User_Id = Convert.ToInt32(cookie.Value);
                if (objLanguage.Language_Id == 0)
                {
                    int i = language.InsertLanguage(objLanguage);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = language.UpdateLanguage(objLanguage);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkLanguage(Language objLanguage)
        {
            string sms = "";
            try
            {
                Language language = new Language();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objLanguage.User_Id = Convert.ToInt32(cookie.Value);
                string[] languagename = objLanguage.Language_Name.Split(',');

                for (int i = 0; i < languagename.Length; i++)
                {
                    Language language1 = new Language();
                    language1.Language_Name = languagename[i].ToString();

                    int j = language.InsertLanguage(language1);
                }
                sms = "**Language bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult DeleteLanguage(Language objLanguage)
        {
            string sms = "";
            try
            {
                Language language = new Language();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objLanguage.User_Id = Convert.ToInt32(cookie.Value);

                int i = language.DeleteLanguage(objLanguage);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult LanguageDetails()
        {
            Language language = new Language();
            List<Language> objLanguage = new List<Language>();
            try
            {
                DataSet ds = language.GetLanguageList(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objLanguage.Add(new Language
                    {
                        Language_Id = Convert.ToInt32(dr["languageId"]),
                        Language_Name = dr["languageName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(objLanguage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult LanguageApproveStatus(Language objLanguage)
        {
            string sms = "";
            try
            {
                Language language = new Language();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objLanguage.User_Id = Convert.ToInt32(cookie.Value);

                int i = language.GetLanguageApproveStatus(objLanguage);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    status = "Approved";
                    //}
                    string Link = "https://testing2.leaderrange.co/Master/LanguageMaster";
                    MailConst.senderMail("Language", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult LanguageName()
        {
            List<SelectListItem> Languagelist = new List<SelectListItem>();
            Language b = new Language();
            try
            {
                DataSet ds = b.GetLanguageList(0);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Languagelist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["languageId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["languageName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Languagelist);
        }
        #endregion

        #region//--------------------Message Master-------------------------
        public ActionResult MessageMaster()
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
        public JsonResult AddMessage(Message objMessage)
        {
            string sms = "";
            Message message = new Message();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objMessage.User_Id = Convert.ToInt32(cookie.Value);
            if (objMessage.Message_Id == 0)
            {
                int i = message.InsertMessage(objMessage);
                if (i == 0)
                {
                    sms = "**Data inserted successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }
            else
            {

                int i = message.UpdateMessage(objMessage);
                if (i == 0)
                {
                    sms = "**Data updated successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }
            return Json(sms);
        }
     

        [HttpGet]
        public JsonResult MessageDetails()
        {
            Message message = new Message();
            List<Message> ObjMessage = new List<Message>();
            DataSet ds = message.GetMessage(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                ObjMessage.Add(new Message
                {
                    Message_Id = Convert.ToInt32(dr["messageId"]),
                    Message_Type = dr["messageType"].ToString(),
                    Message_Description = dr["messageDescription"].ToString(),
                    Approve_Status = dr["isApproved"].ToString()
                });

            }
            return Json(ObjMessage, JsonRequestBehavior.AllowGet);
        }
     

        #endregion
        #endregion

        #region//-------------------------sales group-----------------------
        #region//-----sales group master-----------------------

        public ActionResult SalesGroupMaster()
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
        public JsonResult AddSalesGroup(SalesGroup objsalesgroup)
        {
            string sms = "";
            try
            {
                SalesGroup salesgroup = new SalesGroup();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsalesgroup.User_Id = Convert.ToInt32(cookie.Value);
                if (objsalesgroup.Sales_Group_Id == 0)
                {
                    int i = salesgroup.InsertSalesGroup(objsalesgroup);
                    if (i == 0)
                    {
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = salesgroup.UpdateSalesGroup(objsalesgroup);
                    if (i == 0)
                    {
                        sms = "**Data updated successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkSalesGroup(SalesGroup objsalesgroup)
        {
            string sms = "";
            try
            {
                SalesGroup sg = new SalesGroup();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsalesgroup.User_Id = Convert.ToInt32(cookie.Value);
                string[] sgcode = objsalesgroup.Sales_Group_Code.Split(',');
                string[] sgName = objsalesgroup.Sales_Group_Name.Split(',');
                for (int i = 0; i < sgName.Length; i++)
                {
                    SalesGroup sg1 = new SalesGroup();
                    sg1.Sales_Group_Code = sgcode[i].ToString();
                    sg1.Sales_Group_Name = sgName[i].ToString();
                    int j = sg.InsertSalesGroup(sg1);
                }
                sms = "**sales group bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteSalesGroup(SalesGroup objsalesgroup)
        {
            string sms = "";
            try
            {
                SalesGroup salesgroup = new SalesGroup();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objsalesgroup.User_Id = Convert.ToInt32(cookie.Value);

                int i = salesgroup.DeleteSalesGroup(objsalesgroup);
                if (i == 0)
                {
                    sms = "**Data deleted successfully**";
                }
                else
                {
                    sms = "**Data not deleted successfully**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult SalesGroupDetails()
        {
            SalesGroup salesgroup = new SalesGroup();
            List<SalesGroup> Objsalesgroup = new List<SalesGroup>();
            try
            {
                DataSet ds = salesgroup.GetSalesGroup(0);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Objsalesgroup.Add(new SalesGroup
                    {
                        Sales_Group_Id = Convert.ToInt32(dr["salesGroupId"]),
                        Sales_Group_Code = dr["salesGroupCode"].ToString(),
                        Sales_Group_Name = dr["salesGroupName"].ToString(),
                        Approve_Status = dr["isApproved"].ToString()
                    });

                }
            }
            catch (Exception ex) { }
            return Json(Objsalesgroup, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SalesGroupApproveStatus(SalesGroup objSalesGroup)
        {
            string sms = "";
            try
            {
                SalesGroup salesgroup = new SalesGroup();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objSalesGroup.User_Id = Convert.ToInt32(cookie.Value);

                int i = salesgroup.GetSalesGroupApproveStatus(objSalesGroup);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    // string status = "";
                    //if (objarea.Approve_Status == "1")
                    //{
                    //    status = "Disapproved";
                    //}
                    //else
                    //{
                    //   status = "Approved";
                    // }
                    string Link = "https://testing2.leaderrange.co/Master/SalesGroupMaster";
                    MailConst.senderMail("Sales Group", Link);
                }
                else
                {
                    sms = "**Status not changed**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        #endregion
        #endregion

        #region//-------------------------------------item master excel---------------------------------------
        public ActionResult ItemMasterExcel()
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
        public JsonResult AddItemByExcel(Item objItem)
        {
            string sms = "";
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objItem.User_Id = Convert.ToInt32(cookie.Value);
                //-------------------------------------------------insert item-----------------------------------------------------------------
                string[] itemtype = objItem.Item_Type_Name.Split(',');
                string[] uomname = objItem.UOM_Name.Split(',');
                string[] itemsubtype = objItem.Item_Sub_Type_Name.Split(',');
                string[] itemdesc = objItem.Item_Description.Split(',');
                string[] itemmpn = objItem.Item_Manufacturing_Part_Number.Split(',');
                string[] itemqty = objItem.Item_Quntity_str.Split(',');
                string[] itemrack = objItem.Rack_Name.Split(',');
                string[] itembin = objItem.Rack_Number_Name.Split(',');
                for (int i = 0; i < itemtype.Length; i++)
                {
                    Item item = new Item();
                    Random rand = new Random();
                    long num = (long)(rand.NextDouble() * 9000000000) + 1000000000;
                   // string num = Math.floor(Math.random() * 9000000000) + 1000000000;
                    QRCodeModel qr = new QRCodeModel();
                    qr.QRCodeText = num.ToString();
                    qr.QRCodeImagePath = GenerateQRCode(qr.QRCodeText);
                  
                   
                    item.Item_Type_Name = itemtype[i].ToString();
                    item.UOM_Name = uomname[i].ToString();
                    item.Item_Sub_Type_Name = itemsubtype[i].ToString();
                    item.Item_Description = itemdesc[i].ToString();
                    item.Item_Manufacturing_Part_Number = itemmpn[i].ToString();
                    item.Item_Quntity_str = itemqty[i].ToString();
                    item.Rack_Name = itemrack[i].ToString();
                    item.Rack_Number_Name = itembin[i].ToString();
                    item.Item_QRCode_path = qr.QRCodeImagePath;
                    item.Bar_Code_No = num.ToString();
                    int j = item.InsertItemByExcel(item);
                }
                sms = "**Data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        #endregion
    }
}

