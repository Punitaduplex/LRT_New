using LRT_MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Text;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
//using Office = Microsoft.Office.Core;

namespace LRT_MVC_Project.Controllers
{
    public class Vendor1Controller : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            //if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userid"))
            //{
            //    HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["username"];
            //    Common.CommonSetting.name = cookie1.Value;
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("LogIn", "User");
            //}
        }
        #region //--------------------------vendor Category----------------------------------
        public ActionResult VendorCategory()
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
        public JsonResult AddVendorCategory(VendorCategory objVendorCategory)
        {
            string sms = "";
            VendorCategory vendor = new VendorCategory();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorCategory.User_Id = Convert.ToInt32(cookie.Value);
            if (objVendorCategory.Vendor_Category_Id == 0)
            {
                int i = 0;
                i = vendor.InsertVendorCategory(objVendorCategory);
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
                int i = vendor.UpdateVendorCategory(objVendorCategory);
                if (i == 0)
                {
                    sms = "**Data updated successfully**";
                }
                else
                {
                    sms = "**Data Already Exist**";
                }
            }
            return Json(sms);
        }

        [HttpPost]
        public JsonResult AddBulkVendorCategory(VendorCategory objVendorCategory)
        {
            string sms = "";
            VendorCategory category = new VendorCategory();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorCategory.User_Id = Convert.ToInt32(cookie.Value);
            string[] categoryname = objVendorCategory.Vendor_Category_Name.Split(',');

            for (int i = 0; i < categoryname.Length; i++)
            {
                VendorCategory category1 = new VendorCategory();
                category1.Vendor_Category_Name = categoryname[i].ToString();

                int j = category.InsertVendorCategory(category1);
            }
            sms = "**Vendor category bulk data inserted successfully**";
            return Json(sms);
        }

        [HttpPost]
        public JsonResult DeleteVendorCategory(VendorCategory objVendorCategory)
        {
            string sms = "";
            VendorCategory vendor = new VendorCategory();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorCategory.User_Id = Convert.ToInt32(cookie.Value);

            int i = vendor.DeleteVendorCategory(objVendorCategory);
            if (i == 0)
            {
                sms = "**Data deleted successfully**";
            }
            else
            {
                sms = "**Data not deleted successfully**";
            }

            return Json(sms);
        }

        [HttpGet]
        public JsonResult VendorCategoryDetails()
        {
            VendorCategory vendor = new VendorCategory();
            List<VendorCategory> objVendor = new List<VendorCategory>();
            DataSet ds = vendor.GetVendorCategory(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendor.Add(new VendorCategory
                {
                    Vendor_Category_Id = Convert.ToInt32(dr["vendorcategoryId"]),
                    Vendor_Category_Name = dr["vendorcategoryName"].ToString(),
                    Approve_Status = dr["isApproved"].ToString()

                });

            }
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorCategoryApproveStatus(VendorCategory objVendorCategory)
        {
            string sms = "";
            VendorCategory vendorcategory = new VendorCategory();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorCategory.User_Id = Convert.ToInt32(cookie.Value);

            int i = vendorcategory.GetVendorCategoryApproveStatus(objVendorCategory);
            if (i == 0)
            {
                sms = "Status changed successfully";
            }
            else
            {
                sms = "**Status not changed**";
            }

            return Json(sms);
        }
        [HttpPost]
        public JsonResult VendorCategoryName()
        {
            List<SelectListItem> rolelist = new List<SelectListItem>();
            VendorCategory b = new VendorCategory();
            DataSet ds = b.GetVendorCategoryName();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rolelist.Add(new SelectListItem
                {
                    Value = ds.Tables[0].Rows[i]["vendorcategoryId"].ToString(),
                    Text = ds.Tables[0].Rows[i]["vendorcategoryName"].ToString()
                });
            }

            return Json(rolelist);
        }

        #endregion

        #region //--------------------------vendor Permission----------------------------------
        public ActionResult VendorPermissionMaster()
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
        public JsonResult AddVendorPermission(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission permission = new VendorPermission();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorPermission.User_Id = Convert.ToInt32(cookie.Value);
            if (objVendorPermission.Permission_Id == 0)
            {
                int i = 0;
                i = permission.InsertVendorPermission(objVendorPermission);
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
                int i = permission.UpdateVendorPermission(objVendorPermission);
                if (i == 0)
                {
                    sms = "**Data updated successfully**";
                }
                else
                {
                    sms = "**Data Already Exist**";
                }
            }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkVendorPermission(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission permission = new VendorPermission();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorPermission.User_Id = Convert.ToInt32(cookie.Value);
            string[] permissionname = objVendorPermission.Permission_Name.Split(',');

            for (int i = 0; i < permissionname.Length; i++)
            {
                VendorPermission permission1 = new VendorPermission();
                permission1.Permission_Name = permissionname[i].ToString();

                int j = permission.InsertVendorPermission(permission1);
            }
            sms = "**Vendor permission bulk data inserted successfully**";
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteVendorPermission(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission permission = new VendorPermission();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorPermission.User_Id = Convert.ToInt32(cookie.Value);

            int i = permission.DeleteVendorPermission(objVendorPermission);
            if (i == 0)
            {
                sms = "**Data deleted successfully**";
            }
            else
            {
                sms = "**Data not deleted successfully**";
            }

            return Json(sms);
        }

        [HttpGet]
        public JsonResult VendorPermissionDetails()
        {
            VendorPermission vendorpermission = new VendorPermission();
            List<VendorPermission> objVendorPermission = new List<VendorPermission>();
            DataSet ds = vendorpermission.GetVendorPermissionList(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendorPermission.Add(new VendorPermission
                {
                    Permission_Id = Convert.ToInt32(dr["permissionId"]),
                    Permission_Name = dr["permissionName"].ToString(),
                    Approve_Status = dr["isApproved"].ToString()

                });

            }
            return Json(objVendorPermission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorPermissionApproveStatus(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission vendorpermission = new VendorPermission();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorPermission.User_Id = Convert.ToInt32(cookie.Value);

            int i = vendorpermission.GetVendorPermissionApproveStatus(objVendorPermission);
            if (i == 0)
            {
                sms = "Status changed successfully";
            }
            else
            {
                sms = "**Status not changed**";
            }

            return Json(sms);
        }
        [HttpPost]
        public JsonResult VendorPermissionName()
        {
            List<VendorPermission> permissionlist = new List<VendorPermission>();
            VendorPermission b = new VendorPermission();
            DataSet ds = b.GetVendorPermissionName();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                VendorPermission b1 = new VendorPermission();
                b1.Permission_Id =Convert.ToInt32(ds.Tables[0].Rows[i]["permissionId"]);
                    b1.Permission_Name = ds.Tables[0].Rows[i]["permissionName"].ToString();
                    permissionlist.Add(b1);
            }

            return Json(permissionlist);
        }
        [HttpPost]
        public JsonResult AddActionPermission(VendorRegistation objVendorRegistation)
        {
            string sms = "";
            int i = 0;
            int count = 0;
            VendorRegistation Permission = new VendorRegistation();
            Permission.Vendor_Id = objVendorRegistation.Vendor_Id;

            Permission.User_Id = Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
            if (Permission.Vendor_Id != 0)
            {
                int i1 = Permission.DeleteVendorActionPermission(objVendorRegistation);
                string[] Permissionstr = objVendorRegistation.Permission_Id_Str.Split(',');

                string[] view = objVendorRegistation.Is_View_Str.Split(',');

                for (int j = 0; j < Permissionstr.Length; j++)
                {
                    Permission.Permission_Id = Convert.ToInt32(Permissionstr[j]);
                  
                    Permission.Is_View = Convert.ToBoolean(Convert.ToInt32(view[j]));
                   
                    i = Permission.InsertActionPermission(Permission);
                    if (i == 0)
                    {
                        count = count + 1;
                    }
                }


                if (count > 0)
                {
                    MailConst.senderMailForVenderPermission(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person, objVendorRegistation.Permission_Name, objVendorRegistation.Vendor_LogIn_Id, objVendorRegistation.Vendor_LogIn_Password);
                  // SendEmailToVendorForPermission(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person, objVendorRegistation.Vendor_LogIn_Id, objVendorRegistation.Vendor_LogIn_Password);
                    sms = "**Permission has set successfully**";
                }
                else
                {
                    sms = "**Permission has not set**";
                }
            }
           
            return Json(sms);
        }
        #endregion

        #region //--------------- Vendor Invitation -----------------------
        public ActionResult VendorInvitation()
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
        public JsonResult AddVendorInvitation(Vendor objVendor)
        {
            string sms = "";
            Vendor vendor = new Vendor();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendor.User_Id = Convert.ToInt32(cookie.Value);
            if (objVendor.Vendor_Invitation_Id == 0)
            {
                string s = "";
                s = vendor.InsertVendorInvitation(objVendor);
                string[] s1 = s.Split(',');
                int i = Convert.ToInt32(s1[0]);
                if (i == 0)
                {
                    MailConst.senderMailForInvitation(objVendor.Vendor_Email_ID.Trim(), objVendor.Vendor_Company_Name.Trim(),objVendor.Vendor_Contact_Person);
                   
                    sms = "**Data inserted successfully**";
                }
                else
                {
                    sms = "**Data already exist**";
                }
            }
            else
            {
                int i = vendor.UpdateVendorInvitation(objVendor);
                if (i == 0)
                {
                    sms = "**Data updated successfully**";
                }
                else
                {
                    sms = "**Data Already Exist**";
                }
            }
            return Json(sms);
        }
        //public void SendEmailToVendor(string emailId, string companyname)
        //{
        //    var fromMail = new MailAddress("developduplex@gmail.com", "LRT Admin"); // set your email
        //    var fromEmailpassword = "Hello@2020"; // Set your password
        //    var toEmail = new MailAddress(emailId);
        //    try
        //    {
        //        var smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //       smtp.UseDefaultCredentials = false;
        //       // smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

        //        var Message = new MailMessage(fromMail, toEmail);
        //        Message.Subject = "LRT Vender Invitation Notifiaction";
        //        Message.Body = "<br/> ";
        //        Message.Body += "<br/> <div style='background-color:black;'><div style='background-color:#705e5e;'><div style='margin-left:250px;position:relative;height:70px;'><img src=\"https://testing2.leaderrange.co/Images/LRTLogo.png\"/></div></div>";
        //        Message.Body += "<br/><div style='height:50px;display:inline-block'><img src=\"https://testing2.leaderrange.co/Images/ldr_logo_24.png\"/><span style='font-weight:bold;font-size:30px;color:white;'>Vendor&nbsp;Registration</span></div> ";
        //        Message.Body += "<br/> <hr style='border-top:3px solid #6c18'/>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>Hello&nbsp;'" + companyname + "'</span>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>Kindly Click On the below image for registration form</span>";
        //        Message.Body += "<br/><a href='https://testing2.leaderrange.co/Vendor/VendorRequest' target='_blank'><img src=\"https://testing2.leaderrange.co/Images/newvendorreg.png\" style='height:70px;padding-left: 20px;padding-bottom: 20px; padding-top:15px;'/></a> ";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>This is a system generated email.</span> ";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>System Sent Date And Time:'" + System.DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss tt") + "'</span> </div> ";
        //        Message.Body += "<br/> <br/>Thanks! ";
        //        Message.Body += "<br/> LRT Admin ";
        //        Message.IsBodyHtml = true;
        //        smtp.Send(Message);
        //    }
        //    catch (Exception ex)
        //    { }
        //}
       
        [HttpPost]
        public JsonResult DeleteVendorInvitation(Vendor objVendor)
        {
            string sms = "";
            Vendor vendor = new Vendor();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendor.User_Id = Convert.ToInt32(cookie.Value);

            int i = vendor.DeleteVendorInvitation(objVendor);
            if (i == 0)
            {
                sms = "**Data deleted successfully**";
            }
            else
            {
                sms = "**Data not deleted successfully**";
            }

            return Json(sms);
        }
        [HttpGet]
        public JsonResult VendorInvitationDetails()
        {
            Vendor vendor = new Vendor();
            List<Vendor> objVendor = new List<Vendor>();
            DataSet ds = vendor.GetVendorInvitation(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendor.Add(new Vendor
                {
                    Vendor_Invitation_Id = Convert.ToInt32(dr["vendorinvitationId"]),
                    Vendor_Company_Name = dr["vendorCompany"].ToString(),
                    Vendor_Contact_Person = dr["contactPerson"].ToString(),
                    Vendor_Email_ID = dr["vendorEmail"].ToString(),
                    Approve_Status = dr["isApproved"].ToString(),
                    Access_Permission = ""// dr["accessPermission"].ToString()
                });

            }
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorInvitationApproveStatus(Vendor objVendor)
        {
            string sms = "";
            Vendor vendor = new Vendor();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendor.User_Id = Convert.ToInt32(cookie.Value);


            int i = vendor.GetVendorInvitationApproveStatus(objVendor);
            if (i == 0)
            {
                sms = "Status changed successfully";
                string Link = "https://testing2.leaderrange.co/Vendor/VendorInvitation";
                MailConst.senderMail("Invitation", Link);
               
            }
            else
            {
                sms = "**Status not changed**";
            }

            return Json(sms);
        }
        [HttpPost]
        public JsonResult VendorCompanyAccEmail(Vendor objVendor)
        {
            Vendor vendor = new Vendor();
            List<Vendor> VendorDetailList = new List<Vendor>();
            DataSet ds = vendor.GetVendorCompanyAccEmail(objVendor);
            if (ds.Tables[0].Rows.Count > 0)
            {
                vendor.Vendor_Invitation_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorinvitationId"]);
                vendor.Vendor_Company_Name = Convert.ToString(ds.Tables[0].Rows[0]["vendorCompany"]);
            }
            else
            {
                vendor.Vendor_Invitation_Id = 0;
            }
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }
       
        #endregion

        #region //-------------------------Vendor Request-------------------------------------
        public ActionResult VendorRequest()
        {
            //if (Common.CommonSetting.User_Id == 0 || Common.CommonSetting.User_Id == null)
            //{
            //    return RedirectToAction("LogIn", "User");
            //}
            //else
            //{
                return View();
          //  }
        }
        public ActionResult VendorRegistration()
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
        public JsonResult InsertVendorRegistation(VendorRegistation objVendorRegistation)
        {
            string sms = "";
            VendorRegistation vendor = new VendorRegistation();
            objVendorRegistation.User_Id = 0;// Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
            if (objVendorRegistation.Vendor_Id == 0)
            {
                string s = vendor.InsertVendorRegistation(objVendorRegistation);
                string[] s1 = s.Split(',');
                int i =Convert.ToInt32(s1[0]);
                if (i == 0)
                {
                    MailConst.senderMailForRegistration(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person);
                   // SendEmailToVendorForRegistration(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person);
                    sms =s1[1]+ ",**We have sent your request to the admin successfully**";
                }
                else
                {
                    sms = "**Vendor request already exist**";
                }
            }
           
            return Json(sms);
        }
        [HttpPost]
        public JsonResult PhoneCountryCode()
        {
            List<SelectListItem> countrylist = new List<SelectListItem>();
            Country b = new Country();
            DataSet ds = b.GetPhoneCountryCode();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                countrylist.Add(new SelectListItem
                {
                    Value = dr["countryCode"].ToString(),
                    Text = dr["countryCode"].ToString()
                });
            }

            return Json(countrylist);
        }

        [HttpPost]
        public JsonResult BankSwiftCode()
        {
            List<SelectListItem> countrylist = new List<SelectListItem>();
            Bank b = new Bank();
            DataSet ds = b.GetBankSwiftCode();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                countrylist.Add(new SelectListItem
                {
                    Value = dr["swiftCodeId"].ToString(),
                    Text = dr["swiftCode"].ToString()
                });
            }

            return Json(countrylist);
        }
        [HttpPost]
        public ActionResult UploadNDAFileMethod()
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
                        if ((fileex == "pdf") || (fileex == "PDF"))
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/NDA_form/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/NDA_form/" + name;
                            sms = s[0] + " file uploaded successfully!+" + path;
                        }
                        else
                        { sms = "Please select only pdf file for " + s[0] + " file+" + path; }
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
        public ActionResult UploadSampleFileMethod()
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/vendor_sample_file/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/vendor_sample_file/" + name;
                            sms = s[0] + " file uploaded successfully!+" + path;
                        }
                        else
                        { sms = "Please select only pdf, doc and xls file for " + s[0] + " file+" + path; }
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
        public ActionResult UploadFormFileMethod()
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/form_files/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/form_files/" + name;
                            sms = s[0] + " file uploaded successfully!+" + path;
                        }
                        else
                        { sms = "Please select only pdf, doc and xls file for " + s[0] + " file+" + path; }
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
        public JsonResult VendorInvitationDetailAccCompany(VendorRegistation objVendorRegistation)
        {
            VendorRegistation vendor = new VendorRegistation();
            List<Vendor> VendorRegistationList = new List<Vendor>();
            try
            {
                DataSet ds = vendor.GetVendorInvitationDetailAccCompanyName(objVendorRegistation);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    vendor.Count = Convert.ToInt32(ds.Tables[0].Rows[0]["count1"]);
                    vendor.Contact_Person = Convert.ToString(ds.Tables[0].Rows[0]["contactPerson"]);
                    vendor.Vendor_Email_ID = Convert.ToString(ds.Tables[0].Rows[0]["vendorEmail"]);
                }
            }
            catch (Exception ex) { }
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //-------------------------Vendor Request Detail-------------------------------------
        public ActionResult VendorRequestDetail()
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
        public JsonResult VendorRequestDetails()
        {
            VendorRegistation vendor = new VendorRegistation();
            List<VendorRegistation> objVendor = new List<VendorRegistation>();
            DataSet ds = vendor.GetVendorRequestDetail(0);

            for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
            {
                VendorRegistation vendor1 = new VendorRegistation();
                vendor1.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendorId"]);
                vendor1.Contact_Person = ds.Tables[0].Rows[i]["vendorName"].ToString();
                vendor1.Vendor_Company_Name = ds.Tables[0].Rows[i]["companyName"].ToString();
                vendor1.Vendor_Email_ID = ds.Tables[0].Rows[i]["vendorEmail"].ToString();
                string requestStatus = Convert.ToString(ds.Tables[0].Rows[i]["registrationStatus"]);
                if (requestStatus == "Request")
                {
                    vendor1.Registration_Status = "External";
                }
                else
                {
                    vendor1.Registration_Status = "Internal";
                }
                vendor1.Request_Status = ds.Tables[0].Rows[i]["requestStatus"].ToString();

                vendor1.Approve_Status = Convert.ToString(ds.Tables[0].Rows[i]["isApproved"]);
                objVendor.Add(vendor1);
            }
           

           
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RegisteredVendorDetailsAccSearchValue(VendorRegistation objVendorRegistation)
        {
            VendorRegistation VendorDetaillist = new VendorRegistation();
            List<VendorRegistation> objVendor = new List<VendorRegistation>();
            if (objVendorRegistation.Vendor_Category_Name == "All")
            {
                objVendorRegistation.Vendor_Category_Name = "";
            }
            if (objVendorRegistation.Vendor_Status == "All")
            {
                objVendorRegistation.Vendor_Status = "";
            }
            DataSet ds = VendorDetaillist.GetRegisteredVenderDetailAccSearchValues(objVendorRegistation);

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                VendorRegistation vendor1 = new VendorRegistation();
                vendor1.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendorId"]);
                vendor1.Contact_Person = ds.Tables[0].Rows[i]["vendorName"].ToString();
                vendor1.Vendor_Company_Name = ds.Tables[0].Rows[i]["companyName"].ToString();
                vendor1.Vendor_Email_ID = ds.Tables[0].Rows[i]["vendorEmail"].ToString();
                string requestStatus = Convert.ToString(ds.Tables[0].Rows[i]["registrationStatus"]);
                if (requestStatus == "Request")
                {
                    vendor1.Registration_Status = "External";
                }
                else
                {
                    vendor1.Registration_Status = "Internal";
                }
                vendor1.Request_Status = ds.Tables[0].Rows[i]["requestStatus"].ToString();

                vendor1.Approve_Status = Convert.ToString(ds.Tables[0].Rows[i]["isApproved"]);
                objVendor.Add(vendor1);
            }

            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorRequestDetailsForPopup(VendorRegistation objVendorRegistation)
        {
            VendorRegistation vendor = new VendorRegistation();
            DataSet ds = vendor.GetVendorRegistrationListForPopup(objVendorRegistation);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                vendor.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorId"]);
                vendor.Contact_Person = ds.Tables[0].Rows[0]["vendorSortName"].ToString();
                vendor.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                vendor.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();
                vendor.Vendor_Company_Address = Convert.ToString(ds.Tables[0].Rows[0]["companyAddress"]);
                vendor.Country_Name = ds.Tables[0].Rows[0]["CountryName"].ToString();
                vendor.State_Name = ds.Tables[0].Rows[0]["StateName"].ToString();
                vendor.City_Name = Convert.ToString(ds.Tables[0].Rows[0]["CityName"]);
                vendor.Pin_Code = Convert.ToString(ds.Tables[0].Rows[0]["companypinCode"]);
                vendor.Vendor_company_Telephone = Convert.ToString(ds.Tables[0].Rows[0]["companyTeliphone"]);
                vendor.Vendor_company_FaxNo = Convert.ToString(ds.Tables[0].Rows[0]["companyFaxNo"]);
                vendor.Vendor_company_MobileNo = Convert.ToString(ds.Tables[0].Rows[0]["companyMobileNo"]);
                vendor.Vendor_company_Attn = Convert.ToString(ds.Tables[0].Rows[0]["companyalternanteNo"]);
                vendor.Vendor_company_GST = Convert.ToString(ds.Tables[0].Rows[0]["companyGST"]);
                vendor.Vendor_company_SST = Convert.ToString(ds.Tables[0].Rows[0]["companySST"]);
                vendor.Payment_Term_Name = Convert.ToString(ds.Tables[0].Rows[0]["payemtTermName"]);
                vendor.Currency_Name = Convert.ToString(ds.Tables[0].Rows[0]["CurrencyName"]);
                vendor.Vendor_Category_Name = Convert.ToString(ds.Tables[0].Rows[0]["CategoryName"]);
                vendor.Vendor_Status = Convert.ToString(ds.Tables[0].Rows[0]["companyStatus"]);
                vendor.Vendor_Region = Convert.ToString(ds.Tables[0].Rows[0]["companyRegion"]);
                vendor.Vendor_Work_Sample_Url = Convert.ToString(ds.Tables[0].Rows[0]["companyWorksampleUrl"]);
                vendor.Machine_Name = ds.Tables[0].Rows[0]["machineName"].ToString();
                vendor.Machine_Function = ds.Tables[0].Rows[0]["machineFunction"].ToString();
                vendor.Machine_Capability = ds.Tables[0].Rows[0]["machineCapability"].ToString();
               // vendor.Vendor_System_Id = Convert.ToString(ds.Tables[0].Rows[0]["vendorSystemId"]);
                vendor.Vendor_Name = ds.Tables[0].Rows[0]["vendorName"].ToString();
                vendor.Alt_Vendor_Name1 = ds.Tables[0].Rows[0]["altvendorname1"].ToString();
                vendor.Alt_Vendor_Email1 = Convert.ToString(ds.Tables[0].Rows[0]["altvendorEmail1"]);
                vendor.Alt_Vendor_Name2 = Convert.ToString(ds.Tables[0].Rows[0]["altvendorname2"]);
                vendor.Alt_Vendor_Email2 = Convert.ToString(ds.Tables[0].Rows[0]["altvendorEmail2"]);
                vendor.Vendor_Telephone_Number_1 = Convert.ToString(ds.Tables[0].Rows[0]["vendorteliphoneNo1"]);
                //vendor.Vendor_Telephone_Number_2 = Convert.ToString(ds.Tables[0].Rows[0]["vendorreliphoneNo2"]);
                vendor.Vendor_Address = Convert.ToString(ds.Tables[0].Rows[0]["vendoraddress"]);
                vendor.Vendor_Country_Name = Convert.ToString(ds.Tables[0].Rows[0]["VendorCountryName"]);
                vendor.Vendor_City_Name = Convert.ToString(ds.Tables[0].Rows[0]["VendorCityName"]);
                vendor.Vendor_Longitude = Convert.ToString(ds.Tables[0].Rows[0]["locationLongitude"]);
                vendor.Vendor_Latitude = Convert.ToString(ds.Tables[0].Rows[0]["locationLatitude"]);
                vendor.Vendor_Bank_Name = Convert.ToString(ds.Tables[0].Rows[0]["BankName"]);
                vendor.Vendor_Swift_Code = Convert.ToString(ds.Tables[0].Rows[0]["swiftCode"]);
                vendor.Vendor_Account_Number = Convert.ToString(ds.Tables[0].Rows[0]["accountNo"]);
                vendor.Vendor_Receiver_Name = ds.Tables[0].Rows[0]["reciverName"].ToString();
                vendor.Vendor_Bank_Address = ds.Tables[0].Rows[0]["bankAddress"].ToString();
                vendor.Vendor_Bank_Postal_Code = ds.Tables[0].Rows[0]["postalCode"].ToString();
                vendor.Bank_Contact_Number_1 = Convert.ToString(ds.Tables[0].Rows[0]["bankContactNo1"]);
                vendor.Bank_Contact_Number_2 = ds.Tables[0].Rows[0]["bankContactNo2"].ToString();
                vendor.Paypal_Email_Id = ds.Tables[0].Rows[0]["paypalemailId"].ToString();
                vendor.NDA_Form_Url = Convert.ToString(ds.Tables[0].Rows[0]["ndaformUrl"]);
                vendor.Vendor_Status = Convert.ToString(ds.Tables[0].Rows[0]["companyStatus"]);
                vendor.Vendor_Bank_Type = Convert.ToString(ds.Tables[0].Rows[0]["bankdetailType"]);
                vendor.Bank_Country_Name = Convert.ToString(ds.Tables[0].Rows[0]["BankCountryName"]);
                vendor.Bank_State_Name = Convert.ToString(ds.Tables[0].Rows[0]["BankStateName"]);
                vendor.Bank_City_Name = Convert.ToString(ds.Tables[0].Rows[0]["BankCityName"]);
                vendor.Form_24 = Convert.ToString(ds.Tables[0].Rows[0]["form24"]);
                vendor.Form_48 = Convert.ToString(ds.Tables[0].Rows[0]["form48"]);
                vendor.Form_9 = Convert.ToString(ds.Tables[0].Rows[0]["form9"]);
                vendor.Other_Form = Convert.ToString(ds.Tables[0].Rows[0]["otherform"]);

            }
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorRegistationApproveStatus(VendorRegistation objVendorRegistation)
        {
            string sms = "";
           string status1 = "";
            int i = 0;
            VendorRegistation vendor = new VendorRegistation();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorRegistation.User_Id = Convert.ToInt32(cookie.Value);


            string s = vendor.GetVendorRegistrationApproveStatus(objVendorRegistation);
            string [] s1=s.Split(',');
            i =Convert.ToInt32(s1[0]);
            if (i == 0)
            {
                int status =Convert.ToInt32(s1[1]);
                if (status == 1) {
                    status1 = "Approved";

                }
                else
                {
                    status1 = "Dismissed";
                }
                string Link = "https://testing2.leaderrange.co/Vendor/VendorRequestDetail";
                MailConst.senderMail("Vendor Registation Detail", Link);
              //  email_send();
            // SendEmailToVendorForApproval(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person,status1);
                sms = "Status changed successfully";
            }
            else
            {
                sms = "**Status not changed**";
            }

            return Json(sms);
        }

        //private ExchangeService _service = new ExchangeService(ExchangeVersion.Exchange2013);
        //public void senderMail()
        //{
        //    //bool result = false;
        //    sendMail("way2punita@gmail.com", "SubjectName", "<p> Hi LRT,<br/> This email is just for Testing PurPose,So Dont be Upset,<br/> Regards LRT</p>");
        //    //return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public static void sendMail(string ToEmail, string Subject, string EmailBody)
        //{
        //    //ExchangeClient client = new ExchangeClient("username", "password", "domain");

        //    //// Create instance of type MailMessage
        //    //MailMessage msg = new MailMessage();
        //    //msg.From = "sender@domain.com";
        //    //msg.To = "recipient@ domain.com ";
        //    //msg.Subject = "Sending message from exchange server";
        //    //msg.HtmlBody = "  sending message from exchange server";

        //    //// Send the message
        //    //client.Send(msg);


        //    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
        //    service.UseDefaultCredentials = false;
        //    service.Credentials = new NetworkCredential("it_test@leaderrange.com", "Lrt6425755", "mail.leaderrange.com");
        //    //service.autodiscoverurl("it_test@leaderrange.com");
        //    service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
        //    service.Timeout = 200000;
        //    EmailMessage msg = new EmailMessage(service);
        //    msg.Subject = Subject;

        //    msg.Body = new MessageBody(BodyType.HTML, EmailBody);

        //    msg.ToRecipients.Add(new EmailAddress(ToEmail));

        //    msg.Send();
        //}



        //public void SendEmailToVendorForPermission(string emailId, string companyname, string contactperson, string userid, string password)
        //{
        //    var fromMail = new MailAddress("developduplex@gmail.com", "LRT Admin"); // set your email
        //    var fromEmailpassword = "Hello@2020"; // Set your password
        //    var toEmail = new MailAddress(emailId);
        //    try
        //    {
        //        var smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.UseDefaultCredentials = false;
        //        // smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

        //        var Message = new MailMessage(fromMail, toEmail);
        //        Message.Subject = "LRT Vender Permission Notifiaction To " + companyname;
        //        Message.Body = "<br/> ";
        //        Message.Body += "<br/> <div style='background-color:black;'><div style='background-color:#705e5e;'><div style='margin-left:385px;position:relative;height:70px;font-size:30px;color:white;font-weight:bold;padding-top: 17px;'>Leader Range Technology</div></div>";
        //        Message.Body += "<br/><div style='height:50px;display:inline-block'><span style='font-weight:bold;font-size:30px;color:white;'>&nbsp;Vendor&nbsp;Permission</span></div> ";
        //        Message.Body += "<br/> <hr style='border-top:3px solid #6c18'/>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>Hello&nbsp;'" + contactperson + "'</span>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>Your have some permission for access portal.below has login details for access LRT portal </span></br>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>user Id : '" + userid + "'</span></br>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>Password : '" + password + "'</span></br></br></br>";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>This is a system generated email.</span> ";
        //        Message.Body += "<br/><span style='color:white;padding-left:16px;'>System Sent Date And Time:'" + System.DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss tt") + "'</span> </div> ";
        //        Message.Body += "<br/> <br/>Thanks! ";
        //        Message.Body += "<br/> LRT Admin ";
        //        Message.IsBodyHtml = true;
        //        smtp.Send(Message);
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        [HttpPost]
        public JsonResult VendorRegistationStandByStatus(VendorRegistation objVendorRegistation)
        {
            string sms = "";
            VendorRegistation vendor = new VendorRegistation();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorRegistation.User_Id = Convert.ToInt32(cookie.Value);


            int i = vendor.GetVendorRegistrationStandByStatus(objVendorRegistation);
            if (i == 0)
            {
               
                sms = "Status changed successfully";
            }
            else
            {
                sms = "**Status not changed**";
            }

            return Json(sms);
        }
        #endregion

        #region //-------------------------Set Vendor Permission-------------------------------------
        public ActionResult SetVendorPermission()
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

        #region //-------------------------Vendor Search-------------------------------------
        public ActionResult ApprovedVendor()
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
        public JsonResult VendorDetailsAccSearchValue(VendorRegistation objVendorRegistation)
        {
            VendorRegistation VendorDetaillist = new VendorRegistation();
            List<VendorRegistation> objVendorDetaillist = new List<VendorRegistation>();
            if (objVendorRegistation.Vendor_Category_Name == "All")
            {
                objVendorRegistation.Vendor_Category_Name = "";
            }
            if (objVendorRegistation.Vendor_Status == "All")
            {
                objVendorRegistation.Vendor_Status = "";
            }
            DataSet ds = VendorDetaillist.GetVenderDetailAccSearchValues(objVendorRegistation);

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                VendorRegistation vendor = new VendorRegistation();
                vendor.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendorId"]);
                vendor.Contact_Person = ds.Tables[0].Rows[i]["vendorSortName"].ToString();
                vendor.Vendor_Company_Name = ds.Tables[0].Rows[i]["companyName"].ToString();
                vendor.Vendor_company_GST = ds.Tables[0].Rows[i]["companyGST"].ToString();
                vendor.Vendor_Category_Name = ds.Tables[0].Rows[i]["vendorcategoryName"].ToString();
                vendor.Vendor_Status = ds.Tables[0].Rows[i]["companyStatus"].ToString();
                vendor.Vendor_Email_ID = ds.Tables[0].Rows[i]["vendorEmail"].ToString();
                objVendorDetaillist.Add(vendor);
            }

            return Json(objVendorDetaillist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorTopTenForSearch()
        {
            VendorRegistation VendorDetaillist = new VendorRegistation();
            List<VendorRegistation> objVendorDetaillist = new List<VendorRegistation>();
           
            DataSet ds = VendorDetaillist.GetVenderTopTenDetailForSearch();

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                VendorRegistation vendor = new VendorRegistation();
                vendor.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendorId"]);
                vendor.Contact_Person = ds.Tables[0].Rows[i]["vendorSortName"].ToString();
                vendor.Vendor_Company_Name = ds.Tables[0].Rows[i]["companyName"].ToString();
                vendor.Vendor_company_GST = ds.Tables[0].Rows[i]["companyGST"].ToString();
                vendor.Vendor_Category_Name = ds.Tables[0].Rows[i]["vendorcategoryName"].ToString();
                vendor.Vendor_Status = ds.Tables[0].Rows[i]["companyStatus"].ToString();
                vendor.Vendor_Email_ID = ds.Tables[0].Rows[i]["vendorEmail"].ToString();
                objVendorDetaillist.Add(vendor);
            }

            return Json(objVendorDetaillist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRecordForCompanyName(string prefix="")
        {

            VendorRegistation searchitem = new VendorRegistation();
            List<VendorRegistation> searchvendorList = new List<VendorRegistation>();
            DataSet ds = searchitem.GetSearchVenderAccKeywordsForCompanyName(prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                searchvendorList.Add(new VendorRegistation
                {
                    Search_Name = dr["SearchItem"].ToString(),

                });

            }
            return Json(searchvendorList, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetRecordForSortName(string prefix="")
        {

            VendorRegistation searchitem = new VendorRegistation();
            List<VendorRegistation> searchvendorList = new List<VendorRegistation>();
            DataSet ds = searchitem.GetSearchVenderAccKeywordsForSortName(prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                searchvendorList.Add(new VendorRegistation
                {
                    Search_Name = dr["SearchItem"].ToString(),

                });

            }
            return Json(searchvendorList, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ChangeVendorCompanyStatus(VendorRegistation objVendorRegistation)
        {
            string sms = "";
            VendorRegistation vendor = new VendorRegistation();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorRegistation.User_Id = Convert.ToInt32(cookie.Value);


            int i = vendor.GetChangeVendorCompanyStatus(objVendorRegistation);
            if (i == 0)
            {
                string status = objVendorRegistation.Vendor_Status;
                sms = "Company status changed successfully";
                string message = "";
                if (status != "Active")
                {
                    message = "You have been re-activated to the Vendor Database of Leader Range Technology.Kindly await further updates.";
                }
                else
                {
                    message = "You have been de-activated from the Vendor Database of Leader Range Technology.We thank you for your services.";
                }
                MailConst.senderMailForVenderStatus(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person,message);
            }
            else
            {
                sms = "**Company status not changed**";
            }

            return Json(sms);
        }

        [HttpPost]
        public JsonResult ActionPermissionAccVendorId(VendorRegistation objVendorRegistation)
        {
            VendorRegistation permission = new VendorRegistation();
            List<VendorRegistation> objPermission = new List<VendorRegistation>();
            DataSet ds = permission.GetActionPermissionAccVendorId(objVendorRegistation);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objPermission.Add(new VendorRegistation
                {
                    Permission_Id = Convert.ToInt32(dr["permissionId"]),
                    Permission_Name = dr["permissionName"].ToString(),
                    Is_View = Convert.ToBoolean(dr["isView"]),
                    Count = Convert.ToInt32(dr["count"])
                });
            }
            return Json(objPermission, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region//----------------------------Vendor Dashboard------------------------------
        public ActionResult Dashboard()
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
        public JsonResult VendorStatuschart()
        {
            VendorRegistation Vendor = new VendorRegistation();
            List<VendorRegistation> objVendorlist = new List<VendorRegistation>();
            DataSet ds = Vendor.GetVendorRequestStatusChart();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendorlist.Add(new VendorRegistation
                {
                    Label1 = dr["requestStatus"].ToString(),
                    Count = Convert.ToInt32(dr["Count1"])
                });

            }
            return Json(objVendorlist, JsonRequestBehavior.AllowGet);
        }
       

        [HttpGet]
        public JsonResult VendorApprovalStatuschart()
        {
            VendorRegistation Vendor = new VendorRegistation();
            List<VendorRegistation> objVendorlist = new List<VendorRegistation>();
            DataSet ds = Vendor.GetVendorApprovalStatusChart();

             try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        Vendor.Approved_Count = Convert.ToInt32(dr["approved"]);
                        Vendor.Pending_Count = Convert.ToInt32(dr["pending"]);


                    }
                }
            }
             catch (Exception ex) { }
           

           
            return Json(Vendor, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region//------------------------------Stand by-----------------------------------

        public ActionResult StandBy()
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
        public JsonResult StandbyVendorDetailsAccSearchValue(VendorRegistation objVendorRegistation)
        {
            VendorRegistation VendorDetaillist = new VendorRegistation();
            List<VendorRegistation> objVendorDetaillist = new List<VendorRegistation>();
            if (objVendorRegistation.Vendor_Category_Name == "All")
            {
                objVendorRegistation.Vendor_Category_Name = "";
            }
            if (objVendorRegistation.Vendor_Status == "All")
            {
                objVendorRegistation.Vendor_Status = "";
            }
            DataSet ds = VendorDetaillist.GetStandbyVenderDetailAccSearchValues(objVendorRegistation);

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                VendorRegistation vendor = new VendorRegistation();
                vendor.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendorId"]);
                vendor.Contact_Person = ds.Tables[0].Rows[i]["vendorSortName"].ToString();
                vendor.Vendor_Company_Name = ds.Tables[0].Rows[i]["companyName"].ToString();
                vendor.Vendor_company_GST = ds.Tables[0].Rows[i]["companyGST"].ToString();
                vendor.Vendor_Category_Name = ds.Tables[0].Rows[i]["vendorcategoryName"].ToString();
                vendor.Vendor_Status = ds.Tables[0].Rows[i]["companyStatus"].ToString();
                vendor.Vendor_Email_ID = ds.Tables[0].Rows[i]["vendorEmail"].ToString();
                objVendorDetaillist.Add(vendor);
            }

            return Json(objVendorDetaillist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult StandbyVendorTopTenForSearch()
        {
            VendorRegistation VendorDetaillist = new VendorRegistation();
            List<VendorRegistation> objVendorDetaillist = new List<VendorRegistation>();

            DataSet ds = VendorDetaillist.GetStandbyVenderTopTenDetailForSearch();

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                VendorRegistation vendor = new VendorRegistation();
                vendor.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendorId"]);
                vendor.Contact_Person = ds.Tables[0].Rows[i]["vendorSortName"].ToString();
                vendor.Vendor_Company_Name = ds.Tables[0].Rows[i]["companyName"].ToString();
                vendor.Vendor_company_GST = ds.Tables[0].Rows[i]["companyGST"].ToString();
                vendor.Vendor_Category_Name = ds.Tables[0].Rows[i]["vendorcategoryName"].ToString();
                vendor.Vendor_Status = ds.Tables[0].Rows[i]["companyStatus"].ToString();
                vendor.Vendor_Email_ID = ds.Tables[0].Rows[i]["vendorEmail"].ToString();
                objVendorDetaillist.Add(vendor);
            }

            return Json(objVendorDetaillist, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}