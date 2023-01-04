using Azure.Communication.Email.Models;
using Azure.Communication.Email;
using Azure;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using SoW.Tracker.WebAPI.Models;

namespace SoW.Tracker.WebAPI.ServiceImplementation
{
    public class EmailCommunication
    {
        private ConnectionStrings _connectionString { get; set; }
        public EmailCommunication(ConnectionStrings connectionString)
        {
            _connectionString = connectionString;
            _connectionString.EmailConnectionString = connectionString.EmailConnectionString;
        }

        //public string EmailSend()
        //{
        //    EmailClient emailClient = new EmailClient(_connectionString.EmailConnectionString);
        //    //Replace with your domain and modify the content, recipient details as required

        //    EmailContent emailContent = new EmailContent("Welcome to Azure Communication Service Email APIs.");
        //    emailContent.PlainText = "This email message is sent from Azure Communication Service Email using .NET SDK.";
        //    List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress("emailalias@contoso.com") { DisplayName = "Friendly Display Name" } };
        //    EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
        //    EmailMessage emailMessage = new EmailMessage("donotreply@xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.azurecomm.net", emailContent, emailRecipients);
        //    SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
        //    Console.WriteLine($"MessageId = {emailResult.MessageId}");
        //    Response<SendStatusResult> messageStatus = null;
        //    messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
        //    Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
        //    TimeSpan duration = TimeSpan.FromMinutes(3);
        //    long start = DateTime.Now.Ticks;
        //    do
        //    {
        //        messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
        //        if (messageStatus.Value.Status != SendStatus.Queued)
        //        {
        //            Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
        //            break;
        //        }
        //        Thread.Sleep(10000);
        //        Console.WriteLine($"...");

        //    } while (DateTime.Now.Ticks - start < duration.Ticks);
        //}

    }
}
