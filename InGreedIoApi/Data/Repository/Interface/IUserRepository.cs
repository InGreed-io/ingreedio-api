using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Preference>> GetPreferences(string id);

        Task<ApiUser> GetUserById(string id);
    }
}