using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PhoneStore.Models;
using PhoneStore.Tests.Helpers;
using PhoneStore.ViewModels.PhoneViewModels;
using Xunit;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace PhoneStore.Tests
{
    public class PhoneControllerTests
    {
        private HttpClient _httpClient;
        private  MobileContext _context;

        public PhoneControllerTests()
        {
            var webHost = Utilities.SubstituteDbOnTestDb();
            _httpClient = webHost.CreateClient();
            _context = webHost.Services.CreateScope().ServiceProvider.GetService<MobileContext>();
        }

        [Fact]
        public async Task Send_Create_Phone_With_NotValidName_Returns_BadRequest()
        {

            var phone = new PhoneCreateViewModel
            {
                Name = null,
                Price = 0,
                BrandId = Utilities.CreateBrandAndReturnBrandId(_context)
            };

            string json = JsonSerializer.Serialize(phone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Phones/Create", content);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        [Fact]
        public async Task Send_Create_Phone_With_NotValidPrice_Returns_ValidationProblem()
        {

            var phone = new PhoneCreateViewModel
            {
                Name = "Test",
                Price = 0,
                BrandId = Utilities.CreateBrandAndReturnBrandId(_context)
            };

            string json = JsonSerializer.Serialize(phone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Phones/Create", content);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task Send_Create_Phone_With_NegativePrice_Returns_ValidationProblem()
        {

            var phone = new PhoneCreateViewModel
            {
                Name = "Test",
                Price = -3000M,
                BrandId = Utilities.CreateBrandAndReturnBrandId(_context)
            };

            string json = JsonSerializer.Serialize(phone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Phones/Create", content);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task Get_Phone_By_Id_Returns_PhoneVM()
        {
            var phoneId = Utilities.CreatePhoneAndReturnPhoneId(_context);
            HttpResponseMessage response = await _httpClient.GetAsync($"Phones/About?phoneId={phoneId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var phoneVm = JsonSerializer.Deserialize<PhoneViewModel>(jsonString);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            phoneVm.Should().NotBeNull();
            phoneVm.Brand.Should().NotBeNull();
            phoneVm.Id.Should().Be(phoneId);
        }
    }
}