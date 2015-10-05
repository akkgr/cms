
namespace cms.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDialogService dialogService;

        private ViewModelBase viewModel;
        public ViewModelBase ViewModel
        {
            get
            {
                return viewModel;
            }

            set
            {
                if (Equals(viewModel, value)) return;
                viewModel = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase settingsViewModel;
        public ViewModelBase SettingsViewModel
        {
            get
            {
                return settingsViewModel;
            }

            set
            {
                if (Equals(settingsViewModel, value)) return;
                settingsViewModel = value;
                OnPropertyChanged();
            }
        }

        private string viewTitle;
        public string ViewTitle
        {
            get
            {
                return viewTitle;
            }

            set
            {
                if (Equals(viewTitle, value)) return;
                viewTitle = value;
                OnPropertyChanged();
            }
        }

        private bool isSettingsFlyoutOpen;
        public bool IsSettingsFlyoutOpen
        {
            get { return isSettingsFlyoutOpen; }
            set
            {
                if (value.Equals(isSettingsFlyoutOpen)) return;
                isSettingsFlyoutOpen = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenView { get; set; }

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            OpenView = new RelayCommand(openView, null);
            openView("People");
        }

        private void openView(object obj)
        {
            switch((string)obj)
            {
                case "People":
                    ViewModel = new PeopleViewModel(dialogService,this);
                    break;

                case "Jobs":
                    ViewModel = new JobsViewModel(dialogService,this);
                    break;

                case "ToDoes":
                    ViewModel = new ToDoesViewModel(dialogService,this);
                    break;
            }
        }
    }
}
