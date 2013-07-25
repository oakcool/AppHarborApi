using System.Collections.Generic;
using Newtonsoft.Json;

namespace OakIdeas.AppHarbor.Api.Models
{
    public class User : BaseObject
    {
        public string Id { get; set; }

        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        private List<string> _emailAddressess;
        [JsonProperty("email_addresses")]
        public List<string> EmailAddresses
        {
            get { return _emailAddressess ?? (_emailAddressess = new List<string>()); }
            set
            {
                if (_emailAddressess != value)
                {
                    _emailAddressess = value;
                    NotifyPropertyChanged("EmailAddresses");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
    }
}
