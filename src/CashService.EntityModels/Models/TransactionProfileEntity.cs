using System.ComponentModel.DataAnnotations;

namespace CashService.EntityModels.Models
{
    public class TransactionProfileEntity
    {
        //[Key] public Guid Id { get; set; }
        [Key] public Guid ProfileId { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }

   
}
