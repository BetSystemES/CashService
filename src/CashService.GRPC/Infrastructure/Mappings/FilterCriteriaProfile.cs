using AutoMapper;
using CashService.BusinessLogic.Models.Criterias;
using Google.Protobuf.WellKnownTypes;

namespace CashService.GRPC.Infrastructure.Mappings
{
    public class FilterCriteriaProfile : Profile
    {
        public FilterCriteriaProfile()
        {
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
        
            CreateMap<DateTimeOffset, Timestamp>()
                .ConvertUsing((x, res) => res = x.ToTimestamp());
            CreateMap<Timestamp, DateTimeOffset>()
                .ConvertUsing((x, res) => res = x.ToDateTimeOffset());
        }
    }
}