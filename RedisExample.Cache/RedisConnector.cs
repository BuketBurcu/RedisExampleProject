using StackExchange.Redis;

public class RedisConnector
{
    #region Singleton Section

    private readonly static RedisConnector _instance = new RedisConnector();
    private RedisConnector() { }

    public static RedisConnector Instance = _instance;

    #endregion

    private static ConnectionMultiplexer _redisConnection;
    public ConnectionMultiplexer RedisConnection
    {
        get
        {
            if (_redisConnection == null)
                _redisConnection = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_CONNECTION"));
            return _redisConnection;
        }
        set
        {
            _redisConnection = value;
        }

    }

    public IDatabase GetRedisDatabase(RedisDatabaseEnum databaseIndex)
    {
        return RedisConnection.GetDatabase((int)databaseIndex);
    }
}

