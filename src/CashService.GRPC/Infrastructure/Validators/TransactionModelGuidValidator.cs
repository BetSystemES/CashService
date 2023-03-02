using CashService.GRPC.Extensions;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Validators
{
    public class TransactionModelGuidValidator : AbstractValidator<TransactionModel>
    {
        public TransactionModelGuidValidator()
        {
            RuleFor(x => x.ProfileId)
                .MustBeValidGuid();

            RuleForEach(x => x.Transactions)
                .MustBeValidGuidInTransaction();
        }
    }
}
