using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePointPTSample.Office365
{
    public interface IOffice365Service
    {
        Me Me { get; set; }

        Task<bool> Login(string user, string password);

        Task<List<Contact>> GetContacts();

        Task<bool> CreateContact(Contact contact);

        Task<bool> UpdateContact(Contact contact);

        Task<bool> DeleteContact(Contact contact);
    }
}
