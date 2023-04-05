using AutoMapper;
using CashService.BusinessLogic.Entities;

namespace CashService.GRPC.Infrastructure.Mappings
{
    public class TransactionRequestModelProfile : Profile
    {
        public TransactionRequestModelProfile()
        {
            //proto->Entity
            CreateMap<TransactionRequestModel, ProfileEntity>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.ProfileId)))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions))
                .ForMember(x => x.RowVersion, opt => opt.Ignore())
                .ForMember(x => x.CashAmount, opt => opt.Ignore());

            //Entity->proto
            CreateMap<ProfileEntity, TransactionRequestModel>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));
        }
    }
}