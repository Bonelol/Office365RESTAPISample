using SharePointPTSample.ViewModels;
using SharePointPTSample.Views;
using Xamarin.Forms;

namespace SharePointPTSample.XAML
{
    public class App
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

        public static Page GetMainPage()
        {
            return new NavigationPage(new LoginView());
        }
    }
}
