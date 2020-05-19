using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DataReplicator.Models
{
    public class Message
    {
        [BsonId]
        public object Id { get; set; }
        public string SerialNo { get; set; }
        public string DeviceName { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}
