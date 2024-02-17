﻿using MongoDB.Driver;

namespace Library.MongoService
{

    public class Connection
    {
        #region Singleton

        private static Connection _instance;
        public static Connection Instance
        {
            get
            {
                return _instance ?? (_instance = new Connection());
            }
        }
        private Connection()
        {
            string env = Environment.GetEnvironmentVariable("ENVIRONMENT");
            IConfiguration config = new ConfigurationBuilder().
                AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).
                AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true).
                AddEnvironmentVariables().
                Build();

            this._connectionString = config["MongoDB:Host"];
            this._connectionDatabase = config["MongoDB:Database"];
        }

        #endregion

        private string _connectionString;
        private string _connectionDatabase;
        private IMongoDatabase _database;


        public IMongoDatabase Database
        {
            get
            {
                if (this._database == null)
                {
                    MongoClient client = new(this._connectionString);
                    this._database = client.GetDatabase(this._connectionDatabase);
                }

                return this._database;
            }
        }
    }


}

