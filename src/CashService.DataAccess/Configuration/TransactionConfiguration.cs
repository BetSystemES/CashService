using CashService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashService.DataAccess.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(x => x.TransactionId);
            builder.Property(x => x.TransactionId).ValueGeneratedNever();

            builder.Property(x => x.TransactionProfileId).IsRequired();

            builder.HasOne(x => x.TransactionProfileEntity)
                .WithMany(y => y.Transactions)
                .HasForeignKey(z => z.TransactionProfileId);

            builder.Property(x => x.CashType);
            builder.Property(x => x.Amount);

            builder.ToTable("Transaction");
        }
    }
}
