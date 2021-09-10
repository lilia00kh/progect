using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.EmailService
{
    /// <summary>
    /// Interface for email sender.
    /// </summary>
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
        public string GetAdminEmail();
    }
}
