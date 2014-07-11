using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SharePointPTSample.Office365;
using SharePointPTSample.Views;

namespace SharePointPTSample.ViewModels
{
    public class EditContactViewModel : ViewModelBase
    {
        private readonly IOffice365Service _office365Service;
        private Contact _contact;

        public EditContactViewModel(IOffice365Service office365Service)
        {
            _office365Service = office365Service;
        }

        public async Task<bool> DeleteContact()
        {
            var result = await _office365Service.DeleteContact(Contact);
            if (result)
            {
                ContactsView.Contacts.Remove(Contact);
            }
            return result;
        }

        public async Task<bool> SaveContact()
        {
            if (string.IsNullOrEmpty(_contact.EditLink))
            {
                var createdResult = await _office365Service.CreateContact(Contact);
                if (createdResult)
                {
                    ContactsView.Contacts.Add(Contact);
                }

                return createdResult;
            }
            var index = ContactsView.Contacts.IndexOf(Contact);
            var updatedResult = await _office365Service.UpdateContact(Contact);

            if (updatedResult)
            {
                ContactsView.Contacts[index] = Contact;
            }

            return updatedResult;
        }

        public Contact Contact
        {
            get { return _contact; }
            set
            {
                if (Set(() => Contact, ref _contact, value))
                {
                    RaisePropertyChanged(() => Contact);
                }
            }
        }
    }
}
