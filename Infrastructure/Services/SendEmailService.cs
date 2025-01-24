
using Application.Interfaces.Services;
using Domain.Entities;
using Application.Configurations;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Infrastructure.Utility.Mails;
using Infrastructure.Utility;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service pour envoyer un Email
    /// </summary>
    public class SendEmailService : ISendEmailService
    {
        private readonly ILogger _logger;

        public SendEmailService(UserManager<User> userManager, AppSetting appSetting, ILogger<SendEmailService> logger)
        {
            _userManager = userManager;
            AppSetting = appSetting;
            _logger = logger;
        }

        private readonly UserManager<User> _userManager;
        public AppSetting AppSetting { get; }

        /// <summary>
        /// Envoir l'email de confirmation
        /// </summary>
        /// <param name="email"></param>
        public async void SendEmailConfirmation(User user)
        { 
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = FormatUtility.GenerateEmailConfirmationUrl(AppSetting.LinkUrlConfirmEmail!, user.Id, code);
            var content = ModelMails.MailConfirmationHtlm(user.Member.Name ,callbackUrl);

            await Execute(ModelMails.OBJECTEMAILCONFIRMATION, content, user.Email!);
        }

        /// <summary>
        /// Permet d'envoyer le email
        /// </summary>
        /// <param name="subject">Sujet duu mail</param>
        /// <param name="content">Le contenu</param>
        /// <param name="toEmail">Email destinateur</param>
        /// <returns>Returne objet Task.</returns>
        private async Task Execute(string subject,string content, string toEmail)
        {
            var client = new SendGridClient(AppSetting.SendGridKey); 

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(AppSetting.EmailExp, AppSetting.AppName),
                Subject = subject,
                HtmlContent = content,
            };

            msg.AddTo(new EmailAddress(toEmail));
            msg.SetClickTracking(false, false);

            try
            {
                var response = await client.SendEmailAsync(msg);
                _logger.LogInformation(response.IsSuccessStatusCode
                ? "Email to {toEmail} queued successfully!"
                : "Failure Email to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending an email to {toEmail}.", toEmail);
            }
        }
    }
}
