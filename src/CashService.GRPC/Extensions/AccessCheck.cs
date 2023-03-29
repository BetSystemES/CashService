using System.Security.Claims;
using CashService.BusinessLogic.Models.Enums;
using CashService.BusinessLogic.Models.Enums.Extensions;

namespace CashService.GRPC.Extensions
{
    public static class AccessCheck
    {
        private static bool IsEnabledFilterByProfileIds(ClaimsPrincipal user)
        {
            var authRoles = user
                .FindFirst(ClaimTypes.Role)?.Value.Split(',')
                .Select(x => x.GetEnumItem<AuthRole>());

            return authRoles is not null && authRoles.Contains(AuthRole.Admin);
        }

        private static (bool isCorrect, Guid id) GetCurrentId(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue("id");
            return (Guid.TryParse(userId, out Guid guid), guid);
        }

        public static void CheckIds(List<Guid> ids, ClaimsPrincipal user)
        {
            (bool isCorrect, Guid id) = GetCurrentId(user);
            if (isCorrect)
            {
                var isAdmin = IsEnabledFilterByProfileIds(user);
                if (!isAdmin)
                {
                    ids.RemoveAll(x => x != id);
                }
            }
            else
            {
                ids.Clear();
            }
        }
    }
}
