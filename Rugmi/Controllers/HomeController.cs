using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rugmi.Models;
using Rugmi.Data;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Rugmi.Controllers
{
    public class HomeController : Controller
    {

        private readonly ImageDbContext _context;
        private readonly IHostingEnvironment _environment;


        public HomeController(ImageDbContext context, IHostingEnvironment appEnvironment)
        {
            this._context = context;
            this._environment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        // UPLOAD: the endpoint to accept tghe picture
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Image image)
        {

            // UPLOAD: grabs the files from the incoming form
            var files = HttpContext.Request.Form.Files;

            // UPLOAD: processes each file
            foreach (var _image in files)
            {
                if (_image != null && _image.Length > 0)
                {

                    var file = _image;

                    // UPLOAD: sets the path of the where the file is stored on the server
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images");

                    if (file.Length > 0)
                    {

                        // UPLOAD: creates a new unique file name to store in the uploads folder 
                        var fileName = Guid.NewGuid().ToString() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var _filePath = Path.Combine(uploads, fileName);


                        // UPLOAD: Saves file to local server
                        using (var fileStream = new FileStream(_filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                            // UPLOAD: sets propertoies on the new model
                            image.Url = fileName;
                            image.Name = file.FileName;
                        }


                    }
                }
            }


            // UPLOAD: saves model to database
            _context.Add(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
