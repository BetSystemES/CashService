using AutoMapper;
using CashService.BusinessLogic.Entities;

namespace CashService.GRPC.Infrastructure.Mappings
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
                        opt.MapFrom(src => Guid.Parse(src.TransactionId)))
                .ForMember(dest => dest.CashType,
                    opt =>
                    opt.MapFrom(src => src.CashType))
                .ForMember(dest => dest.Amount,
                    opt =>
                        opt.MapFrom(src => src.Amount))
                .ForMember(x => x.TransactionProfileId, opt => opt.Ignore())
                .ForMember(x => x.TransactionProfileEntity, opt => opt.Ignore());

            //Entity->proto
            CreateMap<TransactionEntity, Transaction>()
                .ForMember(dest => dest.TransactionId,
                    opt =>
                        opt.MapFrom(src => src.TransactionId.ToString()))
                .ForMember(dest => dest.CashType,
                    opt =>
                        opt.MapFrom(src => src.CashType))
                .ForMember(dest => dest.Amount,
                opt =>
                    opt.MapFrom(src => src.Amount));


            //CreateMap<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>();
            //CreateMap<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>();

            //proto->Entity
            CreateMap<TransactionModel, TransactionProfileEntity>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.ProfileId)))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));
            //.ForMember(x=>x.Id, opt=>opt.Ignore());

            //Entity->proto
            CreateMap<TransactionProfileEntity, TransactionModel>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => src.ProfileId.ToString()))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));


            CreateMap<CashType, BusinessLogic.Models.Enums.CashType>().ReverseMap();
        }
    }
}