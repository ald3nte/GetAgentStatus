using GetAgentsStatus.Connectors;
using GetAgentsStatus.DTO;
using GetAgentsStatus.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetAgentsStatus.Repositories
{
    public class AgentStatusRepository : IAgentStatusRepository
    {
        private readonly ILogger<AgentStatusRepository> _logger;
        private readonly string _connectionString;
        public AgentStatusRepository(ILogger<AgentStatusRepository> logger, IConfiguration config)
        {
            _logger = logger;
            _connectionString = config.GetValue<string>("PostgreConnectionString:ConnectionString");
        }

        public IList<AgentStatus> GetAgentsStatusList()
        {
            IList<AgentStatus> AgentsStatusList = new List<AgentStatus>();

            try
            {
                using var con = new NpgsqlConnection(_connectionString);

                con.Open();
                _logger.LogInformation("Open connection");

                var query = @"
                        SELECT q_num,ag_num,max(timeofupdate) as timeOfUpdate 
                        FROM callcent_ag_queuestatus
                        GROUP BY q_num,ag_num
                        ";

                NpgsqlCommand command = new NpgsqlCommand(query, con);

                NpgsqlDataReader output = command.ExecuteReader();
                _logger.LogInformation("Send query...");

                while (output.Read())
                {
                    AgentsStatusList.Add(new AgentStatus { QueueNumber = output[0].ToString(), Extension = output[1].ToString()
                                                            , TimeOfChangeStatus =(DateTime)output[2] });
                    
                    _logger.LogInformation("Insert row: Queue number =  {queueExt}, agent ext number = {agentExt}, timeOfChangeStatus = {TimeOfChangeStatus}"
                            , output[0].ToString(), output[1].ToString(), output[2].ToString());
                   
                }

                con.Close();
            }
            catch (Exception e)
            {
                _logger.LogError("Connection to postgreSql failed. Error message : " + e.Message);
            }

            _logger.LogInformation("Closing connection..");
            return AgentsStatusList;


        }
    
    }
}
