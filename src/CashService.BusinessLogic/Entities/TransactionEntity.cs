using System.ComponentModel.DataAnnotations;
using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Entities
{
    public class TransactionEntity
    {
        [Key] public Guid Id { get; set; }
        [Required] public Guid ProfileId { get; set; }
        public ProfileEntity ProfileEntity { get; set; }

        //public CashValue CashValue { get; set; }
        public CashType CashType { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
    }

    //public class CashValue
    //{
    //    public CashType CashType { get; set; }
    //    public decimal Amount { get; set; }
    //}
}