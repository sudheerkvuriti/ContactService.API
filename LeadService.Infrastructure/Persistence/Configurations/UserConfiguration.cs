using LeadService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserID);
        builder.Property(x => x.UserID).HasMaxLength(24);

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(64);
        
        builder.Property(x => x.EmailID).HasMaxLength(64);
        builder.Property(x => x.PhoneNo).HasMaxLength(16);
        builder.Property(x => x.Address).HasMaxLength(128);
        builder.Property(x => x.Signature).HasMaxLength(256);

        builder.Property(x => x.Grade).HasMaxLength(32);
        builder.Property(x => x.UserCode).HasMaxLength(64);
        builder.Property(x => x.UserType).HasMaxLength(10);

        // Map Photo column as varbinary (image)
        builder.Property(x => x.Photo).HasColumnType("image");

        builder.Property(x => x.IsActive).IsRequired();
    }
}
