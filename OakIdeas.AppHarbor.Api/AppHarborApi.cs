using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Extensions;
using OakIdeas.AppHarbor.Api;
using OakIdeas.AppHarbor.Api.Models;

namespace OakIdeas.AppHarbor.Api
{
    public class AppHarborApi
    {
        private const string _baseUrl = "https://appharbor.com/";
        private const string _applicationsUrl = "applications";
        private const string _applicationUrl = "applications/{slug}";
        private const string _applicationBuildsUrl = "applications/{slug}/builds";
        private const string _applicationBuildUrl = "applications/{slug}/builds/{id}";
        private const string _applicationBuildDeployUrl = "applications/{slug}/builds/{id}/deploy";
        private const string _applicationCollaboratorsUrl = "applications/{slug}/collaborators";
        private const string _applicationCollaboratorUrl = "applications/{slug}/collaborators/{id}";
        private const string _applicationErrorsUrl = "applications/{slug}/errors";
        private const string _applicationErrorUrl = "applications/{slug}/errors/{id}";
        private const string _applicationConfigurationVariablesUrl = "applications/{slug}/configurationvariables";
        private const string _applicationConfigurationVariableUrl = "applications/{slug}/configurationvariables/{id}";
        private const string _applicationBuildTestsUrl = "applications/{slug}/builds/{buildId}/tests";
        private const string _applicationBuildTestUrl = "applications/{slug}/builds/{buildId}/tests/{id}";
        private const string _applicationHostnamesUrl = "applications/{slug}/hostnames";
        private const string _applicationHostnameUrl = "applications/{slug}/hostnames/{id}";
        private const string _applicationServiceHooksUrl = "applications/{slug}/servicehooks";
        private const string _applicationServiceHookUrl = "applications/{slug}/servicehooks/{id}";
        private const string _applicationLogSessionUrl = "applications/{slug}/logsession";
        private const string _applicationDrainsUrl = "applications/{slug}/drains";
        private const string _applicationDrainUrl = "applications/{slug}/drains/{id}";
        private const string _userUrl = "user";

        private static AppHarborApi instance;

        private AppHarborApi()
        {
            
        }

        public static AppHarborApi Instance
        {
            get { return instance ?? (instance = new AppHarborApi()); }
        }
        
        /// <summary>
        /// Gets anything based to the type you pass
        /// </summary>
        /// <typeparam name="T">What you want to get</typeparam>
        /// <param name="token">The access token.</param>
        /// <param name="url">The url of the resource</param>
        /// <returns></returns>
        public async Task<T> GetThingAsync<T>(string token, string url)
        {
            string jsonString = await GetAsync(token, url);

            T serviceHooks = await JsonConvert.DeserializeObjectAsync<T>(jsonString);

            return serviceHooks;
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

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Builds
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the details for the specified build.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The build id.</param>
        /// <returns></returns>
        public async Task<Build> GetBuildAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationBuildUrl.AddSlug(slug).AddId(id);
            
            string jsonString = await GetAsync(token, applicationUrlSlugged);
            
            Build build = await JsonConvert.DeserializeObjectAsync<Build>(jsonString);

            return build;
        }
        /// <summary>
        /// Retrieves a list of builds for the specified application, ordered by created in descending order. Item properties match the build detail response properties with the addition of the build detail URL.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns>List of builds.</returns>
        public async Task<List<Build>> GetBuildsAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationBuildsUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<Build> builds = await JsonConvert.DeserializeObjectAsync<List<Build>>(jsonString);


            return builds;
        }
        /// <summary>
        /// Trigger a specific build for deployment.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The build id.</param>
        /// <returns></returns>
        public async Task DeployBuildAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationBuildDeployUrl.AddSlug(slug).AddId(id);

