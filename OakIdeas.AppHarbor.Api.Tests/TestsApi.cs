using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Core.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Tests
{
    public class TestsApi : AppHarborApi
    {
        private const string _applicationBuildTestsUrl = "applications/{slug}/builds/{buildId}/tests";
        private const string _applicationBuildTestUrl = "applications/{slug}/builds/{buildId}/tests/{id}";

        private static TestsApi instance;
        public static TestsApi Instance
        {
            get { return instance ?? (instance = new TestsApi()); }
        }

        private TestsApi()
        {
            
        }

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

        /// <summary>
        /// Returns a list of test history items for the specified build ordered and grouped by id.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="url">The resource URL
        /// <returns></returns>
        public async Task<List<Test>> GetTestsAsync(string token, string url)
        {
            string jsonString = await GetAsync(token, url);

            List<Test> serviceHooks = await JsonConvert.DeserializeObjectAsync<List<Test>>(jsonString);

            return serviceHooks;
        }
        //---------------------------------------------------------------------------------------------------------------------
        // Tests
        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
    }
}
