﻿using System;
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

            this.dataGrid.ItemsSource = db.People.ToList();
        }
    }
}
