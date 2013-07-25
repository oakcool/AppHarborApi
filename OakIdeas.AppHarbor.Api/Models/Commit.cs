namespace OakIdeas.AppHarbor.Api.Models
{
    public class Commit : BaseObject
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
