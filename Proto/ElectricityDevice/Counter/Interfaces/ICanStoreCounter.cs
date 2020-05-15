using System.Threading.Tasks;

namespace ElectricityDevice.Counter.Interfaces
{
    public interface ICanStoreCounter
    {
        Task StoreCounterForDevice(string serialNo, double counter);
        Task<double> GetCounterForDevice(string serialNo);
    }
}
