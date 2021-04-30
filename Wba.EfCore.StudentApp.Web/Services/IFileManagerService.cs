using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Wba.EfCore.StudentApp.Web.Services
{
    public interface IFileManagerService
    {
        Task<string> SaveFile(IFormFile file, string webRoot);
        void DeleteFile(string fileName,string webRoot);
    }
}