using SharePointPTSample.Office365;
using SharePointPTSample.ViewModels;
using Xamarin.Forms;

namespace SharePointPTSample.Views
{
    public class EditContactView : ContentPage
    {
        private EditContactViewModel _viewModel;

        public EditContactView(Contact contact)
        {
            _viewModel = App.Locator.EditContactViewModel;
            _viewModel.Contact = contact ?? new Contact();
            BindingContext = _viewModel;

            Title = "Edit Contact";
            if (contact == null)
            {
                Title = "Create contact";
            }
            NavigationPage.SetHasNavigationBar(this, true);
            Label title = null;
            if (Device.OS == TargetPlatform.WinPhone)
            {
                title = new Label
                {
                    XAlign = TextAlignment.Center,
                    Text = Title,
                    Font = Font.SystemFontOfSize(42)
                };
            }

            var nameLabel = new Label { Text = "Name" };
            var nameEntry = new Entry();

            nameEntry.SetBinding(Entry.TextProperty, "Contact.Name");

            var emailLabel = new Label { Text = "Email" };
            var emailEntry = new Entry();
            emailEntry.SetBinding(Entry.TextProperty, "Contact.Email");


            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += saveButton_Clicked;

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += deleteButton_Clicked;

            if (contact == null)
            {
                deleteButton.IsEnabled = false;
            }

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += cancelButton_Clicked;

            var stackPanel = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children = {
					nameLabel, nameEntry,
                    emailLabel, emailEntry,
					saveButton, 
                    deleteButton, 
                    cancelButton
				}
            };

            if (Device.OS == TargetPlatform.WinPhone)
            {
                stackPanel.Children.Insert(0, title);
            }
            Content = stackPanel;
        }

        private async void deleteButton_Clicked(object sender, System.EventArgs e)
        {
            var result = await _viewModel.DeleteContact();
            if (result)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("SharePoint Sample", "Wasn´t possible to delete the contact", "Ok", null);
            }
        }

        private async void saveButton_Clicked(object sender, System.EventArgs e)
        {
            var result = await _viewModel.SaveContact();
            if (result)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("SharePoint Sample", "Wasn´t possible to save the contact", "Ok", null);
            }
        }

        private async void cancelButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
