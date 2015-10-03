using cms.ViewModels;
using MahApps.Metro.Controls;

namespace cms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(new DialogService(this));
        }
    }
}
