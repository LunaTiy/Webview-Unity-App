using UnityEngine;

namespace CodeBase.Infrastructure.Data.Extensions
{
    public static class DataExtensions
    {
        public static string ToJson<T>(this T data) =>
            JsonUtility.ToJson(data);
        
        public static T FromJson<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}