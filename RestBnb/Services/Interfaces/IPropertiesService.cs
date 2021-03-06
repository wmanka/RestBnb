﻿using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IPropertiesService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync(GetAllPropertiesFilter filter = null);

        Task<bool> CreatePropertyAsync(Property property);

        Task<Property> GetPropertyByIdAsync(int propertyId);

        Task<bool> UpdatePropertyAsync(Property property);

        Task<bool> DeletePropertyAsync(int propertyId);

        public Task<bool> DoesUserOwnPropertyAsync(int userId, int propertyId);
    }
}