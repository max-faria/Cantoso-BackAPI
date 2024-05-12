namespace ContosoPizza.Models;

public class Adress
{
    public required string Street1 {get; set; }
    public string? Street2 {get; set; }
    public string Number {get; set; }
    public required string City {get; set; }
    public string Suburb {get; set; }
    public string CEP {get; set; }

}