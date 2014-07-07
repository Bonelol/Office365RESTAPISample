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
            try
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
                    ItemTemplate = new DataTemplate(typeof(ContactItemCell))
                };
                _listView.SetBinding(ListView.ItemsSourceProperty, "Contacts");
                _listView.ItemSelected += async (sender, e) =>
                {
                    if (_listView.SelectedItem != null)
                    {
                        await EditContact((Contact)e.SelectedItem);
                    }
                    _listView.SelectedItem = null;
                };

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
            }
            catch (Exception exception)
            {
                // todo handle the error
                var error = exception.ToString();
            }
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

        private void CreatToolbarItems()
        {
            var tbi = new ToolbarItem("+", null, async () =>
            {
                await EditContact(new Contact());
            }, 0, 0);


            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", async () =>
                {
                    await EditContact(new Contact());
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                // the image must be set as content and copy always
                tbi = new ToolbarItem("add", "/Toolkit.Content/ApplicationBar.Add.png", async () =>
                {
                    await EditContact(new Contact());
                }, 0, 0);

            }
            ToolbarItems.Add(tbi);
        }

        protected override void OnDisappearing()
        {
            ToolbarItems.Clear();
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
           
            base.OnAppearing();

            try
            {
                if (_iscreating)
                {
                    //in this case i called the LoadAsync in constructor
                    _iscreating = false;
                }
                else
                {
                    _viewModel.LoadAsync();
                }
                CreatToolbarItems();
            }
            catch (Exception exception)
            {   
                // todo handle the error
                var error = exception.ToString();
            }
        }
    }
}
