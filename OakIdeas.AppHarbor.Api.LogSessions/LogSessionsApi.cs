using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.LogSessions
{
    public class LogSessionsApi : AppHarborApi
    {
        private const string _applicationLogSessionUrl = "applications/{slug}/logsession";

        private static LogSessionsApi instance;
        public static LogSessionsApi Instance
        {
            get { return instance ?? (instance = new LogSessionsApi()); }
        }

        private LogSessionsApi()
        {
            
        }

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
    }
}
