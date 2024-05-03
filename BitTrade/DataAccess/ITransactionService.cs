using BitTrade.Model;

namespace BitTrade.DataAccess
{
    public interface ITransactionService
    {
        Task DeleteTransactions();
        Task DeleteTransaction(string id);
        Task<List<TransactionModel>> GetAllTransactions();
        Task Payment(TransactionModel payment);
        Task Verify(string reference);
    }
}