using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBackend.Api.Models;
using TestBackend.Api.Models.Data;
using TestBackend.Common.Models;

namespace TestBackend.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<PersonsController> _logger;
        
        public PersonsController(ApplicationContext db, ILogger<PersonsController> logger)
        {
            _db = db;
            _logger = logger;
            _logger.LogDebug("NLog injected into TestBackend");
        }

        [HttpGet()]
        public IActionResult GetAllPersons()
        {
            var persons = _db.Persons
                .Include(p => p.Skills)
                .ToList();
            var personModels = persons.Select(p => p.ToDto()).ToList();

            if (personModels.Count != 0)
            {
                return Ok(personModels);
            }
            _logger.LogInformation("Cущность не найдена в системе");
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetPerson(long id)
        {
            var persons = _db.Persons
                .Where(p => p.Id == id)
                .Include(p => p.Skills);
            var personModels = persons.Select(p => p.ToDto()).ToList();

            if (personModels.Count != 0)
            {
                return Ok(personModels);
            }
            _logger.LogInformation("Cущность не найдена в системе");
            return NotFound();
        }

        [HttpPost()]
        public IActionResult CreatePerson([FromBody] PersonModel personModel)
        {
            if (personModel != null)
            {
                Person person = new Person(personModel.Name, personModel.DisplayName);

                if (personModel.Skills.Count > 0 && personModel.Skills.All(s => s.Level >= 1 && s.Level <= 10))
                {
                    person.Skills.AddRange(personModel.Skills.Select(s => new Skill(s.Name, s.Level)));
                    _db.Persons.Add(person);
                    _db.SaveChanges();
                    return Ok();
                }
                _logger.LogInformation("Ошибка при обработке данных");
                return StatusCode(500);
            }
            _logger.LogInformation("Неверный запрос");
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(long id, [FromBody] PersonModel personModel)
        {
            if (personModel != null)
            {
                Person person = _db.Persons.Include(p => p.Skills).FirstOrDefault(x => x.Id == id);
                if (person != null)
                {
                    if (personModel.Skills.Count != 0 && personModel.Skills.All(s => s.Level >= 1 && s.Level <= 10))
                    {
                        person.Name = personModel.Name;
                        person.DisplayName = personModel.DisplayName;
                        _db.Skills.RemoveRange(person.Skills);
                        person.Skills = personModel.Skills.Select(s => new Skill(s.Name, s.Level)).ToList();
                        _db.Persons.Update(person);
                        _db.SaveChanges();
                        return Ok();
                    }
                    _logger.LogInformation("Ошибка при обработке данных");
                    return StatusCode(500);
                }
                _logger.LogInformation("Cущность не найдена в системе");
                return NotFound();
            }
            _logger.LogInformation("Неверный запрос");
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(long id)
        {
            Person person = _db.Persons.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                _db.Persons.Remove(person);
                _db.SaveChanges();
                return Ok();
            }
            _logger.LogInformation("Cущность не найдена в системе");
            return NotFound();
        }       
    }
}
