using MongoDB.Bson.Serialization.Attributes;

namespace ElectricityDevice.Counter.MongoDB
{
    public class ElectricityCounterEntity
    {
        [BsonId]
        public string SerialNo { get; set; }
        public double Value { get; set; }
    }
}
