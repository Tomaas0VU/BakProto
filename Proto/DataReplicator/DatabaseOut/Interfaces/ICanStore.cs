using DataReplicator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataReplicator.DatabaseOut.Interfaces
{
    public interface ICanStore
    {
        Task InsertData(string collectionName, List<Message> data);
    }
}
