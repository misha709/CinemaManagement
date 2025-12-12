using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.Tests.Models;

public class ModelValidationTests
{
    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }

    [Fact]
    public void Movie_ValidModel_PassesValidation()
    {
        // Arrange
        var movie = new Movie
        {
            Title = "Valid Movie",
            DurationMinutes = 120,
            Genre = "Action"
        };

        // Act
        var results = ValidateModel(movie);

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Hall_ValidModel_PassesValidation()
    {
        // Arrange
        var hall = new Hall
        {
            Name = "Hall 1",
            Rows = 10,
            SeatsPerRow = 10
        };

        // Act
        var results = ValidateModel(hall);

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Booking_ValidModel_PassesValidation()
    {
        // Arrange
        var booking = new Booking
        {
            ScreeningId = 1,
            CustomerName = "John Doe",
            RowNumber = 1,
            SeatNumber = 1
        };

        // Act
        var results = ValidateModel(booking);

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Screening_ValidModel_PassesValidation()
    {
        // Arrange
        var screening = new Screening
        {
            MovieId = 1,
            HallId = 1,
            StartTime = DateTime.Now.AddDays(1)
        };

        // Act
        var results = ValidateModel(screening);

        // Assert
        Assert.Empty(results);
    }
}
