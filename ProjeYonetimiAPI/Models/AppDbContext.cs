using Microsoft.EntityFrameworkCore;

namespace ProjeYonetimiAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; } 
    public DbSet<ProjectTask> Tasks { get; set; } // Görevler Tablosu
    public DbSet<Comment> Comments { get; set; } // Yorumlar Tablosu
    public DbSet<ProjectTask> ProjectTasks { get; set; }

    // İlişki çakışmalarını önlemek için özel kurallar
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Görev ile Atanan Kullanıcı ilişkisi (Kullanıcı silinirse görev silinmesin)
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.AssignedUser)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Görev ile Oluşturan Kullanıcı ilişkisi
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.CreatedByUser)
            .WithMany(u => u.CreatedTasks)
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Yorum ile Kullanıcı ilişkisi
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}