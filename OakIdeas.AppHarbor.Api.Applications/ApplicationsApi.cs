using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OakIdeas.AppHarbor.Api.Core.Extensions;

namespace OakIdeas.AppHarbor.Api.Applications
{
    public class ApplicationsApi : AppHarborApi
    {
        private const string _applicationsUrl = "applications";
        private const string _applicationUrl = "applications/{slug}";

        private static ApplicationsApi instance;
        public static ApplicationsApi Instance
        {
            get { return instance ?? (instance = new ApplicationsApi()); }
        }

        private ApplicationsApi()
        {
            
        }

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Application
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the information associated with the specified application slug.
        /// </summary>
        /// <param name="token">>The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns>Retrieve the information associated with the specified application slug.</returns>
        public async Task<Application> GetApplicationAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationUrl.AddSlug(slug);
            string jsonString = await GetAsync(token, applicationUrlSlugged);

            Application application = await JsonConvert.DeserializeObjectAsync<Application>(jsonString);

            return application;
        }
        /// <summary>
        /// Returns a list of all applications for the authorized user.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <returns>Returns a list of all applications for the authorized user.</returns>
        public async Task<List<Application>> GetApplicationsAsync(string token)
        {
            string jsonString = await GetAsync(token, _applicationsUrl);

            List<Application> applications = await JsonConvert.DeserializeObjectAsync<List<Application>>(jsonString);


            return applications;
        }
        /// <summary>
        /// Create an application on the authorized user's account with the specified name. The slug is auto-generated based on the name.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="name">The name of the application.</param>
        /// <param name="region">Provide amazon-web-services::us-east-1 or amazon-web-services::eu-west-1.</param>
        /// <returns></returns>
        public async Task<string> CreateApplicationAsync(string token, string name, string region)
        {
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", name));
            postData.Add(new KeyValuePair<string, string>("region_identifier", region));

            string jsonString = await PostAsync(token, _applicationsUrl, postData);

            return jsonString;
        }
        /// <summary>
        /// Update the information for an existing application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="name">The name of the application.</param>
        /// <returns></returns>
        public async Task<string> UpdateApplicationAsync(string token, string slug, string name)
        {
            string applicationUrlSlugged = _applicationUrl.AddSlug(slug);
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", name));

            string jsonString = await PutAsync(token, applicationUrlSlugged, postData);

            return jsonString;
        }

        /// <summary>
        /// Update the information for an existing application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <param name="name">The name of the application.</param>
        /// <returns></returns>
        public async Task<string> UpdateApplicationWithUrlAsync(string token, string url, string name)
        {
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", name));

            string jsonString = await PutAsync(token, url, postData);

            return jsonString;
        }

        /// <summary>
        /// Remove an application from the authorized user's account.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        /// <remarks>Warning: this will irreversibly remove the application and the associated web site and add-ons.</remarks>
        public async Task<string> DeleteApplicationAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationUrl.AddSlug(slug);

            string jsonString = await DeleteAsync(token, applicationUrlSlugged);

            return jsonString;
        }

        /// <summary>
        /// Remove an application from the authorized user's account.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        /// <remarks>Warning: this will irreversibly remove the application and the associated web site and add-ons.</remarks>
        public async Task<string> DeleteApplicationWithUrlAsync(string token, string url)
        {
            string jsonString = await DeleteAsync(token, url);

            return jsonString;
        }

        //---------------------------------------------------------------------------------------------------------------------
        // Application
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
