using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly MobileContext _db;
        private readonly IDefaultPhoneImagePathProvider _imagePathProvider;
        private readonly UploadService _uploadService;
        private readonly IHostEnvironment _environment;



        public PhoneService(
            MobileContext db, 
            IDefaultPhoneImagePathProvider imagePathProvider, 
            UploadService uploadService, 
            IHostEnvironment environment)
        {
            _db = db;
            _imagePathProvider = imagePathProvider;
            _uploadService = uploadService;
            _environment = environment;
        }

        public async Task CreateAsync(PhoneCreateViewModel entity)
        {
            string imagePath;
            if (entity.File is null)
                imagePath = _imagePathProvider.GetPathToDefaultImage();
            else
            {
                var brand = _db.Brands.FirstOrDefault(b => b.Id == entity.BrandId);
                if (brand is null)
                    throw new Exception();
                string dirPath = Path.Combine(_environment.ContentRootPath, $"wwwroot\\images\\phoneImages\\{brand.Name}");
                string fileName = $"{entity.File.FileName}";
                await _uploadService.UploadAsync(dirPath, fileName, entity.File);
                imagePath = $"images\\phoneImages\\{brand!.Name}\\{fileName}";
            }
                
            _db.Phones.Add(entity.MapToPhone(imagePath));
            await _db.SaveChangesAsync();
        }
    }
}