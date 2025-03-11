using StackExchange.Redis;
namespace OnlineUsersCounter
{
    public class RedisService
    {
        private readonly IDatabase _db;
        private readonly ConnectionMultiplexer _redis;
        private const string OnlineUsersKey = "OnlineUsers";

        public RedisService(string redisConnectionString)
        {
            _redis = ConnectionMultiplexer.Connect(redisConnectionString);
            _db = _redis.GetDatabase();
        }
        public async Task SetUserOnlineAsync(string ipAddress)
        {
            string key = $"{OnlineUsersKey}:{ipAddress}";
            await _db.StringSetAsync(key, "1", TimeSpan.FromSeconds(30));
        }

        public async Task<int> GetOnlineUsersCountAsync()
        {
            var server = _redis.GetServer(_redis.GetEndPoints().First());
            var keys = server.Keys(pattern: $"{OnlineUsersKey}:*").ToArray();
            return keys.Length;
        }
    }
}
