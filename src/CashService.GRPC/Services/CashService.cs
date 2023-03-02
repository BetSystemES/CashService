using AutoMapper;
using CashService.BusinessLogic.Contracts.IServices;
using CashService.EntityModels.Models;
using Grpc.Core;
using static CashService.GRPC.Services.Support;

namespace CashService.GRPC.Services
{
    // TODO: remove all empty lines
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

        public override async Task<GetTransactionsHistoryResponce> GetTransactionsHistory(GetTransactionsHistoryRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            // TODO: typo in profileid. Should be profileId
            //map
            Guid profileid = _mapper.Map<Guid>(request.Profileid);

            //cashService
            TransactionProfileEntity balanceResult = await _cashService.GetBalance(profileid, token);

            // TODO: typo in balanceResponce. Should be balanceResponse
            //map back
            TransactionModel balanceResponce = _mapper.Map<TransactionModel>(balanceResult);

            return new GetTransactionsHistoryResponce
            {
                Balance = balanceResponce
            };
        }

        public override async Task<GetBalanceResponce> GetBalance(GetBalanceRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            // TODO: typo in profileid. Should be profileId
            //map
            Guid profileid = _mapper.Map<Guid>(request.Profileid);

            //cashService
            TransactionProfileEntity balanceResult = await _cashService.CalcBalance(profileid, token);

            // TODO: typo in balanceResponce. Should be balanceResponse
            //map back
            TransactionModel balanceResponce = _mapper.Map<TransactionModel>(balanceResult);

            return new GetBalanceResponce
            {
                Balance = balanceResponce
            };
        }

        public override async Task<DepositResponce> Deposit(DepositRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            TransactionProfileEntity depositTransactionProfile = _mapper.Map<TransactionProfileEntity>(request.Deposit);

            //remap
            EntityRemapper(depositTransactionProfile);

            //cashService
            await _cashService.Deposit(depositTransactionProfile, token);

            return new DepositResponce();
        }


        public override async Task<WithdrawResponce> Withdraw(WithdrawRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            TransactionProfileEntity withdrawTransactionProfile = _mapper.Map<TransactionProfileEntity>(request.Withdrawrequest);

            //remap
            EntityRemapper(withdrawTransactionProfile);
            WithdrawValueConverter(withdrawTransactionProfile);

            //cashService
            TransactionProfileEntity withdrawResult = await _cashService.Withdraw(withdrawTransactionProfile, token);

            // TODO: typo in withdrawResponce. Should be withdrawResponse
            //map back
            TransactionModel withdrawResponce = _mapper.Map<TransactionModel>(withdrawResult);

            return new WithdrawResponce()
            {
                Withdrawresponce = withdrawResponce
            };
        }

       

        public override async Task<DepositRangeResponce> DepositRange(DepositRangeRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            //IEnumerable<TransactionProfileEntity> depositRange = _mapper.Map<IEnumerable<TransactionModel>, IEnumerable<TransactionProfileEntity>>(request.Depositrangerequest);
            var depositRangeTransactionProfileEntities = ReMapRepeatedTransactionModel(_mapper, request.Depositrangerequest, OperationType.Deposit);

            //cashService
            await _cashService.DepositRange(depositRangeTransactionProfileEntities, token);

            return new DepositRangeResponce();
        }

       

        public override async Task<WithdrawRangeResponce> WithdrawRange(WithdrawRangeRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            //var withdrawRange = _mapper.Map<IEnumerable<TransactionModel>, IEnumerable<TransactionEntity>>(request.Withdrawrangerequest);
            var withdrawRangeTransactionProfileEntities = ReMapRepeatedTransactionModel(_mapper, request.Withdrawrangerequest, OperationType.Withdraw);

            //profile service
            List<TransactionProfileEntity> withdrawRangeResult = await _cashService.WithdrawRange(withdrawRangeTransactionProfileEntities, token);

            // TODO: typo in rangesResonces. Should be rangesResponse
            // TODO: remove comment
            //map back
            //IEnumerable<TransactionModel> discounts = _mapper.Map<IEnumerable<TransactionProfileEntity>, IEnumerable<TransactionModel>>(withdrawRangeResult);
            var rangesResonces = ReMapBackRepeatedTransactionModel(_mapper, withdrawRangeResult);

            // TODO: typo in responce. Should be response
            WithdrawRangeResponce responce = new WithdrawRangeResponce();

            responce.Withdrawrangeresponce.AddRange(rangesResonces);

            return responce;
        }

    }
}