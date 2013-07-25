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

        private AppHarborApi() { }

        public static AppHarborApi Instance
        {
            get { return instance ?? (instance = new AppHarborApi()); }
        }
        // tests
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Application
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the information associated with the specified application slug.
        /// </summary>
        /// <param name="token">>The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns>Retrieve the information associated with the specified application slug.</returns>
        public async Task<Application> GetApplication(string token, string slug)
        {
            string applicationUrlSlugged = _applicationUrl.AddSlug(slug);
            string jsonString = await Get(token, applicationUrlSlugged);
            
            Application application = await JsonConvert.DeserializeObjectAsync<Application>(jsonString);

            return application;
        }
        /// <summary>
        /// Returns a list of all applications for the authorized user.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <returns>Returns a list of all applications for the authorized user.</returns>
        public async Task<List<Application>> GetApplications(string token)
        {
            string jsonString = await Get(token, _applicationsUrl);

            List<Application> applications = await JsonConvert.DeserializeObjectAsync<List<Application>>(jsonString);


            return applications;
        }
        /// <summary>
        /// Create an application on the authorized user's account with the specified name. The slug is auto-generated based on the name.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="name">The name of the application.</param>
        /// <param name="region">Provide amazon-web-services::us-east-1 or amazon-web-services::eu-west-1</param>
        /// <returns></returns>
        public async Task<string> CreateApplication(string token, string name, string region)
        {
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", name));
            postData.Add(new KeyValuePair<string, string>("region_identifier", region));

            string jsonString = await Post(token, _applicationsUrl, postData);

            return jsonString;
        }
        /// <summary>
        /// Update the information for an existing application.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="name">The name of the application.</param>
        /// <returns></returns>
        public async Task<string> UpdateApplication(string token,string slug, string name)
        {
            string applicationUrlSlugged = _applicationUrl.AddSlug(slug);
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", name));

            string jsonString = await Put(token, applicationUrlSlugged, postData);

            return jsonString;
        }
        /// <summary>
        /// Remove an application from the authorized user's account.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        /// <remarks>Warning: this will irreversibly remove the application and the associated web site and add-ons.</remarks>
        public async Task<string> DeleteApplication(string token, string slug)
        {
            string applicationUrlSlugged = _applicationUrl.AddSlug(slug);

            string jsonString = await Delete(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Build> GetBuild(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationBuildUrl.AddSlug(slug).AddId(id);
            
            string jsonString = await Get(token, applicationUrlSlugged);
            
            Build build = await JsonConvert.DeserializeObjectAsync<Build>(jsonString);

            return build;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Build>> GetBuilds(string token, string slug)
        {
            string applicationUrlSlugged = _applicationBuildsUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

            List<Build> builds = await JsonConvert.DeserializeObjectAsync<List<Build>>(jsonString);


            return builds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Build>> DeployBuild(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationBuildDeployUrl.AddSlug(slug).AddId(id);

            string jsonString = await Post(token, applicationUrlSlugged, null);

            List<Build> builds = await JsonConvert.DeserializeObjectAsync<List<Build>>(jsonString);


            return builds;
        }

        //---------------------------------------------------------------------------------------------------------------------
        // Builds
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        // Collabortors
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Collaborator> GetCollaborator(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationCollaboratorUrl.AddSlug(slug).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            Collaborator collaborator = await JsonConvert.DeserializeObjectAsync<Collaborator>(jsonString);


            return collaborator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Collaborator>> GetCollaborators(string token, string slug)
        {
            string applicationUrlSlugged = _applicationCollaboratorsUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

            List<Collaborator> collaborators = await JsonConvert.DeserializeObjectAsync<List<Collaborator>>(jsonString);


            return collaborators;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="collaboratorEmail"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<string> CreateCollaborator(string token,string slug, string collaboratorEmail, string role)
        {
            string applicationUrlSlugged = _applicationCollaboratorsUrl.AddSlug(slug);
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("collaboratorEmail", collaboratorEmail));
            postData.Add(new KeyValuePair<string, string>("role", role));

            string jsonString = await Post(token, applicationUrlSlugged, postData);

            return jsonString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="collaboratorEmail"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<string> DeleteCollaborator(string token, string slug, string collaboratorEmail, string role)
        {
            string applicationUrlSlugged = _applicationCollaboratorsUrl.AddSlug(slug);
            
            string jsonString = await Delete(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Error> GetError(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationErrorUrl.AddSlug(slug).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            Error error = await JsonConvert.DeserializeObjectAsync<Error>(jsonString);


            return error;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Error>> GetErrors(string token, string slug)
        {
            string applicationUrlSlugged = _applicationErrorsUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> GetConfigurationVariable(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationConfigurationVariableUrl.AddSlug(slug).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);


            return configurationVariable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<ConfigurationVariable>> GetConfigurationVariables(string token, string slug)
        {
            string applicationUrlSlugged = _applicationConfigurationVariablesUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

            List<ConfigurationVariable> configurationVariables = await JsonConvert.DeserializeObjectAsync<List<ConfigurationVariable>>(jsonString);


            return configurationVariables;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> CreateConfigurationVariable(string token, string slug, string key, string value)
        {
            string applicationUrlSlugged = _applicationConfigurationVariablesUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("key", key.Replace(" ", "_")));
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await Post(token, applicationUrlSlugged, postData);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);

            return configurationVariable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> UpdateConfigurationVariable(string token, string slug, string id, string key, string value)
        {
            string applicationUrlSlugged = _applicationConfigurationVariableUrl.AddSlug(slug).AddId(id);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("key", key.Replace(" ","_")));
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await Put(token, applicationUrlSlugged, postData);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);


            return configurationVariable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ConfigurationVariable> DeleteConfigurationVariable(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationConfigurationVariableUrl.AddSlug(slug).AddId(id);

            string jsonString = await Delete(token, applicationUrlSlugged);

            ConfigurationVariable configurationVariable = await JsonConvert.DeserializeObjectAsync<ConfigurationVariable>(jsonString);


            return configurationVariable;
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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Hostname> GetHostname(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationHostnameUrl.AddSlug(slug).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            Hostname hostname = await JsonConvert.DeserializeObjectAsync<Hostname>(jsonString);


            return hostname;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Hostname>> GetHostnames(string token, string slug)
        {
            string applicationUrlSlugged = _applicationHostnamesUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

            List<Hostname> configurationVariables = await JsonConvert.DeserializeObjectAsync<List<Hostname>>(jsonString);


            return configurationVariables;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Hostname> CreateHostname(string token, string slug, string value)
        {
            string applicationUrlSlugged = _applicationHostnamesUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await Post(token, applicationUrlSlugged, postData);

            Hostname hostname = await JsonConvert.DeserializeObjectAsync<Hostname>(jsonString);

            return hostname;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Hostname> DeleteHostname(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationHostnameUrl.AddSlug(slug).AddId(id);

            string jsonString = await Delete(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceHook> GetServiceHook(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationServiceHookUrl.AddSlug(slug).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            ServiceHook serviceHook = await JsonConvert.DeserializeObjectAsync<ServiceHook>(jsonString);

            return serviceHook;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<ServiceHook>> GetServiceHooks(string token, string slug)
        {
            string applicationUrlSlugged = _applicationServiceHooksUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

            List<ServiceHook> serviceHooks = await JsonConvert.DeserializeObjectAsync<List<ServiceHook>>(jsonString);

            return serviceHooks;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<ServiceHook> CreateServiceHook(string token, string slug, string url)
        {
            string applicationUrlSlugged = _applicationServiceHooksUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("url", url));

            string jsonString = await Post(token, applicationUrlSlugged, postData);

            ServiceHook serviceHook = await JsonConvert.DeserializeObjectAsync<ServiceHook>(jsonString);

            return serviceHook;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceHook> DeleteServiceHook(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationServiceHookUrl.AddSlug(slug).AddId(id);

            string jsonString = await Delete(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <returns></returns>
        public async Task<User> GetUser(string token)
        {
            string applicationUrlSlugged = _userUrl;

            string jsonString = await Get(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="buildId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Test> GetTest(string token, string slug, string buildId, string id)
        {
            string applicationUrlSlugged = _applicationBuildTestUrl.AddSlug(slug).AddBuildId(buildId).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            Test serviceHook = await JsonConvert.DeserializeObjectAsync<Test>(jsonString);

            return serviceHook;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public async Task<List<Test>> GetTests(string token, string slug, string buildId)
        {
            string applicationUrlSlugged = _applicationBuildTestsUrl.AddSlug(slug).AddBuildId(buildId);

            string jsonString = await Get(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="tail"></param>
        /// <param name="limit"></param>
        /// <param name="sourceFilter"></param>
        /// <param name="processFilter"></param>
        /// <returns></returns>
        public async Task<Drain> CreateLogSession(string token, string slug, string tail, string limit,
                                                        string sourceFilter, string processFilter)
        {
            string applicationUrlSlugged = _applicationLogSessionUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("tail", tail));
            postData.Add(new KeyValuePair<string, string>("limit", limit));
            postData.Add(new KeyValuePair<string, string>("sourceFilter", sourceFilter));
            postData.Add(new KeyValuePair<string, string>("processFilter", processFilter));

            string jsonString = await Post(token, applicationUrlSlugged, postData);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Drain> GetDrain(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationDrainUrl.AddSlug(slug).AddId(id);

            string jsonString = await Get(token, applicationUrlSlugged);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <returns></returns>
        public async Task<List<Drain>> GetDrains(string token, string slug)
        {
            string applicationUrlSlugged = _applicationDrainsUrl.AddSlug(slug);

            string jsonString = await Get(token, applicationUrlSlugged);

            List<Drain> drains = await JsonConvert.DeserializeObjectAsync<List<Drain>>(jsonString);

            return drains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Drain> CreateDrain(string token, string slug, string value)
        {
            string applicationUrlSlugged = _applicationDrainsUrl.AddSlug(slug);

            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("value", value));

            string jsonString = await Post(token, applicationUrlSlugged, postData);

            Drain drain = await JsonConvert.DeserializeObjectAsync<Drain>(jsonString);

            return drain;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="slug">The globally unique, URL-friendly version of the application name.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Drain> DeleteDrain(string token, string slug, string id)
        {
            string applicationUrlSlugged = _applicationDrainUrl.AddSlug(slug).AddId(id);

            string jsonString = await Delete(token, applicationUrlSlugged);

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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public async Task<string> Get(string token, string resource)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri,resource);

            return await Get(token, uri);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> Get(string token, Uri uri)
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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<string> Post(string token, string resource, List<KeyValuePair<string, string>> postData)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri, resource);

            return await Post(token, uri, postData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<string> Post(string token, Uri uri, List<KeyValuePair<string, string>> postData)
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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<string> Put(string token, string resource, List<KeyValuePair<string, string>> postData)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri, resource);

            return await Put(token, uri, postData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<string> Put(string token, Uri uri, List<KeyValuePair<string, string>> postData)
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
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public async Task<string> Delete(string token, string resource)
        {
            Uri baseUri = new Uri(_baseUrl);
            Uri uri = new Uri(baseUri, resource);

            return await Delete(token, uri);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> Delete(string token, Uri uri)
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
