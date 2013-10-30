using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Core.Extensions;
using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core.Models;

namespace OakIdeas.AppHarbor.Api.Collaborators
{
    public class CollaboratorsApi : AppHarborApi
    {        
        private const string _applicationCollaboratorsUrl = "applications/{slug}/collaborators";
        private const string _applicationCollaboratorUrl = "applications/{slug}/collaborators/{id}";

        private static CollaboratorsApi instance;
        public static CollaboratorsApi Instance
        {
            get { return instance ?? (instance = new CollaboratorsApi()); }
        }

        private CollaboratorsApi()
        {
            
        }

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

    }
}
