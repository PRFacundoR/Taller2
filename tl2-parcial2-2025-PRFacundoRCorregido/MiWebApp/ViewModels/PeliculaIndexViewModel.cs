namespace MiWebApp.ViewModels;
using MiWebApp.Models;

public class PeliculaIndexViewModel
{
    
    public int Id {get; set;}
    public string Titulo {get; set;}

    public int Anio {get; set;}

    public Categoria categoria {get; set;}

    public PeliculaIndexViewModel(){}


    public PeliculaIndexViewModel(Pelicula p)
    {
        Id=p.Id;
        Titulo=p.Titulo;
        Anio=p.Anio;
        categoria=p.categoria;
    }

    public PeliculaIndexViewModel(int id)
    {
        Id=id;
      
    }

    


}