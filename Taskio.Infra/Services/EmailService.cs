using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Taskio.App.IServices;

namespace Taskio.Infra.Services
{
    public class EmailService : IEmailService
{
    public async Task SendEmail(string to, string subject, string body)
    {
        var smtp = new SmtpClient("smtp.example.com");
        var message = new MailMessage("noreply@example.com", to, subject, body);
        await smtp.SendMailAsync(message);
    }
}
}