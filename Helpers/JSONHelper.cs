using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CryptoWebService.Helpers
{
    public static class JSONHelper
    {
        public static Object TransformListToJsonArray<T>(List<T> list)
        {
            JArray jArray = new JArray();
            for (int i = 0; i < list.Count; i++)
            {
                jArray.Add(new JObject(new JProperty("value", list[i])));
            }
            return jArray;
        }

        public static Object TransformArrayToJsonArray<T>(T[] list)
        {
            JArray jArray = new JArray();
            for (int i = 0; i < list.Length; i++)
            {
                jArray.Add(new JObject(new JProperty("value", list[i])));
            }
            return jArray;
        }
    }
}