using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EzPay.Entities
{
    public partial class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apidetails> Apidetails { get; set; }
        public virtual DbSet<ChargeTypeMaster> ChargeTypeMaster { get; set; }
        public virtual DbSet<CompanyMaster> CompanyMaster { get; set; }
        public virtual DbSet<ParameterMaster> ParameterMaster { get; set; }
        public virtual DbSet<PaymentGatewayMaster> PaymentGatewayMaster { get; set; }
        public virtual DbSet<RegionCompanyMaster> RegionCompanyMaster { get; set; }
        public virtual DbSet<RegionMaster> RegionMaster { get; set; }
        public virtual DbSet<TowerMaster> TowerMaster { get; set; }
        public virtual DbSet<PaymentTransactions> PaymentTransactions { get; set; }
        public virtual DbSet<OtpTransaction> OtpTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:ibt-innovations-sql-server.database.windows.net;Database=EzzPayDev;User Id=SQL_Dev;Password=Ibt@2020");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apidetails>(entity =>
            {
                entity.ToTable("APIDetails");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apiurl)
                    .IsRequired()
                    .HasColumnName("APIURL")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeTypeId).HasColumnName("ChargeTypeID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Filters)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TowerId).HasColumnName("TowerID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChargeTypeMaster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChargeType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });

            modelBuilder.Entity<CompanyMaster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PageFlow)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentOn)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionID).HasColumnName("RegionID");
            });

            modelBuilder.Entity<ParameterMaster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChargeTypeId).HasColumnName("ChargeTypeID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Parameter)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PaymentGatewayMaster>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.AccessKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeTypeID).HasColumnName("ChargeTypeID");

                entity.Property(e => e.CompanyID).HasColumnName("CompanyID");

                entity.Property(e => e.MerchantID)
                    .HasColumnName("MerchantID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PGType)
                    .IsRequired()
                    .HasColumnName("PGType")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileID)
                    .HasColumnName("ProfileID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecretKey)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Store)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Terminal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TowerID).HasColumnName("TowerID");
            });

            modelBuilder.Entity<RegionCompanyMaster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");
            });

            modelBuilder.Entity<RegionMaster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TowerMaster>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.TowerName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TowerMaster)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TowerMaster_CompanyMaster");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
