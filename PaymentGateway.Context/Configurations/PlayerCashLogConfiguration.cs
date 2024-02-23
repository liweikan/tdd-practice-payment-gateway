//using Higgs.Data.Common.Context.Postgresql.Configurations;
//using Higgs.Data.Common.Context.Postgresql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Entities;

namespace PaymentGateway.Context.Configurations
{
    internal sealed class PlayerCashLogConfiguration : IEntityTypeConfiguration<PlayerCashLog>
    {
        public const string Boolean = "boolean";
        public const string Smallint = "smallint";
        public const string Integer = "integer";
        public const string Bigint = "bigint";
        public const string Date = "date";
        public const string Timestamp_without_timezone = "timestamp without time zone";
        public const string Timestamp_with_timezone = "timestamp with time zone";
        public const string Uuid = "uuid";
        public const string Json = "json";
        public const string Jsonb = "jsonb";
        public const string Amount = "decimal(19,4)";
        public const string CurrencyCode = "char(3)";
        public const string DisplayId = "varchar(20)";
        public const string AccountNumber = "varchar(25)";
        public const string UserName = "varchar(20)";
        public const string ImageId = "char(24)";
        public const string PlayerDisplayId = "varchar(50)";
        public const string GameLoginId = "char(11)";

        //return propertyBuilder.HasDefaultValueSql<Guid>("'00000000-0000-0000-0000-000000000000'");
        public void Configure(EntityTypeBuilder<PlayerCashLog> builder)
        {
            builder
                .ToTable(nameof(PlayerCashLog))
                .HasKey(a => a.PlayerCashLogId);

            builder
                .Property(a => a.PlayerCashLogId)
                .HasColumnType("uuid")
                .IsRequired()
                .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");

            builder
                .Property(a => a.PlayerId)
                .HasColumnType("varchar(32)")
                .IsRequired();

            builder
                .Property(a => a.Amount)
                .HasColumnType("decimal(19,4)")
                .IsRequired();

            builder
                .Property(a => a.CurrentBalance)
                .HasColumnType("decimal(19,4)")
                .IsRequired();

            builder
                .Property(a => a.PostBalance)
                .HasColumnType("decimal(19,4)")
                .IsRequired();

            builder
                .Property(a => a.TransactionType)
                .HasColumnType("smallint")
                .IsRequired();

            builder
                .Property(a => a.ExternalTransactionId)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder
                .Property(a => a.CreatedDate)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();
        }
    }
}
