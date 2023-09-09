using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vector2IntConverter : JsonConverter<Vector2Int>
{
    /*public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
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
    */
    public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            try
            {
                // Deserialize the string representation of Vector2Int
                string vectorStr = (string)reader.Value;
                vectorStr = vectorStr.Trim('(', ')'); // Remove parentheses
                string[] parts = vectorStr.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int x) && int.TryParse(parts[1].Trim(), out int y))
                {
                    return new Vector2Int(x, y);
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during deserialization
            }
        }
        // Return a default value if deserialization fails
        return default(Vector2Int);
    }

    public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
    {
        // Serialize the Vector2Int as a string with parentheses
        writer.WriteValue($"({value.x}, {value.y})");
    }
}
