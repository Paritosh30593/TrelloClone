using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TC.Application.ServiceContracts;

namespace TC.WebAPI.Services
{
    public sealed class ClaimService : IClaimService
    {
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;
            ClaimsPrincipal principal = httpContext?.User;

            IsAuthenticated = principal?.Identity?.IsAuthenticated == true;

            UserId = FindClaim(principal, "oid", ClaimTypes.NameIdentifier);

            Name = FindClaim(principal, "name", ClaimTypes.Name);

            Email = FindClaim(principal, "preferred_username", ClaimTypes.Email, "email", ClaimTypes.Upn);

            IpAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
        }

        public bool IsAuthenticated { get; }

        public string UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public string IpAddress { get; }

        private static string FindClaim(ClaimsPrincipal principal, params string[] claimTypes)
        {
            foreach (string claimType in claimTypes)
            {
                string value = principal?.FindFirst(claimType)?.Value;

                if (!string.IsNullOrWhiteSpace(value)) return value;
            }
            return null;
        }
    }
}
