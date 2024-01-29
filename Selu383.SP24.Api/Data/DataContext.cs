using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Features.Appointments;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .Property(x => x.Name)
            .HasMaxLength(120);
    }
}