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
    public class AuctionController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AuctionController> _logger;

        public AuctionController(ILogger<AuctionController> logger)
        {
            _logger = logger;
        }

        public class AllAuctions
        {
                public int ID{get;set;}
                public string Name {get;set;}
                public int Startbid {get;set;}
                public string Image {get;set;}
                public string Description {get;set;}

                public string Seller {get;set;}

                public string AuctionEnd {get;set;}
                 
        }        

           
            public ActionResult OnPost(){

                
            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();
            using var cmd = conn.CreateCommand();
                     
            
            cmd.CommandText=$"select `ID`, `Name`, `Seller`, `Startbid`, `AuctionEnd`, `Image`, `Description` from Auction;";

            using var reader= cmd.ExecuteReader();
            List<AllAuctions> result=new List<AllAuctions>();
           

                    while(reader.Read())
                    {
                            result.Add(new AllAuctions
                               {
                                   ID=reader.GetInt32(0),
                                   Name=reader.GetString(1),
                                   Seller=reader.GetString(2),
                                   Startbid=reader.GetInt32(3),
                                   AuctionEnd=reader.GetString(4),
                                   Image=reader.GetString(5),
                                   Description=reader.GetString(6)

                             });
                    }

                return Ok(result);
            }





    }
}