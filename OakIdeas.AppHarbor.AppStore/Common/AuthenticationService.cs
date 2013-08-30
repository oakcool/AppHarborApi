using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OakIdeas.AppHarbor.Api;
using Windows.Security.Authentication.Web;
using Windows.Security.Credentials;

namespace OakIdeas.AppHarbor.AppStore.Common
{
    public class AuthenticationService
    {
        private PasswordVault _passwordVault;
        private string _resourceName = "AppHarborResourceToken";
        private string _consumerKey = "c849ab1a-e308-4041-842b-ce5f8a8375dd";
        private string _consumerSecretKey = "e8f31dec-0044-4fca-9ef9-6b36ef1b8aa3";
        private string _callbackUrl = "http://www.oakideas.com";
        private string _token;
        private string _authorizationCode;
        private string _user = "user";



        public string AuthenticationUrl
        {
            get
            {
                return string.Format("https://appharbor.com/user/authorizations/new?client_id={0}&redirect_uri={1}", _consumerKey, _callbackUrl);
            }
        }

        public string AccessTokenUrl
        {
            get { return string.Format("https://appharbor.com/tokens"); }
        }

        public string User { get; set; }

        public string AccessToken
        {
            get
            {
                try
                {
                    var creds = _passwordVault.FindAllByResource(_resourceName).FirstOrDefault();
                    if (creds != null)
                    {
                        return _passwordVault.Retrieve(_resourceName, _user).Password;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    // if no access token found, the FindAllByResource method throws System.Exception: Element not found
                    return null;
                }
            }
            set { _passwordVault.Add(new PasswordCredential(_resourceName, _user, value)); }
        }

        private static AuthenticationService instance;

        public static AuthenticationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthenticationService();
                }
                return instance;
            }
        }


        private AuthenticationService()
        {
            _passwordVault = new PasswordVault();
        }

        public async Task CheckAndGetAccessToken()
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                await GetAccessToken();
                await GetAuthorizeCode();
            }
        }

        private async Task GetAuthorizeCode()
        {
            var url = AccessTokenUrl;

            using (var httpClient = new HttpClient())
            {
                httpClient.MaxResponseContentBufferSize = int.MaxValue;
                httpClient.DefaultRequestHeaders.ExpectContinue = false;


                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("client_id", _consumerKey));
                postData.Add(new KeyValuePair<string, string>("client_secret", _consumerSecretKey));
                postData.Add(new KeyValuePair<string, string>("code", _authorizationCode));

                HttpContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = await httpClient.PostAsync(new Uri(url), content);
                string responseString = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> tokenData = Helpers.ParseQueryString(responseString);
                AccessToken = tokenData["access_token"];
            }
        }

        private async Task GetAccessToken()
        {
            string url = AuthenticationUrl;

            var startUri = new Uri(url);
            var endUri = new Uri(_callbackUrl);

            WebAuthenticationResult war = await WebAuthenticationBroker.AuthenticateAsync(
                                                        WebAuthenticationOptions.None,
                                                        startUri,
                                                        endUri);
            switch (war.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    {
                        // grab access_token and oauth_verifier
                        var response = war.ResponseData;

                        Uri uri = new Uri(response);

                        int index = uri.Query.LastIndexOf("=") + 1;

                        string code = uri.Query.Substring(index, uri.Query.Length - index);

                        _authorizationCode = code;

                        break;
                    }
                case WebAuthenticationStatus.UserCancel:
                    {

                        break;
                    }
                default:
                case WebAuthenticationStatus.ErrorHttp:

                    break;
            }
        }
    }
}
