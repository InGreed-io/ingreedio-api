﻿using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public IngredientRepository(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> FindAll(string? query)
        {
            var ingredientsPOCO = _context.Ingredients.AsQueryable();

            if(query != null) {
              ingredientsPOCO = ingredientsPOCO.Where(x => x.Name.ToLower().Contains(query.ToLower()));
            }

            return ingredientsPOCO.Select(x => _mapper.Map<Ingredient>(x));
        }
    }
}
