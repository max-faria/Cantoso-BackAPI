
using ContosoPizza.Controllers;
using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService 
{
    private readonly ApplicationDbContext _context;

    public PizzaService(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL PIZZAS
    public async Task<List<Pizza>> GetAllAsync()
    {
        return await _context.Pizzas.ToListAsync();
    }

    // GET PIZZA BY ID
    public async Task<Pizza> GetById(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza == null)
        {
            throw new KeyNotFoundException($"A pizza with the ID {id} was not found.");
        }
        return pizza;
    }

    // CREATE A NEW PIZZA
    public async Task CreatePizza(Pizza pizza)
    {
        _context.Pizzas.Add(pizza);
        await _context.SaveChangesAsync();
    }


    // static List<Pizza> Pizzas {get;}
    // static int nextId = 3;
    // static PizzaService()
    // {
    //     Pizzas = new List<Pizza>
    //     {
    //         new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
    //         new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
    //     };
    // }

    // public static List<Pizza> GetAll() => Pizzas;

    // public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    // public static void Add(Pizza pizza)
    // {
    //     pizza.Id = nextId++;
    //     Pizzas.Add(pizza);
    // }

    // public static void Delete(int id)
    // {
    //     var pizza = Get(id);
    //     if(pizza is null)
    //         return;

    //     Pizzas.Remove(pizza);
    // }

    // public static void Update(Pizza pizza)
    // {
    //     var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
    //     if(index == -1)
    //         return;

    //     Pizzas[index] = pizza;
    // }
}

