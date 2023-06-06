using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.DAL.Models
{
    public partial class Context : IdentityDbContext<User>
    {
        #region Constructor 
        // public Context(DbContextOptions<Context> options) : base(options) { }

        #endregion

        protected readonly IConfiguration Configuration;

        public Context()
        {

        }
        /// <summary>
        /// Консруктор класса Context
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public Context(DbContextOptions<Context> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=LAPTOP-5C0RSPHK\\SQLEXPRESS;Database=foodshop2;Trusted_Connection=True;Encrypt=False;");
        }

        public virtual DbSet<Write> Write { get; set; }                 // таблица Акты списания
        public virtual DbSet<LineWrite> LineWrite { get; set; }         // таблица Строки Актов списания
        public virtual DbSet<Order> Order { get; set; }                // тыблица Заказы
        public virtual DbSet<LineOrder> LineOrder { get; set; }          //  таблица Строки Заказов
        public virtual DbSet<Tovar> Tovar { get; set; }                 //  тыблица Товары


        /// <summary>
        /// Функция создания таблиц
        /// </summary>
        /// <param name="modelBuilder"></param>
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

            //OnModelCreatingPartial(modelBuilder);
        }
       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
