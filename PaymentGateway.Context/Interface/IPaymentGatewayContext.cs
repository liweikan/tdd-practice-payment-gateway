using Higgs.Data.Common.Context.Postgresql.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Entities;

namespace PaymentGateway.Context.Interface
{
    public interface IPaymentGatewayContext : IDbContext
    {
        DbSet<Transaction> Transactions { get; set; }
        DbSet<PlayerCashLog> PlayerCashLogs { get; set; }
        DbSet<PlayerWallet> PlayerWallets { get; set; }
    }
}
