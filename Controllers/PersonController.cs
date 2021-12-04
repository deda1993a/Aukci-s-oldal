using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

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



        public StatusCodeResult OnPost(Person person){
            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();

            string CommandText= @"
            insert into 
                User
                (`ID`, `Name`, `PWHash`,`DoB`)
                values
                (@ID, @Name, @PWHash, @DoB);";

            Console.WriteLine(person.ID);
            using var cmd = conn.CreateCommand();
            cmd.CommandText=CommandText;
            cmd.Parameters.AddWithValue("@ID", person.ID);
            cmd.Parameters.AddWithValue("@Name", person.Name);
            cmd.Parameters.AddWithValue("@PWHash", person.Password);
            cmd.Parameters.AddWithValue("@DoB", person.DoB);
            try{
            cmd.ExecuteNonQuery();
            }catch(Exception ex)
            {
                return new BadRequestResult();
            }

            conn.Close();     
            
            return new OkResult();   
        }

        /*[HttpPost]
        public IEnumerable<Person> OnPost(Person person)
        {
            Console.WriteLine($"name = {person.Name}");
            Console.WriteLine($"password = {person.Password}");

            var result=new List<Person>
            {
               
              person
                
            };
            return result.ToArray();  
        }*/

    }
}
