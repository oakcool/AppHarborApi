using System;
using Newtonsoft.Json;

namespace OakIdeas.AppHarbor.Api.Models
{
    public class LogEntry : BaseObject
    {
        private DateTime? _created;
        public DateTime? Created
        {
            get { return _created; }
            set { _created = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string detailsUrl;
        [JsonProperty("details_url")]
        public string DetailsUrl
        {
            get { return detailsUrl; }
            set { detailsUrl = value; }
        }
    }
}
