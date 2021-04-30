using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.EfCore.StudentApp.Web.Services
{
    public class FileManagerService : IFileManagerService
    {
        public async Task<string> SaveFile(IFormFile file,
            string webRoot)
        {
            //image
            //create the path wwwroot/images/filename.jpg
            //make a filename
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(webRoot
                , "images", fileName
                );
            //store on disk
            FileStream stream
                = new FileStream(filePath, FileMode.Create);
            //copy from viewmodel file
            await file.CopyToAsync(stream);
            stream.Dispose();
            return fileName;
        }

        void IFileManagerService.DeleteFile(string fileName,string webRoot)
        {
            System.IO.File.Delete(Path.Combine(webRoot,fileName));
        }
    }
}
