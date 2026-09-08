using System.Collections.Concurrent;
using AssessmentTest.Application.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AssessmentTest.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private const string QueueKey = "email:queue";

        private readonly EmailSettings _settings;
        private readonly IMemoryCache _memoryCache;

        public EmailService(IOptions<EmailSettings> settings, IMemoryCache memoryCache)
        {
            _settings = settings.Value;
            _memoryCache = memoryCache;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));

            message.To.Add(MailboxAddress.Parse(to));

            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            smtp.CheckCertificateRevocation = false;

            await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_settings.Username, _settings.Password);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }

        public Task QueueEmail(EmailMessage emailMessage)
        {
            GetQueue().Enqueue(emailMessage);

            return Task.CompletedTask;
        }

        public EmailMessage? DequeueEmail() =>
            GetQueue().TryDequeue(out EmailMessage? message) ? message : null;

        private ConcurrentQueue<EmailMessage> GetQueue() =>
            _memoryCache.GetOrCreate(QueueKey, entry =>
            {
                entry.Priority = CacheItemPriority.NeverRemove;

                return new ConcurrentQueue<EmailMessage>();
            })!;
    }
}
