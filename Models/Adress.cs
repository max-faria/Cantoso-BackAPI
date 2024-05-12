using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoPizza.Models;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }
    public required string Street1 {get; set; }
    public string? Street2 {get; set; }
    public required string Number {get; set; }
    public required string City {get; set; }
    public required string State {get; set; }
    public required string CEP {get; set; }
    public required Client Client {get; set;}

}