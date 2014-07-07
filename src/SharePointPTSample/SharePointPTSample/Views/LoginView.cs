using System;
using SharePointPTSample.ViewModels;
using Xamarin.Forms;

namespace SharePointPTSample.Views
{
    public class LoginView : ContentPage
    {
        private LoginViewModel _viewModel;

        public LoginView()
        {
            _viewModel = App.Locator.LoginViewModel;
           BindingContext = _viewModel;

            Title = "Login";
            Label title = null;
            if (Device.OS == TargetPlatform.WinPhone)
            {
                title = new Label
                {
                    Text = "Login",
                    XAlign = TextAlignment.Center,
                    Font = Font.SystemFontOfSize(42)
                };
            }
            NavigationPage.SetHasNavigationBar(this, true);

            var userLabel = new Label { Text = "User Name: " };
            var userEntry = new Entry();
            userEntry.SetBinding(Entry.TextProperty, new Binding("UserName", BindingMode.TwoWay));


            var passwordLabel = new Label {Text = "Password: "};
            var passWordEntry = new Entry { IsPassword = true };
            passWordEntry.SetBinding(Entry.TextProperty, new Binding("Password", BindingMode.TwoWay));
            

            var loginButton = new Button {Text = "Login"};
            loginButton.Clicked += loginButton_Clicked;
            // the root control
            var stackPanel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    userLabel,
                    userEntry,
                    passwordLabel,
                    passWordEntry,
                    loginButton
                }
            };

            // add title for WP
            if (Device.OS == TargetPlatform.WinPhone)
            {
                stackPanel.Children.Insert(0, title);
            }

            Content = stackPanel;
        }

        private async void loginButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var result = await _viewModel.DoLogin();

                if (result)
                {
                    await Navigation.PushAsync(new ContactsView());
                }
                else
                {
                    await DisplayAlert("SharePoint Sample", "The login fails.", "Ok", null);
                }
            }
            catch (Exception exception)
            {
                var error = exception.ToString();
            }
        }
    }
}
