using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using cms.Views;

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

            this.DataContext = new PeopleView();
            this.Title = "Πελατες";
        }

        private void ViewPeople(object sender, RoutedEventArgs e)
        {
            this.DataContext = new PeopleView();
            this.Title = "Πελατες";
        }

        private void ViewJobs(object sender, RoutedEventArgs e)
        {
            this.DataContext = new JobsView();
            this.Title = "Εργασιες";
        }

        private void ViewCalendar(object sender, RoutedEventArgs e)
        {
            this.DataContext = new PeopleView();
            this.Title = "Εκκρεμοτητες";
        }
    }
}
