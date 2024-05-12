using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoPizza.Models;

public class Pizza
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set; }
    public string? Name {get; set; }
    public int Price {get; set; }
    public bool IsGlutenFree {get; set; }
}