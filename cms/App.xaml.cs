using System.Windows;

namespace cms
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var dbi = new Contexts.CmsDBInitializer();
            //dbi.InitializeDatabase(new Contexts.CmsContext());
        }
    }
}
