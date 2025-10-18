using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task SendEmailVerificationAsync(string email, string nom, string prenom, string verificationToken)
        {
            var subject = "Vérifiez votre adresse email - Ligue Dimanche";
            var verificationUrl = $"{_configuration["AppSettings:BaseUrl"]}/auth/verify-email?token={verificationToken}";

            var body = $@"
            <h2>Bonjour {prenom} {nom},</h2>
            <p>Merci de vous être inscrit à la Ligue Dimanche !</p>
            <p>Pour activer votre compte, veuillez cliquer sur le lien ci-dessous :</p>
            <p><a href='{verificationUrl}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Vérifier mon email</a></p>
            <p>Ce lien expire dans 24 heures.</p>
            <p>Si vous n'avez pas créé de compte, ignorez cet email.</p>
        ";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendWelcomeEmailAsync(string email, string nom, string prenom)
        {
            var subject = "Bienvenue à la Ligue Dimanche !";
            var body = $@"
            <h2>Bienvenue {prenom} {nom} !</h2>
            <p>Nous sommes ravis de vous compter parmi les membres de la Ligue Dimanche.</p>
            <p>Commencez à explorer nos fonctionnalités et profitez de votre expérience.</p>
            <p>Si vous avez des questions, n'hésitez pas à nous contacter.</p>";
            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPasswordResetEmailAsync(string email, string nom, string prenom, string resetToken)
        {
            var subject = "Réinitialisation de votre mot de passe - Ligue Dimanche";
            var resetUrl = $"{_configuration["AppSettings:BaseUrl"]}/auth/reset-password?token={resetToken}";

            var body = $@"
            <h2>Bonjour {prenom} {nom},</h2>
            <p>Vous avez demandé à réinitialiser votre mot de passe.</p>
            <p>Cliquez sur le lien ci-dessous pour créer un nouveau mot de passe :</p>
            <p><a href='{resetUrl}' style='background-color: #dc3545; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Réinitialiser mon mot de passe</a></p>
            <p>Ce lien expire dans 1 heure.</p>
        ";

            await SendEmailAsync(email, subject, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            var fromEmail = _configuration["EmailSettings:FromEmail"];
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var portString = _configuration["EmailSettings:Port"];

            if (string.IsNullOrWhiteSpace(fromEmail))
                throw new InvalidOperationException("L'adresse email d'expéditeur n'est pas configurée.");
            if (string.IsNullOrWhiteSpace(smtpServer))
                throw new InvalidOperationException("Le serveur SMTP n'est pas configuré.");
            if (string.IsNullOrWhiteSpace(portString))
                throw new InvalidOperationException("Le port SMTP n'est pas configuré.");

            message.From.Add(new MailboxAddress("Ligue Dimanche", fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpServer, int.Parse(portString), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromEmail, _configuration["EmailSettings:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
