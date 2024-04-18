using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;
using Tutorial5.Repositories;

namespace Tutorial5.Controllers;

[ApiController]
// [Route("/api/animals")]
[Route("/api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        var animals = _animalRepository.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var addAnimal = new AddAnimal
        {
            Name = animal.Name,
            Description = animal.Description,
            Category = animal.Category,
            Area = animal.Area
        };

        _animalRepository.AddAnimal(addAnimal);
        return CreatedAtAction(nameof(GetAnimals), null);
    }

    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, [FromBody] Animal animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var addAnimal = new AddAnimal
        {
            Name = animal.Name,
            Description = animal.Description,
            Category = animal.Category,
            Area = animal.Area
        };

        try
        {
            _animalRepository.UpdateAnimal(idAnimal, addAnimal);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"nie znaleziono zwierzecia o id: {idAnimal}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"error: {ex.Message}");
        }
    }

    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        try
        {
            _animalRepository.DeleteAnimal(idAnimal);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"nie znaleziono zwierzecia o id: {idAnimal}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"błąd: {ex.Message}");
        }
    }
}


