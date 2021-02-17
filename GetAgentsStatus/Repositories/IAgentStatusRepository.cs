using GetAgentsStatus.DTO;
using GetAgentsStatus.Entities;
using System.Collections.Generic;

namespace GetAgentsStatus.Repositories
{
    public interface IAgentStatusRepository
    {
        IList<AgentStatus> GetAgentsStatusList();
    }
}