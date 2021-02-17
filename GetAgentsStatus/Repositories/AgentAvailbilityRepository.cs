using GetAgentsStatus.Connectors;
using GetAgentsStatus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetAgentsStatus.Repositories
{
    public class AgentAvailbilityRepository : IAgentAvailbilityRepository
    {
        private readonly ILogger<AgentAvailbilityRepository> _logger;
        private readonly AppDbContext _appDbContext;

        public AgentAvailbilityRepository(ILogger<AgentAvailbilityRepository> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public void AddAgentToDatabase(IList<WdrAgentAvailability> extensionQueueInfos)
        {

                _appDbContext.WdrAgentAvailabilities.AddRange(extensionQueueInfos);
                try
                {
                    _appDbContext.SaveChanges();
                    _logger.LogInformation("Successful data addition");
                }
                catch (DbUpdateException e)
                {
                    _logger.LogError("Error adding data. Error message: " + e.Message);
                }

        }

        public void DeactivateOldActivity()
        {
            var OldAgentActivities = _appDbContext.WdrAgentAvailabilities.Where( a=> a.Tag == null ||
                                                                                      a.Tag == "").ToList();
            OldAgentActivities.ForEach(a => a.Tag = "0");
            _appDbContext.WdrAgentAvailabilities.UpdateRange(OldAgentActivities);
            _appDbContext.SaveChanges();
        }
    }
}
