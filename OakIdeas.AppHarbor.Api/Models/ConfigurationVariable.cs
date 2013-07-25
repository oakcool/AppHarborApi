namespace OakIdeas.AppHarbor.Api.Models
{
    public class ConfigurationVariable : BaseObject
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    NotifyPropertyChanged("Key");
                }
            }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    NotifyPropertyChanged("Url");
                }
            }
        }
    }
}
