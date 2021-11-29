using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aukcio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<Person> OnPost(Person person)
        {
            Console.WriteLine($"name = {person.Name}");
            Console.WriteLine($"password = {person.Password}");

            var result=new List<Person>
            {
               
              person
                
            };
            return result.ToArray();  
        }

    }
}
