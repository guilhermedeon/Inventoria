using System.Data;
using Microsoft.Data.Sqlite;

namespace Inventoria.Infra.Data.UnitTests;

public class InventoriaSqLiteUnitTests
{
    [Fact]
    public void CreateDbConnection_ReturnsSqliteConnection()
    {
        using var db = new InventoriaSqLite();
        using var conn = db.CreateDbConnection();
        Assert.IsType<SqliteConnection>(conn);
    }

    [Fact]
    public void GetDbConnection_OpensConnection()
    {
        using var db = new InventoriaSqLite();
        using var conn = db.GetDbConnection();
        Assert.Equal(ConnectionState.Open, conn.State);
    }

    [Fact]
    public void ReturnDbConnection_ClosedConnection_Disposes()
    {
        using var db = new InventoriaSqLite();
        var conn = db.CreateDbConnection();
        conn.Open();
        conn.Close();

        db.ReturnDbConnection(conn);

        Assert.Equal(ConnectionState.Closed, conn.State);
    }
}