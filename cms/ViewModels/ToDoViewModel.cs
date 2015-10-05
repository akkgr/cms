using cms.Contexts;
using cms.Models;
using cms.Models.Validations;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace cms.ViewModels
{
    public class ToDoViewModel : ViewModelBase
    {
        private IDialogService dialogService;
        private MainWindowViewModel mainWindowViewModel;
        private CmsContext db;

        public RelayCommand Save { get; set; }
        public RelayCommand GoBack { get; set; }
        public ToDo ToDo { get; set; }

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

        public ToDoViewModel(IDialogService dialogService, MainWindowViewModel mainWindowViewModel, ToDo toDo)
        {
            this.dialogService = dialogService;
            this.mainWindowViewModel = mainWindowViewModel;
            mainWindowViewModel.ViewTitle = "Εκκρεμοτητες";
            this.db = new CmsContext();
            this.People = new ObservableCollection<Person>(db.People.ToList().OrderByDescending(t => t.FullName));
            Save = new RelayCommand(save, null);
            GoBack = new RelayCommand(goBack, null);
            this.ToDo = toDo;

            if (this.ToDo == null)
            {
                this.ToDo = new ToDo();
                this.ToDo.Id = Guid.NewGuid().ToString();
                this.ToDo.ToDoDate = DateTime.Now.Date;
                this.ToDo.Done = false;
                db.Todoes.Add(this.ToDo);
            }
            else
            {
                this.ToDo = db.Todoes.Find(toDo.Id);
            }
        }

        private void save(object obj)
        {
            var validator = new ToDoValidator();
            var results = validator.Validate(this.ToDo);

            if (results.IsValid)
            {
                try
                {
                    db.SaveChanges();
                    dialogService.ShowMessageAsync("Καταχώρηση", "Οι αλλαγές καταχωρήθηκαν επιτυχώς.");
                }
                catch (Exception ex)
                {
                    dialogService.ShowMessageAsync("Σφάλμα", ex.Message);
                }
            }
            else
            {
                var message = string.Join(Environment.NewLine, results.Errors.Select(t => t.ErrorMessage));
                dialogService.ShowMessageAsync("Σφάλμα", message);
            }
        }

        private void goBack(object obj)
        {
            mainWindowViewModel.ViewModel = new ToDoesViewModel(dialogService, mainWindowViewModel);
        }
    }
}
