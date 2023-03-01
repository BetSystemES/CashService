using FluentValidation;
// TODO: remove unused usings
using Google.Protobuf.Collections;

namespace CashService.GRPC.ValidationRules
{
    // TODO: remove all empty lines
    // TODO: change file location to CashService.GRPC.Extensions
    // TODO: rename file to ValidationRulesExtensions
    /// <summary>
    /// Validation rule for CashType
    /// </summary>
    public static class CashTypeTypeValidationRule
    {
        /// <summary>
        /// Must the be valid CashTypeType.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, CashType> MustBeValidCashType<T>(this IRuleBuilder<T, CashType> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .Must(e => e == CashType.Cash || e == CashType.Bonus)
                .WithMessage("Received Unspecified CashType");

            return builderOptions;
        }

        public static IRuleBuilderOptions<T, Transaction> MustBeValidCashTypeInTransaction<T>(this IRuleBuilder<T, Transaction> ruleBuilder)
        {
            var builderOptions = ruleBuilder.SetValidator(new TransactionCashTypeValidator());

            return builderOptions;
        }

        public static IRuleBuilderOptions<T, TransactionModel> MustBeValidCashTypeInTransactionModel<T>(this IRuleBuilder<T, TransactionModel> ruleBuilder)
        {
            var builderOptions = ruleBuilder.SetValidator(new TransactionModelCashTypeValidator());

            return builderOptions;
        }

    }

    // TODO: each validator class should have it's own cs file
    // TODO: Change file location to CashService.Grpc.Infrastructure.Validators
    public class TransactionModelCashTypeValidator : AbstractValidator<TransactionModel>
    {
        public TransactionModelCashTypeValidator()
        {
            RuleForEach(x => x.Transactions)
                .MustBeValidCashTypeInTransaction();
        }
    }

    // TODO: each validator class should have it's own cs file
    // TODO: Change file location to CashService.Grpc.Infrastructure.Validators
    public class TransactionCashTypeValidator : AbstractValidator<Transaction>
    {
        public TransactionCashTypeValidator()
        {
            RuleFor(x => x.Cashtype)
                .MustBeValidCashType()                ;
        }
    }

}
