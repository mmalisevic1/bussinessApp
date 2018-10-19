using AutoMapper;
using server.data;
using server.data.Tables;
using server.services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.services
{
    public class TransactionService
    {
        GlobomanticsContext _globomanticsContext;

        public TransactionService(GlobomanticsContext globomanticsContext)
        {
            _globomanticsContext = globomanticsContext;
        }

        public IEnumerable<TransactionDTO> GetTransactions(long userId, int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1), endDate = new DateTime(year, month < 12 ? month + 1 : month, 1);
            IQueryable<Transactions> query = _globomanticsContext.Transactions.Where(w => w.UserId == userId &&
                                                                                w.Date.CompareTo(startDate) >= 0 &&
                                                                                w.Date.CompareTo(endDate) < 0)
                                                                              .OrderBy(o => o.Date);
            return Mapper.Map<IEnumerable<TransactionDTO>>(query.AsEnumerable());
        }

        public object GetRunningBalance(long userId, int year, int month)
        {
            DateTime endDate = new DateTime(year, month < 12 ? month + 1 : month, 1);
            var query = _globomanticsContext.Transactions.Where(w => w.UserId == userId && w.Date.CompareTo(endDate) < 0);
            decimal? depositsSum = query.Select(s => s.Deposit)
                                        .Sum();
            decimal? chargesSum = query.Select(s => s.Charge)
                                       .Sum();
            return new { chargesSum, depositsSum };
        }

        public async Task<TransactionDTO> CreateTransaction(TransactionDTO transaction)
        {
            if (transaction.UserId <= 0) {
                throw new ArgumentException("Transaction can only be created for an existing user.");
            }

            Transactions createdTransaction = _globomanticsContext.Transactions.Add(Mapper.Map<Transactions>(transaction));

            await _globomanticsContext.SaveChangesAsync();
            return Mapper.Map<TransactionDTO>(createdTransaction);
        }
    }
}
