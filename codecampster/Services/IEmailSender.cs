using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codecampster.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string server, string username, string password, string email, string subject, string message);
    }
}
