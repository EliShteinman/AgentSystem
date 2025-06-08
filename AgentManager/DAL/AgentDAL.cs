using AgentManager.Models;
using MySql.Data.MySqlClient;
using AgentManager.Enums;
namespace AgentManager.DAL;

public static class AgentDAL
{
    private const string ConnectionString = "user=root;" +
                                            "Server=localhost;" +
                                            "Database=eagleEyeDB;" +
                                            "port=3306";
    private static readonly MySqlConnection Conn = new MySqlConnection(ConnectionString);


    public static void AddAgent(Agent agent)
    {
        var sql = """
            INSERT INTO agents (
                id, 
                codeName, 
                realName, 
                location, 
                status, 
                missionsCompleted
            ) 
            VALUES (
                @id,
                @codeName, 
                @realName, 
                @location, 
                @status, 
                @missionsCompleted
            );
            """;
        var cmd = new MySqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@id", agent.Id);
        cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
        cmd.Parameters.AddWithValue("@realName", agent.RealName);
        cmd.Parameters.AddWithValue("@location", agent.Location);
        cmd.Parameters.AddWithValue("@status", agent.Status.ToString());
        cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);
        try
        {
            Conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Conn.Close();
        }
    }

    public static List<Agent> GetAllAgents()
    {
        var sql ="select * from agents;";
        var cmd = new MySqlCommand(sql, Conn);
        List<Agent> agents = [];
        try
        {
            Conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var agent = new Agent(
                    reader.GetInt32("id"),
                    reader.GetString("codeName"),
                    reader.GetString("realName"),
                    reader.GetString("location"),
                    Enum.Parse<AgentStatus>(reader.GetString("status")),
                    reader.GetInt32("missionsCompleted"),
                    false
                    );
                agents.Add(agent);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Conn.Close();
        }
        return agents;
    }

    public static void UpdateAgentLocation(int agentId, string newLocation)
    {
        var sql = """
                  UPDATE agents
                  SET location = @newLocation
                  WHERE id = @agentId;
                  """;
        var cmd = new MySqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@agentId", agentId);
        cmd.Parameters.AddWithValue("@newLocation", newLocation);
        try
        {
            Conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Conn.Close();
        }
    }

    public static void DeleteAgent(int agentId)
    {
        var sql = """
                  DELETE from agents
                  WHERE id = @agentId;
                  """;
        var cmd = new MySqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@agentId", agentId);
        try
        {
            Conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);;
            throw;
        }
        finally
        {
            Conn.Close();
        }
    }

    public static List<Agent> SearchAgentsByCode(string partialCode)
    {
        var sql = """
                  SELECT * FROM agents
                  WHERE codeName LIKE @partialCode;
                  """;
        var cmd = new MySqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@partialCode", $"%{partialCode}%");
        List<Agent> agents = [];
        try
        {
            Conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Agent agent = new Agent(
                    reader.GetInt32("id"),
                    reader.GetString("codeName"),
                    reader.GetString("realName"),
                    reader.GetString("location"),
                    Enum.Parse<AgentStatus>(reader.GetString("status")),
                    reader.GetInt32("missionsCompleted"),
                    false
                    
                );
                agents.Add(agent);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Conn.Close();
        }
        return agents;
    }

    public static Dictionary<string, int> CountAgentsByStatus()
    {
        var sql = """
                  SELECT status, count(*) as count
                  FROM agents
                  GROUP BY status;
                  """;
        var cmd = new MySqlCommand(sql, Conn);
        Dictionary<string, int> counts = [];
        try
        {
            Conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                counts.Add(reader.GetString("status"), reader.GetInt32("count"));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Conn.Close();
        }
        return counts;
    }

    public static void AddMissionCount(int agentId, int missionsToAdd)
    {
        var sql = """
                  UPDATE agents
                  SET missionsCompleted = missionsCompleted + @missionsToAdd
                  WHERE id = @agentId;
                  """;
        var cmd = new MySqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@agentId", agentId);
        cmd.Parameters.AddWithValue("@missionsToAdd", missionsToAdd);
        try
        {
            Conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Conn.Close();
        }
    }

    public static void UpdateStatusAgents(int agentId, AgentStatus newStatus)
    {
        var sql = """
                  UPDATE agents
                  SET status = @newStatus
                  WHERE id = @agentId
                  """;
        var cmd = new MySqlCommand(sql, Conn);
        cmd.Parameters.AddWithValue("@agentId", agentId);
        cmd.Parameters.AddWithValue("@newStatus", newStatus);
        try
        {
            Conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Conn.Close();
        }
    }
}