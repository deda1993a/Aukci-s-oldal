using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace aukcio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidLeaderController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BidLeaderController> _logger;

        public BidLeaderController(ILogger<BidLeaderController> logger)
        {
            _logger = logger;
        }


        public class BidLeader
        {
            public int BidId{get;set;}
            public int BidPrice{get;set;}
            public int BidderId{get;set;}
        }

            public ActionResult OnPost(BidLeader bdt){

                
            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();
            using var cmd = conn.CreateCommand();
                     
            
            cmd.CommandText=@"SELECT BidId, Max(BidPrice), BidderId 
                            FROM Bids 
                            WHERE BidId=@bidid;";

            
            cmd.Parameters.AddWithValue("@bidid", bdt.BidId);
            using var reader= cmd.ExecuteReader();
            List<BidLeader> result=new List<BidLeader>();
           

                    while(reader.Read())
                    {
                            result.Add(new BidLeader
                               {
                                   BidId=reader.GetInt32(0),
                                   BidPrice=reader.GetInt32(1),
                                   BidderId=reader.GetInt32(2),

                             });
                             Console.WriteLine("bidid"+reader.GetInt32(2)+"bidprice"+reader.GetInt32(1));
                    }
                  

                return Ok(result);
            }        


    }
}