﻿using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly DataContext _dataContext;
        private readonly UserResolverService _userResolverService;

        public PropertiesService(
            DataContext dataContext,
            UserResolverService userResolverService)
        {
            _dataContext = dataContext;
            _userResolverService = userResolverService;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync(GetAllPropertiesFilter filter)
        {
            var properties = _dataContext.Properties.AsQueryable();

            properties = AddFiltersOnQuery(filter, properties);

            return await properties.ToListAsync();
        }

        public async Task<bool> CreatePropertyAsync(Property property)
        {
            property.UserId = _userResolverService.GetUserId();

            await _dataContext.Properties.AddAsync(property);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<Property> GetPropertyByIdAsync(int propertyId)
        {
            return await _dataContext.Properties.SingleOrDefaultAsync(x => x.Id == propertyId);
        }

        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            _dataContext.Properties.Update(property);

            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePropertyAsync(int propertyId)
        {
            var property = await GetPropertyByIdAsync(propertyId);

            _dataContext.Properties.Remove(property);

            var removed = await _dataContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<bool> DoesUserOwnPropertyAsync(int userId, int propertyId)
        {
            var property = await _dataContext.Properties
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == propertyId);

            return property != null && property.UserId == userId;
        }

        private static IQueryable<Property> AddFiltersOnQuery(GetAllPropertiesFilter filter, IQueryable<Property> properties)
        {
            if (filter?.MaxPricePerNight > 0)
            {
                properties = properties.Where(x => x.PricePerNight <= filter.MaxPricePerNight);
            }

            if (filter?.MinPricePerNight > 0)
            {
                properties = properties.Where(x => x.PricePerNight >= filter.MinPricePerNight);
            }

            if (filter?.AccommodatesNumber > 0)
            {
                properties = properties.Where(x => x.AccommodatesNumber >= filter.AccommodatesNumber);
            }

            if (filter?.UserId > 0)
            {
                properties = properties.Where(x => x.UserId == filter.UserId);
            }

            if (filter?.CityId > 0)
            {
                properties = properties.Where(x => x.CityId == filter.CityId);
            }

            return properties;
        }
    }
}