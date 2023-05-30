using System.Security.Claims;

namespace WebApp.Extensions
{
    public static class Extension
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal) => 
            claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}