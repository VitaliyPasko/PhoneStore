using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Feedback;
using PhoneStore.ViewModels.PhoneViewModels;

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

        public PhoneViewModel GetPhoneById(int phoneId)
        {
            var phone = _db.Phones
                .Include(p => p.Brand)
                .Include(p => p.Feedbacks)
                .ThenInclude(f => f.User)
                .FirstOrDefault(p => p.Id == phoneId);
                
            var phoneViewModel = new PhoneViewModel
            {
                Brand = phone.Brand,
                Feedbacks = phone.Feedbacks.Select(f => new FeedbackViewModel
                    {
                        Id = f.Id,
                        Phone = f.Phone.MapToPhoneViewModel(),
                        Text = f.Text,
                        User = f.User.MapToUserViewModel(),
                        CreationDateTime = f.CreationDateTime,
                        UserId = f.UserId,
                        PhoneId = f.PhoneId
                    })
                    .OrderByDescending(f => f.CreationDateTime)
                    .ToList(),
                Image = phone.Image,
                Name = phone.Name,
                Price = phone.Price,
                Id = phone.Id
            };
            return phoneViewModel;
        }
    }
}