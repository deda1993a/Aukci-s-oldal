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
    public class CreateUserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CreateUserController> _logger;

        public CreateUserController(ILogger<CreateUserController> logger)
        {
            _logger = logger;
        }



        public StatusCodeResult OnPost(login.User user){

            var hasher = new PasswordHasher();
            var hash = hasher.Encrypt(user.Password);

            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();

            string CommandText= @"
            insert into 
                User
                (`Name`, `PWHash`,`PWSalt`,`DoB`)
                values
                (@Name, @PWHash, @PWSalt, @DoB);";

            //Console.WriteLine(user.ID);
            using var cmd = conn.CreateCommand();
            cmd.CommandText=CommandText;
            //cmd.Parameters.AddWithValue("@ID", user.ID);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@PWHash", hash.Sha);
            cmd.Parameters.AddWithValue("@PWSalt", hash.Salt);
            cmd.Parameters.AddWithValue("@DoB", user.DoB);
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
