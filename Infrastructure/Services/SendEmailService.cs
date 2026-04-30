using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using Infrastructure.Utility.Mails;
using Infrastructure.Utility;
using Infrastructure.Configurations;
using brevo_csharp.Api;

namespace Infrastructure.Services
{
    /// <summary>
    ///     Service pour envoyer des emails via l'API Brevo.
    /// </summary>
    public class SendEmailService : ISendEmailService
    {
        private readonly ILogger<SendEmailService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly AppSetting _appSetting;

        public SendEmailService(UserManager<User> userManager, AppSetting appSetting, ILogger<SendEmailService> logger)
        {
            _userManager = userManager;
            _appSetting = appSetting;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task SendEmailConfirmation(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = FormatUtility.GenerateEmailConfirmationUrl(_appSetting.LinkUrlConfirmEmail!, user.Id, code);
            var content = ModelMails.MailConfirmationHtlm(user.Member.Name, callbackUrl);

            await Execute(ModelMails.OBJECTEMAILCONFIRMATION, content, user.Email!);
        }

        /// <inheritdoc />
        public async Task SendInvitationEmail(string toEmail, string firstName, string departmentName, string invitationCode, int invitationId)
        {
            var joinUrl = $"{_appSetting.FrontUrl}/register?invitationId={invitationId}";
            var content = ModelMails.MailInvitationHtml(firstName, departmentName, invitationCode, joinUrl);
            await Execute(ModelMails.OBJECTINVITATION, content, toEmail);
        }

        /// <inheritdoc />
        public async Task SendResetPasswordEmail(string toEmail, string name, string resetUrl)
        {
            var content = ModelMails.MailResetPasswordHtml(name, resetUrl);
            await Execute(ModelMails.OBJECTRESETPASSWORD, content, toEmail);
        }

        /// <inheritdoc />
        public async Task SendPlanningPdfEmail(string toEmail, string name, string departmentName, string monthYear, byte[] pdfBytes)
        {
            var content = ModelMails.MailPlanningPublishedHtml(name, departmentName, monthYear);
            var fileName = $"Planning_{departmentName.Replace(" ", "_")}_{monthYear.Replace(" ", "_")}.pdf";
            await ExecuteWithAttachment(ModelMails.OBJECTPLANNING + " — " + departmentName, content, toEmail, pdfBytes, fileName);
        }

        /// <summary>
        ///     Lance l'envoi d'un email via l'API Brevo en arrière-plan.
        ///     Ne bloque jamais la réponse HTTP. Les erreurs sont loggées.
        /// </summary>
        /// <param name="subject">Sujet du mail</param>
        /// <param name="htmlContent">Contenu HTML</param>
        /// <param name="toEmail">Email destinataire</param>
        private Task Execute(string subject, string htmlContent, string toEmail)
        {
            var brevo = _appSetting.Brevo;

            if (string.IsNullOrWhiteSpace(brevo?.ApiKey))
            {
                _logger.LogWarning("Clé API Brevo non configurée. Email non envoyé à {ToEmail}.", toEmail);
                return Task.CompletedTask;
            }

            // Fire-and-forget : l'envoi se fait en arrière-plan
            _ = Task.Run(async () =>
            {
                try
                {
                    brevo_csharp.Client.Configuration.Default.ApiKey.Clear();
                    brevo_csharp.Client.Configuration.Default.ApiKey.Add("api-key", brevo.ApiKey);

                    var apiInstance = new TransactionalEmailsApi();

                    var sendSmtpEmail = new brevo_csharp.Model.SendSmtpEmail
                    {
                        Sender = new brevo_csharp.Model.SendSmtpEmailSender(brevo.FromName, brevo.FromEmail),
                        To = new List<brevo_csharp.Model.SendSmtpEmailTo> { new brevo_csharp.Model.SendSmtpEmailTo(toEmail) },
                        Subject = subject,
                        HtmlContent = htmlContent
                    };

                    var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                    _logger.LogInformation("Email envoyé à {ToEmail} avec succès. MessageId: {MessageId}", toEmail, result.MessageId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de l'envoi de l'email à {ToEmail}.", toEmail);
                }
            });

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Envoie un email avec pièce jointe via l'API Brevo en arrière-plan.
        /// </summary>
        private Task ExecuteWithAttachment(string subject, string htmlContent, string toEmail, byte[] attachmentBytes, string fileName)
        {
            var brevo = _appSetting.Brevo;

            if (string.IsNullOrWhiteSpace(brevo?.ApiKey))
            {
                _logger.LogWarning("Clé API Brevo non configurée. Email non envoyé à {ToEmail}.", toEmail);
                return Task.CompletedTask;
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    brevo_csharp.Client.Configuration.Default.ApiKey.Clear();
                    brevo_csharp.Client.Configuration.Default.ApiKey.Add("api-key", brevo.ApiKey);

                    var apiInstance = new TransactionalEmailsApi();

                    var sendSmtpEmail = new brevo_csharp.Model.SendSmtpEmail
                    {
                        Sender = new brevo_csharp.Model.SendSmtpEmailSender(brevo.FromName, brevo.FromEmail),
                        To = new List<brevo_csharp.Model.SendSmtpEmailTo> { new brevo_csharp.Model.SendSmtpEmailTo(toEmail) },
                        Subject = subject,
                        HtmlContent = htmlContent,
                        Attachment = new List<brevo_csharp.Model.SendSmtpEmailAttachment>
                        {
                            new brevo_csharp.Model.SendSmtpEmailAttachment(
                                name: fileName,
                                content: attachmentBytes
                            )
                        }
                    };

                    var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                    _logger.LogInformation("Email avec PDF envoyé à {ToEmail}. MessageId: {MessageId}", toEmail, result.MessageId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de l'envoi de l'email avec PDF à {ToEmail}.", toEmail);
                }
            });

            return Task.CompletedTask;
        }
    }
}
