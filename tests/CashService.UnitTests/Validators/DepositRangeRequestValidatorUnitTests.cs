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
                Depositrangerequest = { transactionModels }
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
                Depositrangerequest =  { transactionModels }
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
