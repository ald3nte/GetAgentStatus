using GetAgentsStatus.DTO;
using System.Collections.Generic;

namespace GetAgentsStatus.Repositories
{
    public interface ITCXRepository
    {
        public IList<AgentAvailabilityDTO> GetAgentsAvailabilityList();
    }
}