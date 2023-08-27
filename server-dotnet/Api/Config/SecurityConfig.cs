namespace Api.Config;

public class SecurityConfig : IConfig
{
    public string[] TestHosts { get; set; }

    public SecurityConfig(string testHosts)
    {
        TestHosts = new[] { testHosts };
    }
}