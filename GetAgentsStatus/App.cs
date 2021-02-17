using GetAgentsStatus.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetAgentsStatus.App
{
    public class App
    {
        private readonly IAgentAvailbilityService _agentAvailbilityService;

        public App(IAgentAvailbilityService agentAvailbilityService)
        {
            _agentAvailbilityService = agentAvailbilityService;
        }

        public void Run()
        {
            _agentAvailbilityService.DeactivateOldAgentAvailability();

            var AgentStatusList =  _agentAvailbilityService.GetAgentsAvailabilityList();

            _agentAvailbilityService.AddAgentsAvailabilityToDatabase(AgentStatusList);

        }
    }
}
