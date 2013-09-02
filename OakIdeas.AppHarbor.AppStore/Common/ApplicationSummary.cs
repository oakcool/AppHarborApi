using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.AppStore.Common
{
    public class ApplicationSummary :  Microsoft.Practices.Prism.StoreApps.BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _slug;
        public string Slug 
        {
            get { return _slug; }
            set { SetProperty(ref _slug, value); }
        }
        private int _buildCount;
        public int BuildCount
        {
            get { return _buildCount; }
            set { SetProperty(ref _buildCount, value); }
        }
        private int _errorCount;
        public int ErrorCount
        {
            get { return _errorCount; }
            set { SetProperty(ref _errorCount, value); }
        }
        private int _configurationVariablesCount;
        public int ConfigurationVariablesCount
        {
            get { return _configurationVariablesCount; }
            set { SetProperty(ref _configurationVariablesCount, value); }
        }
        private int _collaboratorsCount;
        public int CollaboratorsCount
        {
            get { return _collaboratorsCount; }
            set { SetProperty(ref _collaboratorsCount, value); }
        }
        private int _hostnamesCount;
        public int HostnamesCount
        {
            get { return _hostnamesCount; }
            set { SetProperty(ref _hostnamesCount, value); }
        }
        private int _serviceHooksCount;
        public int ServiceHooksCount
        {
            get { return _serviceHooksCount; }
            set { SetProperty(ref _serviceHooksCount, value); }
        }
        private string _applicationMessage;
        public string ApplicationMessage
        {
            get { return _applicationMessage; }
            set { SetProperty(ref _applicationMessage, value); }
        }
    }
}
