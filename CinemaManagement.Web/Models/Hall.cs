using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaManagement.Web.Models;

public class Hall
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;

    [Required]
    [Range(1, 100)]
    public int Rows { get; set; }

    [Required]
    [Range(1, 100)]
    [Display(Name = "Seats Per Row")]
    public int SeatsPerRow { get; set; }

    [NotMapped]
    public int Capacity => Rows * SeatsPerRow;

    public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
