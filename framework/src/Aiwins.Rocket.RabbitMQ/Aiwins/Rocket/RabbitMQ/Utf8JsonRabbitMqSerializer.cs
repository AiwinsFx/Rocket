﻿using System;
using System.Text;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Json;

namespace Aiwins.Rocket.RabbitMQ {
    public class Utf8JsonRabbitMqSerializer : IRabbitMqSerializer, ITransientDependency {
        private readonly IJsonSerializer _jsonSerializer;

        public Utf8JsonRabbitMqSerializer (IJsonSerializer jsonSerializer) {
            _jsonSerializer = jsonSerializer;
        }

        public byte[] Serialize (object obj) {
            return Encoding.UTF8.GetBytes (_jsonSerializer.Serialize (obj));
        }

        public object Deserialize (byte[] value, Type type) {
            return _jsonSerializer.Deserialize (type, Encoding.UTF8.GetString (value));
        }
    }
}