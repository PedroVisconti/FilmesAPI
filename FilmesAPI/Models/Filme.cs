using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FilmesAPI.Models
{
    public class Filme
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "O nome do filme é obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="O genero é obrigatorio")]
        [MaxLength(25, ErrorMessage ="Genero deve ter no maximo 25 caracteres")]
        public string Genero { get; set; }
        [Required(ErrorMessage ="A duração é obrigatorio")]
        [Range(70, 180, ErrorMessage = " O filme precisa ter no minimo 70 minutos e 180 no maximo")]
        public int Duração { get; set; }
        [Required(ErrorMessage = "O diretor é obrigatorio")]
        [MaxLength(100, ErrorMessage ="O diretor deve ter no maximo 100 caracteres")]
        public string Diretor { get; set; }

    }
}
