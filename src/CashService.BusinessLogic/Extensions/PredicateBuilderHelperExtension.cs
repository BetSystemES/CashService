using System.Linq.Expressions;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Helpers;
using CashService.BusinessLogic.Models.Criterias;

namespace CashService.BusinessLogic.Extensions
{
    public static class PredicateBuilderHelperExtension
    {
        public static Expression<Func<TransactionEntity, bool>> FilterPredicateByDate(this Expression<Func<TransactionEntity, bool>> predicate, FilterCriteria filter)
        {
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
               return predicate.And(x => x.Date.Between(filter.StartDate.Value, filter.EndDate.Value));
            }

            if (filter.StartDate.HasValue && !filter.EndDate.HasValue)
            {
                return predicate.And(x => x.Date.Date >= filter.StartDate.Value.Date);
            }

            if (filter.EndDate.HasValue && !filter.StartDate.HasValue)
            {
                return predicate.And(x => x.Date.Date <= filter.EndDate.Value.Date);
            }

            return predicate;
        }

        public static Expression<Func<TransactionEntity, bool>> FilterPredicateByAmount(this Expression<Func<TransactionEntity, bool>> predicate, FilterCriteria filter)
        {
            if (filter.StartAmount.HasValue && filter.EndAmount.HasValue)
            {
               return predicate.And(x => x.Amount >= filter.StartAmount.Value)
                    .And(x => x.Amount <= filter.EndAmount.Value);
            }

            if (filter.StartAmount.HasValue && !filter.EndAmount.HasValue)
            {
                return predicate.And(x => x.Amount >= filter.StartAmount.Value);
            }

            if (filter.EndAmount.HasValue && !filter.StartAmount.HasValue)
            {
                return predicate.And(x => x.Amount <= filter.EndAmount.Value);
            }

            return predicate;
        }
    }
}