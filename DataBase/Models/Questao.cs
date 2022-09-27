using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Questao
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(900)]
        public string Corpo { get; set; }

        [StringLength(80)]
        public string ?Resposta { get; set; }
    }
}
