using AgentManager.DAL;
using AgentManager.Enums;
using AgentManager.Models;
namespace AgentManager;

class Program
{
    public static void Main()
    {
        var agent1 = new Agent(3, "avi", "Eli Shteinman", "elad", AgentStatus.Active, 0, false);
        var agent2 = new Agent(4, "Kobe", "Izak Kakon", "Beni brak", AgentStatus.Injured, 0, false);
        AgentDal.AddAgent(agent1);
        AgentDal.AddAgent(agent2);
        var agents = AgentDal.GetAllAgents();
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
        AgentDal.UpdateAgentLocation(1, "jerusalem");
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
        AgentDal.DeleteAgent(1);
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