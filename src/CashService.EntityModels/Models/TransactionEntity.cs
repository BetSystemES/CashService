using System.ComponentModel.DataAnnotations;

namespace CashService.EntityModels.Models
{
    public class TransactionEntity
    {
        [Key] public Guid TransactionId { get; set; }

        [Required] public Guid TransactionProfileId { get; set; }
        public TransactionProfileEntity TransactionProfileEntity { get; set; }

        public CashType CashType { get; set; }
        public decimal Amount { get; set; }
    }

    public enum CashType
    {
        Unspecified = 0,
        Cash = 1,
        Bonus = 2
    }

}
