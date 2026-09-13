using System;
using System.Collections.Generic;
using System.Text;
using Lms.Domain;
using Lms.Domain.Entity;
using Xunit;

namespace Lms.Domain.Tests
{
    public class CourseTests
    {
        [Fact]
        public void Publish_WhenCourseHasNoLessons_ShouldThrowInvalidOperationException()
        {
            var course = new Course("ASP.NET Core Fundamentals", "A practical backend course for LMS students.");

            var act = ()=> course.Publish(); 

            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void Publish_WhenCourseHasAtLeastOneLesson_ShouldMarkCourseAsPublished()
        {
            var course = new Course(
       "ASP.NET Core Fundamentals",
       "A practical backend course for LMS students.");

            course.AddLesson("Introduction to ASP.NET Core");

            course.Publish();

            Assert.True(course.IsPublished);
        }

        [Fact]
        public void AddLesson_WhenCalledMultipleTimes_ShouldAssignSequentialOrder()
        {
            var course = new Course(
       "ASP.NET Core Fundamentals",
       "A practical backend course for LMS students.");

            course.AddLesson("Introduction to ASP.NET Core season two part one");
            course.AddLesson("Introduction to ASP.NET Core season two part two");

            Assert.Equal(2, course.Lessons.Count);
            Assert.Equal(1, course.Lessons.ElementAt(0).Order);
            Assert.Equal(2, course.Lessons.ElementAt(1).Order);

        }

        [Fact]
        public void CreateCourse_WithWhitespaceInTitle_ShouldTrimTitle()
        {
            var course = new Course("  ASP.NET Core   ", "Course");

           var act = () => course.Publish();
            Assert.Equal("ASP.NET Core", course.Title);
        }
    }
}
