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
    public class LoginUserController : ControllerBase
    {

        static Dictionary<string, string> AuthorizedUsers=new Dictionary<string, string>();

        private readonly ILogger<LoginUserController> _logger;

  

        public class Logged
        {
                public string Name {get;set;}
                 public string Password {get;set;}
                 public int DoB{get;set;}
        }

        public LoginUserController(ILogger<LoginUserController> logger)
        {
            _logger = logger;
        }

            
            public StatusCodeResult OnPost(Logged user2){
Console.WriteLine("sikeres");
            Console.WriteLine(user2.Password);
            var hasher = new PasswordHasher();
            Hash hs=new Hash();

            //var hash = hasher.Encrypt(user.Password);

            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();
            using var cmd = conn.CreateCommand();


            

           
            
            cmd.CommandText=$"select `Name`, `PwHash`,`PwSalt` from `User`;";

                    using var reader= cmd.ExecuteReader();
                        //List<Logged> result=new List<Logged>();
                    while(reader.Read())
                    {
                        hasher.ComputeHash(reader.GetString(2), user2.Password);
                            //result.Add(new Logged
                               // {
                                   hs.Salt=reader.GetString(2);
                                   hs.Sha=reader.GetString(1);

                                   
                             if(reader.GetString(0).Equals(user2.Name)==true && hasher.Verify(user2.Password,hs)==true)
                             {
                                 Console.WriteLine("oke");
                             }
                            //user.PwHash=reader.GetString(1);
                            //user.DoB=reader.GetInt32(2);                                 
                                //});
                    }
                            Console.WriteLine(new OkResult());
                            return new OkResult(); 
                            
            
            }

     
    

                
    }
}