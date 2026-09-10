using Lms.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Infrastructure.Persistence.Configurations
{
    public class LessonProgressConfiguration : IEntityTypeConfiguration<LessonProgress>
    {
        public void Configure(EntityTypeBuilder<LessonProgress> builder)
        {
            builder.ToTable("LessonProgresses");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.EnrollmentId, x.LessonId })
                .IsUnique();
            builder.HasOne<Enrollment>()
                .WithMany()
                .HasForeignKey(x => x.EnrollmentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Lesson>()
                .WithMany()
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
