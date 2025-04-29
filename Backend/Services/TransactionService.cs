using Backend.Controllers;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// Creates a transaction for the user. If the transaction is a withdrawal, it checks if the user has enough balance.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> CreateTransaction(CreateTransactionRequest request, Guid userId);
        /// <summary>
        /// Retrieves all transactions for the user, ordered by date
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Transaction[]> GetTransactions(Guid userId);
        /// <summary>
        /// Gets the balance for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetBalance(Guid userId);
    }
    public class TransactionService(Db db) : ITransactionService
    {
        private readonly Db _db = db;

        public async Task<bool> CreateTransaction(CreateTransactionRequest request, Guid userId)
        {
            if (request.Amount <= 0)
            {
                return false; // Invalid amount
            }

            if (request.IsWithdrawal)
            {
                var balance = await getBalanceForUser(userId);
                if (balance < request.Amount)
                {
                    return false; // Insufficient funds
                }
            }

            var transaction = new Transaction(userId, request.Amount, request.IsWithdrawal);
            await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Transaction[]> GetTransactions(Guid userId)
        {
            return await getTransactions(userId);
        }

        public async Task<int> GetBalance(Guid userId)
        {
            return await getBalanceForUser(userId);
        }

        private async Task<Transaction[]> getTransactions(Guid userId)
        {
            var transactions = await _db.Transactions.Where(t => t.UserId == userId).OrderByDescending(t => t.DateCreated).ToArrayAsync();
            return transactions;
        }

        private async Task<int> getBalanceForUser(Guid userId)
        {
            var transactions = await getTransactions(userId);
            var totalWithdrawn = transactions.Where(t => t.IsWithdrawal).Sum(t => t.Amount);
            var totalDeposited = transactions.Where(t => !t.IsWithdrawal).Sum(t => t.Amount);
            return totalDeposited - totalWithdrawn;
        }
    }
}
