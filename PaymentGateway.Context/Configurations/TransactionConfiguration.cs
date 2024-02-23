//using Higgs.Data.Common.Context.Postgresql.Configurations;
//using Higgs.Data.Common.Context.Postgresql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Entities;

namespace PaymentGateway.Context.Configurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .ToTable(nameof(Transaction))
                .HasKey(a => a.TransactionId);

            builder
                .Property(a => a.TransactionId)
                .HasColumnType("uuid")
                .IsRequired()
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");

            builder
                .Property(a => a.MerchantTransactionId)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder
                .Property(a => a.ProviderTransactionId)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder
                .Property(a => a.Provider)
                .HasColumnType("smallint")
                .IsRequired();

            builder
                .Property(a => a.TokenId)
                .HasColumnType("uuid")
                .IsRequired();


            builder
                .Property(a => a.Status)
                .HasColumnType("smallint")
                .IsRequired();

            builder
                .Property(a => a.Amount)
                .HasColumnType("decimal(19,4)")
                .IsRequired();

            builder
                .Property(a => a.BankCode)
                .HasColumnType("varchar(10)")
                .IsRequired();

            builder
                .Property(a => a.PlayerId)
                .HasColumnType("varchar(32)")
                .IsRequired();

            builder
                .Property(a => a.PlayerRealName)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder
                .Property(a => a.PlayerCardNumber)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(a => a.CreatedUser)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(a => a.UpdatedUser)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(a => a.CreatedDate)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();

            builder
                .Property(a => a.UpdatedDate)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();
        }
    }
}
