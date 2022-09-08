using Edge.Bills.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Edge.Bills.Data.EntityMap
{
    internal class BillEntityMap : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

            builder.Property(x => x.Value)
            .IsRequired()
            .HasDefaultValue(0);

            builder.Property(x => x.Status)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Bills)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.ToTable("Bill");
        }
    }
}
