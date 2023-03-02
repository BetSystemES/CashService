using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
{
    public class TransactionCashTypeValidator : AbstractValidator<Transaction>
    {
        public TransactionCashTypeValidator()
        {
            RuleFor(x => x.CashType)
                .MustBeValidCashType();
        }
    }
}
