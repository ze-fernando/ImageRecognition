using ImageRecognition.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageRecognition.Entity;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Image> Images { get; set; }
}