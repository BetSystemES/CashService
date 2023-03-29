using System.ComponentModel.DataAnnotations;

namespace CashService.BusinessLogic.Entities
{
    public class ProfileEntity
    {
        [Key] public Guid Id { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }
}