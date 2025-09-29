using FirstAsp.Models;
using FirstAsp.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstAsp.Services;

public class PizzaService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Pizza>> GetAll() => await _context.Pizzas.ToListAsync();

    public async Task<Pizza?> Get(int id) => await _context.Pizzas.FindAsync(id);

    public async Task<List<Pizza>> GetAllGlutenFree() 
    {
        return await _context.Pizzas
            .Where(p => p.IsGlutenFree)
            .ToListAsync();
    }

    public async Task Add(Pizza pizza)
    {
        _context.Pizzas.Add(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var pizza = await Get(id);
        if (pizza is null)
            return;

        _context.Pizzas.Remove(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Pizza pizza)
    {
        var existingPizza = await Get(pizza.Id);
        if (existingPizza is null)
            return;

        _context.Entry(existingPizza).CurrentValues.SetValues(pizza);
        await _context.SaveChangesAsync();
    }
}