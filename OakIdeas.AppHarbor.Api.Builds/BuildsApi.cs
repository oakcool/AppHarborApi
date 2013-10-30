using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Core.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Builds
{
    public class BuildsApi : AppHarborApi
    {
        private const string _applicationBuildsUrl = "applications/{slug}/builds";
        private const string _applicationBuildUrl = "applications/{slug}/builds/{id}";
        private const string _applicationBuildDeployUrl = "applications/{slug}/builds/{id}/deploy";
        

        private static BuildsApi instance;
        public static BuildsApi Instance
        {
            get { return instance ?? (instance = new BuildsApi()); }
        }

        private BuildsApi()
        {
            
        }

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
        /// Retrieve the details for the specified build.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        public async Task<Build> GetBuildWithUrlAsync(string token, string url)
        {
            string jsonString = await GetAsync(token, url);

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
    }
}
