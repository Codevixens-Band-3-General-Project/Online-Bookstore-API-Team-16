using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BookstoreAPI.Extensions
{
    public static class SessionExtensions
    {
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var jsonString = session.GetString(key);
            return jsonString == null ? default : JsonSerializer.Deserialize<T>(jsonString);
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            var jsonString = JsonSerializer.Serialize(value);
            session.SetString(key, jsonString);
        }
    }
}


