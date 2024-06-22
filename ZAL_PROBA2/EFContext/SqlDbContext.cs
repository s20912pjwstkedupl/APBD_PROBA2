using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ZAL_PROBA2.Models;
using Task = ZAL_PROBA2.Models.Task;

namespace ZAL_PROBA2.EFContext;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<User> Users { get; set; }
    
    
      protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser)
                .HasName("User_pk");
            entity.ToTable("User");
            entity.Property(e => e.IdUser).ValueGeneratedNever();
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask)
                .HasName("Task_pk");
            entity.ToTable("Task");
            
            entity.Property(e => e.IdTask).ValueGeneratedNever();
            
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired();
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            entity.HasOne(d => d.VProject)
                .WithMany(p => p.VTasks)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_Project");
            
            entity.HasOne(d => d.VReporter)
                .WithMany(p => p.VTaskReporters)
                .HasForeignKey(d => d.IdReporter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_User_Reporter");
            
            entity.HasOne(d => d.VAssignee)
                .WithMany(p => p.VTaskAssignees)
                .HasForeignKey(d => d.IdAssignee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_User_Assignee");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("Project_pk");
            entity.ToTable("Project");
            entity.Property(e => e.IdProject).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.HasOne(d => d.VDefaultAssignee)
                .WithMany(p => p.VProjects)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Project_User");
        });
        
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.IdUser);
            entity.HasKey(e => e.IdProject)
                .HasName("Access_pk");

            entity.ToTable("Access");
            
            entity.HasOne(d => d.VUser)
                .WithMany(p => p.VAccesses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProjectAccess_User");
            
            entity.HasOne(d => d.VProject)
                .WithMany(p => p.VAccesses)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProjectAccess_Project");

        });

        base.OnModelCreating(modelBuilder);
    }
}