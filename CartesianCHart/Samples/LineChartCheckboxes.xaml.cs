using System.Windows.Controls;
using CartesianCHart.ViewModels;

namespace CartesianCHart.Samples
{
    /// <summary>
    /// Interaction logic for LineChart.xaml
    /// </summary>
    public partial class LineChartCheckboxes : Page
    {
        public LineChartCheckboxes()
        {
            InitializeComponent();
            DataContext = new MainViewModelCheck();
        }
    }
}
