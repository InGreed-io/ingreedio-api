using InGreedIoApi.Model;
using InGreedIoApi.DTO;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Preference>> GetPreferences(string id);
        Task<Preference> CreatePreference(string userId, CreatePreferenceDTO args);

        Task<ApiUser> GetUserById(string id);
    }
}
