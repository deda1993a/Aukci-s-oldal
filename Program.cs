using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aukcio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string cs= "loggindata.db";
            using SqliteConnection conn=new SqliteConnection(cs);

            conn.Open();

            string CommandText= @"
            insert into 
                User
                (`ID`, `Name`, `PWHash`,`DoB`)
                values
                (@ID, @Name, @PWHash, @DoB);";

            using var cmd = conn.CreateCommand();
            cmd.CommandText=CommandText;
            cmd.Parameters.AddWithValue("@ID", 1);
            cmd.Parameters.AddWithValue("@Name", "Tibor");
            cmd.Parameters.AddWithValue("@PWHash", "---");
            cmd.Parameters.AddWithValue("@DoB", 2000);

            conn.Close();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
