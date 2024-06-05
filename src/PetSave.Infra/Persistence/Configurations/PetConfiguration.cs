using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;

namespace PetSave.Infra.Migrations.Persistence.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder
            .HasKey(k => k.Id);

        builder
            .HasMany(d => d.Donations)
            .WithOne(p => p.Pet)
            .HasForeignKey(p => p.IdPet);

        builder
            .Property(d => d.Status)
            .HasConversion(s => s.ToString(), s => (DonationStatus)Enum.Parse(typeof(DonationStatus), s));

        builder
            .Property(d => d.Species)
            .HasConversion(s => s.ToString(), s => (Species)Enum.Parse(typeof(Species), s));
    }
}