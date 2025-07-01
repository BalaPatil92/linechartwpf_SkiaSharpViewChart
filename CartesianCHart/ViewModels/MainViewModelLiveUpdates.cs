using LiveCharts.Defaults;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace CartesianCHart.ViewModels
{
    public class MainViewModelLiveUpdates : INotifyPropertyChanged
    {
        private ObservableCollection<double> _values;
        private ObservableCollection<string> _labels;

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        private int _maxPointsToShow = 300;
        private DispatcherTimer _timer;

        public MainViewModelLiveUpdates()
        {
            _values = new ObservableCollection<double>();
            _labels = new ObservableCollection<string>();

            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = _values,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.DodgerBlue, 2),
                    GeometrySize = 3,
                    Name = "Live Data"
                }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = _labels,
                    LabelsRotation = 45,
                    TextSize = 12
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    TextSize = 12
                }
            };

            StartSimulation();
        }

        private void StartSimulation()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                var value = GetRandomValue();
                var timestamp = DateTime.Now.ToString("HH:mm:ss");

                if (_values.Count >= _maxPointsToShow)
                {
                    _values.RemoveAt(0);
                    _labels.RemoveAt(0);
                }

                _values.Add(value);
                _labels.Add(timestamp);
                OnPropertyChanged(nameof(Series));
            };
            _timer.Start();
        }

        private double GetRandomValue()
        {
            Random rand = new Random();
            return rand.NextDouble() * 100;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
