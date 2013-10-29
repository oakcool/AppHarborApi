using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Models;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Errors
{
    public class ErrorsApi : AppHarborApi
    {
        private const string _applicationErrorsUrl = "applications/{slug}/errors";
        private const string _applicationErrorUrl = "applications/{slug}/errors/{id}";

        private static ErrorsApi instance;
        public static ErrorsApi Instance
        {
            get { return instance ?? (instance = new ErrorsApi()); }
        }

        private ErrorsApi()
        {
            
        }

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
    }
}
