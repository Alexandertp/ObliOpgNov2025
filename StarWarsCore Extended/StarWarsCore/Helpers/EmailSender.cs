using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StarWarsCore.Helpers
{
    public class EmailSender
    {        
        // w/o async - tillader tilbagemelding on-screen
        public string Email(string mailAddress, string subject, string messageBody)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {                        
                        UserName = "finnvs@gmail.com",
                        Password = "SECRETPWD"
                    };

                    client.Credentials = credential;                    
                    client.Host = "smtp.gmail.com";
                    client.Port = 587; // 25; 587; 465;
                    client.EnableSsl = true;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(mailAddress));                        
                        emailMessage.From = new MailAddress("dv@deathstar.com");
                        emailMessage.Subject = subject;
                        emailMessage.Body = messageBody;
                        client.Send(emailMessage);
                    }
                }           
                return "Email message was sucessfully sent.";
            }
            catch (Exception e)
            {
                // Message to user:
                return "Sorry, we couldn't send the email with a record of your epic Jedi battle. The error message was: " + e.Message;
            }

        }

        // The async asp net core 2 solution:
        public async Task<string> SendEmail(string mailAddress, string subject, string messageBody)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        // UserName = _configuration["Email:Email"],
                        // Password = _configuration["Email:Password"]
                        UserName = "finnvs@gmail.com",
                        Password = "SECRETPWD"
                    };

                    client.Credentials = credential;
                    // client.Host = _configuration["Email:Host"];
                    // client.Port = int.Parse(_configuration["Email:Port"]);
                    client.Host = "smtp.gmail.com";
                    client.Port = 465;
                    client.EnableSsl = true;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(mailAddress));
                        // emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.From = new MailAddress("dv@deathstar.com");
                        emailMessage.Subject = subject;
                        emailMessage.Body = messageBody;
                        client.Send(emailMessage);
                    }
                }
                await Task.CompletedTask;
                return "Email message was sucessfully sent.";
            }
            catch (Exception e)
            {
                // Message to user:
                return "Sorry, we couldn't send the email with a record of your epic Jedi battle. The error message was: " + e.Message;
            }

        }
    }
}


