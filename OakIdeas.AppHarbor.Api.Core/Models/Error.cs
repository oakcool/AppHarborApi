using System;
using Newtonsoft.Json;

namespace OakIdeas.AppHarbor.Api.Core.Models
{
    public class Error : BaseObject
    {
        private string _commitId;
        [JsonProperty("commit_id")]
        public string CommitId
        {
            get { return _commitId; }
            set
            {
                if (_commitId != value)
                {
                    _commitId = value;
                    NotifyPropertyChanged("CommitId");
                }
            }
        }

        private DateTime? _date ;
        public DateTime? Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        private string _requestPath;
        [JsonProperty("request_path")]
        public string RequestPath
        {
            get { return _requestPath; }
            set
            {
                if (_requestPath != value)
                {
                    _requestPath = value;
                    NotifyPropertyChanged("RequestPath");
                }
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }

        private AppHarborException _exception;
        public AppHarborException Exception
        {
            get { return _exception; }
            set
            {
                if (_exception != value)
                {
                    _exception = value;
                    NotifyPropertyChanged("Exception");
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
