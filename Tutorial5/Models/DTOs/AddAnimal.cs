using System.ComponentModel.DataAnnotations;

namespace Tutorial5.Models.DTOs;

public class AddAnimal
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(200)]
    public string Description { get; set; }

    [StringLength(200)]
    public string Category { get; set; }

    [StringLength(200)]
    public string Area { get; set; }
}