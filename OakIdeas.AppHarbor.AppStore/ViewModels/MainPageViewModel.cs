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
    public class MainPageViewModel : ViewModel
    {
        private ObservableCollection<ApplicationViewModel> _applications;

        public ObservableCollection<ApplicationViewModel> Applications
        {
            get { return _applications ?? (_applications = new ObservableCollection<ApplicationViewModel>()); }
        }

        public MainPageViewModel()
        {
            LoadData();
        }

        public async void LoadData()
        {
            if (String.IsNullOrEmpty(AuthenticationService.Instance.AccessToken))
            {
                await AuthenticationService.Instance.CheckAndGetAccessToken();
            }
            List<OakIdeas.AppHarbor.Api.Models.Application> applications = await
                AppHarborApi.Instance.GetApplications(AuthenticationService.Instance.AccessToken);

            foreach (Application application in applications)
            {
                _applications.Add(new ApplicationViewModel(application));
            }



        }
    }
}
