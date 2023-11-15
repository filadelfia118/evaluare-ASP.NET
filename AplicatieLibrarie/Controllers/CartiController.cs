using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicatieLibrarie.Models;

namespace AplicatieLibrarie.Controllers
{
    public class CartiController : Controller
    {
        private readonly CartiContext _context;

        public CartiController(CartiContext context)
        {
            _context = context;
        }

        // GET: Carti
        public IActionResult Index(string listaSpecificata)
        {
            var model = new CartiViewModels
            {
                ListaCartilor = _context.Carti.ToList(),
                ListOptions = new SelectList(new List<string>
                {
                    "Stocul de cărți ce au prețul cuprins între 150 și 500 lei",
                    "Stocul de cărți cu numele autorului ce se începe cu o consoană",
                    "Stocul de cărți (sau cartea) ce are prețul maximal",
                    "Stocul de cărți (sau cartea) ce are prețul minimal"

                })

            };
            if (!string.IsNullOrEmpty(listaSpecificata))
            {
                switch (listaSpecificata)
                {
                    case "Stocul de cărți ce au prețul cuprins între 150 și 500 lei": model.ListaCartilor = _context.GetCartePretCupris(); break;
                    case "Stocul de cărți cu numele autorului ce se începe cu o consoană":model.ListaCartilor = _context.SelecteazaCartiCuAutoriCuConsoana(); break;
                    case "Stocul de cărți (sau cartea) ce are prețul maximal": model.ListaCartilor = _context.GetCartePretMaximal(); break;
                    case "Stocul de cărți (sau cartea) ce are prețul minimal": model.ListaCartilor = _context.GetCartePretMinimal(); break;
                    default:
                        break;

                }
            }
            return View(model);
        }

        // GET: Carti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carti == null)
            {
                return NotFound();
            }

            var carte = await _context.Carti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carte == null)
            {
                return NotFound();
            }

            return View(carte);
        }

        // GET: Carti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Autor,Price,Imagine")] Carte carte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carte);
        }

        // GET: Carti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carti == null)
            {
                return NotFound();
            }

            var carte = await _context.Carti.FindAsync(id);
            if (carte == null)
            {
                return NotFound();
            }
            return View(carte);
        }

        // POST: Carti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Autor,Price,Imagine")] Carte carte)
        {
            if (id != carte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarteExists(carte.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carte);
        }

        // GET: Carti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carti == null)
            {
                return NotFound();
            }

            var carte = await _context.Carti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carte == null)
            {
                return NotFound();
            }

            return View(carte);
        }

        // POST: Carti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carti == null)
            {
                return Problem("Entity set 'CartiContext.Carti'  is null.");
            }
            var carte = await _context.Carti.FindAsync(id);
            if (carte != null)
            {
                _context.Carti.Remove(carte);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarteExists(int id)
        {
          return (_context.Carti?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
