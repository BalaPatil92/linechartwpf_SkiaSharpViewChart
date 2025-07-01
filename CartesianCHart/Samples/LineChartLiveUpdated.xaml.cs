using CartesianCHart.ViewModels;
using System.Windows.Controls;

namespace CartesianCHart.Samples
{
    /// <summary>
    /// Interaction logic for LineChartLiveUpdated.xaml
    /// </summary>
    public partial class LineChartLiveUpdated : Page
    {
        public LineChartLiveUpdated()
        {
            InitializeComponent();
            DataContext = new MainViewModelLiveUpdates();
        }
    } 


}
