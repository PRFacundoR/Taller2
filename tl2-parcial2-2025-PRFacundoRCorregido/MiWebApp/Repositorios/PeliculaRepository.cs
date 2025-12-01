namespace MiWebApp.Repositorios;

using Microsoft.Data.Sqlite;
using MiWebApp.Interfaces;
using MiWebApp.Models;

public class PeliculaRepository : IPeliculaRepository
{

    private string _coneccionADB = "Data Source=DB/streaming.db";
    public List<Pelicula> GetAll()
    {

        string sql_query = "SELECT Id, Titulo, Anio, Categoria FROM Peliculas ORDER BY Titulo ASC;";
        List<Pelicula> pelis = [];

        using var conecttion = new SqliteConnection(_coneccionADB);
        conecttion.Open();

        var comando = new SqliteCommand(sql_query, conecttion);

        using (SqliteDataReader reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                var peli = new Pelicula();
                peli.Id = Convert.ToInt32(reader["Id"]);
                peli.Titulo = reader["Titulo"].ToString();
                peli.Anio = Convert.ToInt32(reader["Anio"]);
                peli.CategoriaString = reader["Categoria"].ToString();
                Categoria cat;
                Enum.TryParse<Categoria>(peli.CategoriaString, true, out cat);
                peli.categoria = cat;

                pelis.Add(peli);

            }
            conecttion.Close();

        }
        return pelis;
    }

    public Pelicula GetById(int idPeli)
    {
        using (var conexion = new SqliteConnection(_coneccionADB))
        {
            conexion.Open();
            string sql = "SELECT Id, Titulo, Anio, Categoria FROM Peliculas WHERE Id = @Id;";
            using (var comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", idPeli);
                using (var lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        var peli = new Pelicula();
                        peli.Id = Convert.ToInt32(lector["Id"]);
                        peli.Titulo = lector["Titulo"].ToString();
                        peli.Anio = Convert.ToInt32(lector["Anio"]);
                        peli.CategoriaString = lector["Categoria"].ToString();
                        Categoria cat;
                        Enum.TryParse<Categoria>(peli.CategoriaString, true, out cat);
                        peli.categoria = cat;


                        return peli;
                    }
                    else
                    {
                        return null; 
                    }
                }

            }

        }
    }







    public void Add(Pelicula peli)
    {


        using (SqliteConnection coneccion = new SqliteConnection(_coneccionADB))
        {
            coneccion.Open();
            string sql = "INSERT INTO Peliculas (Titulo, Anio, Categoria) VALUES (@Titulo, @Anio, @Categoria); ";
            using (var comando = new SqliteCommand(sql, coneccion))
            {

                comando.Parameters.Add(new SqliteParameter("@Titulo", peli.Titulo));
                comando.Parameters.Add(new SqliteParameter("@Anio", peli.Anio));
                comando.Parameters.Add(new SqliteParameter("@Categoria", peli.categoria.ToString()));
                comando.ExecuteNonQuery();
            }

        }

    }

    public void Update(int idPeli, Pelicula peli)
    {
        using (SqliteConnection conexion = new SqliteConnection(_coneccionADB))
        {
            conexion.Open();
            string sql = "UPDATE Peliculas SET Titulo = @Titulo, Anio = @Anio, Categoria = @Categoria WHERE Id = @Id;";
            using (var comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Titulo", peli.Titulo);
                comando.Parameters.AddWithValue("@Anio", peli.Anio);
                comando.Parameters.AddWithValue("@Categoria", peli.categoria);
                comando.Parameters.AddWithValue("@Id", idPeli);
                comando.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int idPeli)
    {

        using (var conexion = new SqliteConnection(_coneccionADB))
        {
            conexion.Open();

            string sql = "DELETE FROM Peliculas WHERE Id = @Id;";
            using (var comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.Add(new SqliteParameter("@Id", idPeli));
                comando.ExecuteNonQuery();
            }


        }

    }
}