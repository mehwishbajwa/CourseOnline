using CourseOnline.Domain.Entities;
using Xunit;

namespace CourseOnline.Tests;

public class CourseTests
{
    [Fact]
    public void Course_Should_Have_Title()
    {
        // Arrange
        var course = new Course
        {
            Title = "Test Course",
            Description = "Test Description"
        };

        // Act
        var result = course.Title;

        // Assert
        Assert.Equal("Test Course", result);
    }

    [Fact]
    public void Course_Should_Store_Description()
    {
        // Arrange
        var course = new Course
        {
            Title = "Test",
            Description = "Backend course"
        };

        // Act
        var result = course.Description;

        // Assert
        Assert.Equal("Backend course", result);
    }
}