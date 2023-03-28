using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Models.Criterias
{
    public class OrderCriteria : PaginationCriteria
    {
        public string? ColumnName { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
