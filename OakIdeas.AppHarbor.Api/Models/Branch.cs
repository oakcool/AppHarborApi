namespace OakIdeas.AppHarbor.Api.Models
{
    public class Branch
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Commit _commit;
        public Commit Commit
        {
            get
            {
                if (_commit == null)
                {
                    _commit = new Commit();
                }
                return _commit;
            }
            set { _commit = value; }
        }


    }
}
