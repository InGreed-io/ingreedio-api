using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.POCO;
using InGreedIoApi.Utils.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUserPOCO> _userManager;

        public UserRepository(ApiDbContext context, IMapper mapper, UserManager<ApiUserPOCO> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiUser> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ApiUser>(user);
        }

        public async Task<IPage<ApiUserListItemDTO>> GetUsers(string? emailQuery, int pageIndex, int pageSize) {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(emailQuery)) 
            {
                query = query.Where(user => user.Email.ToLower().Contains(emailQuery.ToLower()));
            }

            var usersPage = await query.OrderBy(user => user.Email).ThenBy(user => user.Id)
                .ProjectToPageAsync<ApiUserPOCO, ApiUserListItemDTO>(pageIndex, pageSize, _mapper.ConfigurationProvider);

            foreach (var userDTO in usersPage.Contents) {
                var user = await _userManager.FindByIdAsync(userDTO.Id);
                if (user == null) throw new InGreedIoException("User not found", StatusCodes.Status404NotFound);
                var userRoles = await _userManager.GetRolesAsync(user);
                userDTO.Role = userRoles.FirstOrDefault() ?? "No role";
            }

            return usersPage;
        }

        public async Task<IEnumerable<Preference>> GetPreferences(string id)
        {
            var preferences = await _context.Users.Where(x => x.Id == id).SelectMany(x => x.Preferences).ToListAsync();

            return preferences.Select(x => _mapper.Map<Preference>(x));
        }
    }
}
