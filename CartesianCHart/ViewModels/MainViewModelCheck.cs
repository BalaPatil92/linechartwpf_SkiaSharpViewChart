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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CartesianCHart.ViewModels
{
    public class MainViewModelCheck : INotifyPropertyChanged
    {
        private List<List<DataPoint>> _aggregatedDataSets;

        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<DatasetToggle> DatasetToggles { get; set; }

        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public MainViewModelCheck()
        {
            _aggregatedDataSets = new List<List<DataPoint>>();
            Series = new ObservableCollection<ISeries>();
            DatasetToggles = new ObservableCollection<DatasetToggle>();

            for (int i = 0; i < 6; i++)
            {
                var rawData = DataHelper.GenerateRawData();
                var aggregated = DataHelper.AggregatePerMinute(rawData);
                _aggregatedDataSets.Add(aggregated);

                // Approach1 - InMemory Operation
                var lineSeries = new LineSeries<double>
                {
                    Values = aggregated.Select(x => x.Value).ToArray(),
                    Fill = null,
                    Stroke = new SolidColorPaint(GetColorByIndex(i), 2),
                    GeometrySize = 1,
                    Name = $"Dataset {i + 1}",
                    IsVisible = true // initially visible
                };
                Series.Add(lineSeries);

                var toggle = new DatasetToggle
                {
                    Name = $"Dataset {i + 1}",
                    Index = i,
                    IsChecked = true
                };
                toggle.Toggled += OnDatasetToggleChanged;
                DatasetToggles.Add(toggle);
            }

            RefreshSeries();

            XAxes = new Axis[]
            {
            new Axis
            {
                Labeler = value =>
                {
                    int index = (int)value;
                    if (index >= 0 && index < _aggregatedDataSets[0].Count)
                        return _aggregatedDataSets[0][index].Timestamp.ToString("MM-dd HH:mm");
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

        private void OnDatasetToggleChanged(object sender, EventArgs e)
        {
            if (sender is DatasetToggle toggle)
            {
                var series = Series.ElementAtOrDefault(toggle.Index);
                if (series != null)
                {
                    series.IsVisible = toggle.IsChecked;
                }
            }
            // Approach 2: Dataset rebinds
           // RefreshSeries();
        }

        private void RefreshSeries()
        {
            Series.Clear();

            foreach (var toggle in DatasetToggles.Where(t => t.IsChecked))
            {
                var dataset = _aggregatedDataSets[toggle.Index];
                Series.Add(new LineSeries<double>
                {
                    Values = dataset.Select(x => x.Value).ToArray(),
                    Fill = null,
                    Stroke = new SolidColorPaint(GetColorByIndex(toggle.Index), 2),
                    GeometrySize = 1,
                    Name = toggle.Name
                });
            }

            OnPropertyChanged(nameof(Series));
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
