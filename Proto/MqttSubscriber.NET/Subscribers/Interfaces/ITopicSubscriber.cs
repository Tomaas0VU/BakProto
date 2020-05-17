using System.Threading.Tasks;

namespace MqttSubscriber.NET.Subscribers.Interfaces
{
    public interface ITopicSubscriber
    {
        Task SubscribeAsync();
    }
}
