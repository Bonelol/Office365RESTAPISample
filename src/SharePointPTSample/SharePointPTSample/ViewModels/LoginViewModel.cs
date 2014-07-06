using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SharePointPTSample.Office365;

namespace SharePointPTSample.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IOffice365Service _office365Service;
        private string _userName;
        private string _password;

        public LoginViewModel(IOffice365Service office365Service)
        {
            _office365Service = office365Service;
        }
        
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (Set(() => UserName, ref _userName, value))
                {
                    RaisePropertyChanged(() => UserName);
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (Set(() => Password, ref _password, value))
                {
                    RaisePropertyChanged(() => Password);
                }
            }
        }
        
        public async Task<bool> DoLogin()
        {
            return await _office365Service.Login(UserName, Password);
        }
    }
}
