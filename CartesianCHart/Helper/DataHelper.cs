using CartesianCHart.Model;
using CartesianCHart.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianCHart.Helper
{
    public static class DataHelper
    {
        public static List<DataPoint> GenerateRawData(int seconds = 260_000)
        {
            var data = new List<DataPoint>();
            DateTime start = DateTime.Now.AddDays(-3);
            Random rand = new Random();

            for (int i = 0; i < seconds; i++)
            {
                data.Add(new DataPoint
                {
                    Timestamp = start.AddSeconds(i),
                    Value = rand.NextDouble() * 100
                });
            }

            return data;
        }

        public static List<DataPoint> AggregatePerMinute(List<DataPoint> rawData)
        {
            return rawData
                .GroupBy(dp => new DateTime(dp.Timestamp.Year, dp.Timestamp.Month, dp.Timestamp.Day,
                                            dp.Timestamp.Hour, dp.Timestamp.Minute, 0))
                .Select(g => new DataPoint
                {
                    Timestamp = g.Key,
                    Value = g.Average(x => x.Value)
                }).ToList();
        }
    }
}
