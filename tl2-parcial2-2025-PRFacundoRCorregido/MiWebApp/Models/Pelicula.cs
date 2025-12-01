namespace MiWebApp.Models;
using MiWebApp.ViewModels;


public enum Categoria
{
    Accion,
    Drama,
    Comedia,
    SciFi
}
public class Pelicula
{
    public int Id {get; set;}
    public string Titulo {get; set;}

    public int Anio {get; set;}

    public string CategoriaString{get; set;}


    public Categoria categoria {get; set;}

   public Pelicula(){}

   
   public Pelicula(PeliculaCreateViewModel pvm)
    {
        Titulo=pvm.Titulo;
        Anio=pvm.Anio;
        categoria=pvm.categoria;
    }
  


    
}