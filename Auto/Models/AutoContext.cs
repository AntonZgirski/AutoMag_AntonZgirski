using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auto.Models;

public partial class AutoContext : DbContext
{
    public AutoContext()
    {
    }

    public AutoContext(DbContextOptions<AutoContext> options)
        : base(options)
    {
    }
        
    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Auto> Autos { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Magazine> Magazines { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=USERPC;Initial Catalog=Auto;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicId);

            entity.ToTable("Application");

            entity.Property(e => e.ApplicId).HasColumnName("ApplicID");
            entity.Property(e => e.AutoId).HasColumnName("AutoID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Auto>(entity =>
        {
            entity.ToTable("Auto");

            entity.Property(e => e.AutoId).HasColumnName("AutoID");
            entity.Property(e => e.AutoModel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClaentId);

            entity.ToTable("Client");

            entity.Property(e => e.ClaentId).HasColumnName("ClaentID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientSname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ClientSName");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Role");
        });

        modelBuilder.Entity<Magazine>(entity =>
        {
            entity.ToTable("Magazine");

            entity.Property(e => e.MagazineId).HasColumnName("MagazineID");
            entity.Property(e => e.AutoId).HasColumnName("AutoID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Auto).WithMany(p => p.Magazines)
                .HasForeignKey(d => d.AutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Magazine_Auto");

            entity.HasOne(d => d.Client).WithMany(p => p.Magazines)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Magazine_Client");

            entity.HasOne(d => d.Employee).WithMany(p => p.Magazines)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Magazine_Employee");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleSalary).HasColumnType("money");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ConPasword)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
