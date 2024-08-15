using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLPhongKH.Models;

public partial class KhContext : DbContext
{
    public KhContext()
    {
    }

    public KhContext(DbContextOptions<KhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaiViet> BaiViets { get; set; }

    public virtual DbSet<ChucNang> ChucNangs { get; set; }

    public virtual DbSet<LoaiBaiViet> LoaiBaiViets { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-E7BVGP7;Initial Catalog=kh;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiViet>(entity =>
        {
            entity.HasKey(e => e.MaBaiViet);

            entity.ToTable("BaiViet");

            entity.Property(e => e.HinhAnh).HasMaxLength(200);
            entity.Property(e => e.TacGia).HasMaxLength(50);
            entity.Property(e => e.TenBaiViet).HasMaxLength(1000);
            entity.Property(e => e.TomTat).HasMaxLength(1000);

            entity.HasOne(d => d.LoaiBaiVietNavigation).WithMany(p => p.BaiViets)
                .HasForeignKey(d => d.LoaiBaiViet)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BaiViet_LoaiBaiViet");
        });

        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.HasKey(e => e.MaChucNang).HasName("PK__ChucNang__B26DC25708195EAA");

            entity.ToTable("ChucNang");

            entity.Property(e => e.TenChucNang).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiBaiViet>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiBaiViet");

            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => new { e.IdTk, e.MaChucNang });

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.IdTk).HasColumnName("IdTK");
            entity.Property(e => e.GhiChu).HasMaxLength(200);

            entity.HasOne(d => d.IdTkNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.IdTk)
                .HasConstraintName("FK_PhanQuyen_TaiKhoan");

            entity.HasOne(d => d.MaChucNangNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaChucNang)
                .HasConstraintName("FK_PhanQuyen_ChucNang");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3214EC078F20C1BB");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.PassWord).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
