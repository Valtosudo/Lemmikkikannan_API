using Microsoft.Data.Sqlite;

public class Ihmiset
{
    public string? Nimi { get; set; }
    public int Puhelin { get; set; }
    public int Id { get; set; }
}

public class Elaimet
{
    public string? Nimi { get; set; }
    public string? Rotu { get; set; }
    public int OmistajaId { get; set; }
    public int Id { get; set; }
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

            command.CommandText = @"CREATE TABLE IF NOT EXISTS Lemmikit (id INTEGER PRIMARY KEY, Nimi TEXT, Rotu TEXT, OmistajaId int, FOREIGN KEY(OmistajaId) REFERENCES Henkilot(id));";
            command.ExecuteNonQuery();
        }
    }
    public void LisaaHenkilo(string nimi, int puhelin, int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Henkilot (Nimi, Puhelin, Id) VALUES ($nimi, $puhelin, $Id);";
            command.Parameters.AddWithValue("$nimi", nimi);
            command.Parameters.AddWithValue("$puhelin", puhelin);
            command.Parameters.AddWithValue("$Id", id);
            command.ExecuteNonQuery();
        }
    }
    public void LisaaLemmikki(string nimi, string rotu, int omistajaId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Lemmikit (Nimi, Rotu, OmistajaId) VALUES ($nimi, $rotu, $omistajaId);";
            command.Parameters.AddWithValue("$nimi", nimi);
            command.Parameters.AddWithValue("$rotu", rotu);
            command.Parameters.AddWithValue("$omistajaId", omistajaId);
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
            command.CommandText = @"SELECT H.Puhelin FROM Henkilot H JOIN Lemmikit L ON H.id = L.OmistajaId WHERE L.Nimi = $lemmikinNimi;";
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