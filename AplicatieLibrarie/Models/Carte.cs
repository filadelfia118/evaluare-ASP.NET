using System.ComponentModel.DataAnnotations;

namespace AplicatieLibrarie.Models
{
    public class Carte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campul ,Nume' este obliatoriu")]
        public string? Nume { get; set; }

        [Required(ErrorMessage = "Campul ,Autor' este obliatoriu")]
        public string? Autor { get; set; }


        [Required(ErrorMessage = "Pretul este obligatoriu de introdus")]
        public double Price { get; set; }

        public string? Imagine { get; set; }
    }
}
