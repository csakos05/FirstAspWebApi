using FirstAsp.Models;
using FirstAsp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstAsp.Controllers;

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
    public async Task<ActionResult<List<Pizza>>> GetAll() =>
        await _pizzaService.GetAll();

    // GET all gluten-free action
    [HttpGet("glutenfree")]
    public async Task<ActionResult<List<Pizza>>> GetAllGlutenFree() =>
        await _pizzaService.GetAllGlutenFree();

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _pizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action
    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        await _pizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = await _pizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        await _pizzaService.Update(pizza);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pizza = await _pizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        await _pizzaService.Delete(id);

        return NoContent();
    }
}