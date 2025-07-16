using System.Net.Mail;
using BillingGateway.Application.Interfaces.Services;
using BillingGateway.Application.Settings;

namespace BillingGateway.Application.Services;

public class EmailService(SmtpSettings smtpSettings) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
        {
            Credentials = new System.Net.NetworkCredential(smtpSettings.Username, smtpSettings.Password),
            EnableSsl = true
        };
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings.Username),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);
        
        await client.SendMailAsync(mailMessage);
    }
}