using System.Windows.Controls;
using CartesianCHart.ViewModels;

namespace CartesianCHart.Samples
{
    /// <summary>
    /// Interaction logic for LineChart.xaml
    /// </summary>
    public partial class LineChart : Page
    {
        public LineChart()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
