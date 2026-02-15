<<<<<<< HEAD
﻿    using Adros.Core.DomainServices.IDomainService;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    namespace Adros.Core.DomainServices
    {
        public class CurrentUserService : ICurrentUserService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public Guid UserId
            {
                get
                {
                    var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                                    //?? _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
                    return !string.IsNullOrEmpty(userIdClaim) ? Guid.Parse(userIdClaim) : Guid.Empty;
                }
            }
        }
    }
=======
﻿using Adros.Core.DomainServices.IDomainService;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Adros.Core.DomainServices
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
            }
        }
    }
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
