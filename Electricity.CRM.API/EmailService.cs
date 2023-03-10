using System.Net.Mail;
using System.Net;
using System;

namespace Electricity.CRM.API
{
    public class EmailService
    {
        public static void Email(string htmlString,string emailSubject,string toEmail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("dubeytapan12@gmail.com");
                message.To.Add(new MailAddress(toEmail));
                message.Subject = emailSubject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dubeytapan12@gmail.com", "jzjihdwrwnpqbpnu");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
