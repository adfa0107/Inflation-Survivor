using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Assertions;

namespace InflationSurvivor.Core;

[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
public class PolyConverter<TBase> : JsonConverter
{
    private static readonly Dictionary<string, Type> _derivedTypes = new Dictionary<string, Type>();

    public static void RegisterDerivedType(string name, Type derivedType)
    {
        Assert.IsTrue(derivedType.IsSubclassOf(typeof(TBase)));
        Assert.IsFalse(_derivedTypes.ContainsKey(name));
        _derivedTypes[name] = derivedType;
    }
    
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TBase);
    }
    
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject item = JObject.Load(reader);
        Assert.IsNotNull(item["type"]);
        
        string typeName = item["type"].Value<string>();
        
        Assert.IsTrue(_derivedTypes.ContainsKey(typeName));
        
        return item.ToObject(_derivedTypes[typeName], serializer);
    }

    public override bool CanWrite => false;
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

}