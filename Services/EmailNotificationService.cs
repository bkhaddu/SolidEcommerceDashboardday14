using SolidEcommerceDashboard.Interfaces;

namespace SolidEcommerceDashboard.Services
{
    public class EmailNotificationService : INotificationService
    {
        public string SendNotification(string message)
        {
            return $"Email sent: {message}";
        }
    }
}