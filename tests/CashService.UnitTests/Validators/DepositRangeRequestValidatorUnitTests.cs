using CashService.GRPC;
using CashService.GRPC.Infrastructure.Validators;
using CashService.UnitTests.Validators.TestData;
using FluentAssertions;
using FluentValidation;
using Google.Protobuf.Collections;

namespace CashService.UnitTests.Validators
{
    public class DepositRangeRequestValidatorUnitTests
    {
        private readonly IValidator<DepositRangeRequest> _validator;

        public DepositRangeRequestValidatorUnitTests()
        {
            _validator = new DepositRangeRequestValidator();
        }

        [Theory]
        [MemberData(nameof(RepeatedTransactionModelRequestData.RepeatedTransactionModelRequestDataValid), MemberType = typeof(RepeatedTransactionModelRequestData))]
        public async Task DepositRangeRequest_Should_Be_Valid(RepeatedField<TransactionModel> transactionModels)
        {
            // Arrange
            var model = new DepositRangeRequest()
            {
                DepositRangeRequests = { transactionModels }
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
        public async Task DepositRangeRequest_Should_Be_Invalid(RepeatedField<TransactionModel> transactionModels)
        {
            // Arrange
            var model = new DepositRangeRequest()
            {
                DepositRangeRequests =  { transactionModels }
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
