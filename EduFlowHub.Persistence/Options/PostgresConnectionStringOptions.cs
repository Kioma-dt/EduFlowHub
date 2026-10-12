namespace EduFlowHub.Persistence.Options;

public class PostgresConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings:Postgres";
    
    public string DB_HOST { get; set; }
    public string DB_PORT { get; set; }
    public string DB_NAME { get; set; }
    public string DB_USER { get; set; }
    public string DB_PASSWORD { get; set; }
}