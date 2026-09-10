using Lms.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Infrastructure.Persistence.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Order)
            .IsRequired();

            builder.HasIndex(x => new { x.CourseId, x.Order })
                .IsUnique();

            builder.HasOne<Course>()
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
