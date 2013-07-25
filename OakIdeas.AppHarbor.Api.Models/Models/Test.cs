using System.Collections.Generic;

namespace OakIdeas.AppHarbor.Api.Models
{
    public class Test : BaseObject
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("Id");
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

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }

        private string _kind;
        public string Kind
        {
            get { return _kind; }
            set
            {
                if (_kind != value)
                {
                    _kind = value;
                    NotifyPropertyChanged("Kind");
                }
            }
        }

        private string _duration;
        public string Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    NotifyPropertyChanged("Duration");
                }
            }
        }

        private List<Test> _tests; 
        public List<Test> Tests
        {
            get { return _tests ?? (_tests = new List<Test>()); }
            set
            {
                if (_tests != value)
                {
                    _tests = value;
                    NotifyPropertyChanged("Tests");
                }
            }
        }
    }
}
