using Microsoft.EntityFrameworkCore;
using TutorDesk.Domain.Modules.Students.Entities;

namespace TutorDesk.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Class).HasMaxLength(50);
            entity.Property(x => x.Subject).HasMaxLength(100);
            entity.Property(x => x.ParentPhone).HasMaxLength(15);
        });
    }
}