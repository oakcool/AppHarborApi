using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Microsoft.Practices.Unity;
using OakIdeas.AppHarbor.AppStore.Common;
using OakIdeas.AppHarbor.AppStore.ViewModels;
using OakIdeas.AppHarbor.AppStore.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace OakIdeas.AppHarbor.AppStore
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : MvvmAppBase
    {
        IUnityContainer _container = new UnityContainer();
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            
        }

        protected override void OnInitialize(IActivatedEventArgs args)
        {
            base.OnInitialize(args);
            //ViewModelLocator.Register(typeof(MainPage).ToString(), ()=> new MainPageViewModel(NavigationService) );
            _container.RegisterInstance<INavigationService>(NavigationService);
            ViewModelLocator.SetDefaultViewModelFactory((viewModelType) => _container.Resolve(viewModelType));
        }

        protected async override void OnLaunchApplication(LaunchActivatedEventArgs args)
        {
            await CheckValidation();

            NavigationService.Navigate("Main", null);
        }

        private async static Task CheckValidation()
        {
            await AuthenticationService.Instance.CheckAndGetAccessToken();
        }

    }
}
