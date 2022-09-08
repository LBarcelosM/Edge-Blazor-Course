using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edge.Bills.Domain.Entities;

namespace Edge.Bills.Data.EntityMap
{
    internal class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

            builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(128);

            builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(10);

            builder.Property(x => x.UserType)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.ToTable("BillUser");
        }
    }
}
