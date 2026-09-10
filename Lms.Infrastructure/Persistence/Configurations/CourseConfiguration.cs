using Lms.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(2000);
            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();


            builder.Navigation(x => x.Lessons)
                 .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}