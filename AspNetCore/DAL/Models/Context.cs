using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace DAL.Models
{
    public partial class Context : IdentityDbContext<User>
    {

        #region Constructor 
        // public Context(DbContextOptions<Context> options) : base(options) { }

        #endregion

        protected readonly IConfiguration Configuration;
        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual DbSet<Write> Write { get; set; }
        public virtual DbSet<LineWrite> LineWrite { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<LineOrder> LineOrder { get; set; }
        public virtual DbSet<Tovar> Tovar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Number).IsRequired();
            });

            modelBuilder.Entity<Write>(entity =>
            {
                entity.Property(e => e.NumberAct).IsRequired();
            });

            modelBuilder.Entity<LineWrite>(entity =>
            {
                entity.HasOne(w => w.Write).WithMany(lw => lw.LineWrites).HasForeignKey(w => w.NumberActWrite_FK_);
            });

            modelBuilder.Entity<LineOrder>(entity =>
            {
                entity.HasOne(o => o.Order).WithMany(lo => lo.LineOrders).HasForeignKey(o => o.NumberOrder_FK_);
            });

            modelBuilder.Entity<Tovar>(entity =>
            {
                entity.Property(e => e.CodTovara).IsRequired();
            });
        }
    }
}
