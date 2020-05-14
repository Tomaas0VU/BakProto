using System.Threading.Tasks;

namespace MqttSubscriber.Subscribers.Interfaces
{
    public interface ITopicSubscriber
    {
        Task SubscribeAsync();
    }
}
