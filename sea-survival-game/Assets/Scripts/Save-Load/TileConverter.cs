using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileConverter : JsonConverter<TileBase>
{
    public override void WriteJson(JsonWriter writer, TileBase value, JsonSerializer serializer)
    {
        if (value is Tile)
        {
            // Serialize a Tile object to its name or some identifying property
            var tile = (Tile)value;
            var data = new SerializableTileData
            {
                TileName = tile.name // You may need to adjust this depending on your Tile setup
            };
            serializer.Serialize(writer, data);
        }
    }

    public override TileBase ReadJson(JsonReader reader, System.Type objectType, TileBase existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var data = serializer.Deserialize<SerializableTileData>(reader);

        // Load the Tile by its name or identifying property
        TileBase tile = Resources.Load<TileBase>(data.TileName); // Adjust this to your Tile setup
        return tile;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class SerializableTileData
{
    [JsonProperty]
    public string TileName { get; set; }
}
