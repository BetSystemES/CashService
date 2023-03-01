using AutoMapper;
using CashService.EntityModels.Models;
using Google.Protobuf.Collections;

namespace CashService.GRPC.Services
{
    // TODO: class can be converted to Extension class. (e.g. ConvertExtensions)
    // TODO: change file location to CashService.GRPC.Extensions
    public static class Support
    {
        public static void EntityRemapper(TransactionProfileEntity transactionProfileEntity)
        {
            // TODO: remove comment
            //transactionProfileEntity.Id = Guid.NewGuid();
            foreach (var transactionEntity in transactionProfileEntity.Transactions)
            {
                transactionEntity.TransactionProfileId = transactionProfileEntity.ProfileId;
                // TODO: remove comment
                //transactionEntity.TransactionProfileEntity = transactionProfileEntity;
            }
        }
        public static void WithdrawValueConverter(TransactionProfileEntity transactionProfileEntity)
        {
            // TODO: remove comment
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

    // TODO: Change file location to CashService.GRPC.Enums
    public enum OperationType
    {
        Deposit = 0,
        Withdraw = 1,
    }
}
