using AutoMapper;
using CashService.BusinessLogic.Entities;
using Google.Protobuf.WellKnownTypes;

namespace CashService.GRPC.Infrastructure.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            //proto->Entity
            CreateMap<Transaction, TransactionEntity>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.Ignore())
                .ForMember(dest => dest.CashType,
                    opt =>
                        opt.MapFrom(src => src.CashType))
                .ForMember(dest => dest.Amount,
                    opt =>
                        opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date,
                    opt =>
                        opt.MapFrom(src => src.Date.ToDateTimeOffset()))
                .ForMember(x => x.ProfileId, opt => opt.Ignore())
                .ForMember(x => x.ProfileEntity, opt => opt.Ignore());

            //Entity->proto
            CreateMap<TransactionEntity, Transaction>()
                .ForMember(dest => dest.CashType,
                    opt =>
                        opt.MapFrom(src => src.CashType))
                .ForMember(dest => dest.Amount,
                    opt =>
                        opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date,
                    opt =>
                        opt.MapFrom(src => src.Date.ToTimestamp()));
        }
    }
}