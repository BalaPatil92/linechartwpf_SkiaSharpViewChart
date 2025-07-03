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
using System.Windows.Threading;

namespace CartesianCHart.ViewModels
{
    public class MainViewModelLiveUpdates : INotifyPropertyChanged
    {
        private List<double> _values;
        private List<string> _labels;

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        private const int PointsPerSecond = 50000;
        private const int TotalDurationInSeconds = 600; // 10 minutes
        private const int MaxPointsToShow = 300_000; // show only last 300K points for performance

        private DispatcherTimer _timer;
        private Random _rand = new Random();

        private LineSeries<double> _lineSeries;

        public MainViewModelLiveUpdates()
        {
            _values = new List<double>();
            _labels = new List<string>();

            _lineSeries = new LineSeries<double>
            {
                Values = _values,
                Fill = null,
                Stroke = new SolidColorPaint(SKColors.OrangeRed, 1.5f),
                GeometrySize = 0, // Disable geometry for performance
                Name = "High Frequency Data",
                LineSmoothness = 0 // Disable smoothing for performance
            };

            Series = new ISeries[] { _lineSeries };

            XAxes = new Axis[]
            {
            new Axis
            {
                LabelsRotation = 0,
                TextSize = 10,
                Name = "Time (Last 10 min)",
                MinStep = 5000 // reduce label rendering
            }
            };

            YAxes = new Axis[]
            {
            new Axis
            {
                TextSize = 10,
                Name = "Values"
            }
            };

            StartSimulation();
        }

        private void StartSimulation()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += (s, e) =>
            {
                var newData = GenerateData(PointsPerSecond);
                _values.AddRange(newData);

                if (_values.Count > MaxPointsToShow)
                    _values.RemoveRange(0, _values.Count - MaxPointsToShow);

                _lineSeries.Values = null; // force refresh
                _lineSeries.Values = _values;

                OnPropertyChanged(nameof(Series));
            };

            _timer.Start();
        }

        private List<double> GenerateData(int count)
        {
            var data = new List<double>(count);
            for (int i = 0; i < count; i++)
            {
                data.Add(_rand.NextDouble() * 100);
            }
            return data;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
