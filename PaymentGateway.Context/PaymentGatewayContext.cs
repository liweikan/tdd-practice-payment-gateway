using Microsoft.EntityFrameworkCore;
using PaymentGateway.Context.Configurations;
using PaymentGateway.Context.Interface;
using PaymentGateway.Entities;

namespace PaymentGateway.Context
{
    public class PaymentGatewayContext : DbContext, IPaymentGatewayContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerWalletConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerCashLogConfiguration());
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PlayerWallet> PlayerWallets { get; set; }
        public DbSet<PlayerCashLog> PlayerCashLogs { get; set; }

        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options) { }
    }
}
