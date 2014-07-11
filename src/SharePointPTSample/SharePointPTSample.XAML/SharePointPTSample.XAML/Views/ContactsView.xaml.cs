using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharePointPTSample.Office365;

namespace SharePointPTSample.Views
{
    public partial class ContactsView
    {
        public ContactsView()
        {
            InitializeComponent();
        }

        // I will use it until add database
        public static ObservableCollection<Contact> Contacts { get; set; }
    }
}
