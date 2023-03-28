using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Models.Criterias
{
    public class FilterCriteria : OrderCriteria
    {
        public List<Guid>? UserIds { get; set; }
        public CashType? CashType { get; set; }
        public Equals? EqualsAmount { get; set; }
        public decimal? Amount { get; set; }
        public Equals? EqualsDate{ get; set; }
        public DateTimeOffset? Date { get; set; }
    }
}
