﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Funkmap.Statistics.Data.Entities
{
    public class CityStatisticsEntity: BaseStatisticsEntity

    {
        public CityStatisticsEntity()
        {
            CountStatistics = new List<CountStatisticsEntity<string>>();
            StatisticsType = StatisticsType.City;
        }

        [BsonElement("cs")]
        public List<CountStatisticsEntity<string>> CountStatistics { get; set; }

        public override BaseStatisticsEntity Merge(BaseStatisticsEntity second)
        {
            var firstCurrent = this;
            var secondCurrent = second as CityStatisticsEntity;
            if (firstCurrent == null || secondCurrent == null) throw new InvalidOperationException("invalid parameter types");

            var newStatisticsDictionary = secondCurrent.CountStatistics.ToDictionary(x => x.Key);
            foreach (var countStatistic in firstCurrent.CountStatistics)
            {
                if (newStatisticsDictionary.ContainsKey(countStatistic.Key))
                {
                    countStatistic.Count += newStatisticsDictionary[countStatistic.Key].Count;
                }
            }
            return firstCurrent;
        }
    }
}

