﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Funkmap.Data.Entities;
using Funkmap.Data.Entities.Abstract;
using Funkmap.Data.Repositories;
using Funkmap.Data.Services.Abstract;
using Funkmap.Statistics.Data.Entities;
using Funkmap.Statistics.Data.Repositories;
using Funkmap.Statistics.Data.Repositories.Abstract;
using Funkmap.Statistics.Data.Services;
using Funkmap.Statistics.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.GridFS;

namespace Funkmap.Statistics.Tests
{
    [TestClass]
    public class CityStatisticsTest
    {
        private CityStatisticsRepository _repository;

        private BaseRepository _profilesRepository;

        private StatisticsMerger _merger;


        [TestInitialize]
        public void Initialize()
        {
            var db = FunkmapStatisticsTestDbProvider.Database;
            db.DropCollection(StatisticsCollectionNameProvider.StatisticsCollectionName);


            var profilesCollection = db.GetCollection<BaseEntity>(CollectionNameProvider.BaseCollectionName);
            var typeStatisticsCollection = db.GetCollection<CityStatisticsEntity>(StatisticsCollectionNameProvider.StatisticsCollectionName);

            var citiesProvider = new CitiesInfoProvider();
            _repository = new CityStatisticsRepository(typeStatisticsCollection, profilesCollection, citiesProvider);

            using (var mock = AutoMock.GetLoose())
            {
                var gridFs = mock.Mock<IGridFSBucket>();
                var filterFactory = mock.Mock<IFilterFactory>();

                _profilesRepository = new BaseRepository(profilesCollection, gridFs.Object, filterFactory.Object);
            }

            var statisticsCollection = db.GetCollection<BaseStatisticsEntity>(StatisticsCollectionNameProvider.StatisticsCollectionName);

            var statisticsRepositories = new List<IStatisticsRepository>() { _repository };

            _merger = new StatisticsMerger(statisticsCollection, statisticsRepositories);



        }

        [TestMethod]
        public void GetFullStatistics()
        {
            var fullStatistics = _repository.BuildFullStatisticsAsync().GetAwaiter().GetResult() as CityStatisticsEntity;
            _repository.UpdateAsync(fullStatistics).GetAwaiter().GetResult();

            var savedStatistic = _repository.GetAsync(fullStatistics.Id.ToString()).GetAwaiter().GetResult();

            Assert.IsNotNull(savedStatistic);

            Assert.IsTrue(CompareStatistics(savedStatistic, fullStatistics));

            var begin = new DateTime(2016, 11, 9);
            var end = new DateTime(2017, 11, 9);
            var periodStatistics = _repository.BuildStatisticsAsync(begin, end).GetAwaiter().GetResult();
            Assert.IsNotNull(periodStatistics);

            _merger.MergeStatistics().GetAwaiter().GetResult();
            var mergedStatistics = _repository.GetAsync(fullStatistics.Id.ToString()).GetAwaiter().GetResult();

            Assert.IsTrue(CompareStatistics(mergedStatistics, fullStatistics));


            var newEntity = new BandEntity()
            {
                Login = Guid.NewGuid().ToString(),
                Name = "qweqwe",
                CreationDate = DateTime.UtcNow
            };
            _profilesRepository.CreateAsync(newEntity).GetAwaiter().GetResult();

            _merger.MergeStatistics().GetAwaiter().GetResult();
            var newMergedStatistics = _repository.GetAsync(fullStatistics.Id.ToString()).GetAwaiter().GetResult();
            Assert.AreNotEqual(mergedStatistics, newMergedStatistics);


        }

        private bool CompareStatistics(CityStatisticsEntity savedStatistic, CityStatisticsEntity fullStatistics)
        {
            bool areEquals = true;
            foreach (var statistic in savedStatistic.CountStatistics)
            {
                var buildedStatistic = fullStatistics.CountStatistics.SingleOrDefault(x => x.Key == statistic.Key);

                if (buildedStatistic == null || buildedStatistic.Count != statistic.Count)
                {
                    areEquals = false;
                    break;
                }
            }
            return areEquals;
        }
    }
}

