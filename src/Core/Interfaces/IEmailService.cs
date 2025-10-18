using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string toEmail, string nom, string prenom, string verificationToken);
        Task SendPasswordResetEmailAsync(string toEmail, string nom, string prenom, string resetToken);
        Task SendWelcomeEmailAsync(string toEmail, string nom, string prenom);

    }
}
