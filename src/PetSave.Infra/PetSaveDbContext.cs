using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PetSave.Domain.Entities.v1;

namespace PetSave.Infra;

public class PetSaveDbContext(DbContextOptions<PetSaveDbContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<User> Users => Set<User>();
    public DbSet<PetDonation> PetDonations => Set<PetDonation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}