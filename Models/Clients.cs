using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoPizza.Models;

public class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientId {get; set; } //Primary Key
    public required string Name {get; set; }
    public required string Email {get; set;}
    public required string Phone {get; set;}
    public DateTime Birth {get; set; }
    public required  Address Address {get; set; }
    public int AddressId { get; set; } //Foreign Key
}

