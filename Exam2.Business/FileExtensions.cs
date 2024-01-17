using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business
{
    public static class FileExtensions
    {


        public const string AboutImagesPath = "aboutimages";

        public static async Task<string> SaveAndProvideNameAsync(this IFormFile file, IWebHostEnvironment env)
        {
            var rootPath = env.WebRootPath;

            var dirPath = Path.Combine(rootPath, AboutImagesPath);

            if(!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var fileName = Guid.NewGuid().ToString() + file.FileName;
            var filePath = Path.Combine(dirPath,fileName);
            using (var newFile = File.Create(filePath))
            { 
                await file.CopyToAsync(newFile);
            }

            return fileName;
        }


    }
}
