
public class RedisIndexer
{
    #region Singleton Section

    private static Lazy<RedisIndexer> _instance = new Lazy<RedisIndexer>(() => new RedisIndexer());
    public RedisIndexer() { }
    public static RedisIndexer Intance => _instance.Value;

    #endregion

    public string GetProduct(string key)
    {
        string keyModel = string.Format($"Id_{key}");
        var result = RedisConnector.Instance.GetRedisDatabase(RedisDatabaseEnum.PRODUCTINDEX).StringGet(keyModel);
        return result;
    }

    public void SetProduct(string key, string value)
    {
        string keyModel = string.Format($"Id_{key}");
        RedisConnector.Instance.GetRedisDatabase(RedisDatabaseEnum.PRODUCTINDEX).StringSet(keyModel, value, new TimeSpan(00, 05, 0));
    }

    public void DeleteProduct(string key)
    {
        string keyModel = string.Format($"Id_{key}");
        RedisConnector.Instance.GetRedisDatabase(RedisDatabaseEnum.PRODUCTINDEX).KeyDelete(keyModel);
    }
}
