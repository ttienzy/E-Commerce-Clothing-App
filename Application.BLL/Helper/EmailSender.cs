using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Helper
{
    public static class EmailSender
    {
        public static void SendEmailWithGoogleSMTP(string from_email
            ,string from_passworld
            ,string OTP_Code
            ,string to_email)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from_email);
            message.Subject = "OTP code validation";
            message.To.Add(new MailAddress(to_email));
            message.Body = $"<html><body> Your OTP is : <b>{OTP_Code}</b> </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(from_email, from_passworld),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}
