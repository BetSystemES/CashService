using AutoMapper;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.GRPC.Enums;
using CashService.GRPC.Extensions;
using Grpc.Core;

namespace CashService.GRPC.Services
{
    public class CashService : GRPC.CashService.CashServiceBase
    {
        private readonly ILogger<CashService> _logger;

        private readonly IMapper _mapper;

        private readonly ICashService _cashService;
        public CashService(ILogger<CashService> logger, IMapper mapper, ICashService cashService)
        {
            _logger = logger;
            _mapper = mapper;
            _cashService = cashService;
        }

        public override async Task<GetTransactionsHistoryResponse> GetTransactionsHistory(GetTransactionsHistoryRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid profileid = _mapper.Map<Guid>(request.ProfileId);

            //cashService
            TransactionProfileEntity balanceResult = await _cashService.GetBalance(profileid, token);

            //map back
            TransactionModel balanceResponse = _mapper.Map<TransactionModel>(balanceResult);

            return new GetTransactionsHistoryResponse
            {
                Balance = balanceResponse
            };
        }

        public override async Task<GetBalanceResponse> GetBalance(GetBalanceRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid profileId = _mapper.Map<Guid>(request.ProfileId);

            //cashService
            TransactionProfileEntity balanceResult = await _cashService.CalcBalance(profileId, token);

            //map back
            TransactionModel balanceResponse = _mapper.Map<TransactionModel>(balanceResult);

            return new GetBalanceResponse
            {
                Balance = balanceResponse
            };
        }

        public override async Task<DepositResponse> Deposit(DepositRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            TransactionProfileEntity depositTransactionProfile = _mapper.Map<TransactionProfileEntity>(request.Deposit);

            //remap
            depositTransactionProfile.EntityRemapper();

            //cashService
            await _cashService.Deposit(depositTransactionProfile, token);

            return new DepositResponse();
        }

        public override async Task<WithdrawResponse> Withdraw(WithdrawRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            TransactionProfileEntity withdrawTransactionProfile = _mapper.Map<TransactionProfileEntity>(request.Withdrawrequest);

            //remap
            withdrawTransactionProfile.EntityRemapper();
            withdrawTransactionProfile.WithdrawValueConverter();

            //cashService
            TransactionProfileEntity withdrawResult = await _cashService.Withdraw(withdrawTransactionProfile, token);

            //map back
            TransactionModel withdrawResponse = _mapper.Map<TransactionModel>(withdrawResult);

            return new WithdrawResponse()
            {
                Withdrawresponse = withdrawResponse
            };
        }

        public override async Task<DepositRangeResponse> DepositRange(DepositRangeRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            var depositRangeTransactionProfileEntities = request.DepositRangeRequests.ReMapRepeatedTransactionModel(_mapper, OperationType.Deposit);

            //cashService
            await _cashService.DepositRange(depositRangeTransactionProfileEntities, token);

            return new DepositRangeResponse();
        }

        public override async Task<WithdrawRangeResponse> WithdrawRange(WithdrawRangeRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            var withdrawRangeTransactionProfileEntities = request.WithdrawRangeRequests.ReMapRepeatedTransactionModel(_mapper,  OperationType.Withdraw);

            //profile service
            List<TransactionProfileEntity> withdrawRangeResult = await _cashService.WithdrawRange(withdrawRangeTransactionProfileEntities, token);

            //map back
            var rangesResponses = withdrawRangeResult.ReMapBackRepeatedTransactionModel(_mapper);

            WithdrawRangeResponse response = new ();

            response.WithdrawRangeResponses.AddRange(rangesResponses);

            return response;
        }
    }
}