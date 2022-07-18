using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels;
using Order = PhoneStore.Enums.Order;

namespace PhoneStore.Services
{
    public class UserService : IUserService
    {
        private readonly MobileContext _db;
        private readonly IUsersSortService _sortService;
        private readonly IUsersFilter _usersFilter;
        private readonly IPaginationService<User> _paginationService;
        private readonly IConfiguration _configuration;

        public UserService(
            MobileContext db, 
            IUsersSortService sortService, 
            IUsersFilter usersFilter, 
            IPaginationService<User> paginationService, 
            IConfiguration configuration)
        {
            _db = db;
            _sortService = sortService;
            _usersFilter = usersFilter;
            _paginationService = paginationService;
            _configuration = configuration;
        }

        public async Task<UsersViewModel> GetAll(Order order, string filterByName, int page)
        {
            var pageSize = int.Parse(_configuration["UsersPageSize"]);
            var users = _db.Users.AsQueryable();
            var filteredUsers = _usersFilter.FilterByName(users, filterByName);
            var sortedUsers = _sortService.Sort(filteredUsers, order);
            var (queryable, count) = await _paginationService.GetABatchOfData(sortedUsers, page, pageSize);
            
            return queryable.MapToUsersViewModel(filterByName, page, count, pageSize);
        }
    }
}