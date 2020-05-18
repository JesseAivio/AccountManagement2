using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AccountManagement.Library.API.http
{
    public class HttpClientSettings
    {
        public static HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(URL.BaseAddress)
        };
        public HttpClientSettings()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}
