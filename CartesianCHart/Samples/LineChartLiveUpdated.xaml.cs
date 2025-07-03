using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CartesianCHart.Samples
{
    public partial class LineChartLiveUpdated : Page
    {
        public List<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        private DispatcherTimer _timer;
        private List<List<double>> _datasets;
        private int _maxPointsPerSeries = 300000;
        private int _pointsPerSecond;
        private int _remainingSeconds;
        private Random _rand = new Random();

        public LineChartLiveUpdated()
        {
            InitializeComponent();

            Series = new List<ISeries>();
            XAxes = new Axis[]
            {
                new Axis { Name = "Datapoints on Timespan", TextSize = 12 }
            };
            YAxes = new Axis[]
            {
                new Axis { Name = "Value Series", TextSize = 12 }
            };

            DataContext = this;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PointsPerSecondBox.Text, out _pointsPerSecond) ||
                !int.TryParse(DurationBox.Text, out int durationMinutes) ||
                !int.TryParse(DatasetCountBox.Text, out int datasetCount))
            {
                MessageBox.Show("Enter valid numeric values.");
                return;
            }

            _remainingSeconds = durationMinutes * 60;
            _datasets = new List<List<double>>();
            Series.Clear();

            for (int i = 0; i < datasetCount; i++)
            {
                var dataset = new List<double>();
                _datasets.Add(dataset);

                var color = new SKColor(
                    (byte)_rand.Next(50, 256),
                    (byte)_rand.Next(50, 256),
                    (byte)_rand.Next(50, 256));

                var lineSeries = new LineSeries<double>
                {
                    Values = dataset,
                    Name = $"Dataset {i + 1}",
                    Stroke = new SolidColorPaint(color, 1.5f),
                    Fill = null,
                    GeometrySize = 1,
                    LineSmoothness = 0
                };

                Series.Add(lineSeries);
            }

            Chart.Series = Series;
            ChartContainer.Visibility = Visibility.Visible;

            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_remainingSeconds <= 0)
            {
                _timer.Stop();
                return;
            }

            for (int i = 0; i < _datasets.Count; i++)
            {
                var list = _datasets[i];

                for (int j = 0; j < _pointsPerSecond; j++)
                {
                    list.Add(_rand.NextDouble() * 100);
                }

                if (list.Count > _maxPointsPerSeries)
                {
                    list.RemoveRange(0, list.Count - _maxPointsPerSeries);
                }
            }

            _remainingSeconds--;
            Chart.Series = null;
            Chart.Series = Series;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop timer
            _timer.Stop();

            // Clear series
            Series.Clear();
            Chart.Series = null;

            // Reset input fields
            PointsPerSecondBox.Text = "";
            DurationBox.Text = "";
            DatasetCountBox.Text = "";

            // Hide chart
            ChartContainer.Visibility = Visibility.Collapsed;
        }
    }
}
