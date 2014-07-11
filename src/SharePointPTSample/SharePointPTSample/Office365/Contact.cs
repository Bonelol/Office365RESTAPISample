using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace SharePointPTSample.Office365
{
    public class ContactResponse
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }
         [JsonProperty(PropertyName = "value")]
        public List<Contact> Contacts { get; set; }
    }

    public class Contact : ObservableObject
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "EmailAddress1")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "GivenName")]
        public string Name { get; set; }
         [JsonProperty(PropertyName = "@odata.editLink")]
        public string EditLink { get; set; }
    }
}
