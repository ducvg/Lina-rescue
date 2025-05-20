using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Vector3IntKeyDictionaryConverter<TValue> : JsonConverter<Dictionary<Vector3Int, TValue>>
{
    public override void WriteJson(JsonWriter writer, Dictionary<Vector3Int, TValue> value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        foreach (var kvp in value)
        {
            string key = $"({kvp.Key.x}, {kvp.Key.y}, {kvp.Key.z})";
            writer.WritePropertyName(key);
            serializer.Serialize(writer, kvp.Value);
        }
        writer.WriteEndObject();
    }

    public override Dictionary<Vector3Int, TValue> ReadJson(JsonReader reader, Type objectType, Dictionary<Vector3Int, TValue> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var result = new Dictionary<Vector3Int, TValue>();
        var obj = JObject.Load(reader);

        foreach (var property in obj.Properties())
        {
            // Parse "(x, y, z)" format
            string[] parts = property.Name.Trim('(', ')').Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);

            Vector3Int key = new Vector3Int(x, y, z);
            TValue value = property.Value.ToObject<TValue>(serializer);
            result[key] = value;
        }

        return result;
    }
}
