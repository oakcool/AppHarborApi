namespace OakIdeas.AppHarbor.Api.Models
{
    public class Application : BaseObject
    {
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

        private string _slug;
        public string Slug
        {
            get { return _slug; }
            set
            {
                if (_slug != value)
                {
                    _slug = value;
                    NotifyPropertyChanged("Slug");
                }
            }
        }

        private string _regionidentifier;
        public string RegionIdentifier
        {
            get { return _regionidentifier; }
            set
            {
                if (_regionidentifier != value)
                {
                    _regionidentifier = value;

                    NotifyPropertyChanged("RegionIdentifier");
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
