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
        }

        public DbSet<Transaction> Transactions { get; set; }

        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options) { }
    }
}
