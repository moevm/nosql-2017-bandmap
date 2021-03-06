﻿using System;
using System.Collections.Generic;
using System.Linq;
using Funkmap.Data.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Funkmap.Statistics.Data.Entities
{
    public class TopStylesStatisticsEntity : BaseStatisticsEntity
    {
        public TopStylesStatisticsEntity()
        {
            CountStatistics = new List<CountStatisticsEntity<Styles>>();
            StatisticsType = StatisticsType.TopStyles;
        }

        [BsonElement("cs")]
        public List<CountStatisticsEntity<Styles>> CountStatistics { get; set; }

        public override BaseStatisticsEntity Merge(BaseStatisticsEntity second)
        {
            var firstCurrent = this;
            var secondCurrent = second as TopStylesStatisticsEntity;
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

