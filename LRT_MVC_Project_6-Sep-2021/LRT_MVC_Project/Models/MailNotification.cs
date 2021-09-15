using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace LRT_MVC_Project.Models
{
    public class MailNotification
    {
        public static string Username = "it_test@leaderrange.com";
        public static string Password = "Lrt6425755";
        public const string SmtpServer = "mail.leaderrange.com";

        #region//----------------------------mail for Invitation vendor---------------------------------------------------
        public static void senderMailForInvitation(string Tovendor, string companyname, string contactperson)
        {
            //System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            string Message;
            // Message.Subject = "LRT Vender Invitation Notifiaction";#EE0000-red
            string subject = "LRT Vender Invitation Notifiaction To " + companyname;

            Message = "<br/> ";
            Message += "<div style='background-color:#525252;width:650px;padding-bottom: 5px;padding-top: 5px;'>";
            Message += " <div style='background-color:#6D6D6D;margin-left: 10px;margin-right: 10px;border-top: 1px solid #A4A4A4;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4'><div style='position:relative;height:100px;text-align:center'><img src='cid:01' style='padding-top:10px'></div></div>";
            Message += "<div style='margin-left: 10px;margin-right: 10px;padding-bottom: 10px;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4;padding-top: 40px'><div style='margin-left: 35px;margin-right: 10px;'><img src='cid:02'><span style='FONT-WEIGHT:bold;font-size:20px;color:white;'>&nbsp;Vendor&nbsp;Invitation</span> ";
            Message += "<br/> <hr style='border-top:1px solid #CCCCCC;margin-left: 10px;margin-right: 20px'/>";
            // Message.Body += "<br/><span style='color:white;padding-left:16px;'>Hello&nbsp;'" + companyname + "'</span>";
            Message += "<span style='color:WHITE;padding-left:16px;font-size: 14px;'>Hello&nbsp;'" + contactperson + "'</span>";
            Message += "<br/>";
            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;font-weight: 600; padding-right: 10px;'>Kindly Click On the below image for registration form</span>";
            Message += "<br/><a href='https://testing2.leaderrange.co/Vendor/VendorInvitation' target='_blank'><img src='cid:03' style='padding-top:10px'/></a>";
            // Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 14px;font-weight: 600;    padding-right: 61px;'>LINK</span><span style='color:white;font-size:14px'>:</span><span style='color:white;padding-left:16px;font-size: 14px;font-weight: 600;color:white'><a style='color:white' href='" + Link + "'>" + Link + "</a> </span> ";
            Message += "<br/><br/><br/>";

            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;'>This is system generated email kindly do not reply.</span> ";
            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;'>Admin must contact the user if any issue with the changes made.</span><img src='cid:04'> ";
            Message += "<br/><br/>";
            Message += "<br/><br/>";
            Message += "<br/><br/>";
            Message += "<br/><span style='color:#CCCCCC;padding-left:16px;FONT-WEIGHT: 600;'>System Sent Date And Time:'" + System.DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss tt") + "'</span> </div> ";
            Message += "<br/></div>";
            Message += " <div style='background-color:#EE0000;border-bottom: 1px solid #a4a4a4;margin-left: 10px;margin-right: 10px;height:100px;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4;'><div style='position:relative;color:#CCCCCC;padding-top: 25px;;margin-left: 125px;'>@ Leader Range Technology Sdn. Bhd. All Rights Reserved</div></div>";
            Message += "</div>";



            sendMailForInvitation(Tovendor, subject, Message);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static void sendMailForInvitation(string ToEmail1, string Subject, string EmailBody)
        {
            try
            {

                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
                service.UseDefaultCredentials = false;
                service.Credentials = new NetworkCredential(Username, Password, SmtpServer);
                //service.autodiscoverurl("it_test@leaderrange.com");
                service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                string logoImage = HttpContext.Current.Server.MapPath(@"~/Images/LRTLogo.png");
                string logoImage1 = HttpContext.Current.Server.MapPath(@"~/Images/reg_vendor.png");
                string logoImage2 = HttpContext.Current.Server.MapPath(@"~/Images/newvendorreg.png");
                string logoImage3 = HttpContext.Current.Server.MapPath(@"~/Images/chatlogo1.png");
                //AlternateView view = AlternateView.CreateAlternateViewFromString(EmailBody, null, MediaTypeNames.Text.Html);
                //  LinkedResource resource = new LinkedResource(logoImage);
                //  resource.ContentId = "01";  
                // view.LinkedResources.Add(resource);

                service.Timeout = 200000;
                EmailMessage msg = new EmailMessage(service);

                FileAttachment attachment = msg.Attachments.AddFileAttachment(logoImage);

                attachment.ContentId = "01"; // this should be unique - say a GUID

                FileAttachment attachment2 = msg.Attachments.AddFileAttachment(logoImage1);
                attachment2.ContentId = "02";
                FileAttachment attachment3 = msg.Attachments.AddFileAttachment(logoImage2);
                attachment3.ContentId = "03";
                FileAttachment attachment4 = msg.Attachments.AddFileAttachment(logoImage3);
                attachment4.ContentId = "04";
                msg.Subject = Subject;

                msg.Body = new MessageBody(BodyType.HTML, EmailBody);

                msg.ToRecipients.Add(new EmailAddress(ToEmail1));

                msg.Send();
            }
            catch (Exception ex) { }
        }
        #endregion


        #region//----------------------------mail for vendor rEGISTRATION---------------------------------------------------
        public static void senderMailForRegistration(string Tovendor, string companyname, string contactperson)
        {
            //System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            string Message;
            // Message.Subject = "LRT Vender Invitation Notifiaction";#EE0000-red
            string subject = "LRT Vender Registration Notifiaction To " + companyname;

            Message = "<br/> ";
            Message += "<div style='background-color:#525252;width:650px;padding-bottom: 5px;padding-top: 5px;'>";
            Message += " <div style='background-color:#6D6D6D;margin-left: 10px;margin-right: 10px;border-top: 1px solid #A4A4A4;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4'><div style='position:relative;height:100px;text-align:center'><img src='cid:01' style='padding-top:10px'></div></div>";
            Message += "<div style='margin-left: 10px;margin-right: 10px;padding-bottom: 10px;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4;padding-top: 40px'><div style='margin-left: 35px;margin-right: 10px;'><img src='cid:02'><span style='FONT-WEIGHT:bold;font-size:20px;color:white;'>&nbsp;Vendor Registration &nbsp;Notification</span> ";
            Message += "<br/> <hr style='border-top:1px solid #CCCCCC;margin-left: 10px;margin-right: 20px'/>";
            // Message.Body += "<br/><span style='color:white;padding-left:16px;'>Hello&nbsp;'" + companyname + "'</span>";
            Message += "<span style='color:WHITE;padding-left:16px;font-size: 14px;'>Hello&nbsp;'" + contactperson + "'</span>";
            Message += "<br/>";
            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;font-weight: 600; padding-right: 10px;'>Your resignation has completed successfully</span>";
            //Message += "<br/><a href='https://testing2.leaderrange.co/Vendor/VendorMaster' target='_blank'><img src='cid:03' style='padding-top:10px'/></a>";
            // Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 14px;font-weight: 600;    padding-right: 61px;'>LINK</span><span style='color:white;font-size:14px'>:</span><span style='color:white;padding-left:16px;font-size: 14px;font-weight: 600;color:white'><a style='color:white' href='" + Link + "'>" + Link + "</a> </span> ";
            Message += "<br/><br/><br/>";

            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;'>This is system generated email kindly do not reply.</span> ";
            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;'>Admin must contact the user if any issue with the changes made.</span><img src='cid:04'> ";
            Message += "<br/><br/>";
            Message += "<br/><br/>";
            Message += "<br/><br/>";
            Message += "<br/><span style='color:#CCCCCC;padding-left:16px;FONT-WEIGHT: 600;'>System Sent Date And Time:'" + System.DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss tt") + "'</span> </div> ";
            Message += "<br/></div>";
            Message += " <div style='background-color:#EE0000;border-bottom: 1px solid #a4a4a4;margin-left: 10px;margin-right: 10px;height:100px;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4;'><div style='position:relative;color:#CCCCCC;padding-top: 25px;;margin-left: 125px;'>@ Leader Range Technology Sdn. Bhd. All Rights Reserved</div></div>";
            Message += "</div>";



            sendMailForRegistration(Tovendor, subject, Message);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static void sendMailForRegistration(string ToEmail, string Subject, string EmailBody)
        {
            try
            {

                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
                service.UseDefaultCredentials = false;
                service.Credentials = new NetworkCredential(Username, Password, SmtpServer);
                //service.autodiscoverurl("it_test@leaderrange.com");
                service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                string logoImage = HttpContext.Current.Server.MapPath(@"~/Images/LRTLogo.png");
                string logoImage1 = HttpContext.Current.Server.MapPath(@"~/Images/vendor_notification.png");
                // string logoImage2 = HttpContext.Current.Server.MapPath(@"~/Images/newvendorreg.png");
                string logoImage3 = HttpContext.Current.Server.MapPath(@"~/Images/chatlogo1.png");
                //AlternateView view = AlternateView.CreateAlternateViewFromString(EmailBody, null, MediaTypeNames.Text.Html);
                //  LinkedResource resource = new LinkedResource(logoImage);
                //  resource.ContentId = "01";  
                // view.LinkedResources.Add(resource);

                service.Timeout = 200000;
                EmailMessage msg = new EmailMessage(service);

                FileAttachment attachment = msg.Attachments.AddFileAttachment(logoImage);

                attachment.ContentId = "01"; // this should be unique - say a GUID

                FileAttachment attachment2 = msg.Attachments.AddFileAttachment(logoImage1);
                attachment2.ContentId = "02";
                // FileAttachment attachment3 = msg.Attachments.AddFileAttachment(logoImage2);
                // attachment3.ContentId = "03";
                FileAttachment attachment4 = msg.Attachments.AddFileAttachment(logoImage3);
                attachment4.ContentId = "04";
                msg.Subject = Subject;

                msg.Body = new MessageBody(BodyType.HTML, EmailBody);

                msg.ToRecipients.Add(new EmailAddress(ToEmail));

                msg.Send();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region//----------------------------mail for vendor status---------------------------------------------------
        public static void senderMailForVenderStatus(string Tovendor, string companyname, string contactperson, string Statusmessage)
        {
            //System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            string Message;
            // Message.Subject = "LRT Vender Invitation Notifiaction";#EE0000-red
            string subject = "LRT Vender Status Notifiaction To " + companyname;

            Message = "<br/> ";
            Message += "<div style='background-color:#525252;width:650px;padding-bottom: 5px;padding-top: 5px;'>";
            Message += " <div style='background-color:#6D6D6D;margin-left: 10px;margin-right: 10px;border-top: 1px solid #A4A4A4;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4'><div style='position:relative;height:100px;text-align:center'><img src='cid:01' style='padding-top:10px'></div></div>";
            Message += "<div style='margin-left: 10px;margin-right: 10px;padding-bottom: 10px;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4;padding-top: 40px'><div style='margin-left: 35px;margin-right: 10px;'><img src='cid:02'><span style='FONT-WEIGHT:bold;font-size:20px;color:white;'>&nbsp;Status&nbsp;Notification</span> ";
            Message += "<br/> <hr style='border-top:1px solid #CCCCCC;margin-left: 10px;margin-right: 20px'/>";
            // Message.Body += "<br/><span style='color:white;padding-left:16px;'>Hello&nbsp;'" + companyname + "'</span>";
            Message += "<span style='color:WHITE;padding-left:16px;font-size: 14px;'>Hello&nbsp;'" + contactperson + "'</span>";
            Message += "<br/>";
            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;font-weight: 600; padding-right: 10px;'>" + Statusmessage + "</span>";
            //Message += "<br/><a href='https://testing2.leaderrange.co/Vendor/VendorRequest' target='_blank'><img src='cid:03' style='padding-top:10px'/></a>";
            // Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 14px;font-weight: 600;    padding-right: 61px;'>LINK</span><span style='color:white;font-size:14px'>:</span><span style='color:white;padding-left:16px;font-size: 14px;font-weight: 600;color:white'><a style='color:white' href='" + Link + "'>" + Link + "</a> </span> ";
            Message += "<br/><br/><br/>";

            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;'>This is system generated email kindly do not reply.</span> ";
            Message += "<br/><span style='color:WHITE;padding-left:16px;font-size: 13px;'>Admin must contact the user if any issue with the changes made.</span><img src='cid:04'> ";
            Message += "<br/><br/>";
            Message += "<br/><br/>";
            Message += "<br/><br/>";
            Message += "<br/><span style='color:#CCCCCC;padding-left:16px;FONT-WEIGHT: 600;'>System Sent Date And Time:'" + System.DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss tt") + "'</span> </div> ";
            Message += "<br/></div>";
            Message += " <div style='background-color:#EE0000;border-bottom: 1px solid #a4a4a4;margin-left: 10px;margin-right: 10px;height:100px;border-left: 1px solid #A4A4A4;border-right: 1px solid #A4A4A4;'><div style='position:relative;color:#CCCCCC;padding-top: 25px;;margin-left: 125px;'>@ Leader Range Technology Sdn. Bhd. All Rights Reserved</div></div>";
            Message += "</div>";



            sendMailForVenderStatus(Tovendor, subject, Message);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static void sendMailForVenderStatus(string ToEmail1, string Subject, string EmailBody)
        {
            try
            {

                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
                service.UseDefaultCredentials = false;
                service.Credentials = new NetworkCredential(Username, Password, SmtpServer);
                //service.autodiscoverurl("it_test@leaderrange.com");
                service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                string logoImage = HttpContext.Current.Server.MapPath(@"~/Images/LRTLogo.png");
                string logoImage1 = HttpContext.Current.Server.MapPath(@"~/Images/vendor-notification.jpg");
                // string logoImage2 = HttpContext.Current.Server.MapPath(@"~/Images/newvendorreg.png");
                string logoImage3 = HttpContext.Current.Server.MapPath(@"~/Images/chatlogo1.png");
                //AlternateView view = AlternateView.CreateAlternateViewFromString(EmailBody, null, MediaTypeNames.Text.Html);
                //  LinkedResource resource = new LinkedResource(logoImage);
                //  resource.ContentId = "01";  
                // view.LinkedResources.Add(resource);

                service.Timeout = 200000;
                EmailMessage msg = new EmailMessage(service);

                FileAttachment attachment = msg.Attachments.AddFileAttachment(logoImage);

                attachment.ContentId = "01"; // this should be unique - say a GUID

                FileAttachment attachment2 = msg.Attachments.AddFileAttachment(logoImage1);
                attachment2.ContentId = "02";
                // FileAttachment attachment3 = msg.Attachments.AddFileAttachment(logoImage2);
                // attachment3.ContentId = "03";
                FileAttachment attachment4 = msg.Attachments.AddFileAttachment(logoImage3);
                attachment4.ContentId = "04";
                msg.Subject = Subject;

                msg.Body = new MessageBody(BodyType.HTML, EmailBody);

                msg.ToRecipients.Add(new EmailAddress(ToEmail1));

                msg.Send();
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
