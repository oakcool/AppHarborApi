using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.ServiceHooks
{
    public class ServiceHooksApi : AppHarborApi
    {
        private const string _applicationServiceHooksUrl = "applications/{slug}/servicehooks";
        private const string _applicationServiceHookUrl = "applications/{slug}/servicehooks/{id}";

        private static ServiceHooksApi instance;
        public static ServiceHooksApi Instance
        {
            get { return instance ?? (instance = new ServiceHooksApi()); }
        }

        private ServiceHooksApi()
        {
            
        }

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Service Hooks
        //---------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieve the details for an existing service hook.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The service hook id.</param>
        /// <returns></returns>
        public async Task<ServiceHook> GetServiceHookAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationServiceHookUrl.AddSlug(slug).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            ServiceHook serviceHook = await JsonConvert.DeserializeObjectAsync<ServiceHook>(jsonString);

            return serviceHook;
        }
        /// <summary>
        /// Returns a list of service hooks for the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<ServiceHook>> GetServiceHooksAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationServiceHooksUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<ServiceHook> serviceHooks = await JsonConvert.DeserializeObjectAsync<List<ServiceHook>>(jsonString);

            return serviceHooks;
        }
        /// <summary>
        /// Create a new service hook to receive requests for the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="url">The URL to use for the service hook requests.</param>
        /// <returns></returns>
        public async Task<ServiceHook> CreateServiceHookAsync(string token, string slug, string url)
        {
            string applicationUrlSlugged = _applicationServiceHooksUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("url", url));

            string jsonString = await PostAsync(token, applicationUrlSlugged, postData);

            ServiceHook serviceHook = await JsonConvert.DeserializeObjectAsync<ServiceHook>(jsonString);

            return serviceHook;
        }

        /// <summary>
        /// Remove the service hook from the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The service hook id.</param>
        /// <returns></returns>
        public async Task<ServiceHook> DeleteServiceHookAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationServiceHookUrl.AddSlug(slug).AddId(id);

            string jsonString = await DeleteAsync(token, applicationUrlSlugged);

            ServiceHook serviceHook = await JsonConvert.DeserializeObjectAsync<ServiceHook>(jsonString);


            return serviceHook;
        }

        /// <summary>
        /// Remove the service hook from the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        public async Task<ServiceHook> DeleteServiceHookAsync(string token, string url)
        {
            string jsonString = await DeleteAsync(token, url);

            ServiceHook serviceHook = await JsonConvert.DeserializeObjectAsync<ServiceHook>(jsonString);


            return serviceHook;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // Service Hooks
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
