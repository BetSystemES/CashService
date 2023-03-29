using AutoMapper;
using CashService.BusinessLogic.Entities;
using CashService.GRPC.Enums;
using Google.Protobuf.Collections;

namespace CashService.GRPC.Extensions
{
    public static class ConvertExtensions
    {
        public static void EntityRemapper(this ProfileEntity profileEntity)
        {
            foreach (var transactionEntity in profileEntity.Transactions)
            {
                transactionEntity.ProfileId = profileEntity.Id;
            }
        }
        public static void WithdrawValueConverter(this ProfileEntity profileEntity)
        {
            foreach (var transactionEntity in profileEntity.Transactions)
            {
                transactionEntity.Amount *=  (-1);
            }
        }

        public static List<ProfileEntity> ReMapRepeatedTransactionModel(this RepeatedField<TransactionModel> transactionModels, IMapper mapper,  OperationType operationType)
        {
            List<ProfileEntity> rangeTransactionProfileEntities = new();
            foreach (var transactionModel in transactionModels)
            {
                ProfileEntity profile = mapper.Map<ProfileEntity>(transactionModel);
                EntityRemapper(profile);
                if (operationType == OperationType.Withdraw)
                {
                    WithdrawValueConverter(profile);
                }
                rangeTransactionProfileEntities.Add(profile);
            }

            return rangeTransactionProfileEntities;
        }

        public static RepeatedField<TransactionModel> ReMapBackRepeatedTransactionModel(this IEnumerable<ProfileEntity> profileEntities, IMapper mapper)
        {
            RepeatedField<TransactionModel> transactionModels = new();
            foreach (var profileEntity in profileEntities)
            {
                TransactionModel transactionModel = mapper.Map<TransactionModel>(profileEntity);
                transactionModels.Add(transactionModel);
            }

            return transactionModels;
        }
    }
}
