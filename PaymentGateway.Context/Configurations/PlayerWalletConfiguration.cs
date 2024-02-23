//using Higgs.Data.Common.Context.Postgresql.Configurations;
//using Higgs.Data.Common.Context.Postgresql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Entities;

namespace PaymentGateway.Context.Configurations
{
    internal sealed class PlayerWalletConfiguration : IEntityTypeConfiguration<PlayerWallet>
    {
        public void Configure(EntityTypeBuilder<PlayerWallet> builder)
        {
            builder
                .ToTable(nameof(PlayerWallet))
                .HasKey(a => a.PlayerId);

            builder
                .Property(a => a.PlayerId)
                .HasColumnType("varchar(32)")
                .IsRequired();

            builder
                .Property(a => a.Balance)
                .HasColumnType("decimal(19,4)")
                .IsRequired();

            builder
                .Property(a => a.UpdatedDate)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();
        }
    }
}
