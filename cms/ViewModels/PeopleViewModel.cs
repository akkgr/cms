using cms.Contexts;
using cms.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace cms.ViewModels
{
    public class PeopleViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        private IDialogService dialogService;
        private CmsContext db;

        public RelayCommand FindItem { get; set; }
        public RelayCommand NewItem { get; set; }
        public RelayCommand EditItem { get; set; }
        public RelayCommand DeleteItem { get; set; }

        public PeopleViewModel(IDialogService dialogService, MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            mainWindowViewModel.ViewTitle = "Πελατες";
            this.dialogService = dialogService;
            db = new CmsContext();
            FindItem = new RelayCommand(findItem, null);
            NewItem = new RelayCommand(newItem, null);
            EditItem = new RelayCommand(editItem, o => o != null);
            DeleteItem = new RelayCommand(deleteItem, o => o != null);

            getData();
        }

        private ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get { return people; }
            set
            {
                if (Equals(value, people)) return;
                people = value;
                OnPropertyChanged();
            }
        }

        private Person selectedItem;
        public Person SelectedItem 
        {
            get { return selectedItem; }
            set
            {
                if (Equals(value, selectedItem)) return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (Equals(value, searchText)) return;
                searchText = value;
                OnPropertyChanged();
            }
        }

        public int rows;
        public int Rows
        {
            get { return rows; }
            set
            {
                if (Equals(value, rows)) return;
                rows = value;
                OnPropertyChanged();
            }
        }

        public int totalRows;
        public int TotalRows
        {
            get { return totalRows; }
            set
            {
                if (Equals(value, totalRows)) return;
                totalRows = value;
                OnPropertyChanged();
            }
        }
   
        private void findItem(object obj)
        {
            getData(); 
        }

        private void newItem(object obj)
        {
            mainWindowViewModel.ViewModel = new PersonViewModel(dialogService, mainWindowViewModel, null);
        }

        private void editItem(object obj)
        {
            mainWindowViewModel.ViewModel = new PersonViewModel(dialogService, mainWindowViewModel, SelectedItem);
        }

        private async void deleteItem(object obj)
        {
            var message = string.Format("Να διαγραφεί o πελάτης \"{0}\";", SelectedItem.FullName);
            var result = await dialogService.AskQuestionAsync("Διαγραφή", message);
            if (result == MessageDialogResult.Affirmative)
            {
                bool hasError = false;
                string errorMessage = string.Empty;
                try
                {
                    db.People.Remove(selectedItem);
                    db.SaveChanges();
                    People.Remove(selectedItem);
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    hasError = true;
                    errorMessage = ex.Message;
                }

                if(hasError)
                    await dialogService.ShowMessageAsync("Σφάλμα", errorMessage);
            }
        }

        private async void getData()
        {
            TotalRows = await db.People.CountAsync();

            var items = await (from item in db.People
                        select item).ToListAsync();

            if(!string.IsNullOrWhiteSpace(SearchText))
            {
                items = items.Where(t=>t.SearchText.ToUpper().Contains(SearchText.ToUpper())).ToList();
            }

            People = new ObservableCollection<Person>(items.OrderByDescending(t=>t.FullName));
            Rows = People.Count;
        }
    }
}
  