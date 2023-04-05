using CashService.GRPC;
using CashService.GRPC.Infrastructure.Validators;
using CashService.UnitTests.Validators.TestData;
using FluentAssertions;
using FluentValidation;

namespace CashService.UnitTests.Validators
{
    public class WithdrawRequestValidatorUnitTests
    {
        private readonly IValidator<WithdrawRequest> _validator;

        public WithdrawRequestValidatorUnitTests()
        {
            _validator = new WithdrawRequestValidator();
        }

        [Theory]
        [MemberData(nameof(TransactionModelRequestData.TransactionModelRequestDataValid), MemberType = typeof(TransactionModelRequestData))]
        public async Task WithdrawRequest_Should_Be_Valid(TransactionRequestModel transactionRequestModel)
        {
            // Arrange
            var model = new WithdrawRequest()
            {
                Withdrawrequest = transactionRequestModel
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
        public async Task WithdrawRequest_Should_Be_Invalid(TransactionRequestModel transactionRequestModel)
        {
            // Arrange
            var model = new WithdrawRequest()
            {
                Withdrawrequest = transactionRequestModel
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
