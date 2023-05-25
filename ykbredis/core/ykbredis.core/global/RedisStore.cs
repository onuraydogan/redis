using StackExchange.Redis;

namespace ykbredis.core.global
{
    public class RedisStore
    {
        private static Lazy<ConnectionMultiplexer> redisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions conf = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                ConnectTimeout = 30000,
                ConnectRetry = 5,
                KeepAlive = 30,
                SyncTimeout = 30000,
                AsyncTimeout = 30000
            };

            conf.EndPoints.Add("127.0.0.1", 6379);

            return ConnectionMultiplexer.Connect(conf.ToString());
        });
        public static ConnectionMultiplexer Connection => redisConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();

        public static ISubscriber Subscriber => Connection.GetSubscriber();

    }

}
