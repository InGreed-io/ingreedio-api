﻿using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Preference>> GetPreferences(string id);

        Task<ApiUser> GetUserById(string id);

        Task<IPage<ProductDTO>> GetFavourites(ProductQueryDTO productQueryDto, string userId);
    }
}