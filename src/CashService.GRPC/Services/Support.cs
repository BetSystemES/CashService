using CashService.BusinessLogic.Models;

namespace CashService.GRPC.Services
{
    public static class Support
    {
        public static void EntityRemapper(TransactionProfileEntity transactionProfileEntity)
        {
            //transactionProfileEntity.Id = Guid.NewGuid();
            foreach (var transactionEntity in transactionProfileEntity.Transactions)
            {
                transactionEntity.TransactionProfileId = transactionProfileEntity.ProfileId;
                //transactionEntity.TransactionProfileEntity = transactionProfileEntity;
            }
        }
        public static void WithdrawValueConverter(TransactionProfileEntity transactionProfileEntity)
        {
            //transactionProfileEntity.Id = Guid.NewGuid();
            foreach (var transactionEntity in transactionProfileEntity.Transactions)
            {
                transactionEntity.Amount *=  (-1);
            }
        }
    }
}
