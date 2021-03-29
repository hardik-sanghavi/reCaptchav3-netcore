using Newtonsoft.Json.Linq;
using reCaptchav3.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace reCaptchav3.Repository.Services
{
    public class CaptchaValidator : ICaptchaValidator
    {
        private readonly HttpClient _httpClient;
        private const string RemoteAddress = "https://www.google.com/recaptcha/api/siteverify";
        private string _secretKey;

        public CaptchaValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _secretKey = "ijiias";
        }

        public async Task<bool> IsCaptchaPassedAsync(string token)
        {
            dynamic response = await GetCaptchaResultDataAsync(token);
            return response.success == "true";
        }

        public async Task<JObject> GetCaptchaResultDataAsync(string token)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token)
            });
            var res = await _httpClient.PostAsync(RemoteAddress, content);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException(res.ReasonPhrase);
            }
            var jsonResult = await res.Content.ReadAsStringAsync();
            return JObject.Parse(jsonResult);
        }
    }
}
