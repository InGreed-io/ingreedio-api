using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.POCO;
using InGreedIoApi.Services;
using InGreedIoApi.Utils.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly UserManager<ApiUserPOCO> _userManager;

        public UserRepository(ApiDbContext context, IMapper mapper, IProductService productService, UserManager<ApiUserPOCO> userManager)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
            _userManager = userManager;
        }

        public async Task<ApiUser> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ApiUser>(user);
        }

        public async Task<IPage<ApiUserListItemDTO>> GetUsers(string? emailQuery, int pageIndex, int pageSize) 
        {
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
            var preferences = await _context.Preferences
            .Include(p => p.Wanted)
            .Include(p => p.Unwanted)
            .Where(p => p.UserId == id)
            .ToListAsync();

            return preferences.Select(x => _mapper.Map<Preference>(x));
        }

        public async Task<IPage<ProductDTO>> GetFavourites(ProductQueryDTO productQueryDto, string userId)
        {
            var queryable = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(productQueryDto.query))
                queryable = queryable.Where(p => p.Name.ToLower().Contains(productQueryDto.query.ToLower()));

            if (productQueryDto.categoryId.HasValue)
                queryable = queryable.Where(p => p.CategoryId == productQueryDto.categoryId.Value);

            var wanted = UpdateWantedAndUnwantedFromPreference(productQueryDto, ref queryable);
            //sort elements by enum
            _productService.SortProductQueryDto(productQueryDto, ref queryable, wanted);

            //filter only favourites
            queryable = queryable.Where(p => p.FavouriteBy.Any(u => u.Id == userId));

            return await queryable.ProjectToPageAsync<ProductPOCO, ProductDTO>(
                productQueryDto.pageIndex, productQueryDto.pageSize, _mapper.ConfigurationProvider
            );
        }

        private IEnumerable<int> UpdateWantedAndUnwantedFromPreference(ProductQueryDTO productQueryDto, ref IQueryable<ProductPOCO> queryable)
        {
            var wanted = productQueryDto.ingredients;
            var unwanted = new List<int>();

            if (productQueryDto.preferenceId.HasValue)
            {
                //Get preference
                var preferencePoco = _context.Preferences.Single(pref => pref.Id == productQueryDto.preferenceId);
                var preference = _mapper.Map<Preference>(preferencePoco);

                //Get wanted and unwanted
                ICollection<int> wantedFromPreference = preference.Wanted.Select(i => i.Id).ToList();
                wanted = wanted.Concat(wantedFromPreference).ToList();
                unwanted = preference.Unwanted.Select(i => i.Id).ToList();
            }

            // filter products that doesnt have any unwanted ingredient and has all wanted igredients
            if (wanted is not null && wanted.Count > 0)
            {
                queryable = queryable.Where(p => p.Ingredients.Any(i => wanted.Contains(i.Id)));
            }
            if (unwanted.Count > 0)
            {
                queryable = queryable.Where(p => !p.Ingredients.Any(i => unwanted.Contains(i.Id)));
            }

            return wanted ?? new List<int>();
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

        public async Task LockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                throw new InGreedIoException("Could not find user.", StatusCodes.Status404NotFound);

            var enableLockoutResult = await _userManager.SetLockoutEnabledAsync(user, true);
            if (!enableLockoutResult.Succeeded) 
                throw new InGreedIoException("Could not deactivate user.", StatusCodes.Status400BadRequest);

            var lockoutDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            if (!lockoutDateResult.Succeeded) 
                throw new InGreedIoException("Could not deactivate user.", StatusCodes.Status400BadRequest);
        }

        public async Task UnlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                throw new InGreedIoException("Could not find user.", StatusCodes.Status404NotFound);

            var enableLockoutResult = await _userManager.SetLockoutEnabledAsync(user, true);
            if (!enableLockoutResult.Succeeded) 
                throw new InGreedIoException("Could not activate user.", StatusCodes.Status400BadRequest);

            var lockoutDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            if (!lockoutDateResult.Succeeded) 
                throw new InGreedIoException("Could not activate user.", StatusCodes.Status400BadRequest);
        }

        public async Task<bool> IsUserLocked(string userId) 
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                throw new InGreedIoException("Could not find user.", StatusCodes.Status404NotFound);
            
            var isLockedOut = await _userManager.IsLockedOutAsync(user);
            return isLockedOut;
        }
    }
}
