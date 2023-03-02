using CashService.GRPC.Infrastructure.Validators;
using FluentValidation;

namespace CashService.GRPC.Extensions
{
    /// <summary>
    /// Validation rule for CashType
    /// </summary>
    public static class CashTypeValidationRuleExtensions
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
}