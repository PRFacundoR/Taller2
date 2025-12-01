
using ProgramTv;

namespace APIRest.Services
{
    public interface IAccesoADatos
    {
        List<TvProgram> leerArchivo();
        void guardarArchivo(List<TvProgram> programa);
    }   
}