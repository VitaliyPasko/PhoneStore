
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IPaginationService _paginationService;

        public UserService(
            MobileContext db, 
            IUsersSortService sortService, 
            IUsersFilter usersFilter, 
            IPaginationService paginationService)
        {
            _db = db;
            _sortService = sortService;
            _usersFilter = usersFilter;
            _paginationService = paginationService;
        }

        public async Task<UsersViewModel> GetAll(Order order, string filterByName, int pageSize, int page)
        {
            var users = _db.Users.AsQueryable();
            var filteredUsers = _usersFilter.FilterByName(users, filterByName);
            var sortedUsers = _sortService.Sort(filteredUsers, order);
            var paginationData = await _paginationService.GetABatchOfData(sortedUsers, page, pageSize);
            
            return paginationData.Item1.MapToUsersViewModel(filterByName, page, paginationData.Item2, pageSize);
        }
    }
}