using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class ReadFilmeDto
    {
        public string Nome { get; set; }

        public string Genero { get; set; }
        public int Duração { get; set; }
        public string Diretor { get; set; }

        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;

    }
}
