namespace ContosoPizza.Models;

public class Clients
{
    public int Id {get; set; }
    public required string Name {get; set; }
    public required string Email {get; set;}
    public required string Phone {get; set;}
    public DateTime Brith {get; set; }
    public Adress Adress {get; set; }
}

