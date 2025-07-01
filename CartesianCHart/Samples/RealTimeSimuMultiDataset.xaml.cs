using CartesianCHart.ViewModels;
using System.Windows.Controls;

namespace CartesianCHart.Samples
{
    /// <summary>
    /// Interaction logic for LineChartLiveUpdated.xaml
    /// </summary>
    public partial class RealTimeSimuMultiDataset : Page
    {
        public RealTimeSimuMultiDataset()
        {
            InitializeComponent();
            DataContext = new RealTImeSimulationMultiDataset();
        }
    } 


}
