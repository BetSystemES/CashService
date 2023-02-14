using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.GRPC;
using CashService.GRPC.Validators;
using FluentAssertions;
using FluentValidation;
using Google.Protobuf.Collections;

namespace CashService.UnitTests.Validators
{
    public class WithdrawRangeRequestValidatorUnitTests
    {
        private readonly IValidator<WithdrawRangeRequest> _validator;

        public WithdrawRangeRequestValidatorUnitTests()
        {
            _validator = new WithdrawRangeRequestValidator();
        }

        [Theory]
        [MemberData(nameof(RepeatedTransactionModelRequestData.RepeatedTransactionModelRequestDataValid), MemberType = typeof(RepeatedTransactionModelRequestData))]
        public async Task WithdrawRangeRequest_Should_Be_Valid(RepeatedField<TransactionModel> transactionModels)
        {
            // Arrange
            var model = new WithdrawRangeRequest()
            {
                Withdrawrangerequest = {  transactionModels }
            };

            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(true);
        }

        [Theory]
        [MemberData(nameof(RepeatedTransactionModelRequestData.RepeatedTransactionModelRequestDataInvalid), MemberType = typeof(RepeatedTransactionModelRequestData))]
        public async Task WithdrawRangeRequest_Should_Be_Invalid(RepeatedField<TransactionModel> transactionModels)
        {
            // Arrange
            var model = new WithdrawRangeRequest()
            {
                Withdrawrangerequest =  { transactionModels }
            };

            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(false);
        }
    }
}
