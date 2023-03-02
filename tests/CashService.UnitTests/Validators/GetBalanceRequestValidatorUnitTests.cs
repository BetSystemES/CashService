using CashService.GRPC;
using CashService.GRPC.Infrastructure.Validators;
using FluentAssertions;
using FluentValidation;

namespace CashService.UnitTests.Validators
{
    public class GetBalanceRequestValidatorUnitTests
    {
        private readonly IValidator<GetBalanceRequest> _validator;

        public GetBalanceRequestValidatorUnitTests()
        {
            _validator = new GetBalanceRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6")]
        public async Task GetBalanceRequest_Should_Be_Valid(string profileId)
        {
            // Arrange
            var model = new GetBalanceRequest()
            {
                ProfileId = profileId
            };

            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(true);
        }

        [Theory]
        [InlineData("c0631390")]
        [InlineData("")]
        public async Task GetBalanceRequest_Should_Be_Invalid(string profileId)
        {
            // Arrange
            var model = new GetBalanceRequest()
            {
                ProfileId = profileId
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
