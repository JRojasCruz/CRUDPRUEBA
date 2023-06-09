﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDPRUEBA.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace CRUDPRUEBA.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadoresController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
            var trabajadores = await _context.spTrabajadores.FromSqlRaw("EXECUTE obtenerTrabajadores").ToListAsync();
            Console.WriteLine(trabajadores);
            return View(trabajadores);
        }


        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadores = await _context.Trabajadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadores == null)
            {
                return NotFound();
            }

            return Json(trabajadores);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            return PartialView("_CreatePartialView");
        }


        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadores trabajadores)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(trabajadores);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch{
                    throw;
                }
                
            }
            return View(trabajadores);
        }   

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadores = await _context.Trabajadores.FindAsync(id);
            if (trabajadores == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajadores.IdDepartamentoNavigation);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadores.IdDistritoNavigation);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadores.IdProvinciaNavigation);
            return Json(trabajadores);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadores trabajadores)
        {
            if (id != trabajadores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajadores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadoresExists(trabajadores.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", trabajadores.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "Id", trabajadores.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "Id", trabajadores.IdProvincia);
            return View(trabajadores);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadores = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadores == null)
            {
                return NotFound();
            }

            return View(trabajadores);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Trabajadores'  is null.");
            }
            var trabajadores = await _context.Trabajadores.FindAsync(id);
            if (trabajadores != null)
            {
                _context.Trabajadores.Remove(trabajadores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoresExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
