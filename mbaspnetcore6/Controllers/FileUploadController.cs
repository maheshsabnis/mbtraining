using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mbaspnetcore6.Controllers
{
    public class FileUploadController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        public FileUploadController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            ProfileInfo profiles = new ProfileInfo();
            return View(profiles);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(ProfileInfo profile)
        {
            // 1. Get the Uploaded file
            IFormFile file = profile.Picture;

            // 2. Process the FIle
            if (file.Length > 0)
            {
                // 2.a. Read the Uploaded file from the HTTP Protocol
                // ContentDispositionHeaderValue: Point to HTTP Header the
                // has enctype=multipart/form-data
                var postedFile = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName.Trim('"');
                // 2.b. Point to the Local Path where the File is Stored
                var finalPath = Path.Combine(hostEnvironment.WebRootPath, "images", postedFile);

                // 2.c. Write the 'poatsedFile' file using 'file' to local path

                using (FileStream fs = new FileStream(finalPath, FileMode.CreateNew))
                {
                    // A File is created
                    await file.CopyToAsync(fs);
                }
                profile.UploadStatus = $"File {file.FileName} is uploaded Successfully";
                profile.FileName = file.FileName;
            }
            else
            {
                profile.UploadStatus = "The File is empty so not uploaded";
            }
            return View(profile);
        }
    }
}