using CashService.GRPC.Infrastructure.Validators;
using FluentValidation;

namespace CashService.GRPC.Extensions
{
    /// <summary>
    /// Validation rule for guid
    /// </summary>
    public static class GuidValidationRulesExtensions
    {
        /// <summary>
        /// Must the be valid unique identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .NotNull()
                .NotEmpty()
                .Must(e => Guid.TryParse(e, out var guid))
                .WithMessage("Received invalid guid");

            return builderOptions;
        }

        public static IRuleBuilderOptions<T, TransactionModel> MustBeValidGuidInTransactionModel<T>(this IRuleBuilder<T, TransactionModel> ruleBuilder)
        {
            var builderOptions = ruleBuilder.SetValidator(new TransactionModelGuidValidator());

            return builderOptions;
        }

        public static IRuleBuilderOptions<T, Transaction> MustBeValidGuidInTransaction<T>(this IRuleBuilder<T, Transaction> ruleBuilder)
        {
            var builderOptions = ruleBuilder.SetValidator(new TransactionGuidValidator());

            return builderOptions;
        }
    }
}