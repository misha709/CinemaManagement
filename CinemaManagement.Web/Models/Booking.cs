using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.Web.Models;

public class Booking
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Screening")]
    public int ScreeningId { get; set; }
    public Screening Screening { get; set; } = default!;

    [Required]
    [StringLength(100)]
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; } = default!;

    [StringLength(100)]
    [EmailAddress]
    [Display(Name = "Customer Email")]
    public string? CustomerEmail { get; set; }

    [Required]
    [Range(1, 100)]
    [Display(Name = "Row Number")]
    public int RowNumber { get; set; }

    [Required]
    [Range(1, 100)]
    [Display(Name = "Seat Number")]
    public int SeatNumber { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Booked";
}
