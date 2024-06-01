using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;
using InGreedIoApi.DTO;
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

        public async Task<Preference> CreatePreference(string userId, CreatePreferenceDTO args)
        {
            var user = await _context.Users
                .Include(u => u.Preferences)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var newPreference = new PreferencePOCO
            {
                Name = args.Name,
                UserId = userId,
                User = user,
                IsActive = true,
                Wanted = new List<IngredientPOCO>(),
                Unwanted = new List<IngredientPOCO>(),
            };

            user.Preferences.Add(newPreference);

            await _context.SaveChangesAsync();

            // await _context.Entry(newPreference).Reference(p => p.User).LoadAsync();

            return _mapper.Map<Preference>(newPreference);
        }

    }
}
