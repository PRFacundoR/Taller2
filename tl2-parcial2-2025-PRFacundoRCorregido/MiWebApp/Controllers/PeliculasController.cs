using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace MiWebApp.Controllers;

using MiWebApp.Repositorios;
using MiWebApp.ViewModels;
using MiWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiWebApp.Interfaces;
public class PeliculasController : Controller
{

    private readonly IPeliculaRepository peliculas;
    private readonly IAutentificarService autorizacion;

    public PeliculasController(IPeliculaRepository pel, IAutentificarService auto)
    {
        peliculas = pel;
        autorizacion = auto;
    }

    private IActionResult CheckAdminPermissions()
    {
        
        if (!autorizacion.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }

        
        if (!autorizacion.HasAccesLevel("Administrador"))
        {
            
            return RedirectToAction(nameof(AccesoDenegado));
        }
        return null; 
    }



    [HttpGet]
    public IActionResult Index()
    {
        if (!autorizacion.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }

        if (autorizacion.HasAccesLevel("Administrador") || autorizacion.HasAccesLevel("Cliente"))
        {
            List<Pelicula> pelis = peliculas.GetAll();
            List<PeliculaIndexViewModel> presupuestosVM = pelis.Select(p => new PeliculaIndexViewModel(p)).ToList();
            return View(presupuestosVM);
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }

    }
    public IActionResult AccesoDenegado()
    {
        
        return View();
    }



    [HttpGet]
    public IActionResult Details(int id)
    {
        if (!autorizacion.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }

        if (autorizacion.HasAccesLevel("Administrador") || autorizacion.HasAccesLevel("Cliente"))
        {


            List<Pelicula> pelis = peliculas.GetAll();

            var aux = pelis.FirstOrDefault(p => p.Id == id);
            return View(new PeliculaIndexViewModel(aux));
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }

    }

    [HttpPost]

    public IActionResult Details(PeliculaIndexViewModel pelivm)
    {
        if (!autorizacion.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }

        if (autorizacion.HasAccesLevel("Administrador") || autorizacion.HasAccesLevel("Cliente"))
        {
            var aux = peliculas.GetById(pelivm.Id);
            var aux1 = new PeliculaIndexViewModel(aux);
            return View(aux1);
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }

    }









    [HttpGet]

    public IActionResult Create()
    {

        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        List<Pelicula> pelis = peliculas.GetAll();




        var items = Enum.GetValues(typeof(Categoria)).Cast<Categoria>().Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList();
        var pvm = new PeliculaCreateViewModel
        {

            ListaCategoria = new SelectList(items, "Value", "Text")

        };
        return View(pvm);
    }


    [HttpPost]

    public IActionResult Create(PeliculaCreateViewModel pVM)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

         if (!(pVM.Anio <= DateTime.Now.Year))
        {
            ModelState.AddModelError("Anio", "La fecha no puede ser posterior a este año");


        }
        if (!ModelState.IsValid)
        {
            List<Pelicula> pelis = peliculas.GetAll();
            var items = Enum.GetValues(typeof(Categoria)).Cast<Categoria>().Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList();
            pVM.ListaCategoria = new SelectList(items, "Value", "Text");

            return View(pVM);
        }




        Pelicula nuevaP = new Pelicula(pVM);

        peliculas.Add(nuevaP);
        return RedirectToAction("Index");



    }

















    [HttpGet]
    public IActionResult Edit(int id)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        List<Pelicula> pelis = peliculas.GetAll();
        var peliEncontrado = pelis.FirstOrDefault(p => p.Id == id);

        if (peliEncontrado == null)
            return NotFound();


        

        var items = Enum.GetValues(typeof(Categoria)).Cast<Categoria>().Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList();
        var pvm = new PeliculaUpdateViewModel
        {
            Id=id,
            ListaCategoria = new SelectList(items, "Value", "Text")

        };


        var vm = new PeliculaUpdateViewModel(peliEncontrado);
        return View(pvm);
    }



    [HttpPost]
    public IActionResult Edit(PeliculaUpdateViewModel pvm)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (!(pvm.Anio <= DateTime.Now.Year))
        {
            ModelState.AddModelError("Anio", "La fecha no puede ser posterior a este año");


        }
        if (!ModelState.IsValid)
        {
            List<Pelicula> pelis = peliculas.GetAll();
            var items = Enum.GetValues(typeof(Categoria)).Cast<Categoria>().Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList();
            pvm.ListaCategoria = new SelectList(items, "Value", "Text");

            return View(pvm);
        }


        var peli = new Pelicula
        {
            Id = pvm.Id,
            Titulo = pvm.Titulo,
            Anio = pvm.Anio,
            categoria = pvm.categoria
        };
        peliculas.Update(peli.Id, peli);
        return RedirectToAction("Index");
    }











    [HttpGet]
    public IActionResult Delete(int id)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        return View(new PeliculaIndexViewModel(id));
    }



    [HttpPost]
    public IActionResult Delete(PeliculaIndexViewModel peli)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        peliculas.Delete(peli.Id);
        return RedirectToAction("Index");
    }





}