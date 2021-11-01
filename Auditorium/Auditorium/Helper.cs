namespace Auditorium
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Auditorium.Models;

    public static class Helper
    {
        public const string BaseUrl = "http://192.168.43.246:1337";

        public static CurrentUserModel CurrentUser = null;

        public static HttpContent GetHttpContent(object obj) =>
            new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

        public static async Task<object> GetResponse(Type returnType, HttpResponseMessage httpResponseMessage)
            => JsonSerializer.Deserialize(
                await httpResponseMessage.Content.ReadAsStringAsync(),
                returnType,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

        public static void AddRange<T>(this ObservableCollection<T> collection, List<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}