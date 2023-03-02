using System.ComponentModel.DataAnnotations;

namespace CashService.EntityModels.Models
{
    // TODO: Remove empty lines
    // TODO: Change file location to CashService.BusinessLogic.Entities
    public class TransactionProfileEntity
    {
        // TODO: remove comment
        //[Key] public Guid Id { get; set; }
        [Key] public Guid ProfileId { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }

   
}
