using System.Threading.Tasks;

namespace Database.Services
{
    public interface IEmailSender
    {
        public Task SendAsync(
            (string name, string address) to,
            string subject,
            string body
        );
    }
}