﻿using System.Configuration;
using Funkmap.Tests.Funkmap.BigData;
using MongoDB.Driver;

namespace Funkmap.Tests.Funkmap.Data
{
    public class FunkmapTestDbProvider
    {
        public static IMongoDatabase DropAndCreateDatabase
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["FunkmapMongoConnection"].ConnectionString;
                var databaseName = ConfigurationManager.AppSettings["FunkmapDbName"];
                var mongoClient = new MongoClient(connectionString);
                mongoClient.DropDatabase(databaseName);
                var db = mongoClient.GetDatabase(databaseName);
                new FunkmapBigDataSeeder(db).SeedData();
                return db;
            }
        }
    }
}
