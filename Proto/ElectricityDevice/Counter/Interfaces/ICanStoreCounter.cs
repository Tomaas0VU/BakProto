using ElectricityDevice.Models;
using System;
using System.Threading.Tasks;

namespace ElectricityDevice.Counter.Interfaces
{
    public interface ICanStoreCounter
    {
        Task StoreConfigForDevice(string serialNo, DateTime time, double counter);
        Task<ElectricityDeviceConfig> GetConfigForDevice(string serialNo);
    }
}
