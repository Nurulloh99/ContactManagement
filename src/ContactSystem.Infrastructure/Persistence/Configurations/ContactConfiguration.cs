using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactSystem.Infrastructure.Persistence.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");

        builder.HasKey(c => c.ContactId);

        builder.Property(c => c.ContactName)
               .IsRequired(true)
               .HasMaxLength(100);

        builder.Property(c => c.ContactPhoneNumber)
               .IsRequired(true)
               .HasMaxLength(50);

        builder.Property(c => c.ContactEmail)
               .IsRequired(false)
               .HasMaxLength(100);

        builder.Property(c => c.ContactAddress)
               .IsRequired(false)
               .HasMaxLength(200);

        builder.Property(c => c.CreatedAt)
               .HasDefaultValueSql("GETDATE()");
    }
}
