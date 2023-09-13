using Newtonsoft.Json;
using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

public class DictionaryVector2IntJsonConverter : JsonConverter<Dictionary<Vector2Int, CropsTile>>
{
    public override Dictionary<Vector2Int, CropsTile> ReadJson(JsonReader reader, Type objectType, Dictionary<Vector2Int, CropsTile> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var result = new Dictionary<Vector2Int, CropsTile>();

        if (reader.TokenType == JsonToken.StartObject)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                {
                    return result;
                }

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var keyString = reader.Value.ToString();
                    var key = ParseVector2IntFromKeyString(keyString);

                    reader.Read();
                    var value = serializer.Deserialize<CropsTile>(reader);
                    result[key] = value;
                }
            }
        }

        return result;
    }

    public override void WriteJson(JsonWriter writer, Dictionary<Vector2Int, CropsTile> value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        foreach (var pair in value)
        {
            var keyString = pair.Key.ToString();
            writer.WritePropertyName(keyString);
            serializer.Serialize(writer, pair.Value);
        }

        writer.WriteEndObject();
    }

    private Vector2Int ParseVector2IntFromKeyString(string keyString)
    {
        var components = keyString.Trim('(', ')').Split(',');
        if (components.Length == 2 && int.TryParse(components[0], out int x) && int.TryParse(components[1], out int y))
        {
            return new Vector2Int(x, y);
        }
        return Vector2Int.zero;
    }
}