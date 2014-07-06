using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using SharePointPTSample.Office365;

namespace SharePointPTSample.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        private readonly IOffice365Service _office365Service;

        public ContactsViewModel(IOffice365Service office365Service)
        {
            _office365Service = office365Service;
        }
        
        public async void LoadAsync()
        {
            var items = await _office365Service.GetContacts();
            Contacts = new ObservableCollection<Contact>(items);
            RaisePropertyChanged(()=>Contacts);
        }

        public ObservableCollection<Contact> Contacts { get; set; }
    }
}
