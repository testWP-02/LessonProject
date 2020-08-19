using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsProject.Client.Helpers
{
    public class HttpClientWithToken
    {
        public HttpClient _httpClient { get; }
        public HttpClientWithToken(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    }
}
