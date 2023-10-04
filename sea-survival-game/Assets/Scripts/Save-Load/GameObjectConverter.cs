using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectConverter : JsonConverter<GameObject>
{
    public override void WriteJson(JsonWriter writer, GameObject value, JsonSerializer serializer)
    {
        // Create a serializable data structure to hold GameObject information
        var data = new SerializableGameObjectData
        {
            Name = value.name,
            Position = value.transform.position,
            Rotation = value.transform.rotation,
            Scale = value.transform.localScale
            // Add more properties as needed
        };

        // Serialize the data structure
        serializer.Serialize(writer, data);
    }

    public override GameObject ReadJson(JsonReader reader, System.Type objectType, GameObject existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Deserialize the data structure
        var data = serializer.Deserialize<SerializableGameObjectData>(reader);

        // Create a new GameObject using the deserialized data
        GameObject newGameObject = new GameObject(data.Name);
        newGameObject.transform.position = data.Position;
        newGameObject.transform.rotation = data.Rotation;
        newGameObject.transform.localScale = data.Scale;
        // Add more components and properties as needed

        return newGameObject;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class SerializableGameObjectData
{
    [JsonProperty]
    public string Name { get; set; }

    [JsonProperty]
    public Vector3 Position { get; set; }

    [JsonProperty]
    public Quaternion Rotation { get; set; }

    [JsonProperty]
    public Vector3 Scale { get; set; }

    // Add more properties as needed
}
