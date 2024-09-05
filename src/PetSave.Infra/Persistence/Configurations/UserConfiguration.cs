using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetSave.Domain.Entities.v1;

namespace PetSave.Infra.Migrations.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .HasMany(p => p.Pets)
            .WithOne(t => t.Tutor)
            .HasForeignKey(f => f.IdTutor);

        builder
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}