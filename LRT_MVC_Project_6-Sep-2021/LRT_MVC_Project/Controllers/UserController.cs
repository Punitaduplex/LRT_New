using LRT_MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LRT_MVC_Project.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        #region//-----log in-----------------------
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckLogin(UserRegistration objUserRegistration)
        {
            UserRegistration user = new UserRegistration();
            //string Userid = "admin@gmail.com";
            //string PWD = "admin";
            int i = 0;
            try
            {
                i = user.CheckUserLogIn(objUserRegistration);
                DataSet ds = user.GetUserDetailForSession(objUserRegistration);
                if (i == 1)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Common.CommonSetting.User_Id = Convert.ToInt32(dr["newuserId"]);
                        Common.CommonSetting.name = dr["Name1"].ToString();
                        Common.CommonSetting.User_Email = dr["useremail"].ToString();
                       // HttpContext.Session.SetString("name", "Jignesh Trivedi"); 
                        HttpCookie cookie = new HttpCookie("userid");
                        cookie.Value = Convert.ToString(dr["newuserId"]);
                        cookie.Expires = DateTime.Now.AddSeconds(5000);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                        HttpCookie cookie1 = new HttpCookie("username");
                        cookie1.Value = Convert.ToString(dr["Name1"].ToString());
                        cookie1.Expires = DateTime.Now.AddSeconds(5000);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie1);


                    }
                    string sourceIP = Request.UserHostName;
                    string browser = (Request.UserAgent.ToString().Contains("Chrome")) ? "Google Chrome" : Request.Browser.Browser.ToString();
                    browser = browser + " " + Request.Browser.Version;
                    string plateform = Request.Browser.Platform;
                    //System.Net.IPAddress[] strClientIPAddress = System.Net.Dns.GetHostAddresses(Environment.MachineName);
                    //string ip = strClientIPAddress[2].ToString();
                    string userip = Request.UserHostAddress;
                    string ipaddress;
                    ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = Request.ServerVariables["REMOTE_ADDR"];


                    string strDNS = Dns.GetHostName();
                    UserSession Session = new UserSession();
                    Session.InsertUserSession(Convert.ToDateTime(System.DateTime.Now), Common.CommonSetting.User_Id, userip, "Active", browser, plateform);

                }
            }
            catch (Exception ex) { }
            return Json(i);
        }


        public ActionResult LogOut()
        {
            int i = 0;
            try
            {
                string sourceIP = Request.UserHostName;
                string browser = (Request.UserAgent.ToString().Contains("Chrome")) ? "Google Chrome" : Request.Browser.Browser.ToString();
                browser = browser + " " + Request.Browser.Version;
                string plateform = Request.Browser.Platform;
                System.Net.IPAddress[] strClientIPAddress = System.Net.Dns.GetHostAddresses(Environment.MachineName);
                string ip = strClientIPAddress[3].ToString();
                //string ip = "";
                string strDNS = Dns.GetHostName();
                UserSession session = new UserSession();
                session.UpdateUserSession(Common.CommonSetting.User_Session_Id, Common.CommonSetting.User_Id, ip, "Active", browser, plateform, Convert.ToDateTime(System.DateTime.Now), Convert.ToDateTime(System.DateTime.Now));
               
            }
            catch (Exception ex) { }
            FClear();
            this.ControllerContext.HttpContext.Request.Cookies.Clear();
            return View("LogIn");
        }

        public void FClear()
        {
            Common.CommonSetting.User_Id = 0;
            Common.CommonSetting.User_Session_Id = 0;
            Common.CommonSetting.User_Email = "";
            Common.CommonSetting.name = "";
        }
        public ActionResult TempLogOut()
        {
            int i = 0;
            try
            {
                string sourceIP = Request.UserHostName;
                string browser = (Request.UserAgent.ToString().Contains("Chrome")) ? "Google Chrome" : Request.Browser.Browser.ToString();
                browser = browser + " " + Request.Browser.Version;
                string plateform = Request.Browser.Platform;
                System.Net.IPAddress[] strClientIPAddress = System.Net.Dns.GetHostAddresses(Environment.MachineName);
                string ip = strClientIPAddress[3].ToString();
                //string ip = "";
                string strDNS = Dns.GetHostName();
                UserSession session = new UserSession();
                session.UpdateUserSession(Common.CommonSetting.User_Session_Id, Common.CommonSetting.User_Id, ip, "Active", browser, plateform, Convert.ToDateTime(System.DateTime.Now), Convert.ToDateTime(System.DateTime.Now));
                FClear();
            }
            catch (Exception ex) { }
            return View("LogIn");
        }

        #endregion

        #region //--------------- ResetPassword  -----------------------
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddPassword(UserRegistration objUserRegistration)
        {
            string sms = "";
            UserRegistration resetpassword = new UserRegistration();
            try
            {
                objUserRegistration.User_Id = Common.CommonSetting.User_Id;
                int i = resetpassword.UpdateUserPassword(objUserRegistration);
                if (i == 0)
                {
                    sms = "**Password updated successfully**";
                }
                else if (i == 1)
                {
                    sms = "**Please insert correct current password**";
                }
                else if (i == 2)
                {
                    sms = "**This is not your current password**";
                }
                else if (i == 3)
                {
                    sms = "**This password is alredy exist**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        #endregion

        #region //--------------- Forget Password Report -----------------------
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendPasswordInMail(UserRegistration objforgetpassword)
        {
            string sms = "";
            UserRegistration userregistration = new UserRegistration();
            try
            {
                userregistration.User_Id = 0;
                string userpass = (userregistration.GetForgetPassword(objforgetpassword.Email_ID)).ToString();
                SendPasswordInEmail(objforgetpassword.Email_ID, userpass);
                if (userpass != null)
                {
                    sms = "**Password Sent successfully in Your Mail ID**";
                }
            }
            catch (Exception ex) { }
            return Json(sms);
        }

        public void SendPasswordInEmail(string emailId, string password)
        {
            try
            {
                var fromMail = new MailAddress("developduplex@gmail.com", "LRT Admin"); // set your email  
                var fromEmailpassword = "Hello@2020"; // Set your password   
                var toEmail = new MailAddress(emailId);
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);
                var Message = new MailMessage(fromMail, toEmail);

                Message.Subject = "LRT Team Retrive Your Password Notifiaction";
                Message.Body = "<br/> Dear User.";
                Message.Body += "<br/> Don't share your LRT Access password with anyone";
                Message.Body += "<br/> <table cellpadding='0' cellspacing='0' width='1040' align='center' border='1'>";
                Message.Body += "<tr><td> Your UserId : " + emailId + "</td></tr>";
                Message.Body += "<tr><td> Your Password : <span style='color:#15c;'>" + password + "</span></td></tr></table>";
                Message.Body += "<br/>Go Through the Login And change Your Password.";
                Message.Body += "<br/><br/><a href='https://testing2.leaderrange.co/User/LogIn?emailid+" + emailId + "&pass=" + password + "'>LRTPanel Login Click</a>";
                Message.Body += "<br/> Thanks! ";
                Message.Body += "<br/> LRT Admin ";

                Message.IsBodyHtml = true;
                smtp.Send(Message);
            }
            catch (Exception ex) { }
        }
        #endregion 
    }
}