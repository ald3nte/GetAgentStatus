using GetAgentsStatus.DTO;
using GetAgentsStatus.Entities;
using GetAgentsStatus.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetAgentsStatus.Services
{
    public class AgentAvailbilityService : IAgentAvailbilityService
    {
        private readonly ILogger<AgentAvailbilityService> _logger;
        private readonly IAgentStatusRepository _agentsStatusRepository;
        private readonly IAgentAvailbilityRepository _agentAvailbilityRepository;
        private readonly ITCXRepository _tcxRepository;

        public AgentAvailbilityService(ILogger<AgentAvailbilityService> logger, IAgentStatusRepository agentsStatusRepository,
                                        IAgentAvailbilityRepository agentAvailbilityRepository, ITCXRepository tcxRepository)
        {
            _logger = logger;
            _agentsStatusRepository = agentsStatusRepository;
            _agentAvailbilityRepository = agentAvailbilityRepository;
            _tcxRepository = tcxRepository;
        }

        public IEnumerable<WdrAgentAvailability> GetAgentsAvailabilityList()
        {
            _logger.LogInformation("get List WdrAgentAvailability to add to database");

            var AgentsStatus = _agentsStatusRepository.GetAgentsStatusList();

            var AgentsAvailability = _tcxRepository.GetAgentsAvailabilityList();

            var result = AgentsStatus.Join(AgentsAvailability, arg => new { arg.QueueNumber, arg.Extension }, arg => new { arg.QueueNumber, arg.Extension },
                (f, s) => new WdrAgentAvailability
                (
                    s.FirstName,
                    s.LastName,
                    f.Extension,
                    s.QueueName,
                    s.QueueNumber,
                    s.Phone,
                    s.Status,
                    f.TimeOfChangeStatus
                ));


            return result;
        }

        public void AddAgentsAvailabilityToDatabase(IEnumerable<WdrAgentAvailability> extensionQueueInfos)
        {
            if (extensionQueueInfos.Count() ==0)
            {
                _logger.LogInformation("There is no data to add.");
            }
            else
            {
                _logger.LogInformation("Adding data to the database.");
                _agentAvailbilityRepository.AddAgentToDatabase(extensionQueueInfos.ToList());
            }

        }

        public void DeactivateOldAgentAvailability()
        {
            _agentAvailbilityRepository.DeactivateOldActivity();
        }
    }
}
