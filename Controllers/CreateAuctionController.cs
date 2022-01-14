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
    public class CreateAuctionController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CreateAuctionController> _logger;

        public CreateAuctionController(ILogger<CreateAuctionController> logger)
        {
            _logger = logger;
        }

        public class NewAuctions
        {
               
                public string Name {get;set;}
                public int Startbid {get;set;}
                public string Image {get;set;}
                public string Description {get;set;}

                public string Seller {get;set;}

                public string AuctionEnd {get;set;}
                 
        }        

            private int tmpitemid;
            public StatusCodeResult OnPost(NewAuctions cr){

            
                
            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();
                     
            
            string CommandText=@"insert into Auction (`Name`, `Seller`, `Startbid`, `AuctionEnd`, `Image`, `Description`)
            values (@Name, @Seller, @Startbid, @AuctionEnd, @Image, @Description);";

            using var cmd = conn.CreateCommand();
            cmd.CommandText=CommandText;
            
            cmd.Parameters.AddWithValue("@Name", cr.Name);
            cmd.Parameters.AddWithValue("@Seller", cr.Seller);
            cmd.Parameters.AddWithValue("@Startbid", cr.Startbid);
            cmd.Parameters.AddWithValue("@AuctionEnd", cr.AuctionEnd);
            cmd.Parameters.AddWithValue("@Image", cr.Image);
            cmd.Parameters.AddWithValue("@Description", cr.Description);
            
            cmd.ExecuteNonQuery();
            conn.Close(); 
            
            using SqliteConnection conn3=new SqliteConnection(cs); 
            conn3.Open();
            using var cmd3 = conn3.CreateCommand();

            cmd3.CommandText=@"select `ID` from `Auction` where `Name` is @Name and `Seller` is @Seller and `Startbid` is @Startbid 
            and `AuctionEnd` is @AuctionEnd and `Image` is @Image and `Description` is @Description;";

            cmd3.Parameters.AddWithValue("@Name", cr.Name);
            cmd3.Parameters.AddWithValue("@Seller", cr.Seller);
            cmd3.Parameters.AddWithValue("@Startbid", cr.Startbid);
            cmd3.Parameters.AddWithValue("@AuctionEnd", cr.AuctionEnd);
            cmd3.Parameters.AddWithValue("@Image", cr.Image);
            cmd3.Parameters.AddWithValue("@Description", cr.Description);

                    using var reader3= cmd3.ExecuteReader();
                        
                    while(reader3.Read())  
                    {
                        tmpitemid=reader3.GetInt32(0);
                    }


             


            
            using SqliteConnection conn2=new SqliteConnection(cs);   
            conn2.Open();
            
            string CommandText2= @"
            insert into 
                Bids
                (`BidId`, `BidPrice`,`BidderId`)
                values
                (@BidId, @BidPrice, @BidderId);";                 

            using var cmd2 = conn2.CreateCommand();

            cmd2.CommandText=CommandText2;

                    cmd2.Parameters.AddWithValue("@BidId", tmpitemid); 
                    cmd2.Parameters.AddWithValue("@BidPrice", cr.Startbid);
                    cmd2.Parameters.AddWithValue("@BidderId", 0);
                    cmd2.ExecuteNonQuery();
           

                    conn2.Close();

                return new OkResult(); 
            }





    }
}