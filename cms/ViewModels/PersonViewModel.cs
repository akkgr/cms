using cms.Contexts;
using cms.Models;
using cms.Models.Validations;
using System;
using System.Collections.Generic;
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
        public ObservableCollection<Job> Jobs { get { return new ObservableCollection<Job>(Person.Jobs); } }
        public ObservableCollection<ToDo> ToDoes { get { return new ObservableCollection<ToDo>(Person.ToDoes); } }
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
            this.mainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
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

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsSettingsFlyoutOpen"))
            {
                OnPropertyChanged(() => Jobs);
                OnPropertyChanged(() => ToDoes);
            }
        }

        private void save(object obj)
        {
            var validator = new PersonValidator();
            var results = validator.Validate(this.Person);

            var jobErrors = Person.Jobs.Where(t=>!string.IsNullOrEmpty(t.Error)).Select(t => t.Error);
            var toDoErrors = Person.ToDoes.Where(t => !string.IsNullOrEmpty(t.Error)).Select(t => t.Error);

            if (results.IsValid && jobErrors.Count() == 0 && toDoErrors.Count() == 0)
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
                message += string.Join(Environment.NewLine, jobErrors);
                message += string.Join(Environment.NewLine, toDoErrors);
                dialogService.ShowMessageAsync("Σφάλμα", message);
            }
        }

        private void goBack(object obj)
        {
            mainWindowViewModel.ViewModel = new PeopleViewModel(dialogService, mainWindowViewModel);
        }

        private void newJob(object obj)
        {
            var job = new Job();
            job.Id = Guid.NewGuid().ToString();
            job.PersonId = Person.Id;
            job.Implemented = DateTime.Now.Date;
            job.Amount = 0;

            Person.Jobs.Add(job);

            SelectedJob = job;

            mainWindowViewModel.SettingsViewModel = new JobFlyOutViewModel(SelectedJob);
            mainWindowViewModel.IsSettingsFlyoutOpen = true;
        }

        private void editJob(object obj)
        {
            mainWindowViewModel.SettingsViewModel = new JobFlyOutViewModel(SelectedJob);
            mainWindowViewModel.IsSettingsFlyoutOpen = true;
        }

        private void deleteJob(object obj)
        {
            db.Jobs.Remove(SelectedJob);
            OnPropertyChanged("Jobs");
        }

        private void newToDo(object obj)
        {
            var toDo = new ToDo();
            toDo.Id = Guid.NewGuid().ToString();
            toDo.PersonId = Person.Id;
            toDo.ToDoDate = DateTime.Now.Date;
            toDo.Done = false;

            Person.ToDoes.Add(toDo);

            SelectedToDo = toDo;

            mainWindowViewModel.SettingsViewModel = new ToDoFlyOutViewModel(SelectedToDo);
            mainWindowViewModel.IsSettingsFlyoutOpen = true;
        }

        private void editToDo(object obj)
        {
            mainWindowViewModel.SettingsViewModel = new ToDoFlyOutViewModel(SelectedToDo);
            mainWindowViewModel.IsSettingsFlyoutOpen = true;
        }

        private void deleteToDo(object obj)
        {
            db.ToDoes.Remove(SelectedToDo);
            OnPropertyChanged("ToDoes");
        }
    }
}
