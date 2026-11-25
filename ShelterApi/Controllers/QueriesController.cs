using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterApi.Controllers
{
    [Route("queries")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public QueriesController(AnimalshelterContext context)
        {
            _context = context;
        }

       
        [HttpGet("allSheltersWithAnimals")]
        public async Task<IActionResult> AllSheltersWithAnimals()
        {
            try
            {
                var shelters = await _context.Shelters
                    .Include(s => s.Animals)
                    .ToListAsync();

                return StatusCode(200, new { message = "Lekérdezve", result = shelters });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        [HttpGet("allAnimalsWithShelter")]
        public async Task<IActionResult> AllAnimalsWithShelter()
        {
            try
            {
                var animals = await _context.Animals
                    .Join(_context.Shelters,
                        a => a.ShelterId,
                        s => s.Id,
                        (a, s) => new
                        {
                            a.Id,
                            a.Name,
                            a.Species,
                            a.Age,
                            a.Weight,
                            a.RegTime,
                            ShelterName = s.Name
                        })
                    .ToListAsync();

                return StatusCode(200, new { message = "Lekérdezve", result = animals });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        [HttpGet("animalsOfShelter")]
        public async Task<IActionResult> AnimalsOfShelter(Guid shelterId)
        {
            try
            {
                var shelter = await _context.Shelters
                    .Include(s => s.Animals)
                    .Where(s => s.Id == shelterId)
                    .FirstOrDefaultAsync();

                if (shelter is not null)
                {
                    return StatusCode(200, new { message = "Lekérdezve", result = shelter });
                }

                return StatusCode(404, new { message = "Nincs találat", result = shelter });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        [HttpGet("animalCountByShelter")]
        public async Task<IActionResult> AnimalCountByShelter()
        {
            try {
                return StatusCode(200);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
