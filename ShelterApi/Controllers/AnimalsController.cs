using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;
using ShelterApi.Models.DTOs;

namespace ShelterApi.Controllers
{
    [Route("animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public AnimalsController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpPost("newAnimal")]
        public async Task<IActionResult> NewAnimal(NewAnimalDTO newAnimalDto)
        {
            try
            {
                var shelter = await _context.Shelters.FindAsync(newAnimalDto.ShelterId);

                if (shelter is null)
                {
                    return StatusCode(404, new { message = "Nincs ilyen menhely", result = shelter });
                }

                var count = await _context.Animals.CountAsync(a => a.ShelterId == shelter.Id);

                if (count >= shelter.Capacity)
                {
                    return StatusCode(409, new { message = "A menhely megtelt", result = shelter });
                }

                var animal = new Animal
                {
                    Id = Guid.NewGuid(),
                    Name = newAnimalDto.Name,
                    Species = newAnimalDto.Species,
                    Age = newAnimalDto.Age,
                    Weight = newAnimalDto.Weight,
                    RegTime = DateTime.Now,
                    ShelterId = newAnimalDto.ShelterId
                };

                await _context.Animals.AddAsync(animal);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { message = "Felvéve", result = animal });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("allAnimal")]
        public async Task<IActionResult> AllAnimal()
        {
            try
            {
                var animals = await _context.Animals.ToListAsync();

                if (animals.Count > 0)
                {
                    return StatusCode(200, new { message = "Lekérdezve", result = animals });
                }

                return StatusCode(404, new { message = "Nincs találat", result = animals });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> AnimalById(Guid id)
        {
            try
            {
                var animal = await _context.Animals.SingleOrDefaultAsync(a => a.Id == id);

                if (animal is not null)
                {
                    return StatusCode(200, new { message = "Lekérdezve", result = animal });
                }

                return StatusCode(404, new { message = "Nincs találat", result = animal });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAnimal(Guid id, UpdateAnimalDTO updateAnimalDto)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

                if (animal is null)
                {
                    return StatusCode(404, new { message = "Nincs találat", result = animal });
                }

                var shelter = await _context.Shelters.FindAsync(updateAnimalDto.ShelterId);

                if (shelter is null)
                {
                    return StatusCode(404, new { message = "Nincs ilyen menhely", result = shelter });
                }

                if (animal.ShelterId != shelter.Id &&
                    await _context.Animals.CountAsync(a => a.ShelterId == shelter.Id) >= shelter.Capacity)
                {
                    return StatusCode(409, new { message = "A menhely megtelt", result = shelter });
                }

                animal.Name = updateAnimalDto.Name;
                animal.Species = updateAnimalDto.Species;
                animal.Age = updateAnimalDto.Age;
                animal.Weight = updateAnimalDto.Weight;
                animal.ShelterId = updateAnimalDto.ShelterId;

                _context.Animals.Update(animal);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Frissítve", result = animal });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("deleteAnimal")]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);

                if (animal is not null)
                {
                    _context.Animals.Remove(animal);
                    await _context.SaveChangesAsync();

                    return StatusCode(200, new { message = "Törölve", result = animal.Name });
                }

                return StatusCode(404, new { message = "Nincs találat", result = animal });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
