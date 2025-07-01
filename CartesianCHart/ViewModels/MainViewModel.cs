using CartesianCHart.Helper;
using CartesianCHart.Samples;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartesianCHart.Model;

namespace CartesianCHart.ViewModels
{
    public class MainViewModel
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public MainViewModel()
        {
            // Simulate 6 datasets
            List<List<DataPoint>> aggregatedDataSets = new List<List<DataPoint>>();

            for (int i = 0; i < 6; i++)
            {
                var rawData = DataHelper.GenerateRawData(); // 260,000 points
                var aggregated = DataHelper.AggregatePerMinute(rawData); // ~4320 points
                aggregatedDataSets.Add(aggregated);
            }

            // Create line series for each dataset
            Series = aggregatedDataSets.Select((dataset, index) => new LineSeries<double>
            {
                Values = dataset.Select(x => x.Value).ToArray(),
                Fill = null,
                Stroke = new SolidColorPaint(GetColorByIndex(index), 2),
                GeometrySize = 1,
                Name = $"Dataset {index + 1}"
            }).ToArray();

            // X axis based on first dataset's timestamps
            var firstDatasetTimestamps = aggregatedDataSets[0].Select(x => x.Timestamp.ToString("MM-dd HH:mm")).ToArray();

            XAxes = new Axis[]
            {
            new Axis
            {
               Labeler = value =>
                {
                    int index = (int)value;
                    if (index >= 0 && index < aggregatedDataSets[0].Count)
                        return aggregatedDataSets[0][index].Timestamp.ToString("MM-dd HH:mm");
                    return "";
                },
                LabelsRotation = 45,
                TextSize = 12,
            }
            };

            YAxes = new Axis[]
            {
            new Axis
            {
                TextSize = 12
            }
            };
        }

        private SKColor GetColorByIndex(int index)
        {
            SKColor[] colors =
            {
            SKColors.DodgerBlue,
            SKColors.Red,
            SKColors.Green,
            SKColors.Orange,
            SKColors.Purple,
            SKColors.Brown
        };
            return colors[index % colors.Length];
        }
    }
}
