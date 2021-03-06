using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniLife.Server.Helpers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Email;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Search;
using Microsoft.Extensions.Logging;
using static Microsoft.AspNetCore.Http.StatusCodes;
using MimeKit;

namespace UniLife.Server.Managers
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly ILogger<EmailManager> _logger;

        public EmailManager(IEmailConfiguration emailConfiguration, ILogger<EmailManager> logger)
        {
            _emailConfiguration = emailConfiguration;
            _logger = logger;
        }

        public async Task<ApiResponse> Send(EmailDto parameters)
        {
            var email = new EmailMessageDto();

            email.ToAddresses.Add(new EmailAddressDto(parameters.ToName, parameters.ToAddress));

            //This forces all emails from the API to use the Test template to prevent spam
            parameters.TemplateName = "Test";

            //Send a Template email or a custom one since it is hardcoded to Test template it will not do a custom email.
            if (!string.IsNullOrEmpty(parameters.TemplateName))
            {
                switch (parameters.TemplateName)
                {
                    case "Test":
                        email.BuildTestEmail(); //example of email Template usage
                        break;
                    default:
                        break;
                }
            }
            else
            {
                email.Subject = parameters.Subject;
                email.Body = parameters.Body;
            }

            //Add a new From Address if you so choose, default is set in appsettings.json
            //email.FromAddresses.Add(new EmailAddress("New From Name", "email@domain.com"));
            _logger.LogInformation("Test Email: {0}", email.Subject);
            try
            {
                await SendEmailAsync(email);
                return new ApiResponse(Status200OK, "Email Successfuly Sent");
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status500InternalServerError, ex.Message);
            }
        }

        public Task<ApiResponse> Receive()
        {
            throw new System.NotImplementedException();
        }

                public List<EmailMessageDto> ReceiveEmail(int maxCount = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> ReceiveMailImapAsync()
        {
            using (var emailClient = new ImapClient())
            {
                try
                {
                    // use this if you need to specify using ssl; MailKit should usually be able to autodetect the appropriate settings
                    // await emailClient.ConnectAsync(_emailConfiguration.ImapServer, _emailConfiguration.ImapPort, _emailConfiguration.ImapUseSSL).ConfigureAwait(false);

                    await emailClient.ConnectAsync(_emailConfiguration.ImapServer, _emailConfiguration.ImapPort).ConfigureAwait(false);

                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");


                    if (!string.IsNullOrWhiteSpace(_emailConfiguration.ImapUsername))
                    {
                        await emailClient.AuthenticateAsync(_emailConfiguration.ImapUsername, _emailConfiguration.ImapPassword).ConfigureAwait(false);
                    }

                    List<EmailMessageDto> emails = new List<EmailMessageDto>();
                    await emailClient.Inbox.OpenAsync(MailKit.FolderAccess.ReadOnly);

                    //TODO implement email results filtering
                    var uids = await emailClient.Inbox.SearchAsync(SearchQuery.All);
                    foreach (var uid in uids)
                    {
                        var message = await emailClient.Inbox.GetMessageAsync(uid);

                        var emailMessage = new EmailMessageDto
                        {
                            Body = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,

                            Subject = message.Subject
                        };
                        emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddressDto(x.Name, x.Address)));
                        emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddressDto(x.Name, x.Address)));

                        emails.Add(emailMessage);
                    }

                    await emailClient.DisconnectAsync(true);
                    return new ApiResponse(Status200OK, null, emails);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Imap Email Retrieval failed: {0}", ex.Message);
                    return new ApiResponse(Status500InternalServerError, ex.Message);
                }
            }
        }

        public async Task<ApiResponse> ReceiveMailPopAsync(int min = 0, int max = 0)
        {
            using (var emailClient = new Pop3Client())
            {
                try
                {
                    await emailClient.ConnectAsync(_emailConfiguration.PopServer, _emailConfiguration.PopPort).ConfigureAwait(false);     // omitting usessl to allow mailkit to autoconfigure

                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (!String.IsNullOrWhiteSpace(_emailConfiguration.PopUsername))
                    {
                        await emailClient.AuthenticateAsync(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword).ConfigureAwait(false);
                    }

                    List<EmailMessageDto> emails = new List<EmailMessageDto>();

                    if (max == 0) max = await emailClient.GetMessageCountAsync(); // if max not defined, get all messages

                    for (int i = min; i < max; i++)
                    {
                        var message = await emailClient.GetMessageAsync(i);

                        var emailMessage = new EmailMessageDto
                        {
                            Body = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                            IsHtml = !string.IsNullOrEmpty(message.HtmlBody) ? true : false,
                            Subject = message.Subject

                        };
                        emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddressDto(x.Name, x.Address)));
                        emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddressDto(x.Name, x.Address)));

                        emails.Add(emailMessage);
                    }

                    await emailClient.DisconnectAsync(true);
                    return new ApiResponse(Status200OK, null, emails);
                }
                catch (Exception ex)
                {
                    return new ApiResponse(Status500InternalServerError, ex.Message);
                }
            }
        }

        public void Send(EmailMessageDto emailMessage)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> SendEmailAsync(EmailMessageDto emailMessage)
        {
            try
            {
                //TODO:
                //var message = new MimeMessage();

                //// Set From Address it was not set
                //if (emailMessage.FromAddresses.Count == 0)
                //{
                //    emailMessage.FromAddresses.Add(new EmailAddressDto(_emailConfiguration.FromName, _emailConfiguration.FromAddress));
                //}

                //message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                //message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                //message.Cc.AddRange(emailMessage.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                //message.Bcc.AddRange(emailMessage.BccAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                ////Use for testing - send a copy of any email to from address when in debug mode
                ////if (System.Diagnostics.Debugger.IsAttached)
                ////{
                ////    message.To.Clear();
                ////    message.To.Add(new MailboxAddress(_emailConfiguration.FromName, _emailConfiguration.FromAddress));
                ////}

                //message.Subject = emailMessage.Subject;

                //message.Body = emailMessage.IsHtml ? new BodyBuilder { HtmlBody = emailMessage.Body }.ToMessageBody() : new TextPart("plain") { Text = emailMessage.Body };

                ////TODO store all emails in Database

                ////Be careful that the SmtpClient class is the one from Mailkit not the framework!
                //using (var emailClient = new SmtpClient())
                //{
                //    if (!_emailConfiguration.SmtpUseSSL)
                //    {
                //        emailClient.ServerCertificateValidationCallback = (object sender2, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                //    }

                //    await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, _emailConfiguration.SmtpUseSSL).ConfigureAwait(false);

                //    //Remove any OAuth functionality as we won't be using it.
                //    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                //    if (!string.IsNullOrWhiteSpace(_emailConfiguration.SmtpUsername))
                //    {
                //        await emailClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword).ConfigureAwait(false);
                //    }

                //    await emailClient.SendAsync(message).ConfigureAwait(false);

                //    await emailClient.DisconnectAsync(true).ConfigureAwait(false);
                return new ApiResponse(203);
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError("Email Send Failed: {0}", ex.Message);
                return new ApiResponse(Status500InternalServerError, ex.Message);
            }
        }
    }
}
