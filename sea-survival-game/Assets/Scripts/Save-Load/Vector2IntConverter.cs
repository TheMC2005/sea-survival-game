using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

public class Vector2IntConverter : JsonConverter<Vector2Int>
{
    
    public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
    {
        var token = JToken.FromObject(new { x = value.x, y = value.y });
        token.WriteTo(writer);
    }
    public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);
        int x = jObject["x"].Value<int>();
        int y = jObject["y"].Value<int>();
        return new Vector2Int(x, y);
    }
    
}
