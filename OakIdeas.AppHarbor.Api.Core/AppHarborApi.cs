using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Core
{
    public abstract class AppHarborApi
    {
        protected const string _baseUrl = "https://appharbor.com/";

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
            Uri uri = new Uri(baseUri, resource);

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
