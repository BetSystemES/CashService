using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashService.DataAccess.Configuration
{
    public class TransactionProfileEntityConfiguration : IEntityTypeConfiguration<TransactionProfileEntity>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<TransactionProfileEntity> builder)
        {
            builder.HasKey(x => x.ProfileId);
            builder.Property(x => x.ProfileId).ValueGeneratedNever();

            builder.HasMany(x => x.Transactions)
                .WithOne(y => y.TransactionProfileEntity)
                .HasForeignKey(z => z.TransactionProfileId);

            builder.ToTable("TransactionProfile");
        }
    }
}