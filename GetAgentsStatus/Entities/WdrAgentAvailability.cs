using System;
using System.Collections.Generic;
using System.Text;

namespace GetAgentsStatus.Entities
{
    public class WdrAgentAvailability
    {
        public Guid WdrAgentAvailabilityRowguid { get;protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Extension { get; protected set; }
        public string QueueName { get; protected set; }
        public string QueueNumber { get; protected set; }
        public string Phone { get; protected set; }
        public string Status { get; protected set; }
        public string Tag { get;  set; }
        public DateTime TimeOfChangeStatus { get; protected set; }
        public DateTime? TimeOfCreation { get; protected set; }
        public DateTime? TimeOfModification { get; protected set; }

        public WdrAgentAvailability(string firstName, string lastName, string extension, string queueName, string queueNumber, string phone, string status, DateTime timeOfChangeStatus)
        {
            FirstName = firstName;
            LastName = lastName;
            Extension = extension;
            QueueName = queueName;
            QueueNumber = queueNumber;
            Phone = phone;
            Status = status;
            TimeOfChangeStatus = timeOfChangeStatus;
            WdrAgentAvailabilityRowguid = Guid.NewGuid();
            Tag = "";
            TimeOfCreation = DateTime.Now;
            TimeOfModification = DateTime.Now;

        }
    }
}
