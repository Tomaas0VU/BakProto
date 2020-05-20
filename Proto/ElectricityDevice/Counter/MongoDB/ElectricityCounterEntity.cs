using MongoDB.Bson.Serialization.Attributes;
using System;
using ElectricityDevice.Models;

namespace ElectricityDevice.Counter.MongoDB
{
    public class ElectricityCounterEntity : ElectricityDeviceConfig
    {
        [BsonId]
        public string SerialNo { get; set; }
    }
}
