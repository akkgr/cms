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
using cms.Contexts;
using cms.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace cms.Views
{
    /// <summary>
    /// Interaction logic for PeopleView.xaml
    /// </summary>
    public partial class PeopleView : UserControl
    {
        private CmsContext db;

        public PeopleView()
        {
            InitializeComponent();

            db = new CmsContext();

            var itemSourceList = new CollectionViewSource() { Source = db.People.ToList() };            
            ICollectionView Itemlist = itemSourceList.View;
            this.dataGrid.ItemsSource = Itemlist;
        }

        private void find(object sender, RoutedEventArgs e)
        {
            
            var yourCostumFilter = new Predicate<object>(ComplexFilter);

            ICollectionView itemlist = this.dataGrid.ItemsSource as ICollectionView;
            
            itemlist.Filter = yourCostumFilter;
        }

        private bool ComplexFilter(object obj)
        {
            string search = this.SearchText.Text;
            var person = obj as Person;

            if (person.Lastname.Contains(search))
                return true;

            if (person.Firstname.Contains(search))
                return true;

            return false;
        }
    }
}
