using GetAgentsStatus.DTO;
using GetAgentStatus.Connectors;
using GetAgentStatus.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TCX.Configuration;

namespace GetAgentsStatus.Repositories
{
    public class TCXRepository : ITCXRepository
    {
        private readonly ILogger<TCXRepository> _logger;
        private readonly Connector _connection;
        public TCXRepository(ILogger<TCXRepository> logger,IConfiguration configuration,Connector connection)
        {

            _logger = logger;
            _connection = connection;
            _connection.Connect();
        }

        private Extension[] ExtensionList
        {
            get
            {
                return PhoneSystem.Root.GetExtensions();
            }
        }

        public IList<AgentAvailabilityDTO> GetAgentsAvailabilityList()
        {
            IList<AgentAvailabilityDTO> _agentAvailabilityList = new List<AgentAvailabilityDTO>();
            if (ExtensionList.Length != 0)
            {

                foreach (var ext in ExtensionList)
                {

                    foreach (var queue in ext.QueueMembership)
                    {
                        var Agent = new AgentAvailabilityDTO
                        {
                            FirstName = ext.FirstName,
                            LastName = ext.LastName,
                            Extension = ext.Number,
                            QueueName = queue.Queue.Name,
                            QueueNumber = queue.Queue.Number,
                            Phone = GetPhoneNumber(ext.InOfficeInboundReferences),
                            Status = GetAgentStatus(ext.QueueStatus, queue.QueueStatus),
                        };
                        _agentAvailabilityList.Add(Agent);

                        _logger.LogInformation("Agent {firstName} {lastName} {ext} {number} is logged to {queue} : {status}",
                                               Agent.FirstName, Agent.LastName, Agent.Extension,
                                               Agent.Phone, Agent.QueueName, Agent.Status
                                               );
                    }
                }
            }
            else
            {
                _logger.LogDebug("No agents in queues");
            }
            return _agentAvailabilityList;
        }

        private string GetPhoneNumber(ExternalLineRule[] InboundRule)
        {
            foreach (var Rule in InboundRule)
            {
                if (Rule.Data != null && Rule.Data != "")
                {
                    return Rule.Data;
                }

            }
            return "";
        }

        private string GetAgentStatus(QueueStatusType agentStatus, QueueStatusType queueStatus)
        {
            if (agentStatus == QueueStatusType.LoggedIn && queueStatus == QueueStatusType.LoggedIn)
                return "LoggedIn";
            return "LoggedOut";
        }
    }
}
