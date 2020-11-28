using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace trading.Models
{
    public partial class DB_A6159D_tradingDEVContext : DbContext
    {
        public DB_A6159D_tradingDEVContext()
        {
        }

        public DB_A6159D_tradingDEVContext(DbContextOptions<DB_A6159D_tradingDEVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Txn> Txn { get; set; }
        public virtual DbSet<TxnView> TxnView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SQL5053.site4now.net;Initial Catalog=DB_A6159D_tradingDEV;User Id=DB_A6159D_tradingDEV_admin;Password=phoenix1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
