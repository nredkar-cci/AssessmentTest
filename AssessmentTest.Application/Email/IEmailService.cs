namespace AssessmentTest.Application.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);

        Task QueueEmail(EmailMessage emailMessage);

        EmailMessage? DequeueEmail();
    }
}
