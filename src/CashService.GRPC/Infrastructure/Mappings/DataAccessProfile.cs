using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Criterias;

namespace CashService.GRPC.Infrastructure.Mappings
{
    public class DataAccessProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile()
        {
            //proto->Entity
            CreateMap<Transaction, TransactionEntity>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.TransactionId)))
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
                .ForMember(dest => dest.TransactionId,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CashType,
                    opt =>
                        opt.MapFrom(src => src.CashType))
                .ForMember(dest => dest.Amount,
                opt =>
                    opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date,
                    opt =>
                        opt.MapFrom(src => src.Date.ToTimestamp()));


            //CreateMap<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>();
            //CreateMap<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>();

            //proto->Entity
            CreateMap<TransactionModel, ProfileEntity>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.ProfileId)))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));
            //.ForMember(x=>x.Id, opt=>opt.Ignore());

            //Entity->proto
            CreateMap<ProfileEntity, TransactionModel>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Transactions,
                    opt =>
                        opt.MapFrom(src => src.Transactions));

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));


            CreateMap<CashType, BusinessLogic.Models.Enums.CashType>().ReverseMap();

            CreateMap<FilterCriteria, TransactionHistoryFilter>()
                .ForMember(dest => dest.UserIds,
                    opt =>
                        opt.MapFrom(src => src.UserIds.Select(x => x.ToString())))
                .ForMember(dest => dest.StartDate,
                    opt =>
                        opt.MapFrom(src => src.StartDate != null ? ((DateTimeOffset)src.StartDate!).ToTimestamp() : (DateTimeOffset.MinValue).ToTimestamp()))
                .ForMember(dest => dest.EndDate,
                    opt =>
                        opt.MapFrom(src => src.EndDate != null ? ((DateTimeOffset)src.EndDate!).ToTimestamp() : (DateTimeOffset.MinValue).ToTimestamp()));

            CreateMap<TransactionHistoryFilter, FilterCriteria>()
                .ForMember(dest => dest.UserIds,
                    opt =>
                        opt.MapFrom(src => src.UserIds.Select(Guid.Parse).ToList()))
                .ForMember(dest => dest.PageSize,
                    opt =>
                        opt.MapFrom(src => src.PageSize == -1 ? (int?)null : src.PageSize))
                .ForMember(dest => dest.PageNumber,
                    opt =>
                        opt.MapFrom(src => src.PageNumber == -1 ? (int?)null : src.PageNumber))
                .ForMember(dest => dest.StartAmount,
                    opt =>
                        opt.MapFrom(src => src.StartAmount == -1 ? (decimal?)null : (decimal)src.StartAmount))
                .ForMember(dest => dest.EndAmount,
                    opt =>
                        opt.MapFrom(src => src.EndAmount == -1 ? (decimal?)null : (decimal)src.EndAmount))
                .ForMember(dest => dest.StartDate,
                    opt =>
                        opt.MapFrom(src => src.StartDate == (DateTimeOffset.MinValue).ToTimestamp() ? (DateTimeOffset?)null : src.StartDate.ToDateTimeOffset()))
                .ForMember(dest => dest.EndDate,
                    opt =>
                        opt.MapFrom(src => src.EndDate == (DateTimeOffset.MinValue).ToTimestamp() ? (DateTimeOffset?)null : src.EndDate.ToDateTimeOffset()));
        }
    }
}