using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.Web.Models;

public class Screening
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Movie")]
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;

    [Required]
    [Display(Name = "Hall")]
    public int HallId { get; set; }
    public Hall Hall { get; set; } = default!;

    [Required]
    [Display(Name = "Start Time")]
    public DateTime StartTime { get; set; }

    [Required]
    [Range(0, 1000)]
    [Display(Name = "Base Price")]
    [DataType(DataType.Currency)]
    public decimal BasePrice { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
