using Newtonsoft.Json;
using OakIdeas.AppHarbor.Api.Core;
using OakIdeas.AppHarbor.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Users
{
    public class UsersApi : AppHarborApi
    {
        private const string _userUrl = "user";

        private static UsersApi instance;
        public static UsersApi Instance
        {
            get { return instance ?? (instance = new UsersApi()); }
        }

        private UsersApi()
        {
            
        }

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
    }
}
