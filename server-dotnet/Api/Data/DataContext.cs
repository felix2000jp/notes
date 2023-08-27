using Api.Modules.Note;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class DataContext : DbContext
{
    private readonly string _connectionString; 

    public DataContext(DbContextOptions<DataContext> options, string connectionString) : base(options)
    {
        _connectionString = connectionString;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Note>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Text).HasMaxLength(300).IsRequired();
        });
    }
    
    public DbSet<Note> Notes { get; set; }
}