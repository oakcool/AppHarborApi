using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Drains
{
    public class DrainsApi : AppHarborApi
    {
        private const string _applicationDrainsUrl = "applications/{slug}/drains";
        private const string _applicationDrainUrl = "applications/{slug}/drains/{id}";

        private static DrainsApi instance;
        public static DrainsApi Instance
        {
            get { return instance ?? (instance = new DrainsApi()); }
        }

        private DrainsApi()
        {
            
        }

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Drains
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the settings for the specified drain.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The drain id.</param>
        /// <returns></returns>
        public async Task<Drain> GetDrainAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationDrainUrl.AddSlug(slug).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;
        }
        /// <summary>
        /// Retrieves a list of drains for the application. The item properties are the same as the drain detail with the addition of the URL for the detail resource for each item.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Drain>> GetDrainsAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationDrainsUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<Drain> drains = await JsonConvert.DeserializeObjectAsync<List<Drain>>(jsonString);

            return drains;
        }

        /// <summary>
        /// Adds the specified drain url to the application.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="value">The drain url to add</param>
        /// <returns></returns>
        public async Task<Drain> CreateDrainAsync(string token, string slug, string value)
        {
            string applicationUrlSlugged = _applicationDrainsUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await PostAsync(token, applicationUrlSlugged, postData);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;
        }

        /// <summary>
        /// Delete a drain from the application.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The drain id</param>
        /// <returns></returns>
        public async Task<Drain> DeleteDrainAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationDrainUrl.AddSlug(slug).AddId(id);

            string jsonString = await DeleteAsync(token, applicationUrlSlugged);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;

        }

        /// <summary>
        /// Delete a drain from the application.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        public async Task<Drain> DeleteDrainAsync(string token, string url)
        {
            string jsonString = await DeleteAsync(token, url);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;
        }

        //---------------------------------------------------------------------------------------------------------------------
        // Drains
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
