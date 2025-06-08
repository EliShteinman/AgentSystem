using AgentManager.Enums;
using AgentManager.DAL;

namespace AgentManager.Models;

public class Agent
{
    public int Id { get; protected set; } 
    public string CodeName { get; protected set; } 
    public string RealName { get; protected set; } 
    public string Location { get; protected set; } 
    public AgentStatus Status { get; protected set; } 
    public int MissionsCompleted { get; protected set; }

    public Agent(
        int id,
        string codeName,
        string realName,
        string location,
        AgentStatus status,
        int missionsCompleted = 0,
        bool creation = true)
    {
        Id = id;
        CodeName = codeName;
        RealName = realName;
        Location = location;
        Status = status;
        MissionsCompleted = missionsCompleted;
        if (creation)
        {
            AgentDAL.AddAgent(this);
        }
    }

    public void UpdateStatus(AgentStatus status)
    {
        Status = status;
        AgentDAL.UpdateStatusAgents(Id, status);
    }
    public void UpdateMissionsCompleted(int missionsCompleted)
    {
        var temp = missionsCompleted - MissionsCompleted;
        MissionsCompleted = missionsCompleted;
        AgentDAL.AddMissionCount(Id, temp);
        
    }
    public void UpdateLocation(string location)
    {
        Location = location;
        AgentDAL.UpdateAgentLocation(Id, location);
    }
}