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
                if (filter.StartDate.Value == filter.EndDate.Value)
                {
                    return predicate.And(x => x.Date == filter.StartDate.Value);
                }

                return predicate.And(x => x.Date.Between(filter.StartDate.Value, filter.EndDate.Value));
            }

            if (filter.StartDate.HasValue && filter.EndDate == null)
            {
                return predicate.And(x => x.Date >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue && filter.StartDate == null)
            {
                return predicate.And(x => x.Date <= filter.EndDate.Value);
            }

            return predicate;
        }

        public static Expression<Func<TransactionEntity, bool>> FilterPredicateByAmount(this Expression<Func<TransactionEntity, bool>> predicate, FilterCriteria filter)
        {
            if (filter.StartAmount.HasValue && filter.EndAmount.HasValue)
            {
                if (filter.StartAmount.Value == filter.EndAmount.Value)
                {
                    return predicate.And(x => x.Amount == filter.StartAmount.Value);
                }

                return predicate.And(x => x.Amount >= filter.StartAmount.Value)
                    .And(x => x.Amount <= filter.EndAmount.Value);
            }

            if (filter.StartAmount.HasValue && filter.EndAmount == null)
            {
                return predicate.And(x => x.Amount >= filter.StartAmount.Value);
            }

            if (filter.EndAmount.HasValue && filter.StartAmount == null)
            {
                return predicate.And(x => x.Amount <= filter.EndAmount.Value);
            }

            return predicate;
        }
    }
}