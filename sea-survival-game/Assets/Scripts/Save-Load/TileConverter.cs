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
            // Serialize a Tile object to its name, sprite name, collider type, and color
            var tile = (Tile)value;
            var data = new SerializableTileData
            {
                TileName = tile.name, // You may need to adjust this depending on your Tile setup
                SpriteName = tile.sprite != null ? tile.sprite.name : null, // Save the sprite's name
                ColliderType = tile.colliderType,
                TileColor = tile.color // Save the color of the tile
            };
            serializer.Serialize(writer, data);
        }
    }

    public override TileBase ReadJson(JsonReader reader, System.Type objectType, TileBase existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var data = serializer.Deserialize<SerializableTileData>(reader);

        // Load the tile using its name
        TileBase tile = Resources.Load<TileBase>(data.TileName); // Adjust this to your Tile setup

        // If the tile has a sprite name, load the sprite by name and set it
        if (tile is Tile && !string.IsNullOrEmpty(data.SpriteName))
        {
            var tileWithSprite = (Tile)tile;
            Sprite loadedSprite = Resources.Load<Sprite>(data.SpriteName);

            if (loadedSprite != null)
            {
                tileWithSprite.sprite = loadedSprite;
            }
            else
            {
                Debug.LogWarning("Sprite not found: " + data.SpriteName);
            }
        }

        // Set collider type and color properties
        if (tile is Tile)
        {
            var tileWithProperties = (Tile)tile;
            tileWithProperties.colliderType = data.ColliderType;
            tileWithProperties.color = data.TileColor; // Set the color of the tile
        }

        return tile;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class SerializableTileData
{
    [JsonProperty]
    public string TileName { get; set; }

    [JsonProperty]
    public string SpriteName { get; set; } // Add a property to store the sprite's name

    [JsonProperty]
    public Tile.ColliderType ColliderType { get; set; } // Add a property to store collider type

    [JsonProperty]
    public Color TileColor { get; set; } // Add a property to store the color of the tile
}