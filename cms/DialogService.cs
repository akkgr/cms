using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace cms
{
    public interface IDialogService
    {
        Task<MessageDialogResult> AskQuestionAsync(string title, string message);
        Task<ProgressDialogController> ShowProgressAsync(string title, string message);
        Task ShowMessageAsync(string title, string message);
    }

    public class DialogService : IDialogService
    {
        private readonly MainWindow metroWindow;
        
        public DialogService(MainWindow metroWindow)
        {
            this.metroWindow = metroWindow;
        }

        public Task<MessageDialogResult> AskQuestionAsync(string title, string message)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            return metroWindow.ShowMessageAsync(title, message, 
                MessageDialogStyle.AffirmativeAndNegative, settings);
        }

        public Task<ProgressDialogController> ShowProgressAsync(string title, string message)
        {
            return metroWindow.ShowProgressAsync(title, message);
        }

        public Task ShowMessageAsync(string title, string message)
        {
            return metroWindow.ShowMessageAsync(title, message);
        }
    }
}