using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiUser> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ApiUser>(user);
        }

        public async Task<IEnumerable<Preference>> GetPreferences(string id)
        {
            var preferences = await _context.Users.Where(x => x.Id == id).SelectMany(x => x.Preferences).ToListAsync();

            return preferences.Select(x => _mapper.Map<Preference>(x));
        }
    }
}