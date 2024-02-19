using Higgs.Data.Common.Context.Postgresql.Configurations;
using Higgs.Data.Common.Context.Postgresql.Extensions;
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
                .HasColumnType(SqlDbTypes.Uuid)
                .IsRequired()
                .HasGuidDefaultValueSql();


            builder
                .Property(a => a.MerchantTransactionId)
                .HasColumnType(SqlDbTypes.Varchar(36))
                .IsRequired();

            builder
                .Property(a => a.ProviderTransactionId)
                .HasColumnType(SqlDbTypes.Varchar(36))
                .IsRequired();

            builder
                .Property(a => a.Provider)
                .HasColumnType(SqlDbTypes.Smallint)
                .IsRequired();

            builder
                .Property(a => a.TokenId)
                .HasColumnType(SqlDbTypes.Uuid)
                .IsRequired();


            builder
                .Property(a => a.Status)
                .HasColumnType(SqlDbTypes.Smallint)
                .IsRequired();


            builder
                .Property(a => a.Amount)
                .HasColumnType(SqlDbTypes.Amount)
                .IsRequired();

            builder
                .Property(a => a.BankCode)
                .HasColumnType(SqlDbTypes.Varchar(10))
                .IsRequired();

            builder
                .Property(a => a.PlayerId)
                .HasColumnType(SqlDbTypes.Varchar(64))
                .IsRequired();

            builder
                .Property(a => a.PlayerRealName)
                .HasColumnType(SqlDbTypes.Varchar(64))
                .IsRequired();

            builder
                .Property(a => a.PlayerCardNumber)
                .HasColumnType(SqlDbTypes.Varchar(50))
                .IsRequired();

            builder
                .Property(a => a.CreatedUser)
                .HasColumnType(SqlDbTypes.Varchar(20))
                .IsRequired();

            builder
                .Property(a => a.UpdatedUser)
                .HasColumnType(SqlDbTypes.Varchar(20))
                .IsRequired();

            builder
                .Property(a => a.CreatedDate)
                .HasColumnType(SqlDbTypes.Timestamp_with_timezone)
                .HasDateTimeOffsetDefaultValueSql()
                .IsRequired();

            builder
                .Property(a => a.UpdatedDate)
                .HasColumnType(SqlDbTypes.Timestamp_with_timezone)
                .HasDateTimeOffsetDefaultValueSql()
                .IsRequired();
        }
    }
}
