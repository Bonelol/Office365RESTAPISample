using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharePointPTSample.Office365
{
    public class Office365Service : IOffice365Service
    {
        private const string _getMe = "https://outlook.office365.com/ews/odata/Me/";
        private const string _getContactsUrl = "https://outlook.office365.com/ews/odata/Me/Contacts?$select=DisplayName,EmailAddress1";
        private IEnumerable<Contact> _contacts;
        private string _user;
        private string _password;

        public Me Me { get; set; }

        public async Task<bool> Login(string user, string password)
        {
            _user = user;
            _password = password;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_getMe));

            // Add the Authorization header with the basic login credentials.
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user + ":" + password));
            request.Headers.Add("Authorization", auth);
           
            // Send the registration request.
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Me = JsonConvert.DeserializeObject<Me>(await response.Content.ReadAsStringAsync());
                return true;
            }
            return false;
        }

        public async Task<List<Contact>> GetContacts()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_getContactsUrl));

            // Add the Authorization header with the basic login credentials.
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_user + ":" + _password));
            request.Headers.Add("Authorization", auth);

            // Send the registration request.
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var contactResponse = JsonConvert.DeserializeObject<ContactResponse>(await response.Content.ReadAsStringAsync());
                return contactResponse.Contacts;
            }

            return null;
        }

        public async Task<bool> CreateContact(Contact contact)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://outlook.office365.com/ews/odata/Me/Contacts"));

            // Add the Authorization header with the basic login credentials.
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_user + ":" + _password));
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", auth);
            var createResponse = new JObject();
            createResponse["@odata.type"] = "#Microsoft.Exchange.Services.OData.Model.Contact";
            createResponse["DisplayName"] = contact.Name;
            createResponse["EmailAddress1"] = contact.Email;

            // this field is required!!!
            createResponse["GivenName"] = contact.Name;
            request.Content = new StringContent(JsonConvert.SerializeObject(createResponse));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            var url = string.Format("https://outlook.office365.com/ews/odata/Me/Contacts('{0}')", contact.Id);
            var client = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), new Uri(url));

            // Add the Authorization header with the basic login credentials.
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_user + ":" + _password));
            request.Headers.Add("Authorization", auth);
            var updateRequest = new JObject();
            updateRequest["DisplayName"] = contact.Name;
            updateRequest["EmailAddress1"] = contact.Email;
            request.Content = new StringContent(JsonConvert.SerializeObject(updateRequest), Encoding.UTF8, "application/json");

            // Send the registration request.
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteContact(Contact contact)
        {
            var url = string.Format("https://outlook.office365.com/ews/odata/Me/Contacts('{0}')", contact.Id);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));

            // Add the Authorization header with the basic login credentials.
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_user + ":" + _password));
            request.Headers.Add("Authorization", auth);

            // Send the registration request.
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
