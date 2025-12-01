
namespace MiWebApp.Interfaces;

using MiWebApp.Models;


public interface IPeliculaRepository
{
    
    List<Pelicula> GetAll();
    Pelicula GetById(int idPeli);
    void Add(Pelicula peli);

    void Update(int idPeli,Pelicula peli);

    void Delete(int idPeli);

}