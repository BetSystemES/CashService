using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CashService.BusinessLogic.Models
{
    public class TransactionProfileEntity
    {
        //[Key] public Guid Id { get; set; }
        [Key] public Guid ProfileId { get; set; }
        public List<TransactionEntity>  Transactions { get; set; }
    }

   
}
