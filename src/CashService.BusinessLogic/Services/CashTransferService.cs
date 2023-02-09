using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.BusinessLogic.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.BusinessLogic.Models;

namespace CashService.BusinessLogic.Services
{
    public class CashTransferService : ICashService
    {
        private readonly IRepository<TransactionEntity> _transactionEntityRepository;
        private readonly IRepository<TransactionProfileEntity> _transactionProfileRepository;
        private readonly ICashProvider _cashProvider;

        private readonly IDataContext _context;

        public CashTransferService(
            IRepository<TransactionEntity> transactionEntityRepository,
            IRepository<TransactionProfileEntity> transactionProfileRepository,
            ICashProvider cashProvider,
            IDataContext context)
        {
            _transactionEntityRepository=transactionEntityRepository;
            _transactionProfileRepository=transactionProfileRepository;
            _cashProvider=cashProvider;
            _context=context;
        }

        public async Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileid, token);
            return balance;
        }

        public async Task Deposit(TransactionProfileEntity depositTransactionProfile, CancellationToken token)
        {
            await _transactionProfileRepository.Add(depositTransactionProfile, token);
            await _context.SaveChanges(token);
        }

        public async Task<TransactionProfileEntity> Withdraw(TransactionProfileEntity withdrawTransactionProfile, CancellationToken token)
        {
            var profileid = withdrawTransactionProfile.ProfileId;
            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileid, token);


            await _transactionProfileRepository.Add(withdrawTransactionProfile, token);
            await _context.SaveChanges(token);
            TransactionProfileEntity withdraw = new TransactionProfileEntity();
            //TransactionProfileEntity withdraw = await _cashProvider.GetWithdraw(withdrawTransactionProfile, token);
            return withdraw;
        }

        public Task DepositRange(List<TransactionProfileEntity> depositRangeTransactionProfileEntities, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransactionProfileEntity>> WithdrawRange(List<TransactionProfileEntity> withdrawRangeTransactionProfileEntities, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
