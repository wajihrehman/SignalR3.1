using Final.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final.Interfaces
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(List<Years> year);

    }
}
