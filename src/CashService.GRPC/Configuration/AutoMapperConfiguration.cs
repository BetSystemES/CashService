using AutoMapper;
using CashService.BusinessLogic.Models;

namespace CashService.GRPC.Configuration
{
    public class DataAccessProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile()
        {
            //proto->Entity
            CreateMap<Transaction, TransactionEntity>()
                .ForMember(dest => dest.TransactionId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Transactionid)))
                .ForMember(dest => dest.CashType,
                    opt =>
                    opt.MapFrom(src => src.Cashtype))
                .ForMember(dest => dest.Amount,
                    opt =>
                        opt.MapFrom(src => src.Amount))
                .ForMember(x => x.TransactionProfileId, opt => opt.Ignore())
                .ForMember(x => x.TransactionProfileEntity, opt => opt.Ignore());

            //Entity->proto
            CreateMap<TransactionEntity, Transaction>()
                .ForMember(dest => dest.Transactionid,
                    opt =>
                        opt.MapFrom(src => src.TransactionId.ToString()))
                .ForMember(dest => dest.Cashtype,
                    opt =>
                        opt.MapFrom(src => src.CashType))
                .ForMember(dest => dest.Amount,
                opt =>
                    opt.MapFrom(src => src.Amount));

          
            CreateMap<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>();
            CreateMap<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>();

            //proto->Entity
            CreateMap<TransactionModel, TransactionProfileEntity>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Profileid)))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));

            //Entity->proto
            CreateMap<TransactionProfileEntity, TransactionModel>()
                .ForMember(dest => dest.Profileid,
                    opt =>
                        opt.MapFrom(src => src.ProfileId.ToString()))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));


            CreateMap<CashType, BusinessLogic.Models.CashType>();
            CreateMap<CashType, CashType>().ReverseMap();
        }

    }
}