using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTest.Client
{
    public static class HttpHelper
    {
        public static readonly Uri BaseUri = new Uri("http://localhost:57844/");

        async public static Task<T> GetAsync<T>(string uri)
        {
            using (var http = new HttpClient { BaseAddress = BaseUri })
            {
                var response = await http.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public static Task<T> GetAsync<T>(string uri, object query) =>
            GetAsync<T>(AddQuery(uri, query));

        async public static Task<HttpStatusCode> GetAsync_StatusCode(string uri)
        {
            using (var http = new HttpClient { BaseAddress = BaseUri })
            {
                var response = await http.GetAsync(uri);
                return response.StatusCode;
            }
        }

        public static string AddQuery(string uri, object query) => $"{uri}?{query.ToFormUrlEncoded()}";

        public static string ToFormUrlEncoded(this object value)
        {
            using (var content = new FormUrlEncodedContent(value.EnumerateProperties()))
                return content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static IEnumerable<KeyValuePair<string, string>> EnumerateProperties(this object value) =>
             TypeDescriptor.GetProperties(value)
                .Cast<PropertyDescriptor>()
                .Select(d => new KeyValuePair<string, string>(d.Name, d.GetValue(value)?.ToString()));
    }
}
