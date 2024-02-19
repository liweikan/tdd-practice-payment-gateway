using Microsoft.EntityFrameworkCore;
using PaymentGateway.Context.Configurations;
using PaymentGateway.Context.Interface;
using PaymentGateway.Entities;

namespace PaymentGateway.Context
{
    public class ReadOnlyPaymentGatewayContext : DbContext, IReadOnlyPaymentGatewayContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }

        public DbSet<Transaction> Transactions { get; set; }

        public ReadOnlyPaymentGatewayContext(DbContextOptions<ReadOnlyPaymentGatewayContext> options) : base(options) { }
    }
}
