using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aukcio.Controllers
{
    public class UploadController : Controller
    {
       
        public ActionResult OnPostUppy(List<IFormFile> files)
        {
            Console.WriteLine("public ActionResult OnPostUppy(List<IFormFile> files)");
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = $"Upload/{formFile.FileName}";

                    using var stream = System.IO.File.Create(filePath);
                    
                        formFile.CopyTo(stream);
                    
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }
    }
}
