using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Tests.Controllers;

public class BookingsControllerTests
{
    private CinemaContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<CinemaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new CinemaContext(options);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfBookings()
    {
        // Arrange
        var context = GetInMemoryContext();
        
        var movie = new Movie { Id = 1, Title = "Test Movie", DurationMinutes = 120 };
        var hall = new Hall { Id = 1, Name = "Test Hall", Rows = 10, SeatsPerRow = 10 };
        var screening = new Screening 
        { 
            Id = 1, 
            MovieId = 1, 
            HallId = 1, 
            StartTime = DateTime.Now,
            Movie = movie,
            Hall = hall
        };
        
        context.Movies.Add(movie);
        context.Halls.Add(hall);
        context.Screenings.Add(screening);
        
        context.Bookings.AddRange(
            new Booking { Id = 1, ScreeningId = 1, CustomerName = "John Doe", RowNumber = 1, SeatNumber = 1 },
            new Booking { Id = 2, ScreeningId = 1, CustomerName = "Jane Smith", RowNumber = 1, SeatNumber = 2 }
        );
        await context.SaveChangesAsync();

        var controller = new BookingsController(context);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Booking>>(viewResult.Model);
        Assert.Equal(12, model.Count());//TODO Fix. Temporary change to check failed ci stage. 12 -> 2
    }

    [Fact]
    public async Task Create_Post_AddsBookingAndRedirects()
    {
        // Arrange
        var context = GetInMemoryContext();
        
        var screening = new Screening 
        { 
            Id = 1, 
            MovieId = 1, 
            HallId = 1, 
            StartTime = DateTime.Now 
        };
        context.Screenings.Add(screening);
        await context.SaveChangesAsync();

        var controller = new BookingsController(context);
        var newBooking = new Booking 
        { 
            ScreeningId = 1, 
            CustomerName = "Test Customer", 
            RowNumber = 2,
            SeatNumber = 5 
        };

        // Act
        var result = await controller.Create(newBooking);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal(1, await context.Bookings.CountAsync());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenBookingDoesNotExist()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new BookingsController(context);

        // Act
        var result = await controller.Details(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_Post_RemovesBookingAndRedirects()
    {
        // Arrange
        var context = GetInMemoryContext();
        
        var screening = new Screening 
        { 
            Id = 1, 
            MovieId = 1, 
            HallId = 1, 
            StartTime = DateTime.Now 
        };
        context.Screenings.Add(screening);
        
        var booking = new Booking 
        { 
            Id = 1, 
            ScreeningId = 1, 
            CustomerName = "To Delete", 
            RowNumber = 3,
            SeatNumber = 3 
        };
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();

        var controller = new BookingsController(context);

        // Act
        var result = await controller.DeleteConfirmed(1);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal(0, await context.Bookings.CountAsync());
    }
}
