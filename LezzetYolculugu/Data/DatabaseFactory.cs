using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LezzetYolculugu.Data
{
    public interface IDatabaseFactory
    {
        SqlConnection GetConnection(RolesEnum role);
        SqlConnection GetConnectionWithUser(System.Security.Claims.ClaimsPrincipal claim);
    }

    public class DatabaseFactory: IDatabaseFactory, IDisposable
    {
        private IConfiguration Configuration;
        private SqlConnection AdminConnection;
        private SqlConnection NormalConnection;
        private SqlConnection AnonymousConnection;

        public DatabaseFactory(IConfiguration configuration)
        {
            Configuration = configuration;
            
            AdminConnection = new SqlConnection(Configuration.GetConnectionString("LezzetYolculuguDbAdmin"));
            AdminConnection.Open();
            NormalConnection = new SqlConnection(Configuration.GetConnectionString("LezzetYolculuguDbNormal"));
            NormalConnection.Open();
            AnonymousConnection = new SqlConnection(Configuration.GetConnectionString("LezzetYolculuguDbAnonymous"));
            AnonymousConnection.Open();
        }

        public void Dispose()
        {
            AdminConnection.Close();
            NormalConnection.Close();
            AnonymousConnection.Close();
        }

        public SqlConnection GetConnection(RolesEnum role)
        {
            switch (role)
            {
                case RolesEnum.Admin:
                    return AdminConnection;
                case RolesEnum.Normal:
                    return NormalConnection;
                default:
                    return AnonymousConnection;
            }
        }

        public SqlConnection GetConnectionWithUser(ClaimsPrincipal claim)
        {
            if (claim.IsInRole(RolesRegistry.Admin))
            {
                return GetConnection(RolesEnum.Admin);
            }
            if (claim.IsInRole(RolesRegistry.Normal))
            {
                return GetConnection(RolesEnum.Normal);
            }
            return GetConnection(RolesEnum.Anonymous);
        }
    }
}
