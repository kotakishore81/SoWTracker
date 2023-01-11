using Azure.Communication.Email.Models;
using Azure.Communication.Email;
using Azure;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.ServiceInterface;

namespace SoW.Tracker.WebAPI.ServiceImplementation
{
    public class EmailCommunication : IEmailCommunication
    {
        private ConnectionStrings _connectionString { get; set; }
        public EmailCommunication(ConnectionStrings connectionString)
        {
            _connectionString = connectionString;
            _connectionString.EmailConnectionString = connectionString.EmailConnectionString;
        }

        public string EmailSend()
        {
            EmailClient emailClient = new EmailClient(_connectionString.EmailConnectionString);
            //Replace with your domain and modify the content, recipient details as required

            EmailContent emailContent = new EmailContent("SoW Tracker");
            var url = "https://sow-tracker.azurewebsites.net";
            var link = $"<a href='{url}'>Click here</a>";
            // emailContent.PlainText = "Thanks for creating SoW please check the below link to update the status.";

            emailContent.Html = $"<p>Thanks for creating SoW please check the below link to update the status.</p>";
            emailContent.Html = emailContent.Html + $"<a href='{url}'>Click here</a>";

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress("kota.malli.kishore@ibm.com") { DisplayName = "Friendly Display Name" } };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("donotreply@d64f3b80-830c-4e0a-b5ca-72ef1db7617e.azurecomm.net", emailContent, emailRecipients);
            SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
            Console.WriteLine($"MessageId = {emailResult.MessageId}");
            Response<SendStatusResult> messageStatus = null;
            messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
            Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
            TimeSpan duration = TimeSpan.FromMinutes(3);
            long start = DateTime.Now.Ticks;
            do
            {
                messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
                if (messageStatus.Value.Status != SendStatus.Queued)
                {
                    Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
                    break;
                }
                Thread.Sleep(10000);

            } while (DateTime.Now.Ticks - start < duration.Ticks);
            return "";
        }

    }
}
