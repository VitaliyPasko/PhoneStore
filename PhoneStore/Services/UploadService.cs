using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PhoneStore.Services
{
    public class UploadService
    {
        public async Task UploadAsync(string dirPath, string fileName, IFormFile file)
        {
            await using var stream = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}