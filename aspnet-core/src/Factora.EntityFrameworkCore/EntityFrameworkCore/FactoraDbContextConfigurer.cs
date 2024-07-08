using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Factora.EntityFrameworkCore
{
    public static class FactoraDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FactoraDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FactoraDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
