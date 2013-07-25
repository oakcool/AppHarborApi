namespace OakIdeas.AppHarbor.Api.Models
{
    public class Collaborator : BaseObject
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _collaboratorEmail;
        public string CollaboratorEmail
        {
            get { return _collaboratorEmail; }
            set { _collaboratorEmail = value; }
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
