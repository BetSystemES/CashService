using CashService.BusinessLogic.Entities;
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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ProfileId).IsRequired();

            builder.HasOne(x => x.ProfileEntity)
                .WithMany(y => y.Transactions)
                .HasForeignKey(z => z.ProfileId);

            builder.Property(x => x.CashType);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Date)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            builder.ToTable("Transaction");
        }
    }
}
