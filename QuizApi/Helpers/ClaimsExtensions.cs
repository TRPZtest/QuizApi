using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace QuizApi.Helpers
{
    public static class ClaimsExtensions
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            long.TryParse(user?.Identity?.Name, out long id);

            if (id == 0)           
                throw new Exception("User id parsing error");
          
            return id;
        }
    }
}
