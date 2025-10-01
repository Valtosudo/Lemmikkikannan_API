

using Microsoft.Data.Sqlite;

public class Ihmiset
{
    public string? Nimi { get; set; }
    public int Puhelin { get; set; }
}

public class Elaimet
{
    public string? Nimi { get; set; }
    public string? Rotu { get; set; }
    public int OmistajaID { get; set; }
}

public class Taulut
{
    private string _connectionString = "Data Source = Lemmikki.db";

    public Taulut()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS Henkilot (id INTEGER PRIMARY KEY, Nimi TEXT, Puhelin INTEGER);";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE IF NOT EXISTS Lemmikit (id INTEGER PRIMARY KEY, Nimi TEXT, Rotu TEXT, OmistajaID INTEGER, FOREIGN KEY(OmistajaID) REFERENCES Henkilot(id));";
            command.ExecuteNonQuery();
        }
    }
    public void LisaaHenkilo(string nimi, int puhelin)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Henkilot (Nimi, Puhelin) VALUES ($nimi, $puhelin);";
            command.Parameters.AddWithValue("$nimi", nimi);
            command.Parameters.AddWithValue("$puhelin", puhelin);
            command.ExecuteNonQuery();
        }
    }
    public void LisaaLemmikki(string nimi, string rotu, int omistajaID)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Lemmikit (Nimi, Rotu, OmistajaID) VALUES ($nimi, $rotu, $omistajaID);";
            command.Parameters.AddWithValue("$nimi", nimi);
            command.Parameters.AddWithValue("$rotu", rotu);
            command.Parameters.AddWithValue("$omistajaID", omistajaID);
            command.ExecuteNonQuery();
        }
    }
    public void PaivitaHenkilonPuhelin(string nimi, int uusiPuhelin)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Henkilot SET Puhelin = $uusiPuhelin WHERE Nimi = $nimi;";
            command.Parameters.AddWithValue("$uusiPuhelin", uusiPuhelin);
            command.Parameters.AddWithValue("$nimi", nimi);
            command.ExecuteNonQuery();
        }
    }
    public int? NaytaPuhelin(string lemmikinNimi)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT H.Puhelin FROM Henkilot H JOIN Lemmikit L ON H.id = L.OmistajaID WHERE L.Nimi = $lemmikinNimi;";
            command.Parameters.AddWithValue("$lemmikinNimi", lemmikinNimi);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetInt32(0);
            }
            return null;
        }
    }
}