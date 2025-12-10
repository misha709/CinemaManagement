using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.Web.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = default!;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required]
    [Range(1, 500)]
    [Display(Name = "Duration (Minutes)")]
    public int DurationMinutes { get; set; }

    [StringLength(50)]
    public string? Genre { get; set; }

    [StringLength(20)]
    [Display(Name = "Age Rating")]
    public string? AgeRating { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }

    public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
