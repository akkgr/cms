using cms.Contexts;
using cms.Models;
using cms.Models.Validations;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace cms.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private CmsContext db;
        private MainWindowViewModel mainWindowViewModel;
        private IDialogService dialogService;

        public RelayCommand Save { get; set; }
        public RelayCommand GoBack { get; set; }
        public RelayCommand NewJob { get; set; }
        public RelayCommand EditJob { get; set; }
        public RelayCommand DeleteJob { get; set; }
        public RelayCommand NewToDo { get; set; }
        public RelayCommand EditToDo { get; set; }
        public RelayCommand DeleteToDo { get; set; }

        public Person Person { get; set; }
        public int JobRows { get { return Person.Jobs.Count; } }
        public decimal JobSum { get { return Person.Jobs.Sum(t=>t.Amount); } }
        public int TodoRows { get { return Person.ToDoes.Count; } }

        private Job selectedJob;
        public Job SelectedJob
        {
            get { return selectedJob; }
            set
            {
                if (Equals(value, selectedJob)) return;
                selectedJob = value;
                OnPropertyChanged();
            }
        }

        private ToDo selectedToDo;
        public ToDo SelectedToDo
        {
            get { return selectedToDo; }
            set
            {
                if (Equals(value, selectedToDo)) return;
                selectedToDo = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel(IDialogService dialogService, MainWindowViewModel mainWindowViewModel, Person person)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.dialogService = dialogService;
            this.Person = person;
            mainWindowViewModel.ViewTitle = "Πελατες";
            GoBack = new RelayCommand(goBack, null);
            Save = new RelayCommand(save, null);
            NewJob = new RelayCommand(newJob, null);
            EditJob = new RelayCommand(editJob, o=>o!=null);
            DeleteJob = new RelayCommand(deleteJob, o => o != null);
            NewToDo = new RelayCommand(newToDo, null);
            EditToDo = new RelayCommand(editToDo, o => o != null);
            DeleteToDo = new RelayCommand(deleteToDo, o => o != null);

            this.db = new CmsContext();

            if (this.Person == null)
            {
                this.Person = new Person();
                Person.Id = Guid.NewGuid().ToString();
                db.People.Add(Person);
            }
            else
            {
                Person = db.People.Find(person.Id);
            }
        }

        private void save(object obj)
        {
            var validator = new PersonValidator();
            var results = validator.Validate(this.Person);

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
            mainWindowViewModel.ViewModel = new PeopleViewModel(dialogService, mainWindowViewModel);
        }

        private void newJob(object obj)
        {

        }

        private void editJob(object obj)
        {

        }

        private void deleteJob(object obj)
        {

        }

        private void newToDo(object obj)
        {

        }

        private void editToDo(object obj)
        {

        }

        private void deleteToDo(object obj)
        {

        }
    }
}
