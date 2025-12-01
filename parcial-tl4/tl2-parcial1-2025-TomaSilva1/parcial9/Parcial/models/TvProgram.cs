namespace ProgramTv
{
    public class TvProgram
    {
        private int id;
        private string title;
        private string genre;
        private int diaDeLaSemana;
        private int startTime;
        private int durationMinutes;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Genre { get => genre; set => genre = value; }
        public int DiaDeLaSemana { get => diaDeLaSemana; set => diaDeLaSemana = value; }
        public int StartTime { get => startTime; set => startTime = value; }
        public int DurationMinutes { get => durationMinutes; set => durationMinutes = value; }

        public TvProgram()
        {

        }

        public TvProgram(int id, string title, string genre, int dia, int start, int duration)
        {
            Id = id;
            Title = title;
            Genre = genre;
            DiaDeLaSemana = dia;
            StartTime = start;
            DurationMinutes = duration;
        }


    }

}

public enum diaSemana
{
    Lunes = 1,
    Martes = 2,
    Miercoles = 3,
    Jueves = 4,
    Viernes = 5,
    Sabado = 6,
    Domingo = 7
}