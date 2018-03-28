﻿using LanguageServer.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace LanguageServer.Infrastructure.JsonDotNet
{
    public class JsonDotNetSerializer : Serializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new JsonConverter[] { new EitherConverter() },
				ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public override object Deserialize(Type objectType, string json)
        {
            return JsonConvert.DeserializeObject(json, objectType, _settings);
        }

        public override string Serialize(Type objectType, object value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }
    }
}
