using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aukcio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        [HttpGet]
        public ActionResult Get(string itemid)
        {
            Console.WriteLine($"itemid={itemid}");

            return new OkResult();
        }
    }
}