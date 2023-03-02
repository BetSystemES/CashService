using AutoMapper;
using CashService.BusinessLogic.Entities;
using CashService.GRPC.Enums;
using Google.Protobuf.Collections;

namespace CashService.GRPC.Extensions
{
    public static class ConvertExtensions
    {
        public static void EntityRemapper(this TransactionProfileEntity transactionProfileEntity)
        {
            foreach (var transactionEntity in transactionProfileEntity.Transactions)
            {
                transactionEntity.TransactionProfileId = transactionProfileEntity.ProfileId;
            }
        }
        public static void WithdrawValueConverter(this TransactionProfileEntity transactionProfileEntity)
        {
            foreach (var transactionEntity in transactionProfileEntity.Transactions)
            {
                transactionEntity.Amount *=  (-1);
            }
        }

        public static List<TransactionProfileEntity> ReMapRepeatedTransactionModel(this RepeatedField<TransactionModel> transactionModels, IMapper mapper,  OperationType operationType)
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

        public static RepeatedField<TransactionModel> ReMapBackRepeatedTransactionModel(this IEnumerable<TransactionProfileEntity> transactionProfileEntities, IMapper mapper)
        {
            RepeatedField<TransactionModel> transactionModels = new();
            foreach (var transactionProfileEntity in transactionProfileEntities)
            {
                TransactionModel transactionModel = mapper.Map<TransactionModel>(transactionProfileEntity);
                transactionModels.Add(transactionModel);
            }

            return transactionModels;
        }
    }
}
