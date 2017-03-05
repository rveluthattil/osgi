using System;
using System.Windows;

namespace ChartBuilder
{
    public partial class App : Application
    {
#if SILVERLIGHT
        public App()
        {
            Startup += Application_Startup;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RootVisual = new Page();
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            (RootVisual as Page).ReportException(e.ExceptionObject);
        }
#else
        public App()
        {
            StartupUri = new Uri("Window1.xaml", UriKind.Relative);
        }
#endif
    }
}