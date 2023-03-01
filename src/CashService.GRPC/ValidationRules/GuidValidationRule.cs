﻿using FluentValidation;

namespace CashService.GRPC.ValidationRules
{
    // TODO: remove all empty lines
    // TODO: move functionality to ValidationRulesExtensions.cs file (description in CashTypeTypeValidationRule.cs)
    /// <summary>
    /// Validation rule for guid
    /// </summary>
    public static class GuidValidationRule
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

    // TODO: each validator class should have it's own cs file
    // TODO: change file location to CashService.GRPC.Validators
    public class TransactionGuidValidator : AbstractValidator<Transaction>
    {
        public TransactionGuidValidator()
        {
            RuleFor(x => x.Transactionid)
                .MustBeValidGuid();
        }
    }

    // TODO: each validator class should have it's own cs file
    // TODO: change file location to CashService.GRPC.Validators
    public class TransactionModelGuidValidator : AbstractValidator<TransactionModel>
    {
        public TransactionModelGuidValidator()
        {
            RuleFor(x => x.Profileid)
                .MustBeValidGuid();

            RuleForEach(x => x.Transactions)
                .MustBeValidGuidInTransaction();
        }
    }

    
}
