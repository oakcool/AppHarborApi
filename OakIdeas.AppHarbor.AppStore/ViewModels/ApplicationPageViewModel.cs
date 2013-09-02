using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using OakIdeas.AppHarbor.Api;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.AppStore.Common;

namespace OakIdeas.AppHarbor.AppStore.ViewModels
{
    public class ApplicationPageViewModel : ViewModel
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private readonly INavigationService _navigationService;
        private Application _application;
        
        public DelegateCommand GoBackCommand { get; set; }
        public ObservableCollection<Build> Builds { get; set; }
        public ObservableCollection<Error> Errors { get; set; }
        public ObservableCollection<ConfigurationVariable> ConfigurationVariables { get; set; }
        public ObservableCollection<Collaborator> Collaborators { get; set; }
        public ObservableCollection<Hostname> Hostnames { get; set; }
        public ObservableCollection<ServiceHook> ServiceHooks { get; set; }

        private string _slug;
        public string Slug
        {
            get { return _slug; }
            set { SetProperty(ref _slug, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _buildCount;
        public int BuildCount
        {
            get { return _buildCount; }
            set { SetProperty(ref _buildCount, value); }
        }

        private string _buildsMessage;
        public string BuildsMessage
        {
            get { return _buildsMessage; }
            set { SetProperty(ref _buildsMessage, value); }
        }

        private int _errorCount;
        public int ErrorCount
        {
            get { return _errorCount; }
            set { SetProperty(ref _errorCount, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private int _configurationVariablesCount;
        public int ConfigurationVariablesCount
        {
            get { return _configurationVariablesCount; }
            set { SetProperty(ref _configurationVariablesCount, value); }
        }

        private string _configurationVariablesMessage;
        public string ConfigurationVariablesMessage
        {
            get { return _configurationVariablesMessage; }
            set { SetProperty(ref _configurationVariablesMessage, value); }
        }

        private int _collaboratorsCount;
        public int CollaboratorsCount
        {
            get { return _collaboratorsCount; }
            set { SetProperty(ref _collaboratorsCount, value); }
        }

        private string _collaboratorsMessage;
        public string CollaboratorsMessage
        {
            get { return _collaboratorsMessage; }
            set { SetProperty(ref _collaboratorsMessage, value); }
        }

        private int _hostnamesCount;
        public int HostnamesCount
        {
            get { return _hostnamesCount; }
            set { SetProperty(ref _hostnamesCount, value); }
        }

        private string _hostnamesMessage;
        public string HostnamesMessage
        {
            get { return _hostnamesMessage; }
            set { SetProperty(ref _hostnamesMessage, value); }
        }

        private int _serviceHooksCount;
        public int ServiceHooksCount
        {
            get { return _serviceHooksCount; }
            set { SetProperty(ref _serviceHooksCount, value); }
        }

        private string _serviceHooksMessage;
        public string ServiceHooksMessage
        {
            get { return _serviceHooksMessage; }
            set { SetProperty(ref _serviceHooksMessage, value); }
        }
        
        public ApplicationPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new DelegateCommand(() => _navigationService.GoBack(), () => _navigationService.CanGoBack());
            Builds = new ObservableCollection<Build>();
            Errors = new ObservableCollection<Error>();
            ConfigurationVariables = new ObservableCollection<ConfigurationVariable>();
            Collaborators = new ObservableCollection<Collaborator>();
            Hostnames = new ObservableCollection<Hostname>();
            ServiceHooks = new ObservableCollection<ServiceHook>();
        }

        public async Task LoadData()
        {
            if (String.IsNullOrEmpty(Slug)) return;

            try
            {
                _application = await AppHarborApi.Instance.GetApplication(AuthenticationService.Instance.AccessToken, Slug);
            }
            catch (Exception err)
            {
                Message = String.Format("Error - {0}", err.Message);
            }

            if (_application == null) return;

            Name = _application.Name;

            await GetBuilds();

            await GetErrors();

            await GetConfigurationVariables();

            await GetCollaborators();

            await GetHostnames();

            await GetServiceHooks();

            Message = String.Empty;
        }

        private async Task GetBuilds()
        {
            try
            {
                BuildsMessage = "Loading Data";
                var task = AppHarborApi.Instance.GetBuilds(AuthenticationService.Instance.AccessToken, Slug);

                foreach (Build build in await task)
                {
                    Builds.Add(build);
                }

                BuildCount = Builds.Count;
                BuildsMessage = String.Empty;
            }
            catch (Exception err)
            {
                BuildsMessage = String.Format("Error - {0}", err.Message);
            }
        }

        private async Task GetErrors()
        {
            try
            {
                ErrorMessage = "Loading Data";
                var task = AppHarborApi.Instance.GetErrors(AuthenticationService.Instance.AccessToken, Slug);

                foreach (Error error in await task)
                {
                    Errors.Add(error);
                }

                ErrorCount = Errors.Count;
                ErrorMessage = String.Empty;
            }
            catch (Exception err)
            {
                ErrorMessage = String.Format("Error - {0}", err.Message);
            }
        }

        private async Task GetServiceHooks()
        {
            try
            {
                ServiceHooksMessage = "Loading Data";
                var task = AppHarborApi.Instance.GetServiceHooks(AuthenticationService.Instance.AccessToken, Slug);

                foreach (ServiceHook serviceHook in await task)
                {
                    ServiceHooks.Add(serviceHook);
                }
                ServiceHooksCount = ServiceHooks.Count;
                ServiceHooksMessage = String.Empty;
            }
            catch (Exception err)
            {
                ServiceHooksMessage = String.Format("Error - {0}", err.Message);
            }
        }

        private async Task GetHostnames()
        {
            try
            {
                HostnamesMessage = "Loading Data";
                var task = AppHarborApi.Instance.GetHostnames(AuthenticationService.Instance.AccessToken, Slug);

                foreach (Hostname hostname in await task)
                {
                    Hostnames.Add(hostname);
                }
                HostnamesCount = Hostnames.Count;
                HostnamesMessage = String.Empty;
            }
            catch (Exception err)
            {
                HostnamesMessage = String.Format("Error - {0}", err.Message);
            }
        }

        private async Task GetCollaborators()
        {
            try
            {
                CollaboratorsMessage = "Loading Data";
                var task = AppHarborApi.Instance.GetCollaborators(AuthenticationService.Instance.AccessToken, Slug);

                foreach (Collaborator collaborator in await task)
                {
                    Collaborators.Add(collaborator);
                }

                CollaboratorsCount = Collaborators.Count;
                CollaboratorsMessage = String.Empty;
            }
            catch (Exception err)
            {
                CollaboratorsMessage = String.Format("Error - {0}", err.Message);
            }
        }

        private async Task GetConfigurationVariables()
        {
            try
            {
                ConfigurationVariablesMessage = "Loading Data";
                var task = AppHarborApi.Instance.GetConfigurationVariables(AuthenticationService.Instance.AccessToken,
                                                                    Slug);

                foreach (ConfigurationVariable configurationVariable in await task)
                {
                    ConfigurationVariables.Add(configurationVariable);
                }
                ConfigurationVariablesCount = ConfigurationVariables.Count;
                ConfigurationVariablesMessage = String.Empty;
            }
            catch (Exception err)
            {
                ConfigurationVariablesMessage = String.Format("Error - {0}", err.Message);
            }
        }

        public async override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
            if (String.IsNullOrEmpty(Slug))
            {
                try
                {
                    ApplicationSummary applicationSummary = navigationParameter as ApplicationSummary;
                    if (applicationSummary != null)
                    {
                        Slug = applicationSummary.Slug;
                        await LoadData();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

    }
}
