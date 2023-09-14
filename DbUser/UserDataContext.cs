using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DbUser
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options){
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id_user).HasName("PK_User");
            modelBuilder.Entity<Ms_voucher>().HasKey(xo => xo.id_voucher).HasName("PK_vouchers");
            modelBuilder.Entity<Ms_voucher>().HasOne(xo => xo.User).WithMany(x => x.Fk_voucherss)
                .HasForeignKey(x => x.id_user);
            modelBuilder.Entity<UserLogin>().HasKey(xa => xa.id_userlogin).HasName("PK_userlogin");
            modelBuilder.Entity<UserLogin>().HasOne(xa => xa.User).WithMany(x => x.Fk_userlogins).
                HasForeignKey(x => x.user_id);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ms_voucher>ms_Vouchers { get; set; }
        public DbSet<UserLogin> Userlogins { get; set; }
    }
}
