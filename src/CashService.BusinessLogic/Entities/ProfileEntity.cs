using System.ComponentModel.DataAnnotations;

namespace CashService.BusinessLogic.Entities
{
    public class ProfileEntity
    {
        [Key] public Guid Id { get; set; }
        public decimal CashAmount { get; set; }

        public List<TransactionEntity> Transactions { get; set; }

        public uint RowVersion { get; set; }
    }
}