using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Models.Criterias
{
    public class FilterCriteria : OrderCriteria
    {
        public List<Guid>? UserIds { get; set; }
        public CashType? CashType { get; set; }
        public decimal? StartAmount { get; set; }
        public decimal? EndAmount { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
}
