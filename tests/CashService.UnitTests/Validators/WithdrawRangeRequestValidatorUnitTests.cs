using CashService.GRPC;
using CashService.GRPC.Infrastructure.Validators;
using CashService.UnitTests.Validators.TestData;
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
                WithdrawRangeRequests = {  transactionModels }
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
                WithdrawRangeRequests =  { transactionModels }
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
