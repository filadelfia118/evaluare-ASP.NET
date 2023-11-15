using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplicatieLibrarie.Models
{
    public class CartiViewModels
    {
        public List<Carte>? ListaCartilor { get; set; }
        public string? ListaSpecificata { get; set; }
        public SelectList? ListOptions { get; set; }
    }
}
