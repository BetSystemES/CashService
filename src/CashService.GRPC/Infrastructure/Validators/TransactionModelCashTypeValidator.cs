using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
{
    public class TransactionModelCashTypeValidator : AbstractValidator<TransactionModel>
    {
        public TransactionModelCashTypeValidator()
        {
            RuleForEach(x => x.Transactions)
                .MustBeValidCashTypeInTransaction();
        }
    }
}
