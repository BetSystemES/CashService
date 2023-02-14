using AutoMapper;
using CashService.BusinessLogic.Models;
using Google.Protobuf.Collections;

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


        public static List<TransactionProfileEntity> ReMapRepeatedTransactionModel(IMapper mapper, RepeatedField<TransactionModel> transactionModels, OperationType operationType)
        {
            List<TransactionProfileEntity> rangeTransactionProfileEntities = new();
            foreach (var transactionModel in transactionModels)
            {
                TransactionProfileEntity transactionProfile = mapper.Map<TransactionProfileEntity>(transactionModel);
                EntityRemapper(transactionProfile);
                if (operationType == OperationType.Withdraw)
                {
                    WithdrawValueConverter(transactionProfile);
                }
                rangeTransactionProfileEntities.Add(transactionProfile);
            }

            return rangeTransactionProfileEntities;
        }

        public static RepeatedField<TransactionModel> ReMapBackRepeatedTransactionModel(IMapper mapper, IEnumerable<TransactionProfileEntity> transactionProfileEntities)
        {
            RepeatedField<TransactionModel> transactionModels  = new();
            foreach (var transactionProfileEntity in transactionProfileEntities)
            {
                TransactionModel transactionModel = mapper.Map<TransactionModel>(transactionProfileEntity);
                transactionModels.Add(transactionModel);
            }

            return transactionModels;
        }
    }

    public enum OperationType
    {
        Deposit = 0,
        Withdraw = 1,
    }
}
