namespace MiWebApp.ViewModels;

using Microsoft.AspNetCore.Mvc.Rendering;
using MiWebApp.Models;

public class PeliculaUpdateViewModel
{
    public int Id {get; set;}
    public string Titulo {get; set;}

    public int Anio {get; set;}

 
   public Categoria categoria {get; set;}

   public SelectList ListaCategoria { get; set; }


    public PeliculaUpdateViewModel(Pelicula p)
    {
        Id=p.Id;
        Titulo=p.Titulo;
        Anio=p.Anio;
        categoria=p.categoria;
    }

    public PeliculaUpdateViewModel(){}

}