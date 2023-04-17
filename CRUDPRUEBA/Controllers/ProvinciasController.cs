using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDPRUEBA.Models;
using Newtonsoft.Json;

namespace CRUDPRUEBA.Controllers
{
    public class ProvinciasController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public ProvinciasController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Provincias
        public async Task<IActionResult> Index()
        {
            var trabajadoresPruebaContext = _context.Provincia;
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(await trabajadoresPruebaContext.ToListAsync(), jsonSettings);
            return Content(json, "application/json");
        }

        // GET: Provincias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincia = await _context.Provincia
                .Include(p => p.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincia == null)
            {
                return NotFound();
            }

            return View(provincia);
        }

        // GET: Provincias/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id");
            return View();
        }

        // POST: Provincias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDepartamento,NombreProvincia")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", provincia.IdDepartamento);
            return View(provincia);
        }

        // GET: Provincias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincia = await _context.Provincia.FindAsync(id);
            if (provincia == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", provincia.IdDepartamento);
            return View(provincia);
        }

        // POST: Provincias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDepartamento,NombreProvincia")] Provincia provincia)
        {
            if (id != provincia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciaExists(provincia.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", provincia.IdDepartamento);
            return View(provincia);
        }

        // GET: Provincias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincia = await _context.Provincia
                .Include(p => p.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincia == null)
            {
                return NotFound();
            }

            return View(provincia);
        }

        // POST: Provincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Provincia == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Provincia'  is null.");
            }
            var provincia = await _context.Provincia.FindAsync(id);
            if (provincia != null)
            {
                _context.Provincia.Remove(provincia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinciaExists(int id)
        {
          return (_context.Provincia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
