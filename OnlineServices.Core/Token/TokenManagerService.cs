﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly OnlineServicesDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _cache;

        public TokenManagerService(UserManager<IdentityUser> userManager,
            IDistributedCache cache,
            IHttpContextAccessor httpContextAccessor,
            OnlineServicesDbContext db)
        {
            _cache = cache;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }

        public async Task<bool> IsCurrentActiveToken()
         => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await _cache.GetStringAsync(GetKey(token)) == null;

        public async Task DeactivateAsync(string token)
            => await _cache.SetStringAsync(GetKey(token),
                " ");

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
            => $"tokens:{token}:deactivated";

    }
}
