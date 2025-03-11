using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineUsersCounter.Pages;

public class IndexModel : PageModel
{
    private readonly RedisService _redisService;
    public int OnlineUsersCount { get; set; }

    public IndexModel(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task OnGet()
    {
        string userIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        await _redisService.SetUserOnlineAsync(userIp);
        OnlineUsersCount = await _redisService.GetOnlineUsersCountAsync();
    }
}

