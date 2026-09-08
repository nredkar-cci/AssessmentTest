using AssessmentTest.Application.Email;
using Microsoft.Extensions.Logging;

namespace AssessmentTest.Infrastructure.BackgroundServices.EmailJob
{
    public class PendingEmailJob
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<PendingEmailJob> _logger;

        public PendingEmailJob(IEmailService emailService, ILogger<PendingEmailJob> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task SendPendingEmailsAsync(CancellationToken cancellationToken)
        {
            int sent = 0;

            while (!cancellationToken.IsCancellationRequested
                   && _emailService.DequeueEmail() is { } message)
            {
                if (string.IsNullOrWhiteSpace(message.Recipient))
                {
                    _logger.LogWarning("Skipped a queued email with no recipient.");
                    continue;
                }

                try
                {
                    await _emailService.SendEmailAsync(
                        message.Recipient,
                        message.Subject ?? string.Empty,
                        message.Body ?? string.Empty);

                    sent++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send queued email to {Recipient}", message.Recipient);
                }
            }

            if (sent > 0)
            {
                _logger.LogInformation("Sent {Count} queued email(s).", sent);
            }
        }
    }
}
