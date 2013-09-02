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
    public class MainPageViewModel : ViewModel
    {

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private readonly INavigationService _navigationService;

        private ObservableCollection<ApplicationSummary> _applications;

        public ObservableCollection<ApplicationSummary> Applications
        {
            get { return _applications ?? (_applications = new ObservableCollection<ApplicationSummary>()); }
        }

        public DelegateCommand<ApplicationSummary> NavCommand { get; set; } 

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavCommand = new DelegateCommand<ApplicationSummary>(OnApplicationCommand);
            LoadData();
        }

        public async void LoadData()
        {
            //if (String.IsNullOrEmpty(AuthenticationService.Instance.AccessToken))
            //{
            //    await AuthenticationService.Instance.CheckAndGetAccessToken();
            //}
            List<Application> applications = await
                AppHarborApi.Instance.GetApplications(AuthenticationService.Instance.AccessToken);
            
            foreach (Application application in applications.OrderBy(a => a.Name))
            {
                ApplicationSummary applicationSummary = new ApplicationSummary() { Slug = application.Slug, Name = application.Name };
                _applications.Add(applicationSummary);
            }

            foreach (ApplicationSummary applicationSummary in Applications.AsParallel())
            {
                applicationSummary.ApplicationMessage = "Loading Data";
                GetSummary(applicationSummary);
            }
        }

        private void OnApplicationCommand(ApplicationSummary application)
        {
            _navigationService.Navigate("Application", application);
        }

        public async Task<ApplicationSummary> GetSummary(ApplicationSummary applicationSummary)
        {
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                applicationSummary.BuildCount =
                (await AppHarborApi.Instance.GetBuilds(AuthenticationService.Instance.AccessToken, applicationSummary.Slug))
                    .Count;
            }
            catch (Exception err)
            {
                stringBuilder.AppendLine(String.Format("Error - {0}", err.Message));
            }

            try
            {
                applicationSummary.CollaboratorsCount =
                (await AppHarborApi.Instance.GetCollaborators(AuthenticationService.Instance.AccessToken, applicationSummary.Slug))
                    .Count;
            }
            catch (Exception err)
            {
                stringBuilder.AppendLine(String.Format("Error - {0}", err.Message));
            }

            try
            {
                applicationSummary.ConfigurationVariablesCount =
                (await AppHarborApi.Instance.GetConfigurationVariables(AuthenticationService.Instance.AccessToken, applicationSummary.Slug))
                    .Count;
            }
            catch (Exception err)
            {
                stringBuilder.AppendLine(String.Format("Error - {0}", err.Message));
            }

            try
            {
                applicationSummary.ErrorCount =
                (await AppHarborApi.Instance.GetErrors(AuthenticationService.Instance.AccessToken, applicationSummary.Slug))
                    .Count;
            }
            catch (Exception err)
            {
                stringBuilder.AppendLine(String.Format("Error - {0}", err.Message));
            }

            try
            {
                applicationSummary.HostnamesCount =
                (await AppHarborApi.Instance.GetHostnames(AuthenticationService.Instance.AccessToken, applicationSummary.Slug))
                    .Count;
            }
            catch (Exception err)
            {
                stringBuilder.AppendLine(String.Format("Error - {0}", err.Message));
            }

            try
            {
                applicationSummary.ServiceHooksCount =
                (await AppHarborApi.Instance.GetServiceHooks(AuthenticationService.Instance.AccessToken, applicationSummary.Slug))
                    .Count;
            }
            catch (Exception err)
            {
                stringBuilder.AppendLine(String.Format("Error - {0}", err.Message));
            }

            applicationSummary.ApplicationMessage = stringBuilder.ToString();
            return applicationSummary;
        }

    }
}
