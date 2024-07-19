using Microsoft.EntityFrameworkCore;
using Toplearn.Domain.Models;

namespace Toplearn.InfraStructure.Context
{
    public class TopleaarnContext:DbContext
    {
        public TopleaarnContext(DbContextOptions<TopleaarnContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Episod> Episods { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
