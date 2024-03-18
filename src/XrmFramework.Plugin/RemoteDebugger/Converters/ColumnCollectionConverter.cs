using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XrmFramework.Core;

namespace XrmFramework.RemoteDebugger.Converters
{
    public class ColumnCollectionConverter : JsonConverter<ColumnCollection>
    {
        public override void WriteJson(JsonWriter writer, ColumnCollection value, JsonSerializer serializer)
        {
            writer.WriteStartArray();

            foreach (var column in value)
            {
                serializer.Serialize(writer, column);
            }

            writer.WriteEndArray();
        }

        public override ColumnCollection ReadJson(JsonReader reader, Type objectType, ColumnCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var retour = (ColumnCollection)Activator.CreateInstance(objectType);

            var list = serializer.Deserialize<List<Column>>(reader);

            foreach (var o in list)
            {
                retour.Add((Column)o);
            }

            return retour;
        }
    }
}
