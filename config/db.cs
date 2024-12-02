using dotenv.net;
using MongoDB.Driver;

namespace DatabaseConfig
{
    public class DBConfig
    {
        private readonly string mongoConnection;
        private readonly string databaseName;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        public DBConfig()
        {
            DotEnv.Load();
            Console.WriteLine(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));
            Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            mongoConnection = Environment.GetEnvironmentVariable("MONGO_CONNECTION") ?? throw new ArgumentNullException("MONGO_CONNECTION is not set.");
            databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? throw new ArgumentNullException("DATABASE_NAME is not set.");
            client = new MongoClient(mongoConnection);
            database = client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return database;
        }
    }
}