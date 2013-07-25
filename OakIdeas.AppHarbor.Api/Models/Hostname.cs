namespace OakIdeas.AppHarbor.Api.Models
{
    public class Hostname : BaseObject
    {
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

        private string _canonical;
        public string Canonical
        {
            get { return _canonical; }
            set
            {
                if (_canonical != value)
                {
                    _canonical = value;
                    NotifyPropertyChanged("Canonical");
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
