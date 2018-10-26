using server.data.Tables;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace server.data
{
    public interface IGlobomanticsContext : IDisposable
    {
        DbSet<Users> Users { get; }

        DbSet<Transactions> Transactions { get; }

        Task<int> SaveChangesAsync();
    }
}
