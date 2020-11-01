using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Utility
{
    static class JsonHelper
    {
        public static T LoadExternal<T>(string path) where T : class
        {
            var fileData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileData);
        }

        public static T LoadFromJson<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T Load<T>(string path) where T : class
        {
            var targetFile = Resources.Load<TextAsset>(path);
            return JsonConvert.DeserializeObject<T>(targetFile.text);
        }

        public static void Save<T>(T item, string path) where T : class
        {
            var json = JsonConvert.SerializeObject(item, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static void SaveAbstracts<T>(T item, string path) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(path))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                serializer.Serialize(writer, item, typeof(T));
            }

            Debug.Log($"{item} saved at {path}");
        }

        public static T LoadExternalAbstracts<T>(string path) where T : class => ConvertAbstract<T>(LoadExternal(path));
        public static T LoadInternalAbstracts<T>(string path) where T : class => ConvertAbstract<T>(LoadInternal(path));

        private static T ConvertAbstract<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        private static string LoadExternal(string path) => File.ReadAllText(path);
        private static string LoadInternal(string path) => Resources.Load<TextAsset>(path).text;
    }
}
