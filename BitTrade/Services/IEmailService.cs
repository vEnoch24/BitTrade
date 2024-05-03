using BitTrade.Dto;

namespace BitTrade.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDto request);
    }
}