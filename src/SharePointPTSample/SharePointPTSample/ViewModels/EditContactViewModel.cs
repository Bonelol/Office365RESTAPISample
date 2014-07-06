using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SharePointPTSample.Office365;

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
            return await _office365Service.DeleteContact(Contact);
        }

        public async Task<bool> SaveContact()
        {
            if (string.IsNullOrEmpty(_contact.EditLink))
            {
                return await _office365Service.CreateContact(Contact);
            }
            return await _office365Service.UpdateContact(Contact);
        }

        public Contact Contact 
        {
            get
            {
                return _contact;
            }
            set
            {
                if (Set(() => Contact, ref _contact, value))
                {
                    RaisePropertyChanged(() => Contact);
                }
            }
        }}
}
