using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
{
    public class TransactionGuidValidator : AbstractValidator<Transaction>
    {
        public TransactionGuidValidator()
        {
            RuleFor(x => x.TransactionId)
                .MustBeValidGuid();
        }
    }
}
