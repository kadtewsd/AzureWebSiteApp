using AzureWebApp.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AzureWebApp.Models
{
    public class ApplicationDbContext : DbContext
    {

        private static readonly string connectionStringName = null;

        static ApplicationDbContext()
        {
            connectionStringName = LoginManager.GetSettings().DBConnectionName;
        }

        public ApplicationDbContext()
            : base(connectionStringName)
        {
        }

        public DbSet<UserTokenCache> UserTokenCacheList { get; set; }
    }

    public class UserTokenCache
    {
        [Key]
        public int UserTokenCacheId { get; set; }
        public string webUserUniqueId { get; set; }
        public byte[] cacheBits { get; set; }
        public DateTime LastWrite { get; set; }
    }
}