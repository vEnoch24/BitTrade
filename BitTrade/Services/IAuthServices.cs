using BitTrade.Model;

namespace BitTrade.Services
{
    public interface IAuthServices
    {
        Task Login(InputModel model);
        Task Register(InputModel model);
    }
}