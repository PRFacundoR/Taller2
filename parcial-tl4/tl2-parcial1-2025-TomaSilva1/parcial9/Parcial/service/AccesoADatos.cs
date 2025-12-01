using ProgramTv;
using System.Text.Json;
using APIRest.Services;

namespace AccesoADatosJson
{
    public class AccesoADatos : IAccesoADatos
    {
        private readonly string _ruta;

        public AccesoADatos()
        {
            _ruta = "data.json";
        }

        public List<TvProgram> leerArchivo()
        {
            if (!File.Exists(_ruta))
            {
                return new List<TvProgram>();
            }

            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<TvProgram>>(json) ?? new List<TvProgram>();
        }

        public void guardarArchivo(List<TvProgram> programa)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
                //Converters = { new JsonStringEnumConverter() }
            };
            var programas = JsonSerializer.Serialize(programa, options);
            File.WriteAllText(_ruta, programas);
        }
    }
}