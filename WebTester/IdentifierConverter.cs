using CompileFlagTester;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebTester
{
    public class IdentifierConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
            => objectType == typeof(Identifier);

        // this converter is only used for serialization, not to deserialize
        public override bool CanRead => true;

        // implement this if you need to read the string representation to create an AccountId
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
#if ID_GUID
            return new Identifier(Guid.Parse(reader.Value.ToString()));
#else
            return new Identifier(long.Parse(reader.Value.ToString()));
#endif
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Identifier accountId))
                throw new JsonSerializationException("Expected AccountId object value.");

            // custom response 
            writer.WriteValue(accountId.Id);
        }
    }

    public class IdentifierTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var s = value as string;
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Identifier>(@"""" + value.ToString() + @"""");
        }
    }
}
