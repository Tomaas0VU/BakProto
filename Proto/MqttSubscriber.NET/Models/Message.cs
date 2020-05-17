using System;

namespace MqttSubscriber.NET.Models
{
    public class Message
    {
        public string SerialNo { get; set; }
        public string DeviceName { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}
