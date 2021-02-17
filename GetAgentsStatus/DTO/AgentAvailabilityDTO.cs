using System;
using System.Collections.Generic;
using System.Text;

namespace GetAgentsStatus.DTO
{
   public  class AgentAvailabilityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Extension { get; set; }
        public string QueueName { get; set; }
        public string QueueNumber { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}
