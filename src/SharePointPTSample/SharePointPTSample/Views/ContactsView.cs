using System;
using System.Threading.Tasks;
using SharePointPTSample.Office365;
using SharePointPTSample.ViewModels;
using SharePointPTSample.Views.Cells;
using Xamarin.Forms;

namespace SharePointPTSample.Views
{
    public class ContactsView : ContentPage
    {
        private readonly ContactsViewModel _viewModel;
        private readonly ListView _listView;
        private bool _iscreating;
        public ContactsView()
        {
            _viewModel = App.Locator.ContactsViewModel;
            BindingContext = _viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
            _viewModel.LoadAsync();
            _iscreating = true;
            Title = "Contacts";
            Label title = null;
            if (Device.OS == TargetPlatform.WinPhone)
            {
                title = new Label
                {
                    Text = Title,
                    XAlign = TextAlignment.Center,
                    Font = Font.SystemFontOfSize(42)
                };
            }
            NavigationPage.SetHasNavigationBar(this, true);

            _listView = new ListView
            {
                RowHeight = 40,
                ItemTemplate = new DataTemplate(typeof (ContactItemCell))
            };
            _listView.SetBinding(ListView.ItemsSourceProperty, "Contacts");
            _listView.ItemSelected += async (sender, e) => await EditContact((Contact)e.SelectedItem);

            // the root control
            var stackPanel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    _listView
                }
            };

            // add title for WP
            if (Device.OS == TargetPlatform.WinPhone)
            {
                stackPanel.Children.Insert(0, title);
            }

            Content = stackPanel;

            var tbi = new ToolbarItem("-", null, () => EditContact(null), 0, 0);

            if (Device.OS == TargetPlatform.Android)
            {
                // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () => EditContact(null), 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                // the image must be set as content and copy always
                tbi = new ToolbarItem("add", "/Toolkit.Content/ApplicationBar.Add.png", () => EditContact(null), 0, 0);
            }
            ToolbarItems.Add(tbi);
        }

        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Contacts")
            {
                _listView.ItemsSource = _viewModel.Contacts;
            }
        }
        
        private async Task EditContact(Contact contact)
        {
            var editContactView = new EditContactView(contact);
            await Navigation.PushAsync(editContactView);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_iscreating)
            {
                //in this case i called the LoadAsync in constructor
                _iscreating = false;
            }
            else
            {
                _viewModel.LoadAsync();
            }
        }
    }
}
