using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Model.Enum;
using InGreedIoApi.POCO;
using InGreedIoApi.Services;
using InGreedIoApi.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public UserRepository(ApiDbContext context, IMapper mapper, IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
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

    }
}
