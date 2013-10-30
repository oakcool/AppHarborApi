using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace OakIdeas.AppHarbor.Api.Core.Models
{
    public class AppHarborException : BaseObject
    {
        private string _stackTrace;
        [JsonProperty("stack_trace")]
        public string StackTrace
        {
            get { return _stackTrace; }
            set
            {
                if (_stackTrace != value)
                {
                    _stackTrace = value;
                    NotifyPropertyChanged("StackTrace");
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

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private string _innerException;
        [JsonProperty("inner_exception")]
        public string InnerException
        {
            get { return _innerException; }
            set
            {
                if (_innerException != value)
                {
                    _innerException = value;
                    NotifyPropertyChanged("InnerException");
                }
            }
        }
    }
}
