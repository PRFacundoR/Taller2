namespace MiWebApp.ViewModels;
using MiWebApp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PeliculaCreateViewModel
{


    public int Id {get; set;}
    
    [Required(ErrorMessage ="Titulo requerido")]
    [StringLength(100, ErrorMessage ="La descripcion no puede superar los 100 caracteres",MinimumLength =2)]
    public string Titulo {get; set;}

    
    [Required(ErrorMessage ="Anio requerido")]
    public int Anio {get; set;}

    [Required(ErrorMessage ="categoria requerida")]
    public Categoria categoria {get; set;}

    public SelectList ListaCategoria { get; set; }




    public PeliculaCreateViewModel(Pelicula p)
    {
        Titulo=p.Titulo;
        Anio=p.Anio;
        categoria=p.categoria;
    }

    public PeliculaCreateViewModel()
    {}

}