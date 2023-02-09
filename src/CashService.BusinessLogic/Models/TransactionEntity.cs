using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.BusinessLogic.Models
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
