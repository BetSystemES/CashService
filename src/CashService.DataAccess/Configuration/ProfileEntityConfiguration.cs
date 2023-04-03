using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashService.DataAccess.Configuration
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<ProfileEntity>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.CashAmount);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.HasMany(x => x.Transactions)
                .WithOne(y => y.ProfileEntity)
                .HasForeignKey(z => z.ProfileId);

            builder.ToTable("TransactionProfile");
        }
    }
}