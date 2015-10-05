using cms.Contexts;
using cms.Models;
using cms.Models.Validations;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace cms.ViewModels
{
    public class JobViewModel : ViewModelBase
    {
        private IDialogService dialogService;
        private MainWindowViewModel mainWindowViewModel;
        private CmsContext db;

        public RelayCommand Save { get; set; }
        public RelayCommand GoBack { get; set; }

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

        public Job Job { get; set; }

        public JobViewModel(IDialogService dialogService, MainWindowViewModel mainWindowViewModel, Job job)
        {
            this.dialogService = dialogService;
            this.mainWindowViewModel = mainWindowViewModel;
            mainWindowViewModel.ViewTitle = "Εργασιες";
            this.db = new CmsContext();
            this.People = new ObservableCollection<Person>(db.People.ToList().OrderByDescending(t => t.FullName));
            Save = new RelayCommand(save, null);
            GoBack = new RelayCommand(goBack, null);
            this.Job = job;

            if(this.Job == null)
            {
                this.Job = new Job();
                this.Job.Id = Guid.NewGuid().ToString();
                this.Job.Implemented = DateTime.Now.Date;
                this.Job.Amount = 0;
                db.Jobs.Add(this.Job);
            }
            else
            {
                this.Job = db.Jobs.Find(job.Id);
            }
        }

        private void save(object obj)
        {
            var validator = new JobValidator();
            var results = validator.Validate(this.Job);

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
            mainWindowViewModel.ViewModel = new JobsViewModel(dialogService,mainWindowViewModel);
        }
    }
}
