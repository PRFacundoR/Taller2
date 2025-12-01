using Microsoft.Data.Sqlite;
using MiWebApp.Interfaces;
using MiWebApp.Models;


namespace MiWebApp.Repositorios;

class UsuarioRepository : IUserRepository
{

    private string _coneccionADB = "Data Source=DB/streaming.db";
    public Usuario GetUser(string usuario, string contrasena)
    {
        Usuario user = null;
        
        const string sql = @"
 SELECT Id, Nombre, User, Pass, Rol
 FROM Usuarios
 WHERE User = @Usuario AND Pass = @Contrasena";
        using var conexion = new SqliteConnection(_coneccionADB);
        conexion.Open();
        using var comando = new SqliteCommand(sql, conexion);

        

        comando.Parameters.AddWithValue("@Usuario", usuario);
        comando.Parameters.AddWithValue("@Contrasena", contrasena);

        using var reader = comando.ExecuteReader();

        if (reader.Read())
        {
            
            user = new Usuario
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                User = reader.GetString(2),
                Pass = reader.GetString(3),
                Rol = reader.GetString(4)
            };
        }
        return user;
    }
}