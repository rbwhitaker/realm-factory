using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security;

namespace RealmEngine
{
    public class EmailManager
    {
        public static void CreateTestMessage2(string server)
        {
            try
            {
                var fromAddress = new MailAddress("rbwhitaker83@gmail.com", "RB Whitaker");
                var toAddress = new MailAddress("rbwhitaker83@sisna.com", "RB Whitaker");
                const string fromPassword = "!2#4%6&8(0";
                const string subject = "Realm Engine Bug";
                const string body = "Realm Engine Bugs are awesome.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                } 

            }
            catch (Exception)
            {
                Console.WriteLine("There was an error sending the message.");
            }
        }

    }
}
