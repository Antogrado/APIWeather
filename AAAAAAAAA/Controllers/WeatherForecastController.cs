using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace AAAAAAAAA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            Summaries.RemoveAt(index);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("incorrect!");
            }

            Summaries[index] = name;
            return Ok();
        }

        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("incorrect!");
            }

            return Ok(Summaries[index]);
        }

        [HttpGet("{find-by-name}")]
        public IActionResult GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("incorrect!");
            }
            return Ok(Summaries.Count(x => x.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        [HttpGet("{get-all}")]
        public IActionResult GetAll(int? sortStrategy)
        {
            switch (sortStrategy)
            {
                case null:
                    return Ok(Summaries);

                case 1:
                    return Ok(Summaries.OrderBy(x => x));

                case -1:
                    return Ok(Summaries.OrderByDescending(x => x));

                default:
                    return BadRequest("Incorrect value for the sortStrategy parameter");
            }
        }
    }
}