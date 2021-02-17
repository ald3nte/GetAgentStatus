using System;

namespace GetAgentsStatus.DTO

{
    public class AgentStatus
    {
        public string QueueNumber { get; set; }
        public string Extension { get; set; }
        public DateTime TimeOfChangeStatus { get; set; }

        public AgentStatus()
        {
          
        }
    }
}