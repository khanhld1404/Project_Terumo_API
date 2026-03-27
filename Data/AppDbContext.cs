using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_API.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<labelstatus> labelstatuses { get; set; } = null!;
        public virtual DbSet<link> links { get; set; } = null!;
        public virtual DbSet<tblProduct> tblProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<labelstatus>(entity =>
            {
                entity.HasKey(e => e.MAC);

                entity.ToTable("labelstatus");

                entity.Property(e => e.MAC).HasMaxLength(50);

                entity.Property(e => e.BASE_STATION).HasMaxLength(50);

                entity.Property(e => e.BATTERY_VOLTAGE).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DESCRIPTION).HasMaxLength(100);

                entity.Property(e => e.FIRMWARE_SUBVERSION).HasMaxLength(50);

                entity.Property(e => e.FIRMWARE_VERSION).HasMaxLength(50);

                entity.Property(e => e.GROUP).HasMaxLength(50);

                entity.Property(e => e.ID).HasMaxLength(50);

                entity.Property(e => e.IMAGE_FILE).HasMaxLength(50);

                entity.Property(e => e.LANID).HasMaxLength(50);

                entity.Property(e => e.LAST_IMAGE).HasColumnType("datetime");

                entity.Property(e => e.LAST_INFO).HasColumnType("datetime");

                entity.Property(e => e.LAST_POLL).HasColumnType("datetime");

                entity.Property(e => e.PANID).HasMaxLength(50);

                entity.Property(e => e.VARIANT).HasMaxLength(50);
            });

            modelBuilder.Entity<link>(entity =>
            {
                entity.HasKey(e => e.MAC);

                entity.Property(e => e.MAC).HasMaxLength(50);

                entity.Property(e => e.ID).HasMaxLength(50);

                entity.Property(e => e.Variant).HasMaxLength(50);
            });

            modelBuilder.Entity<tblProduct>(entity =>
            {
                entity.HasKey(e => e.IDItem);

                entity.ToTable("tblProduct");

                entity.Property(e => e.IDItem).ValueGeneratedNever();

                entity.Property(e => e.Barcode).HasMaxLength(50);

                entity.Property(e => e.ChungLoaiSP).HasMaxLength(50);

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.ExpiryDate_Warning).HasColumnType("date");

                entity.Property(e => e.HanSuDung).HasMaxLength(50);

                entity.Property(e => e.HeThong).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.ImagePath).HasMaxLength(150);

                entity.Property(e => e.ItemCode).HasMaxLength(50);

                entity.Property(e => e.ItemType).HasMaxLength(50);

                entity.Property(e => e.Line).HasMaxLength(50);

                entity.Property(e => e.LotNo).HasMaxLength(50);

                entity.Property(e => e.MaCD).HasMaxLength(50);

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.NguoiThaoTac).HasMaxLength(50);

                entity.Property(e => e.QRCode).HasMaxLength(50);

                entity.Property(e => e.R_datetime1).HasColumnType("datetime");

                entity.Property(e => e.R_datetime2).HasColumnType("datetime");

                entity.Property(e => e.R_datetime3).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.Remark1).HasMaxLength(50);

                entity.Property(e => e.Remark2).HasMaxLength(50);

                entity.Property(e => e.Remark3).HasMaxLength(50);

                entity.Property(e => e.Remark4).HasMaxLength(50);

                entity.Property(e => e.Remark5).HasMaxLength(50);

                entity.Property(e => e.SoMeSX).HasMaxLength(50);

                entity.Property(e => e.SoThung).HasMaxLength(50);

                entity.Property(e => e.StatusHOLD).HasMaxLength(50);

                entity.Property(e => e.StatusNG).HasMaxLength(50);

                entity.Property(e => e.StatusPASSED).HasMaxLength(50);

                entity.Property(e => e.StatusUNDERQA).HasMaxLength(50);

                entity.Property(e => e.Template).HasMaxLength(50);

                entity.Property(e => e.TenCD).HasMaxLength(50);

                entity.Property(e => e.TenSP).HasMaxLength(50);

                entity.Property(e => e.TrangThaiCD).HasMaxLength(50);

                entity.Property(e => e.TrangThaiSP).HasMaxLength(50);

                entity.Property(e => e.ViTri).HasMaxLength(50);

                entity.Property(e => e.WorkOder).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
