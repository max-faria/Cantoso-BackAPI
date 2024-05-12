using ContosoPizza.Data;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;

    public PizzaController(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    // GET all action
    [HttpGet]
    public async Task<ActionResult<List<Pizza>>> GetAll()
    {
        try
        {
            var pizzas = await _pizzaService.GetAllAsync();
            if (pizzas == null || pizzas.Count == 0)
            {
                // Retorna No Content (204) se não houver dados
                return StatusCode(200, "There are no pizzas!");
            }
            return pizzas;
        }
        catch (System.Exception)
        {
            // Retorna Internal Server Error (500) se ocorrer algum erro
            return StatusCode(500, "An error occurred while retrieving the data.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _pizzaService.GetById(id);
        if (pizza == null)
        {
            return NotFound();
        }
        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]Pizza pizza)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _pizzaService.CreatePizza(pizza);
        return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
    }

    // // GET by Id action
    // [HttpGet("{id}")]
    // public ActionResult<Pizza> Get(int id)
    // {
    //     var pizza = PizzaService.Get(id);
    //     if(pizza == null)
    //         return NotFound();
        
    //     return pizza; 
    // }

    // // POST action
    // [HttpPost]
    // public IActionResult Create(Pizza pizza)
    // {
    //     PizzaService.Add(pizza);
    //     return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
    // }

    // // PUT action
    // [HttpPut("{id}")]
    // public IActionResult Update(int id, Pizza pizza)
    // {
    //     if(id != pizza.Id)
    //     return BadRequest();

    //     var existingPizza = PizzaService.Get(id);
    //     if(existingPizza is null)
    //         return NotFound();
        
    //     PizzaService.Update(pizza);
    //     return NoContent();
    // }

    // // DELETE action
    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     var pizza = PizzaService.Get(id);
    //     if (pizza is null)
    //         return NotFound();
        
    //     PizzaService.Delete(id);
    //     return NoContent();
    // }
}