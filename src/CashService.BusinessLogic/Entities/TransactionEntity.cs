using System.ComponentModel.DataAnnotations;
using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Entities
{
    public class TransactionEntity
    {
        [Key] public Guid TransactionId { get; set; }
        [Required] public Guid TransactionProfileId { get; set; }
        public TransactionProfileEntity TransactionProfileEntity { get; set; }
        public CashType CashType { get; set; }
        public decimal Amount { get; set; }
    }
}
