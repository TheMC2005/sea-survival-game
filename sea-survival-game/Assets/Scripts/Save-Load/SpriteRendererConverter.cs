
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


public class SpriteRendererConverter : JsonConverter<SpriteRenderer>
{
    public override void WriteJson(JsonWriter writer, SpriteRenderer value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteStartObject();

        writer.WritePropertyName("color");
        serializer.Serialize(writer, value.color);

        writer.WritePropertyName("sprite");
        serializer.Serialize(writer, value.sprite);

        writer.WritePropertyName("sortingLayerName");
        serializer.Serialize(writer, value.sortingLayerName);

        writer.WritePropertyName("sortingOrder");
        serializer.Serialize(writer, value.sortingOrder);

        writer.WriteEndObject();
    }

    public override SpriteRenderer ReadJson(JsonReader reader, Type objectType, SpriteRenderer existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonToken.StartObject)
        {
            var spriteRenderer = existingValue ?? new SpriteRenderer();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                {
                    return spriteRenderer;
                }

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = reader.Value.ToString();
                    reader.Read(); // Read the property value
                    switch (propertyName)
                    {
                        case "color":
                            if (spriteRenderer != null)
                                spriteRenderer.color = serializer.Deserialize<Color>(reader);
                            break;
                        case "sprite":
                            if (spriteRenderer != null)
                                spriteRenderer.sprite = serializer.Deserialize<Sprite>(reader);
                            break;
                        case "sortingLayerName":
                            if (spriteRenderer != null)
                                spriteRenderer.sortingLayerName = serializer.Deserialize<string>(reader);
                            break;
                        case "sortingOrder":
                            if (spriteRenderer != null)
                                spriteRenderer.sortingOrder = serializer.Deserialize<int>(reader);
                            break;
                        default:
                            reader.Skip(); // Skip other properties
                            break;
                    }
                }
            }
        }

        return existingValue;
    }
}