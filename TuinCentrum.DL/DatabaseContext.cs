using System;
using System.Data.SqlClient;

public class DatabaseContext : IDisposable
{
    private readonly SqlConnection _connectie;

    public DatabaseContext(string connectieString)
    {
        _connectie = new SqlConnection(connectieString);
        _connectie.Open();
    }

    public void Dispose()
    {
        _connectie.Close();
    }

    public SqlCommand MaakCommando()
    {
        return _connectie.CreateCommand();
    }
}
