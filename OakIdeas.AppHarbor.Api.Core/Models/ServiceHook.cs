namespace OakIdeas.AppHarbor.Api.Core.Models
{
    public class ServiceHook : BaseObject
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
