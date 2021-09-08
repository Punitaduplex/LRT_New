using LRT_MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRT_MVC_Project.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
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
               // string[] results = BarcodeLib.BarcodeReader.BarcodeReader.read("c:/qrcode-aspnet.gif", BarcodeLib.BarcodeReader.BarcodeReader.QRCODE);

                return RedirectToAction("LogIn", "User");
            }
        }
        #region//------------------------------Vendor Sub Type-----------------------------------

        public ActionResult VendorSubType()
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
        public JsonResult AddVendorSubType(VendorSubType objVendorSubType)
        {
            string sms = "";
            VendorSubType VendorSubType = new VendorSubType();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorSubType.User_Id = Convert.ToInt32(cookie.Value);
            if (objVendorSubType.Vendor_Sub_Type_Id == 0)
            {
                int i = 0;
                i = VendorSubType.InsertVendorSubType(objVendorSubType);
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
                int i = VendorSubType.UpdateVendorSubType(objVendorSubType);
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
        [HttpGet]
        public JsonResult VendorSubTypeDetails()
        {
            VendorSubType VendorSubType = new VendorSubType();
            List<VendorSubType> objSubTypeVendor = new List<VendorSubType>();
            DataSet ds = VendorSubType.GetVendorSubType(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objSubTypeVendor.Add(new VendorSubType
                {
                    Vendor_Sub_Type_Id = Convert.ToInt32(dr["vendorSubTypeId"]),
                    Vendor_Sub_Type_Name = dr["vendorSubTypeName"].ToString(),
                    Approve_Status = dr["isApproved"].ToString()

                });

            }
            return Json(objSubTypeVendor, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteVendorSubtype(VendorSubType objVendorSubType)
        {
            string sms = "";
            VendorSubType VendorSubType = new VendorSubType();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            objVendorSubType.User_Id = Convert.ToInt32(cookie.Value);

            int i = VendorSubType.DeleteVendorSubType(objVendorSubType);
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


        [HttpPost]
        public JsonResult VendorSubTypeApproveStatus(VendorSubType objVendorSubType)
        {
            string sms = "";
            VendorSubType VendorSubType = new VendorSubType();
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
            VendorSubType.User_Id = Convert.ToInt32(cookie.Value);

            int i = VendorSubType.GetVendorCategoryApproveStatus(objVendorSubType);
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
        #region//----------------------------vendor type--------------------------
        public ActionResult VendorType()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddVendorType(VendorType objVendor)
        {
            string sms = "";
            VendorType vendor = new VendorType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                //int userid = Convert.ToInt32(cookie.Value);
                objVendor.User_Id = Convert.ToInt32(cookie.Value); //Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
                if (objVendor.Vendor_Type_Id == 0)
                {
                    string s = vendor.InsertVendorType(objVendor);
                    string[] p = s.Split(',');
                    int i = Convert.ToInt32(p[0]);
                    if (i == 0)
                    {

                        //var sublist = objVendor.sublist;
                        //foreach (var c in sublist)
                        //{
                        //    VendorType vendor1 = new VendorType();
                        //    vendor1.Vendor_Type_Id = Convert.ToInt32(p[1]);
                        //    vendor1.Vendor_Sub_Type_Id = c.Vendor_Sub_Type_Id;
                        //    vendor.InsertVendorTypeDetail(vendor1);
                        //}
                        sms = "**Data inserted successfully**";
                    }
                    else
                    {
                        sms = "**Data already exist**";
                    }
                }
                else
                {
                    int i = vendor.UpdateVendorType(objVendor);
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
        public JsonResult AddBulkVendorType(VendorType objVendorType)
        {
            string sms = "";
            VendorType vendor = new VendorType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objVendorType.User_Id = Convert.ToInt32(cookie.Value);
                string[] typename = objVendorType.Vendor_Type.Split(',');

                for (int i = 0; i < typename.Length; i++)
                {
                    VendorType vendor1 = new VendorType();
                    vendor1.Vendor_Type = typename[i].ToString();

                    string j = vendor.InsertVendorType(vendor1);
                }
                sms = "**Vendor category bulk data inserted successfully**";
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult VendorTypeDetails()
        {
            VendorType vendor = new VendorType();
            List<VendorType> objVendor = new List<VendorType>();
            try
            {
                vendor.Vendor_Type_Id = 0;
                DataSet ds = vendor.GetVendorType(vendor);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    List<VendorSubType> objsub = new List<VendorSubType>();
                    VendorType vendor1 = new VendorType();
                    vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendortypeId"]);
                    vendor1.Vendor_Type = ds.Tables[0].Rows[i]["vendortypeName"].ToString();
                    vendor1.Approve_Status = ds.Tables[0].Rows[i]["isApproved"].ToString();
                    //DataSet ds1 = vendor.GetVendorSubTypeAccTypeId(vendor1);
                    //for (int i1 = 0; i1 < ds1.Tables[0].Rows.Count; i1++)
                    //{
                    //    VendorSubType sub = new VendorSubType();
                    //    sub.Vendor_Sub_Type_Id = Convert.ToInt32(ds1.Tables[0].Rows[i1]["vendorsubtypeId"]);
                    //    sub.Vendor_Sub_Type_Name = ds1.Tables[0].Rows[i1]["vendorsubtypeName"].ToString();
                    //    objsub.Add(sub);

                    //}
                    // vendor1.sublist=objsub;
                    objVendor.Add(vendor1);
                }
            }
            catch (Exception ex) { }
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorTypeForEdit(VendorType objVendorType)
        {
            
            VendorType b = new VendorType();
            VendorType Vendor1 = new VendorType();
            try
            {
                DataSet ds = b.GetVendorType(objVendorType);

               
                Vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendortypeId"]);
                Vendor1.Vendor_Type = ds.Tables[0].Rows[0]["vendortypeName"].ToString();

            }
            catch (Exception ex) { }
                return Json(Vendor1);
        }
        [HttpPost]
        public JsonResult DeleteVendorType(VendorType objVendorType)
        {
            string sms = "";
            VendorType vendor = new VendorType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objVendorType.User_Id = Convert.ToInt32(cookie.Value);

                int i = vendor.DeleteVendorType(objVendorType);
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
        [HttpPost]
        public JsonResult VendorTypeApproveStatus(VendorType objVendorType)
        {
            string sms = "";
            VendorType Vendor = new VendorType();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objVendorType.User_Id = Convert.ToInt32(cookie.Value);

                int i = Vendor.GetVendorTypeApproveStatus(objVendorType);
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
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult AddBulkVendorPermission(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission permission = new VendorPermission();
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult DeleteVendorPermission(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission permission = new VendorPermission();
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        [HttpGet]
        public JsonResult VendorPermissionDetails()
        {
            VendorPermission vendorpermission = new VendorPermission();
            List<VendorPermission> objVendorPermission = new List<VendorPermission>();
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(objVendorPermission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorPermissionForEdit(VendorPermission objVendorPermission)
        {

            VendorPermission b = new VendorPermission();
            VendorPermission Vendor1 = new VendorPermission();
            try
            {
                int type = Convert.ToInt32(objVendorPermission.Permission_Id);
                DataSet ds = b.GetVendorPermissionList(type);


                Vendor1.Permission_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["permissionId"]);
                Vendor1.Permission_Name = ds.Tables[0].Rows[0]["permissionName"].ToString();

            }
            catch (Exception ex) { }
            return Json(Vendor1);
        }
        [HttpPost]
        public JsonResult VendorPermissionApproveStatus(VendorPermission objVendorPermission)
        {
            string sms = "";
            VendorPermission vendorpermission = new VendorPermission();
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult VendorPermissionName()
        {
            List<VendorPermission> permissionlist = new List<VendorPermission>();
            VendorPermission b = new VendorPermission();
            try
            {
                DataSet ds = b.GetVendorPermissionName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    VendorPermission b1 = new VendorPermission();
                    b1.Permission_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["permissionId"]);
                    b1.Permission_Name = ds.Tables[0].Rows[i]["permissionName"].ToString();
                    permissionlist.Add(b1);
                }
            }
            catch (Exception ex) { }
            return Json(permissionlist);
        }
        [HttpPost]
        public JsonResult AddActionPermission(VendorRegistation objVendorRegistation)
        {
            string sms = "";
            int i = 0;
            int count = 0;
            VendorRegistation Permission = new VendorRegistation();
            try
            {
                Permission.Vendor_Id = objVendorRegistation.Vendor_Id;

                Permission.User_Id = Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
                if (Permission.Vendor_Id != 0)
                {
                    int i1 = Permission.DeleteVendorActionPermission(objVendorRegistation);
                    string[] Permissionstr = objVendorRegistation.Permission_Id_Str.Split(',');

                    string[] view = objVendorRegistation.Is_View_Str.Split(',');

                    for (int j = 0; j < Permissionstr.Length; j++)
                    {
                        Permission.Vendor_LogIn_Id = objVendorRegistation.Vendor_LogIn_Id;
                        Permission.Vendor_LogIn_Password = objVendorRegistation.Vendor_LogIn_Password;
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
            }
            catch (Exception ex) { }
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
            try
            {
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

                        var sublist = objVendor.Type_List;
                        foreach (var c in sublist)
                        {
                            Vendor vendor1 = new Vendor();
                            vendor1.Vendor_Invitation_Id = Convert.ToInt32(s1[1]);
                            vendor1.Vendor_Type_Id = c.Vendor_Type_Id;
                            vendor.InsertVendorInvitationDetail(vendor1);
                        }
                       
                        //MailConst.senderMailForInvitation(objVendor.Vendor_Email_ID.Trim(), objVendor.Vendor_Company_Name.Trim(), objVendor.Vendor_Contact_Person);

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
                        vendor.DeleteVendorInvitationType(objVendor);
                        var sublist = objVendor.Type_List;
                        foreach (var c in sublist)
                        {
                            Vendor vendor1 = new Vendor();
                            vendor1.Vendor_Invitation_Id = objVendor.Vendor_Invitation_Id;
                            vendor1.Vendor_Type_Id = c.Vendor_Type_Id;
                            vendor.InsertVendorInvitationDetail(vendor1);
                        }
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
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(sms);
        }
        [HttpGet]
        public JsonResult VendorInvitationDetails()
        {
            Vendor vendor = new Vendor();
            List<Vendor> objVendor = new List<Vendor>();
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult VendorInvitationForEdit(Vendor objVendor)
        {

            Vendor vendor = new Vendor();
            Vendor Vendor1 = new Vendor();
            int vendorid = Convert.ToInt32(objVendor.Vendor_Invitation_Id);
            try
            {
                DataSet ds = vendor.GetVendorInvitation(vendorid);

              
                Vendor1.Vendor_Invitation_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorinvitationId"]);
                Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["vendorCompany"].ToString();
                Vendor1.Vendor_Contact_Person = ds.Tables[0].Rows[0]["contactPerson"].ToString();
                Vendor1.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();
            }
            catch (Exception ex) { }
            return Json(Vendor1);
        }
        [HttpPost]
        public JsonResult VendorTypeAccInvitationId(Vendor objVendor)
        {

            Vendor vendor = new Vendor();
            List<Vendor> objVendorlist = new List<Vendor>();
            try
            {
                //  int vendorid = Convert.ToInt32(objVendor.Vendor_Invitation_Id);
                DataSet ds = vendor.GetVendorTypeAccVendorInvitationId(objVendor);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Vendor Vendor1 = new Vendor();
                        Vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["vendortypeId"]);
                        Vendor1.Vendor_Type = Convert.ToString(ds.Tables[0].Rows[i]["vendortypeName"]);
                        objVendorlist.Add(Vendor1);
                    }
                }
            }
            catch (Exception ex) { }
            return Json(objVendorlist);
        }
        [HttpPost]
        public JsonResult VendorInvitationApproveStatus(Vendor objVendor)
        {
            string sms = "";
            Vendor vendor = new Vendor();
            try
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userid"];
                objVendor.User_Id = Convert.ToInt32(cookie.Value);


                int i = vendor.GetVendorInvitationApproveStatus(objVendor);
                if (i == 0)
                {
                    sms = "Status changed successfully";
                    string Link = "https://duplex.leaderrange.co/Vendor/VendorInvitation";
                    MailConst.senderMail("Invitation", Link);

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
        public JsonResult VendorCompanyAccEmail(Vendor objVendor)
        {
            Vendor vendor = new Vendor();
            List<Vendor> VendorDetailList = new List<Vendor>();
            try
            {
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
            }
            catch (Exception ex) { }
            return Json(vendor, JsonRequestBehavior.AllowGet);
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

                    Label2 = dr["vendorName"].ToString(),
                    Label3 = dr["countryName"].ToString(),
                    Count = Convert.ToInt32(dr["Count1"])
                });

            }
            return Json(objVendorlist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult VendorStatusglobechart()
        {
            VendorRegistation Vendor = new VendorRegistation();
            List<VendorRegistation> objVendorlist = new List<VendorRegistation>();
            DataSet ds = Vendor.GetVendorRequestStatusChart();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendorlist.Add(new VendorRegistation
                {
                    Label1 = dr["requestStatus"].ToString(),
                    Label2 = dr["vendorName"].ToString(),
                    Label3 = dr["countryName"].ToString(),
                    Count = Convert.ToInt32(dr["Count1"])
                });

            }
            return Json(objVendorlist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region//-----------------------------Vendor Master----------------------------------
        public ActionResult VendorMaster()
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
        public JsonResult VendorName()
        {
            List<SelectListItem> Citylist = new List<SelectListItem>();
            VendorRegistation b = new VendorRegistation();
            try
            {
                DataSet ds = b.GetVendorName();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Citylist.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["companyName"].ToString()
                    });
                }
            }
            catch (Exception ex) { }
            return Json(Citylist);
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
                   
                });
            }
            return Json(objPermission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActionPermission()
        {
            VendorRegistation permission = new VendorRegistation();
            List<VendorRegistation> objPermission = new List<VendorRegistation>();
            DataSet ds = permission.GetActionPermission();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objPermission.Add(new VendorRegistation
                {
                    Permission_Id = Convert.ToInt32(dr["permissionId"]),
                    Permission_Name = dr["permissionName"].ToString(),
                    Is_View = Convert.ToBoolean(dr["isView"])
                  
                });
            }
            return Json(objPermission, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult VendorDetailForGenratepwd(VendorRegistation objVendorRegistation)
        {
            VendorRegistation vendor = new VendorRegistation();
            try
            {
                DataSet ds = vendor.GetVendorDetailForGenratePassword(objVendorRegistation);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    vendor.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                    vendor.Vendor_Name = ds.Tables[0].Rows[0]["vendorName"].ToString();
                    vendor.Vendor_Email_ID = Convert.ToString(ds.Tables[0].Rows[0]["vendorEmail"]);
                  
                }
            }
            catch (Exception ex) { }
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadCompanyprofileMethod()
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/company_profile/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/company_profile/" + name;
                            // sms = s[0] + " file uploaded successfully!+" + path;
                            sms = "file uploaded successfully!+" + path;
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
        public ActionResult UploadProductCatalogMethod()
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
                            fname = Path.Combine(Server.MapPath("~/uploaded_image/product_catalog/"), DateTime.Now.ToString("yyyyMMddmmss") + "_" + s[1] + "." + fileex);
                            string[] splitpath = fname.Split('\\');
                            string name = (splitpath[splitpath.Length - 1]);
                            file.SaveAs(fname);
                            path = "/uploaded_image/product_catalog/" + name;
                            // sms = s[0] + " file uploaded successfully!+" + path;
                            sms = "file uploaded successfully!+" + path;
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
                           // sms = s[0] + " file uploaded successfully!+" + path;
                            sms =  "file uploaded successfully!+" + path;
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
                           // sms = s[0] + " file uploaded successfully!+" + path;
                            sms = " file uploaded successfully!+" + path;
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
        public JsonResult InsertVendorRegistation(VendorRegistation objVendorRegistation)
        {
            string sms = "";
            VendorRegistation vendor = new VendorRegistation();
            objVendorRegistation.User_Id = 0;// Common.CommonSetting.User_Id;// Convert.ToInt32(Session["user_id"]);
            if (objVendorRegistation.Vendor_Id == 0)
            {
                string s = vendor.InsertVendorRegistation(objVendorRegistation);
                string[] s1 = s.Split(',');
                int i = Convert.ToInt32(s1[0]);
                if (i == 0)
                {
                    var sublist = objVendorRegistation.Type_List;
                    try
                    {
                        foreach (var c in sublist)
                        {
                            VendorRegistation vendor1 = new VendorRegistation();
                            vendor1.Vendor_Id = Convert.ToInt32(s1[1]);
                            vendor1.Vendor_Type_Id = c.Vendor_Type_Id;
                            vendor.InsertVendorRegistationType(vendor1);
                        }
                    }
                    catch (Exception ex) { }
                    string[] machineName = objVendorRegistation.Machine_Name.Split(',');
                    string[] machineFunction = objVendorRegistation.Machine_Function.Split(',');
                    string[] machineCapability = objVendorRegistation.Machine_Capability.Split(',');
                    string[] machineAddition = objVendorRegistation.Machine_Addition.Split(',');
                    for (int j = 0; j < machineName.Length; j++)
                    {
                        VendorRegistation machine = new VendorRegistation();
                        machine.Vendor_Id = Convert.ToInt32(s1[1]);
                        machine.Machine_Name = machineName[j].ToString();
                        machine.Machine_Function = machineFunction[j].ToString();
                        machine.Machine_Capability = machineCapability[j].ToString();
                        machine.Machine_Addition = machineAddition[j].ToString();
                        machine.InsertVendorRegistationMachine(machine);
                    }
                        //MailConst.senderMailForRegistration(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person);
                        // SendEmailToVendorForRegistration(objVendorRegistation.Vendor_Email_ID, objVendorRegistation.Vendor_Company_Name, objVendorRegistation.Contact_Person);
                        sms = s1[1] + ",**save sucessfully**";
                }
                else
                {
                    sms = "**Vendor request already exist**";
                }
            }
            else
            {
                  int i = vendor.UpdateVendorRegistation(objVendorRegistation);
              
                //int i = Convert.ToInt32(s1[0]);
                if (i == 0)
                {
                    vendor.DeleteVendorRegistrationType(objVendorRegistation);
                    vendor.DeleteVendorRegistrationMachine(objVendorRegistation);
                    var sublist = objVendorRegistation.Type_List;
                    try
                    {
                        foreach (var c in sublist)
                        {
                            VendorRegistation vendor1 = new VendorRegistation();
                            vendor1.Vendor_Id = objVendorRegistation.Vendor_Id;
                            vendor1.Vendor_Type_Id = c.Vendor_Type_Id;
                            vendor.InsertVendorRegistationType(vendor1);
                        }
                    }
                    catch (Exception ex) { }
                    string[] machineName = objVendorRegistation.Machine_Name.Split(',');
                    string[] machineFunction = objVendorRegistation.Machine_Function.Split(',');
                    string[] machineCapability = objVendorRegistation.Machine_Capability.Split(',');
                    string[] machineAddition = objVendorRegistation.Machine_Addition.Split(',');
                    for (int j = 0; j < machineName.Length; j++)
                    {
                        VendorRegistation machine = new VendorRegistation();
                        machine.Vendor_Id = objVendorRegistation.Vendor_Id;
                        machine.Machine_Name = machineName[j].ToString();
                        machine.Machine_Function = machineFunction[j].ToString();
                        machine.Machine_Capability = machineCapability[j].ToString();
                        machine.Machine_Addition = machineAddition[j].ToString();
                        machine.InsertVendorRegistationMachine(machine);
                    }
                    sms = "*save sucessfully**";
                }
            }
            return Json(sms);
        }
        [HttpPost]
        public JsonResult VendorRegistrationForEdit(VendorRegistation objVendor)
        {

            VendorRegistation vendor = new VendorRegistation();
            VendorRegistation Vendor1 = new VendorRegistation();
            
            try
            {
                DataSet ds = vendor.GetVendorDetailAccVendorId(objVendor);


                Vendor1.Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorId"]);
                Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["registrationStatus"].ToString();
                Vendor1.Company_Registration_No = ds.Tables[0].Rows[0]["companyregistrationNo"].ToString();
                Vendor1.Contact_Person = ds.Tables[0].Rows[0]["vendorSortName"].ToString();
                Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                Vendor1.Vendor_Company_Address = ds.Tables[0].Rows[0]["companyAddress"].ToString();
                Vendor1.Pin_Code = ds.Tables[0].Rows[0]["companypinCode"].ToString();
                Vendor1.Country_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["companycountryId"]);
                Vendor1.State_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["companystateId"]);
                Vendor1.City_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["companycityId"]);
                Vendor1.Vendor_company_Telephone = ds.Tables[0].Rows[0]["companyTeliphone"].ToString();
                Vendor1.Vendor_company_FaxNo = ds.Tables[0].Rows[0]["companyFaxNo"].ToString();
                Vendor1.Vendor_company_MobileNo = ds.Tables[0].Rows[0]["companyMobileNo"].ToString();
                Vendor1.Vendor_company_Attn = ds.Tables[0].Rows[0]["companyalternanteNo"].ToString();
                Vendor1.Vendor_company_GST = ds.Tables[0].Rows[0]["companyGST"].ToString();
                Vendor1.Vendor_company_SST = ds.Tables[0].Rows[0]["companySST"].ToString();
                Vendor1.Payment_Term_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["paymentTermId"]);
                Vendor1.Currency_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["currencyId"]);
                Vendor1.Vendor_Region = ds.Tables[0].Rows[0]["companyRegion"].ToString();
                Vendor1.Credit_Limit_Type = ds.Tables[0].Rows[0]["creditlimitType"].ToString();
                Vendor1.Website_Link = ds.Tables[0].Rows[0]["websiteLink"].ToString();
                Vendor1.Facebook_link = ds.Tables[0].Rows[0]["facebookLink"].ToString();
                Vendor1.Twitter_link = ds.Tables[0].Rows[0]["twitterlink"].ToString();
                Vendor1.From_Currency = ds.Tables[0].Rows[0]["fromCurrency"].ToString();
                Vendor1.To_Currency = Convert.ToString(ds.Tables[0].Rows[0]["toCurrency"]);
                Vendor1.From_Currency_Amt = Convert.ToString(ds.Tables[0].Rows[0]["fromcurrencyAmt"]);
                Vendor1.To_Currency_Amt = ds.Tables[0].Rows[0]["tocurrencyAmt"].ToString();
                Vendor1.Company_Profile_Type = ds.Tables[0].Rows[0]["companyprofileType"].ToString();
                Vendor1.Company_Profile_Url = ds.Tables[0].Rows[0]["companyprofileLink"].ToString();
                Vendor1.Catalog_Type = ds.Tables[0].Rows[0]["catelogType"].ToString();
                Vendor1.Catelog_Link = ds.Tables[0].Rows[0]["catelogLink"].ToString();
                Vendor1.Vendor_Work_Sample_Url = ds.Tables[0].Rows[0]["companyWorksampleUrl"].ToString();
                Vendor1.NDA_Form_Url = Convert.ToString(ds.Tables[0].Rows[0]["ndaformUrl"]);
                Vendor1.Form_24 = Convert.ToString(ds.Tables[0].Rows[0]["form24"]);
                Vendor1.Form_48 = ds.Tables[0].Rows[0]["form48"].ToString();
                Vendor1.Form_9 = ds.Tables[0].Rows[0]["form9"].ToString();
                Vendor1.Other_Form = ds.Tables[0].Rows[0]["otherform"].ToString();
                Vendor1.Vendor_Longitude = ds.Tables[0].Rows[0]["locationLongitude"].ToString();
                Vendor1.Vendor_Latitude = ds.Tables[0].Rows[0]["locationLatitude"].ToString();
                Vendor1.Primary_Contact = ds.Tables[0].Rows[0]["primaryContect"].ToString();
                Vendor1.Vendor_First_Name = Convert.ToString(ds.Tables[0].Rows[0]["vendorfirstName"]);
                Vendor1.Vendor_Last_Name = Convert.ToString(ds.Tables[0].Rows[0]["vendorlastName"]);
                Vendor1.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();
                Vendor1.Alt_Vendor_Name1 = ds.Tables[0].Rows[0]["altvendorname1"].ToString();
                Vendor1.Alt_Vendor_Email1 = ds.Tables[0].Rows[0]["altvendorEmail1"].ToString();
                Vendor1.Alt_Vendor_Name2 = ds.Tables[0].Rows[0]["altvendorname2"].ToString();
                Vendor1.Alt_Vendor_Email2 = ds.Tables[0].Rows[0]["altvendorEmail2"].ToString();
                Vendor1.Vendor_Telephone_Number_1 = ds.Tables[0].Rows[0]["vendorteliphoneNo1"].ToString();
                Vendor1.Vendor_Address = Convert.ToString(ds.Tables[0].Rows[0]["vendoraddress"]);
                Vendor1.Vendor_Pin_Code = Convert.ToString(ds.Tables[0].Rows[0]["vendorpinCode"]);
                Vendor1.Vendor_City_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorcityId"]);
                Vendor1.Vendor_Country_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorcountryId"]);
                Vendor1.Vendor_Bank_Type = Convert.ToString(ds.Tables[0].Rows[0]["bankdetailType"]);
                Vendor1.Bank_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["bankId"]);
                Vendor1.Vendor_Swift_Code = ds.Tables[0].Rows[0]["swiftCode"].ToString();
                Vendor1.Vendor_Account_Number = ds.Tables[0].Rows[0]["accountNo"].ToString();
                Vendor1.Vendor_Receiver_Name = ds.Tables[0].Rows[0]["reciverName"].ToString();
                Vendor1.Vendor_Bank_Address = Convert.ToString(ds.Tables[0].Rows[0]["bankAddress"]);
                Vendor1.Vendor_Bank_Postal_Code= Convert.ToString(ds.Tables[0].Rows[0]["postalCode"]);
                Vendor1.Bank_Country_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["bankcountryId"]);
                Vendor1.Bank_State_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["bankstateId"]);
                Vendor1.Bank_City_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["bankcityId"]);

                Vendor1.Bank_Contact_Number_1 = ds.Tables[0].Rows[0]["bankContactNo1"].ToString();
                Vendor1.Bank_Contact_Number_2 = ds.Tables[0].Rows[0]["bankContactNo2"].ToString();
                Vendor1.Paypal_Email_Id = ds.Tables[0].Rows[0]["paypalemailId"].ToString();
                Vendor1.Vendor_Status = Convert.ToString(ds.Tables[0].Rows[0]["companyStatus"]);
                Vendor1.Request_Status = Convert.ToString(ds.Tables[0].Rows[0]["requestStatus"]);
                Vendor1.Save_Data_Type = Convert.ToString(ds.Tables[0].Rows[0]["datasaveType"]);
            }
            catch (Exception ex) { }
            return Json(Vendor1);
        }

        public ActionResult ListedVendor()
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
        public JsonResult ListedVendorDetails()
        {
            Vendor vendor = new Vendor();
            List<Vendor> objVendor = new List<Vendor>();
            DataSet ds = vendor.GetVendorListeddtls();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendor.Add(new Vendor
                {
                    Vendor_Name = (dr["vendorfirstName"].ToString()),
                    Vendor_Company_Name = dr["companyName"].ToString(),

                    Vendor_Contact_Person = dr["vendorteliphoneNo1"].ToString(),
                    Vendor_Email_ID = dr["vendorEmail"].ToString(),


                });

            }
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListedVendorName()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Vendor vb = new Vendor();
            try
            {
                DataSet ds = vb.GetVendorListeddtls();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["vendorfirstName"].ToString(),
                        
                        
                    });
                }
            }
            catch (Exception ex) { }
            return Json(list);
        }

        //------------------------------------Pending--------------------------------------------------------------



        [HttpPost]
        public JsonResult PendingVendorName()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Vendor vb = new Vendor();
            try
            {
                DataSet ds = vb.GetpendingVendorListeddtls();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["vendorfirstName"].ToString(),


                    });
                }
            }
            catch (Exception ex) { }
            return Json(list);
        }



        [HttpPost]
        public JsonResult PendingListedVendorCompanyName()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Vendor vb = new Vendor();
            try
            {
                DataSet ds = vb.GetpendingVendorListeddtls();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["companyName"].ToString(),


                    });
                }
            }
            catch (Exception ex) { }
            return Json(list);
        }



        [HttpPost]
        public JsonResult PendingListedVendorvendorEmail()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Vendor vb = new Vendor();
            try
            {
                DataSet ds = vb.GetpendingVendorListeddtls();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["vendorEmail"].ToString(),


                    });
                }
            }
            catch (Exception ex) { }
            return Json(list);
        }
        [HttpPost]
        public JsonResult PendingListedVendorWorkPhone()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Vendor vb = new Vendor();
            try
            {
                DataSet ds = vb.GetpendingVendorListeddtls();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["vendorteliphoneNo1"].ToString(),


                    });
                }
            }
            catch (Exception ex) { }
            return Json(list);
        }


        //------------------------------------end Pending--------------------------------------------------------------
         [HttpPost]
        public JsonResult ListedVendorCompanyName()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Vendor vb = new Vendor();
            try
            {
                DataSet ds = vb.GetVendorListeddtls();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new SelectListItem
                    {
                        Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                        Text = ds.Tables[0].Rows[i]["companyName"].ToString(),


                    });
                }
            }
            catch (Exception ex) { }
            return Json(list);
        }

         [HttpPost]
         public JsonResult ListedVendorvendorEmail()
         {
             List<SelectListItem> list = new List<SelectListItem>();
             Vendor vb = new Vendor();
             try
             {
                 DataSet ds = vb.GetVendorListeddtls();
                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {
                     list.Add(new SelectListItem
                     {
                         Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                         Text = ds.Tables[0].Rows[i]["vendorEmail"].ToString(),


                     });
                 }
             }
             catch (Exception ex) { }
             return Json(list);
         }



         [HttpPost]
         public JsonResult ListedVendorWorkPhone()
         {
             List<SelectListItem> list = new List<SelectListItem>();
             Vendor vb = new Vendor();
             try
             {
                 DataSet ds = vb.GetVendorListeddtls();
                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {
                     list.Add(new SelectListItem
                     {
                         Value = ds.Tables[0].Rows[i]["vendorId"].ToString(),
                         Text = ds.Tables[0].Rows[i]["vendorteliphoneNo1"].ToString(),


                     });
                 }
             }
             catch (Exception ex) { }
             return Json(list);
         }
        public ActionResult PendingVendor()
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
        public JsonResult PendingVendorTypeDetails()
        {
            Vendor vendor = new Vendor();
            List<Vendor> objVendor = new List<Vendor>();
            DataSet ds = vendor.GetPendingVendorListdtls();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objVendor.Add(new Vendor
                {
                    Vendor_Name = (dr["vendorfirstName"].ToString()),
                    Vendor_Company_Name = dr["companyName"].ToString(),

                    Vendor_Contact_Person = dr["vendorteliphoneNo1"].ToString(),
                    Vendor_Email_ID = dr["vendorEmail"].ToString(),


                });

            }
            return Json(objVendor, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult SearchListedVendor(Vendor objlistedvendor)
        {
           
            Vendor b = new Vendor();
            Vendor Vendor1 = new Vendor();
          
            try
            {
                DataSet ds = Vendor1.GetVendorListed_dtls_BySearch(objlistedvendor);

                 
                    Vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorid"]);
                    Vendor1.Vendor_Name = ds.Tables[0].Rows[0]["vendorfirstName"].ToString();
                    Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                    Vendor1.Vendor_Contact_Person = ds.Tables[0].Rows[0]["vendorteliphoneNo1"].ToString();
                    Vendor1.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();
               
                 
            }
            catch (Exception ex)
            { 
            }
            return Json(Vendor1);
        }

        [HttpPost]
         
        public JsonResult SearchListedVendorbysearch(Vendor objlistedvendor)
        {

            Vendor vendor = new Vendor();
            List<Vendor> objVendorlist = new List<Vendor>();
            try
            {
                 
                DataSet ds = vendor.GetVendorListed_dtls_BySearch(objlistedvendor);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Vendor Vendor1 = new Vendor();
                         Vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorid"]);
                Vendor1.Vendor_Name = ds.Tables[0].Rows[0]["vendorfirstName"].ToString();
                Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                Vendor1.Vendor_Contact_Person = ds.Tables[0].Rows[0]["vendorteliphoneNo1"].ToString();
                Vendor1.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();

                        objVendorlist.Add(Vendor1);
                    }
                }
            }
            catch (Exception ex) { }
            return Json(objVendorlist);
        }



        //--------------------------------pending----------------------------------

        [HttpPost]
        public JsonResult SearchpendingListedVendor(Vendor objlistedvendor)
        {

            Vendor b = new Vendor();
            Vendor Vendor1 = new Vendor();

            try
            {
                DataSet ds = Vendor1.GetpendingVendorListed_dtls_BySearch(objlistedvendor);


                Vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorid"]);
                Vendor1.Vendor_Name = ds.Tables[0].Rows[0]["vendorfirstName"].ToString();
                Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                Vendor1.Vendor_Contact_Person = ds.Tables[0].Rows[0]["vendorteliphoneNo1"].ToString();
                Vendor1.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();


            }
            catch (Exception ex)
            {
            }
            return Json(Vendor1);
        }

        [HttpPost]

        public JsonResult SearchpendingListedVendorbysearch(Vendor objlistedvendor)
        {

            Vendor vendor = new Vendor();
            List<Vendor> objVendorlist = new List<Vendor>();
            try
            {

                DataSet ds = vendor.GetpendingVendorListed_dtls_BySearch(objlistedvendor);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Vendor Vendor1 = new Vendor();
                        Vendor1.Vendor_Type_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["vendorid"]);
                        Vendor1.Vendor_Name = ds.Tables[0].Rows[0]["vendorfirstName"].ToString();
                        Vendor1.Vendor_Company_Name = ds.Tables[0].Rows[0]["companyName"].ToString();
                        Vendor1.Vendor_Contact_Person = ds.Tables[0].Rows[0]["vendorteliphoneNo1"].ToString();
                        Vendor1.Vendor_Email_ID = ds.Tables[0].Rows[0]["vendorEmail"].ToString();

                        objVendorlist.Add(Vendor1);
                    }
                }
            }
            catch (Exception ex) { }
            return Json(objVendorlist);
        }

        #endregion
    }
}