            string jsonString = await PostAsync(token, applicationUrlSlugged, null);
        }
        /// <summary>
        /// Trigger a specific build for deployment.
        /// </summary>
        /// <param name="url">The access token.</param>        
        /// <returns></returns>
        public async Task DeployBuildAsync(string token, string url)
        {
            string applicationUrlSlugged = String.Format("{0}/deploy", url);

            string jsonString = await PostAsync(token, applicationUrlSlugged, null);
        }


        //---------------------------------------------------------------------------------------------------------------------
        // Builds
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Collaborators
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retreive the details for the specified collaborator.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The collaborator id.</param>
        /// <returns></returns>
        public async Task<Collaborator> GetCollaboratorAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationCollaboratorUrl.AddSlug(slug).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            Collaborator collaborator = await JsonConvert.DeserializeObjectAsync<Collaborator>(jsonString);


            return collaborator;
        }
        /// <summary>
        /// Retrieve a list of collaborators for an application, ordered by user name. Item properties match collaborator detail response properties with the addition of the detail URL.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Collaborator>> GetCollaboratorsAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationCollaboratorsUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<Collaborator> collaborators = await JsonConvert.DeserializeObjectAsync<List<Collaborator>>(jsonString);


            return collaborators;
        }

        /// <summary>
        /// Create a new collaborator on the application with the specified role.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="collaboratorEmail">The email address of the collaborator to add.</param>
        /// <param name="role">The access level for the collaborator. Accepted values are collaborator or administrator.</param>
        /// <returns></returns>
        public async Task<string> CreateCollaboratorAsync(string token, string slug, string collaboratorEmail, string role)
        {
            string applicationUrlSlugged = _applicationCollaboratorsUrl.AddSlug(slug);
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("collaboratorEmail", collaboratorEmail));
            postData.Add(new KeyValuePair<string, string>("role", role));

            string jsonString = await PostAsync(token, applicationUrlSlugged, postData);

            return jsonString;
        }
        
        /// <summary>
        /// Edit the details for an existing collaborator.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The collaborator id.</param>
        /// <param name="role">The access level for the collaborator. Accepted values are collaborator or administrator.</param>
        /// <returns></returns>
        public async Task<Collaborator> UpdateCollaboratorAsync(string token, string slug, string id, string role)
        {
            string applicationCollaboratorSlugged = _applicationCollaboratorsUrl.AddSlug(slug).AddId(id);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("role", role));

            string jsonString = await PutAsync(token, applicationCollaboratorSlugged, postData);

            Collaborator collaborator = await JsonConvert.DeserializeObjectAsync<Collaborator>(jsonString);


            return collaborator;
        }

        /// <summary>
        /// Edit the details for an existing collaborator.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL        
        /// <param name="role">The access level for the collaborator. Accepted values are collaborator or administrator.</param>
        /// <returns></returns>
        public async Task<Collaborator> UpdateCollaboratorAsync(string token, string url, string role)
        {            
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("role", role));

            string jsonString = await PutAsync(token, url, postData);

            Collaborator collaborator = await JsonConvert.DeserializeObjectAsync<Collaborator>(jsonString);


            return collaborator;
        }
        
        /// <summary>
        /// Delete the collaborator from the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The collaborator id.</param>
        /// <returns></returns>
        public async Task<string> DeleteCollaboratorAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationCollaboratorsUrl.AddSlug(slug).AddId(id);
            
            string jsonString = await DeleteAsync(token, applicationUrlSlugged);

            return jsonString;
        }

        
        /// <summary>
        /// Delete the collaborator from the specified application.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL        
        /// <returns></returns>
        public async Task<string> DeleteCollaboratorAsync(string token, string url)
        {
            string jsonString = await DeleteAsync(token, url);

            return jsonString;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // Collabortors
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
       
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Errors
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the details for the specified error.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id">The error id.</param>
        /// <returns></returns>
        public async Task<Error> GetErrorAsync(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationErrorUrl.AddSlug(slug).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            Error error = await JsonConvert.DeserializeObjectAsync<Error>(jsonString);

            return error;
        }
        /// <summary>
        /// Retrieves a list of the latest unhandled exceptions for the application, in descending order by date. Item properties match the error detail response properties with the addition of the detail URL.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Error>> GetErrorsAsync(string token, string slug)
        {
            string applicationUrlSlugged = _applicationErrorsUrl.AddSlug(slug);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<Error> errors = await JsonConvert.DeserializeObjectAsync<List<Error>>(jsonString);


            return errors;
        }

        //---------------------------------------------------------------------------------------------------------------------
        // Errors
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------

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
            postData.Add(new KeyValuePair<string, string>("key", key.Replace(" ","_")));
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
        
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // User
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the user details.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(string token)
        {
            string applicationUrlSlugged = _userUrl;

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            User user = await JsonConvert.DeserializeObjectAsync<User>(jsonString);

            return user;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // User
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Tests
        //---------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Retrieve the test detail for the specified build and test ID.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="buildId">The build id</param>
        /// <param name="id">The test id</param>
        /// <remarks>Test detail items are represented by a tree structure. The tests property for items of kind Group may contain either groups or tests. Items of type Test will have an empty list for the tests property as they cannot contain children.</remarks>
        /// <returns></returns>
        public async Task<Test> GetTestAsync(string token, string slug, string buildId, string id)
        {
            string applicationUrlSlugged = _applicationBuildTestUrl.AddSlug(slug).AddBuildId(buildId).AddId(id);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            Test serviceHook = await JsonConvert.DeserializeObjectAsync<Test>(jsonString);

            return serviceHook;
        }
        
        /// <summary>
        /// Returns a list of test history items for the specified build ordered and grouped by id.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="buildId">The build id</param>
        /// <returns></returns>
        public async Task<List<Test>> GetTestsAsync(string token, string slug, string buildId)
        {
            string applicationUrlSlugged = _applicationBuildTestsUrl.AddSlug(slug).AddBuildId(buildId);

            string jsonString = await GetAsync(token, applicationUrlSlugged);

            List<Test> serviceHooks = await JsonConvert.DeserializeObjectAsync<List<Test>>(jsonString);

            return serviceHooks;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // Tests
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Log
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates a log session. The log session is valid for 1 minute and has to be accessed before it expires. Keep the connection open to continue to retrieve log messages after that.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="tail">Boolean value indicating whether you want to start a tail log session.</param>
        /// <param name="limit">The number of logs to fetch on the initial request.</param>
        /// <param name="sourceFilter">Only receive log messages from this source when specificed.</param>
        /// <param name="processFilter">Only receive log messages from this process when specificed</param>
        /// <remarks>IMPORTANT: Adding hostnames to an application incurs a cost, see the pricing page for details.</remarks>
        /// <returns></returns>
        public async Task<Drain> CreateLogSessionAsync(string token, string slug, string tail, string limit,
                                                        string sourceFilter, string processFilter)
        {
            string applicationUrlSlugged = _applicationLogSessionUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("tail", tail));
            postData.Add(new KeyValuePair<string, string>("limit", limit));
            postData.Add(new KeyValuePair<string, string>("sourceFilter", sourceFilter));
            postData.Add(new KeyValuePair<string, string>("processFilter", processFilter));

            string jsonString = await PostAsync(token, applicationUrlSlugged, postData);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // Log
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------

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

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Http Requests
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Executes a get request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource">The resource name</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string token, string resource)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri,resource);

            return await GetAsync(token, uri);
        }

        /// <summary>
        /// Executes a get request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri">The api uri</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string token, Uri uri)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", String.Format("BEARER {0}", token));
            HttpResponseMessage requestMessage = await client.GetAsync(uri);
            
            requestMessage.EnsureSuccessStatusCode();

            return await requestMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Executes a post request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource">The resource name</param>
        /// <param name="postData">The data to be posted</param>
        /// <returns></returns>
        public async Task<string> PostAsync(string token, string resource, List<KeyValuePair<string, string>> postData)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri, resource);

            return await PostAsync(token, uri, postData);
        }

        /// <summary>
        /// Executes a post request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri">The api uri</param>
        /// <param name="postData">The data to be posted</param>
        /// <returns></returns>
        public async Task<string> PostAsync(string token, Uri uri, List<KeyValuePair<string, string>> postData)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", String.Format("BEARER {0}", token));

            HttpContent content = null;

            if (postData != null)
            {
                content = new FormUrlEncodedContent(postData);
            }

            HttpResponseMessage requestMessage = await client.PostAsync(uri, content);

            requestMessage.EnsureSuccessStatusCode();

            return await requestMessage.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Executes a put request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource">The resource name</param>
        /// <param name="postData">The data for the update</param>
        /// <returns></returns>
        public async Task<string> PutAsync(string token, string resource, List<KeyValuePair<string, string>> postData)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri, resource);

            return await PutAsync(token, uri, postData);
        }
        /// <summary>
        /// Executes a put request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri">The api uri</param>
        /// <param name="postData">The data for the update</param>
        /// <returns></returns>
        public async Task<string> PutAsync(string token, Uri uri, List<KeyValuePair<string, string>> postData)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", String.Format("BEARER {0}", token));

            HttpContent content = new FormUrlEncodedContent(postData);

            HttpResponseMessage requestMessage = await client.PutAsync(uri, content);

            requestMessage.EnsureSuccessStatusCode();

            return await requestMessage.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Executes a delete request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource">The resource</param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(string token, string resource)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri, resource);

            return await DeleteAsync(token, uri);
        }
        /// <summary>
        /// Executes a delete request
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri">The api uri</param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(string token, Uri uri)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", String.Format("BEARER {0}", token));

            HttpResponseMessage requestMessage = await client.DeleteAsync(uri);

            requestMessage.EnsureSuccessStatusCode();

            return await requestMessage.Content.ReadAsStringAsync();
        }

        //---------------------------------------------------------------------------------------------------------------------
        // Http Requests
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
