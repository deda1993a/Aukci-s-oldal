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

        public static UserManager mUserManager=new UserManager();
        static Dictionary<string, string> AuthorizedUsers=new Dictionary<string, string>();

        private readonly ILogger<LoginUserController> _logger;

  

        public class Logged
        {
                public int ID{get;set;}
                public string Name {get;set;}
                 public string Password {get;set;}
                 public int DoB{get;set;}
        }

        public LoginUserController(ILogger<LoginUserController> logger)
        {
            _logger = logger;
        }

            
            public ActionResult OnPost(Logged user2){
            //Console.WriteLine("sikeres");
            Console.WriteLine("nev: "+user2.Name);
            var hasher = new PasswordHasher();
            Hash hs=new Hash();

            //var hash = hasher.Encrypt(user.Password);

            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();
            using var cmd = conn.CreateCommand();


            

           
            
            cmd.CommandText=$"select `ID`, `Name`, `PwHash`,`PwSalt` from `User`;";

                    using var reader= cmd.ExecuteReader();
                        List<Logged> result=new List<Logged>();
                    while(reader.Read())
                    {
                                   hs.Salt=reader.GetString(3);
                                   hs.Sha=reader.GetString(2);                        
                        hasher.ComputeHash(reader.GetString(3), user2.Password);
                        if(user2.Name.Equals(reader.GetString(1))==true && hasher.Verify(user2.Password,hs)==true){
                            mUserManager.AddUsers(user2.Name);
                           
                            result.Add(new Logged
                                {
                                   ID=reader.GetInt32(0),
                                   Name=reader.GetString(1),
                                   Password=reader.GetString(2),     
                                   DoB=reader.GetInt32(3),                                                                                        
                             
                    
                            //user.PwHash=reader.GetString(1);
                            //user.DoB=reader.GetInt32(2);                                 
                                });
                        }else
                        {
                            
                        }
                        
                    }

                        /*foreach(var tmp in Logged)
                        {

                        }*/
                            
                            return Ok(result);

                            
            
            }

     
    

                
    }
}