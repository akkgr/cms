using cms.Contexts;
using cms.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace cms.ViewModels
{
    public class ToDoesViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        private IDialogService dialogService;
        private CmsContext db;

        public RelayCommand FindItem { get; set; }
        public RelayCommand NewItem { get; set; }
        public RelayCommand EditItem { get; set; }
        public RelayCommand DeleteItem { get; set; }

        public ToDoesViewModel(IDialogService dialogService, MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            mainWindowViewModel.ViewTitle = "Υπενθυμισεις";
            this.dialogService = dialogService;
            db = new CmsContext();

            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = StartDate.AddMonths(1);
            
            FindItem = new RelayCommand(findItem, null);
            NewItem = new RelayCommand(newItem, null);
            EditItem = new RelayCommand(editItem, o => o != null);
            DeleteItem = new RelayCommand(deleteItem, o => o != null);

            getData();
        }

        private ObservableCollection<ToDo> toDoes;
        public ObservableCollection<ToDo> ToDoes
        {
            get { return toDoes; }
            set
            {
                if (Equals(value, toDoes)) return;
                toDoes = value;
                OnPropertyChanged();
            }
        }

        private ToDo selectedItem;
        public ToDo SelectedItem 
        {
            get { return selectedItem; }
            set
            {
                if (Equals(value, selectedItem)) return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (Equals(value, startDate)) return;
                startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (Equals(value, endDate)) return;
                endDate = value;
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

        public decimal sum;
        public decimal Sum
        {
            get { return sum; }
            set
            {
                if (Equals(value, sum)) return;
                sum = value;
                OnPropertyChanged();
            }
        }

        private void findItem(object obj)
        {
            getData();
        }

        private void newItem(object obj)
        {
            mainWindowViewModel.ViewModel = new ToDoViewModel(dialogService, mainWindowViewModel, null);
        }

        private void editItem(object obj)
        {
            mainWindowViewModel.ViewModel = new ToDoViewModel(dialogService, mainWindowViewModel, SelectedItem);
        }

        private async void deleteItem(object obj)
        {
            var message = string.Format("Να διαγραφεί η εργασία \"{0}\";", SelectedItem.Description);
            var result = await dialogService.AskQuestionAsync("Διαγραφή", message);
            if (result == MessageDialogResult.Affirmative)
            {
                bool hasError = false;
                string errorMessage = string.Empty;
                try
                {
                    db.ToDoes.Remove(selectedItem);
                    db.SaveChanges();
                    ToDoes.Remove(selectedItem);
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
            TotalRows = await db.Jobs.CountAsync();

            var items = await (from item in db.ToDoes
                        where item.ToDoDate >= StartDate && item.ToDoDate <= EndDate
                        select item).ToListAsync();

            if(!string.IsNullOrWhiteSpace(SearchText))
            {
                items = items.Where(t=>t.SearchText.ToUpper().Contains(SearchText.ToUpper())).ToList();
            }

            ToDoes = new ObservableCollection<ToDo>(items.OrderByDescending(t => t.ToDoDate));
            Rows = ToDoes.Count;
        }
    }
}
  