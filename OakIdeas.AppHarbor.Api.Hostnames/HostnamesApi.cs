using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Hostnames
{
    public class HostnamesApi : AppHarborApi
    {
        private const string _applicationHostnamesUrl = "applications/{slug}/hostnames";
        private const string _applicationHostnameUrl = "applications/{slug}/hostnames/{id}";

        private static HostnamesApi instance;
        public static HostnamesApi Instance
        {
            get { return instance ?? (instance = new HostnamesApi()); }
        }

        private HostnamesApi()
        {
            
        }

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Hostnames
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the settings for the specified hostname.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The hostname id.</param>
        /// <returns></returns>
        public async Task<Hostname> GetHostnameAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationHostnameUrl.AddSlug(slug).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            Hostname hostname = await JsonConvert.DeserializeObjectAsync<Hostname>(jsonString);


            return hostname;
        }
        /// <summary>
        /// Retrieves a list of the current custom domains for the application. The item properties are the same as the hostname detail with the addition of the URL for the detail resource for each item.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Hostname>> GetHostnamesAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationHostnamesUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<Hostname> configurationVariables = await JsonConvert.DeserializeObjectAsync<List<Hostname>>(jsonString);


            return configurationVariables;
        }
        /// <summary>
        /// Adds the specified domain name to the application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="value">The value.</param>
        /// <remarks>IMPORTANT: Adding hostnames to an application incurs a cost, see the pricing page for details.</remarks>
        /// <returns></returns>
        public async Task<Hostname> CreateHostnameAsync(string token, string slug, string value)
        {
            string applicationUrlSlugged = _applicationHostnamesUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await PostAsync(token, applicationUrlSlugged, postData);

            Hostname hostname = await JsonConvert.DeserializeObjectAsync<Hostname>(jsonString);

            return hostname;
        }

        /// <summary>
        /// Delete a custom hostname from the application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The hostname id.</param>
        /// <returns></returns>
        public async Task<Hostname> DeleteHostnameAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationHostnameUrl.AddSlug(slug).AddId(id);

            string jsonString = await DeleteAsync(token, applicationUrlSlugged);

            Hostname hostname = await JsonConvert.DeserializeObjectAsync<Hostname>(jsonString);


            return hostname;
        }

        /// <summary>
        /// Delete a custom hostname from the application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        public async Task<Hostname> DeleteHostnameAsync(string token, string url)
        {
            string jsonString = await DeleteAsync(token, url);

            Hostname hostname = await JsonConvert.DeserializeObjectAsync<Hostname>(jsonString);


            return hostname;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // Hostnames
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
