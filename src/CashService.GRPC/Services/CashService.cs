using AutoMapper;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models;
using CashService.GRPC.Enums;
using CashService.GRPC.Extensions;
using Grpc.Core;
using CashService.BusinessLogic.Models.Criterias;

namespace CashService.GRPC.Services
{
    public class CashService : GRPC.CashService.CashServiceBase
    {
        private readonly ILogger<CashService> _logger;

        private readonly IMapper _mapper;

        private readonly ICashService _cashService;
        private readonly IProfileService _profileService;

        public CashService(ILogger<CashService> logger, IMapper mapper, ICashService cashService, IProfileService profileService)
        {
            _logger = logger;
            _mapper = mapper;
            _cashService = cashService;
            _profileService = profileService;
        }

        public override async Task<CreateCashProfileResponse> CreateCashProfile(CreateCashProfileRequest request, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;
            var userId = _mapper.Map<Guid>(request.UserId);

            await _profileService.CreateProfile(userId, cancellationToken);

            return new CreateCashProfileResponse();
        }

        public override async Task<GetTransactionsHistoryResponse> GetTransactionsHistory(GetTransactionsHistoryRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid profileid = _mapper.Map<Guid>(request.ProfileId);

            //cashService
            ProfileEntity balanceResult = await _cashService.GetTransactionsHistory(profileid, token);

            //map back
            TransactionModel balanceResponse = _mapper.Map<TransactionModel>(balanceResult);

            return new GetTransactionsHistoryResponse
            {
                Balance = balanceResponse
            };
        }

        public override async Task<GetPagedTransactionsHistoryResponse> GetPagedTransactionHistory(GetTransactionHistoryWithFilterRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            FilterCriteria filterCriteria = _mapper.Map<FilterCriteria>(request.TransactionHistoryFilter);

            AccessCheck.CheckIds(filterCriteria.UserIds, context.GetHttpContext().User);

            //profile service
            PagedResponse<TransactionEntity> items = await _cashService.GetPagedTransactions(filterCriteria, token);

            //map back
            IEnumerable<TransactionModel> transactionEntities = _mapper.Map<IEnumerable<TransactionEntity>, IEnumerable<TransactionModel>>(items.Data);

            GetPagedTransactionsHistoryResponse response = new GetPagedTransactionsHistoryResponse()
            {
                TotalCount = items.TotalCount
            };

            response.Transactions.AddRange(transactionEntities);

            return response;
        }

        public override async Task<GetBalanceResponse> GetBalance(GetBalanceRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid profileId = _mapper.Map<Guid>(request.ProfileId);

            //cashService
            ProfileEntity balanceResult = await _cashService.CalcBalanceWithinCashtype(profileId, token);

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
            ProfileEntity depositProfile = _mapper.Map<ProfileEntity>(request.Deposit);

            //remap
            depositProfile.EntityRemapper();

            //cashService
            var result = await _cashService.Deposit(depositProfile, token);

            //map back
            var transactionModel = _mapper.Map<TransactionModel>(result);

            return new DepositResponse()
            {
                Depositresponse = transactionModel
            };
        }

        public override async Task<WithdrawResponse> Withdraw(WithdrawRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            ProfileEntity withdrawProfile = _mapper.Map<ProfileEntity>(request.Withdrawrequest);

            //remap
            withdrawProfile.EntityRemapper();
            withdrawProfile.WithdrawValueConverter();

            ProfileEntity? withdrawResult = default;
            try
            {
                //cashService
                withdrawResult = await _cashService.Withdraw(withdrawProfile, token);
            }
            catch (Exception)
            {
                throw new RpcException(Status.DefaultCancelled, 
                    $"An error occurred in widthdraw operation with ProfileId:{request.Withdrawrequest.ProfileId}");
            }

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
            List<ProfileEntity> withdrawRangeResult = await _cashService.WithdrawRange(withdrawRangeTransactionProfileEntities, token);

            //map back
            var rangesResponses = withdrawRangeResult.ReMapBackRepeatedTransactionModel(_mapper);

            WithdrawRangeResponse response = new ();

            response.WithdrawRangeResponses.AddRange(rangesResponses);

            return response;
        }
    }
}