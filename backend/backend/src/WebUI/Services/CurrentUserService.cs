using System.Security.Claims;

using backend.Application.Common.Interfaces;

namespace backend.WebUI.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == "id").Select(x => x.Value).SingleOrDefault();

    public string Role => _httpContextAccessor.HttpContext?.User?.Claims.Where(x=>x.Type==ClaimTypes.Role).Select(x=>x.Value).SingleOrDefault();
}
