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
    public class BidController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BidController> _logger;

        public BidController(ILogger<BidController> logger)
        {
            _logger = logger;
        }


        public class Bid
        {
            public int BidId{get;set;}
            public int BidPrice{get;set;}
            public int BidderId{get;set;}
        }

     
        private bool change=false;

        private int BidIdtmp, BidderIdtmp, BidPricetmp; 
            public ActionResult OnPost(Bid bd){
               
                
            string cs= "Data Source=loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);
            
            conn.Open();
            using var cmd = conn.CreateCommand();
                     
            
            cmd.CommandText=@"SELECT BidId, MAX (BidPrice), BidderId 
                            FROM Bids 
                            WHERE BidPrice NOT IN (SELECT Max (BidPrice) 
                            FROM Bids) AND BidId=@bidid;";

            
            cmd.Parameters.AddWithValue("@bidid", bd.BidId);
            using var reader= cmd.ExecuteReader();
            List<Bid> result=new List<Bid>();
           

                    while(reader.Read())
                    {
                        try{
                        if(bd.BidPrice>reader.GetInt32(1)){
                            change=true;
                            Console.WriteLine("ar: "+reader.GetInt32(1));

                        

                             }else{
                              
                                
                                    BidIdtmp=reader.GetInt32(0);
                                    BidPricetmp=reader.GetInt32(1);
                                    BidderIdtmp=reader.GetInt32(2);

                                    
                               
                             }
                        }catch{}
                    }
                    reader.Close();

                    if(BidIdtmp==0){
                        change=true;
                    }

                    if(change==true){
                        
            cmd.CommandText= @"
            insert into 
                Bids
                (`BidId`, `BidPrice`,`BidderId`)
                values
                (@BidId, @BidPrice, @BidderId);";                 

            

                    cmd.Parameters.AddWithValue("@BidId", bd.BidId);
                    cmd.Parameters.AddWithValue("@BidPrice", bd.BidPrice);
                    cmd.Parameters.AddWithValue("@BidderId", bd.BidderId);
                    cmd.ExecuteNonQuery();
                    conn.Close(); 
                    conn.Open();
                    using var cmd2 = conn.CreateCommand();

                    cmd2.CommandText=@"SELECT BidId, MAX (BidPrice), BidderId 
                                    FROM Bids 
                                    WHERE BidPrice NOT IN (SELECT Max (BidPrice) 
                                    FROM Bids) AND BidId=@bidid;";

                    
                    cmd2.Parameters.AddWithValue("@bidid", bd.BidId);
                    using var reader2= cmd2.ExecuteReader();

                            while(reader2.Read()){
                           try{
                              
                                   BidIdtmp=reader2.GetInt32(0);
                                   BidPricetmp=reader2.GetInt32(1);
                                   BidderIdtmp=reader2.GetInt32(2);

                           }catch{

                           }     
                            }


                    }



                    using var cmd3 = conn.CreateCommand();

                    cmd3.CommandText=@"SELECT BidId, Max(BidPrice), BidderId 
                                    FROM Bids 
                                    WHERE BidId=@bidid;";

                    
                    cmd3.Parameters.AddWithValue("@bidid", bd.BidId);
                    using var reader3= cmd3.ExecuteReader();

                            while(reader3.Read()){
                                Console.WriteLine("vezet:"+reader3.GetInt32(1));
                            result.Add(new Bid
                               {
                                   BidId=BidIdtmp,
                                   BidPrice=BidPricetmp,
                                   BidderId=reader3.GetInt32(2),

                             });                    
                            }                            
                                        

                return Ok(result);
            }        


    }
}