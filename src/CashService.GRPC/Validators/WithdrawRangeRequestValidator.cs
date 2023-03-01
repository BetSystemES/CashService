﻿using CashService.GRPC.ValidationRules;
using FluentValidation;

namespace CashService.GRPC.Validators
{
    // TODO: Change file location to CashService.Grpc.Infrastructure.Validators
    /// <summary>
    /// Validation rules for <seealso cref="WithdrawRangeRequest"/>
    /// </summary>
    public class WithdrawRangeRequestValidator : AbstractValidator<WithdrawRangeRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawRangeRequestValidator"/> class.
        /// </summary>
        public WithdrawRangeRequestValidator()
        {
            RuleForEach(e => e.Withdrawrangerequest)
                .MustBeValidCashTypeInTransactionModel();

            RuleForEach(e => e.Withdrawrangerequest)
                .MustBeValidGuidInTransactionModel();
        }
    }
}