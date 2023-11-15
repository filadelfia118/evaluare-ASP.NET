using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AplicatieLibrarie.Models
{
    public class CartiContext:DbContext
    {
        public DbSet<Carte> Carti { get; set; }
        public CartiContext(DbContextOptions<CartiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        //Stocul de cărți ce au prețul cuprins între 150 și 500 lei;
        public List<Carte> GetCartePretCupris()
        {
            var cartePretCuprins = Carti.Where(e => e.Price >= 150 && e.Price <= 500).ToList();
            return cartePretCuprins;
        }
        //Stocul de cărți cu numele autorului ce se începe cu o consoană;
        public List<Carte> SelecteazaCartiCuAutoriCuConsoana()
        {
            var cartiCuConsoana = Carti.Where(c => Regex.IsMatch(c.Autor, "^[^aeiouAEIOU]")) .ToList();

            return cartiCuConsoana;
        }

        //Stocul de cărți (sau cartea) ce are prețul maximal;
        public List<Carte> GetCartePretMaximal()
        {
            var cartePretMaximal = Carti.OrderByDescending(e => e.Price).ToList();
            return cartePretMaximal;
        }
        //Stocul de cărți (sau cartea) ce are prețul minimal;
        public List<Carte> GetCartePretMinimal()
        {
            var cartePretMinimal = Carti.OrderBy(e => e.Price).ToList();
            return cartePretMinimal;
        }
    }
}
