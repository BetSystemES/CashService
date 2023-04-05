using AutoMapper;
using CashService.BusinessLogic.Entities;

namespace CashService.GRPC.Infrastructure.Mappings
{
    public class TransactionModelProfile : Profile
    {
        public TransactionModelProfile()
        {
            //proto->Entity
            CreateMap<TransactionModel, ProfileEntity>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.ProfileId)))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions))
                .ForMember(x => x.RowVersion, opt => opt.Ignore());
            //.ForMember(x=>x.Id, opt=>opt.Ignore());

            //Entity->proto
            CreateMap<ProfileEntity, TransactionModel>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));
        }
    }
}