using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CartesianCHart.Samples;
using CartesianCHart.ViewModels;

namespace CartesianCHart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LineChart()); // Load default page
        }

        private void ChartView_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LineChart());
        }

        private void OtherPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LineChartLiveUpdated());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RealTimeSimuMultiDataset());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LineChartCheckboxes());
        }
    }
}
