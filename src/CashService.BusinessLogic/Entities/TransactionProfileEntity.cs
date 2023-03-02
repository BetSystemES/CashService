using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.BusinessLogic.Entities
{
    public class TransactionProfileEntity
    {
        [Key] public Guid ProfileId { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }
}
