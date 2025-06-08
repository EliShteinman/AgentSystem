using AgentManager.DAL;
using AgentManager.Enums;
using AgentManager.Models;
namespace AgentManager;

class Program
{
    public static void Main()
    {
        var agent1 = new Agent(1, "avi", "Eli Shteinman", "elad", AgentStatus.Active, 0, true);
        var agent2 = new Agent(2, "kobi", "Izak Kakon", "bney brack", AgentStatus.Injured, 0, true);
        var agents = AgentDAL.GetAllAgents();
        foreach (var agent in agents)
        {
            Console.WriteLine($"""
                               name: {agent.RealName}, 
                               code: {agent.CodeName}, 
                               location: {agent.Location}, 
                               status: {agent.Status}, 
                               missions: {agent.MissionsCompleted}
                               """);
        }
        AgentDAL.UpdateAgentLocation(1, "jerusalem");
        foreach (var agent in agents)
        {
            Console.WriteLine($"""
                               name: {agent.RealName}, 
                               code: {agent.CodeName}, 
                               location: {agent.Location}, 
                               status: {agent.Status}, 
                               missions: {agent.MissionsCompleted}
                               """);
        }
        AgentDAL.DeleteAgent(1);
        foreach (var agent in agents)
        {
            Console.WriteLine($"""
                               name: {agent.RealName}, 
                               code: {agent.CodeName}, 
                               location: {agent.Location}, 
                               status: {agent.Status}, 
                               missions: {agent.MissionsCompleted}
                               """);
        }
        
    }
}