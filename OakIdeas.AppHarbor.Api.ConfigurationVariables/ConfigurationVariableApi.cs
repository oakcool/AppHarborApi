using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OakIdeas.AppHarbor.Api.ConfigurationVariables
{
    public class ConfigurationVariableApi : AppHarborApi
    {
        private const string _applicationConfigurationVariablesUrl = "applications/{slug}/configurationvariables";
        private const string _applicationConfigurationVariableUrl = "applications/{slug}/configurationvariables/{id}";

        private static ConfigurationVariableApi instance;
        public static ConfigurationVariableApi Instance
        {
            get { return instance ?? (instance = new ConfigurationVariableApi()); }
        }

        private ConfigurationVariableApi()
        {
            
        }

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Configuration Variables
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the detail for the specified configuration variable.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The configuration variable id.</param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> GetConfigurationVariableAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationConfigurationVariableUrl.AddSlug(slug).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);


            return configurationVariable;
        }
        /// <summary>
        /// Retrieves the list of configuration variables for the application. The item properties are the same as the Configuration Variable detail with the addition of the URL for the detail of each item.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<ConfigurationVariable>> GetConfigurationVariablesAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationConfigurationVariablesUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<ConfigurationVariable> configurationVariables = await JsonConvert.DeserializeObjectAsync<List<ConfigurationVariable>>(jsonString);


            return configurationVariables;
        }
        /// <summary>
        /// Create a new configuration variable for the application with the specified key and value.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="key">The key or name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> CreateConfigurationVariableAsync(string token, string slug, string key, string value)
        {
            string applicationUrlSlugged = _applicationConfigurationVariablesUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("key", key.Replace(" ", "_")));
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await PostAsync(token, applicationUrlSlugged, postData);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);

            return configurationVariable;
        }
        /// <summary>
        /// Edit the details for an existing configuration variable.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The configuration variable id.</param>
        /// <param name="key">The key or name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> UpdateConfigurationVariableAsync(string token, string slug, string id, string key, string value)
        {
            string applicationUrlSlugged = _applicationConfigurationVariableUrl.AddSlug(slug).AddId(id);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("key", key.Replace(" ", "_")));
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await PutAsync(token, applicationUrlSlugged, postData);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);

            return configurationVariable;
        }

        /// <summary>
        /// Edit the details for an existing configuration variable.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <param name="key">The key or name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> UpdateConfigurationVariableAsync(string token, string url, string key, string value)
        {
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("key", key.Replace(" ", "_")));
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await PutAsync(token, url, postData);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);

            return configurationVariable;
        }

        /// <summary>
        /// Delete the configuration variable from the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The configuration variable id.</param>
        /// <returns></returns>
        public async Task<string> DeleteConfigurationVariableAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationConfigurationVariableUrl.AddSlug(slug).AddId(id);

            string jsonString = await DeleteAsync(token, applicationUrlSlugged);

            return jsonString;
        }

        /// <summary>
        /// Delete the configuration variable from the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        public async Task<string> DeleteConfigurationVariableAsync(string token, string url)
        {
            string jsonString = await DeleteAsync(token, url);

            return jsonString;
        }

        //---------------------------------------------------------------------------------------------------------------------
        // Configuration Variables
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
