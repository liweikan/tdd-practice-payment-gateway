using Higgs.Data.Common.Context.Postgresql.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Entities;

namespace PaymentGateway.Context.Interface
{
    public interface IPaymentGatewayContext : IDbContext
    {
        DbSet<Transaction> Transactions { get; set; }
    }
}
