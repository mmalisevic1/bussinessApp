using server.data.CustomAttributes;
using server.data.Tables;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace server.data
{
    public class GlobomanticsContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Transactions> Transactions { get; set; }

        public GlobomanticsContext() : base("name=GlobomanticsContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<GlobomanticsContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties()
                        .Having(h => h.GetCustomAttributes(false)
                                      .OfType<NonUnicode>()
                                      .FirstOrDefault())
                        .Configure((configuration, attribute) =>
                        {
                            configuration.IsUnicode(false);
                        });
        }
    }
}
