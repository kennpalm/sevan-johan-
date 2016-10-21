using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XOCV.Helpers;
using XOCV.Interfaces;
using XOCV.Models;
using XOCV.Models.ResponseModels;

namespace XOCV.Services
{
    public class WebApiHelper : IWebApiHelper
    {
        private const int DefaultMaxResponseContentBufferSize = 256000;
        private readonly string BaseUrl = "http://sevan.pilgrimconsulting.com/api";

        /// <summary>
        ///     Authorization.
        /// </summary>
        /// <param name="login">The login model.</param>
        /// <returns>Getting token for user.</returns>
        public async Task<LoginResponseModel> Authorization (LoginModel login)
        {
            using (var client = new HttpClient ()) {
                try {
                    client.MaxResponseContentBufferSize = DefaultMaxResponseContentBufferSize;

                    var url = new Uri (string.Concat (BaseUrl, "/account"));
                    var json = JsonConvert.SerializeObject (login);
                    var content = new StringContent (json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync (url, content);
                    if (response.IsSuccessStatusCode) {
                        var result = await response.Content.ReadAsStringAsync ();
                        var responseModel = JsonConvert.DeserializeObject<LoginResponseModel> (result);
                        Settings.LocalToken = responseModel.Token;
                        return responseModel;
                    }
                    throw new Exception (response.ReasonPhrase);
                } catch (Exception ex) {
                    throw new Exception (ex.Message);
                }
            }
        }

        /// <summary>
        ///     Get all available moduls and forms.
        /// </summary>
        /// <param name="token">Get moduls with user's token.</param>
        /// <returns>All moduls for current user.</returns>
        public async Task<ContentModel> GetAllContent (string token)
        {
            using (var client = new HttpClient ()) {
                var url = new Uri (string.Concat (BaseUrl, "/GetForm"));

                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode) {
                    try {
                        var result = await response.Content.ReadAsStringAsync ();
                        var responseModel = JsonConvert.DeserializeObject<ContentModel> (result);
                        return responseModel;
                    } catch (Exception e) {
                        var message = e.Message;
                    }
                }
                throw new Exception (response.ReasonPhrase);
            }
        }

        /// <summary>
        ///     Post all filled content from mobile part.
        /// </summary>
        /// <param name="model">Filled model with form.</param>
        /// <returns>True or false. Depends on status code.</returns>
        public async Task<bool> PostAllContent (ContentModel model)
        {
            using (var client = new HttpClient ()) {
                var url = new Uri (string.Concat (BaseUrl, "/AddCapture"));
                var json = JsonConvert.SerializeObject (model);
                var content = new StringContent (json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync (url, content);
                if (response.IsSuccessStatusCode) {
                    return true;
                }
                // ToDo: need implement handling for exception from API side
                return false;
            }
        }
    }
}