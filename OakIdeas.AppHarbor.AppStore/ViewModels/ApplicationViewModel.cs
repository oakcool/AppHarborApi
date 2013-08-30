using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using OakIdeas.AppHarbor.Api;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.AppStore.Common;

namespace OakIdeas.AppHarbor.AppStore.ViewModels
{
    public class ApplicationViewModel : ViewModel
    {
        private Application _application;
        public Application Application
        {
            get { return _application; }
            set
            {
                if (_application != value)
                {
                    _application = value;
                    
                }
            }
        }

        public ObservableCollection<Build> Builds { get; set; }
        public ObservableCollection<Error> Errors { get; set; }
        public ObservableCollection<ConfigurationVariable> ConfigurationVariables { get; set; }
        public ObservableCollection<Collaborator> Collaborators { get; set; }
        public ObservableCollection<Hostname> Hostnames { get; set; }
        public ObservableCollection<ServiceHook> ServiceHooks { get; set; }

        public int BuildCount
        {
            get { return Builds.Count; }
        }
        public int ErrorCount
        {
            get { return Errors.Count; }
        }
        public int ConfigurationVariablesCount
        {
            get { return ConfigurationVariables.Count; }
        }
        public int CollaboratorsCount
        {
            get { return Collaborators.Count; }
        }
        public int HostnamesCount
        {
            get { return Hostnames.Count; }
        }
        public int ServiceHooksCount
        {
            get { return ServiceHooks.Count; }
        }

        public string Name
        {
            get { return Application.Name; }
        }

        public ApplicationViewModel(Application application)
        {
            Application = application;
            Builds = new ObservableCollection<Build>();
            Errors = new ObservableCollection<Error>();
            ConfigurationVariables = new ObservableCollection<ConfigurationVariable>();
            Collaborators = new ObservableCollection<Collaborator>();
            Hostnames = new ObservableCollection<Hostname>();
            ServiceHooks = new ObservableCollection<ServiceHook>();
            LoadData();
        }

        public async void LoadData()
        {
            if (String.IsNullOrEmpty(AuthenticationService.Instance.AccessToken))
            {
                await AuthenticationService.Instance.CheckAndGetAccessToken();
            }
            
            List<Build> tempBuilds = await AppHarborApi.Instance.GetBuilds(AuthenticationService.Instance.AccessToken, _application.Slug);

            foreach (Build tempBuild in tempBuilds)
            {
                Builds.Add(tempBuild);
            }

            List<Error> tempErrors = await AppHarborApi.Instance.GetErrors(AuthenticationService.Instance.AccessToken, _application.Slug);

            foreach (Error tempError in tempErrors)
            {
                Errors.Add(tempError);
            }

            List<ConfigurationVariable> tempAppHarborConfigurationVariables = await AppHarborApi.Instance.GetConfigurationVariables(AuthenticationService.Instance.AccessToken, _application.Slug);

            foreach (ConfigurationVariable tempAppHarborConfigurationVariable in tempAppHarborConfigurationVariables)
            {
                ConfigurationVariables.Add(tempAppHarborConfigurationVariable);
            }

            List<Collaborator> tempCollaborators = await AppHarborApi.Instance.GetCollaborators(AuthenticationService.Instance.AccessToken, _application.Slug);

            foreach (Collaborator tempCollaborator in tempCollaborators)
            {
                Collaborators.Add(tempCollaborator);
            }

            List<Hostname> tempHostnames = await AppHarborApi.Instance.GetHostnames(AuthenticationService.Instance.AccessToken, _application.Slug);

            foreach (Hostname tempHostname in tempHostnames)
            {
                Hostnames.Add(tempHostname);
            }

            List<ServiceHook> tempServiceHooks = await AppHarborApi.Instance.GetServiceHooks(AuthenticationService.Instance.AccessToken, _application.Slug);

            foreach (ServiceHook tempServiceHook in tempServiceHooks)
            {
                ServiceHooks.Add(tempServiceHook);
            }
        }

    }
}
