using CashService.GRPC;
using CashService.GRPC.Infrastructure.Validators;
using CashService.UnitTests.Validators.TestData;
using FluentAssertions;
using FluentValidation;

namespace CashService.UnitTests.Validators
{
    public class DepositRequestValidatorUnitTests
    {
        private readonly IValidator<DepositRequest> _validator;

        public DepositRequestValidatorUnitTests()
        {
            _validator = new DepositRequestValidator();
        }

        [Theory]
        [MemberData(nameof(TransactionModelRequestData.TransactionModelRequestDataValid), MemberType = typeof(TransactionModelRequestData))]
        public async Task DepositRequest_Should_Be_Valid(TransactionModel transactionModel)
        {
            // Arrange
            var model = new DepositRequest()
            {
                Deposit = transactionModel
            };

            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(true);
        }

        [Theory]
        [MemberData(nameof(TransactionModelRequestData.TransactionModelRequestDataInvalid), MemberType = typeof(TransactionModelRequestData))]
        public async Task DepositRequest_Should_Be_Invalid(TransactionModel transactionModel)
        {
            // Arrange
            var model = new DepositRequest()
            {
                Deposit = transactionModel
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
