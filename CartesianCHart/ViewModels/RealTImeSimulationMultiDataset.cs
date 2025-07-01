using LiveCharts.Defaults;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CartesianCHart.ViewModels
{
    public class RealTImeSimulationMultiDataset : INotifyPropertyChanged
    {
        public ObservableCollection<double> FirstDataSet { get; set; } = new ObservableCollection<double>();
        public ObservableCollection<double> SecondDataSet { get; set; } = new ObservableCollection<double>();

        public IEnumerable<ISeries> Series { get; set; }

        private readonly Random _random = new Random();

        public RealTImeSimulationMultiDataset()
        {
            Series = new ISeries[]
            {
             new LineSeries<double>
             {
                 Values = FirstDataSet,
                 Name = "Dataset 1",
                 Stroke = new SolidColorPaint(SKColors.Blue),
                 Fill = null
             },
             new LineSeries<double>
             {
                 Values = SecondDataSet,
                 Name = "Dataset 2",
                 Stroke = new SolidColorPaint(SKColors.Red),
                 Fill = null
             }
                    };

            _ = StartUpdatingDataAsync();
        }

        private async Task StartUpdatingDataAsync()
        {
            while (true)
            {
                await Task.Delay(1000);

                // Add new data point
                FirstDataSet.Add(_random.NextDouble() * 100);
                SecondDataSet.Add(_random.NextDouble() * 100);

                // Keep only last 100 points
                if (FirstDataSet.Count > 100) FirstDataSet.RemoveAt(0);
                if (SecondDataSet.Count > 100) SecondDataSet.RemoveAt(0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
