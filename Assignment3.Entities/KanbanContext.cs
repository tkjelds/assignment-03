using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment3.Entities;

public class KanbanContext : DbContext
{

    public KanbanContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "Kanban.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public string DbPath { get; }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
      
            modelBuilder.Entity<Task>().HasOne(t => t.AssignedTo).WithMany(u => u.Tasks);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>().HasMany(u => u.Tasks).WithOne(t => t.AssignedTo);

            modelBuilder.Entity<Tag>().HasIndex(t => t.Name).IsUnique();
    }
}

