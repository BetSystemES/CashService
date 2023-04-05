using AutoMapper;

namespace CashService.GRPC.Infrastructure.Mappings
{
    public class DataAccessProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile()
        {
            CreateMap<string, Guid>()
                .ConvertUsing((x, res) => res = Guid.TryParse(x, out var id) ? id : Guid.Empty);
            CreateMap<Guid?, string>()
                .ConvertUsing((x, res) => res = x?.ToString() ?? string.Empty);

            CreateMap<CashType, BusinessLogic.Models.Enums.CashType>().ReverseMap();
        }
    }
}