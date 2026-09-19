using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Transaction.Models;

namespace Transaction.BusinessLogic
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string action, Product product, string recipientEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                GetSetting("EmailSettings:FromName"),
                GetSetting("EmailSettings:FromEmail")
            ));
            message.To.Add(new MailboxAddress("Transaction Admin", recipientEmail));
            message.Subject = $"BSIT 3-1: {action}";
            message.Body = new TextPart("plain")
            {
                Text = $"{action}\n\n" +
                       $"ID: {product.Id}\n" +
                       $"Company: {product.Company}\n" +
                       $"Item: {product.Item}\n" +
                       $"Purchase Price: {product.PurchasePrice}\n" +
                       $"Selling Price: {product.SellingPrice}\n" +
                       $"Stock: {product.Stock}\n\n"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(
                    GetSetting("EmailSettings:SmtpHost"),
                    int.Parse(GetSetting("EmailSettings:SmtpPort")),
                    SecureSocketOptions.StartTls
                );

                client.Authenticate(
                    GetSetting("EmailSettings:Username"),
                    GetSetting("EmailSettings:Password")
                );

                client.Send(message);
                client.Disconnect(true);
            }
        }

        // Throws a readable error if a key is missing from appsettings.json
        private string GetSetting(string key)
        {
            string? value = _configuration[key];
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"Missing configuration value: {key}");
            return value;
        }
    }
}