namespace Api.Config;

public class DatabaseConfig : IConfig
{
    public string TestSource { get; set; }

    public DatabaseConfig(string testSource)
    {
        TestSource = testSource;
    }
}