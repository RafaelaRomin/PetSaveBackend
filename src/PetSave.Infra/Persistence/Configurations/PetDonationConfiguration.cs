using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetSave.Domain.Entities.v1;

namespace PetSave.Infra.Persistence.Configurations;

public class PetDonationConfiguration : IEntityTypeConfiguration<PetDonation>
{
    public void Configure(EntityTypeBuilder<PetDonation> builder)
    {
        builder
            .HasKey(k => k.Id);
    }
}