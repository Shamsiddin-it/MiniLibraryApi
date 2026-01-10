using Npgsql;

public class ApplicationDbContext
{
    private readonly string connString = "Host=localhost;Port=5432;Database=minilibrary;Username=postgres;Password=907708429";

    public NpgsqlConnection Connection()=> new NpgsqlConnection(connString);
}