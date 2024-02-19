using Higgs.Data.Common.Context.Postgresql.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Entities;

namespace PaymentGateway.Context.Interface
{
    public interface IReadOnlyPaymentGatewayContext : IReadOnlyDbContext
    {
        DbSet<Transaction> Transactions { get; set; }
    }
}
