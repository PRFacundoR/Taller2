using AccesoADatosJson;
using Microsoft.AspNetCore.Mvc;
using ProgramTv;
using APIRest.Services;

namespace tvProgramControl
{
    [ApiController]
    [Route("[controller]")]
    public class TvProgramController : ControllerBase
    {
        private readonly IAccesoADatos _datos;
        private readonly List<TvProgram> _tvProgram;

        public TvProgramController()
        {
            _datos = new AccesoADatos();
            _tvProgram = _datos.leerArchivo();
        }


        [HttpGet]
        [Route("api/programs")]
        public ActionResult<TvProgram> obtenerLista()
        {
            if (_tvProgram == null)
            {
                return BadRequest("Error al obtener lista");
            }
            else
            {
                return Ok(_tvProgram);
            }
        }

        [HttpPost]
        [Route("api/programs")]
        public ActionResult<TvProgram> agregarPrograma(string title, string genre, int dia, int start, int duration)
        {
            var mismo = _tvProgram.FirstOrDefault(t => t.DiaDeLaSemana == dia);

            if (mismo != null)
            {
                if (mismo.DiaDeLaSemana == dia && mismo.StartTime == start)
                {
                    return BadRequest("Ya hay un programa ese dia a esa hora");
                }
            }          
            if (duration == 30 || duration == 60 || duration == 120)
            {
                int idi = _tvProgram.Max(m => m.Id) + 1;
                TvProgram programa = new TvProgram(idi, title, genre, dia, start, duration);
                _tvProgram.Add(programa);
                _datos.guardarArchivo(_tvProgram);
                return Ok(programa);
            }
            else
            {
                return BadRequest("La duracion debe ser exactamente de 30, 60 o 120 min");
            }
        }

        [HttpPut]
        [Route("api/programs/{id}")]
        public ActionResult<TvProgram> modificarPrograma(int id, string title, string genre)
        {
            var programa = _tvProgram.FirstOrDefault(p => p.Id == id);

            if (programa == null)
            {
                return NotFound("No se encontro el programa con esa id");
            }

            programa.Title = title;
            programa.Genre = genre;

            _datos.guardarArchivo(_tvProgram);
            return Ok(programa);
        }

        [HttpDelete]
        [Route("api/programs/{id}")]
        public ActionResult<TvProgram> elminarPrograma(int id)
        {
            var programa = _tvProgram.Find(p => p.Id == id);
            if (programa == null)
            {
                return NotFound("No se encontro el programa a eliminar");
            }

            _tvProgram.Remove(programa);
            _datos.guardarArchivo(_tvProgram);
            return NoContent();
        }

        [HttpGet]
        [Route("api/programs/by-day/{day}")]
        public ActionResult<TvProgram> listarProgramaDia(int day)
        {
            var programas = _tvProgram.Where(p => p.DiaDeLaSemana == day);
            if (programas == null)
            {
                return BadRequest("Error intero");
            }

            if (programas.Count() == 0)
            {
                return NotFound("No se encotraron programas");
            }

            return Ok(programas);
        }
    }
}