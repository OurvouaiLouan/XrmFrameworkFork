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

        public override ColumnCollection ReadJson(JsonReader reader, Type objectType, ColumnCollection existingValue,
                                                  bool hasExistingValue, JsonSerializer serializer)
        {
            ColumnCollection retour;

            if (hasExistingValue)
            {
                retour = existingValue;
            }
            else
            {
               retour = new ColumnCollection();
            }

            var columns = serializer.Deserialize<List<Column>>(reader);
            if (columns == null) return retour;

            foreach (var column in columns)
            {
                retour.Add(column);
            }

            return retour;
        }
    }
}
