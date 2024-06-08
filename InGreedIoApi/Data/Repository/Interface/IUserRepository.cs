using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Preference>> GetPreferences(string id);

        Task<Preference> CreatePreference(string userId, CreatePreferenceDTO args);

        Task<ApiUser> GetUserById(string id);

        Task<IPage<ApiUserListItemDTO>> GetUsers(string? emailQuery, int pageIndex, int pageSize);

        Task<IPage<ProductDTO>> GetFavourites(ProductQueryDTO productQueryDto, string userId);

        Task LockUser(string userId);

        Task UnlockUser(string userId);
    }
}
