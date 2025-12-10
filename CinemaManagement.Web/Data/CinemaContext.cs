using Microsoft.EntityFrameworkCore;
using CinemaManagement.Web.Models;

namespace CinemaManagement.Web.Data;

public class CinemaContext(DbContextOptions<CinemaContext> options) : DbContext(options)
{
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Screening> Screenings { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for money
        modelBuilder.Entity<Screening>()
            .Property(s => s.BasePrice)
            .HasColumnType("decimal(10,2)");

        // Configure relationships
        modelBuilder.Entity<Screening>()
            .HasOne(s => s.Movie)
            .WithMany(m => m.Screenings)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Screening>()
            .HasOne(s => s.Hall)
            .WithMany(h => h.Screenings)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Screening)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.ScreeningId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
