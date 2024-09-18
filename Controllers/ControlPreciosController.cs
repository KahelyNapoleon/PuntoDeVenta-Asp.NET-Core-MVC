using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class ControlPreciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ControlPreciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ControlPrecios
        public async Task<IActionResult> Index()
        {
            return View(await _context.ControlPrecios.ToListAsync());
        }

        // GET: ControlPrecios/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var controlPrecio = await _context.ControlPrecios
        //        .FirstOrDefaultAsync(m => m.ControlPrecio1 == id);
        //    if (controlPrecio == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(controlPrecio);
        //}

        // GET: ControlPrecios/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ControlPrecios/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ControlPrecio1,CodigoBarra,Nombre,Costo,PrecioVenta")] ControlPrecio controlPrecio)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(controlPrecio);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(controlPrecio);
        //}

        // GET: ControlPrecios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlPrecio = await _context.ControlPrecios.FindAsync(id);
            if (controlPrecio == null)
            {
                return NotFound();
            }
            return View(controlPrecio);
        }

        // POST: ControlPrecios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ControlPrecio1,CodigoBarra,Nombre,Costo,PrecioVenta")] ControlPrecio controlPrecio)
        {
            if (id != controlPrecio.ControlPrecio1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Update(controlPrecio);
                        await _context.SaveChangesAsync();
                        
                        //ACTUALIZA PRECIO Y COSTO DEL PRODUCTO DESDE CONTROL DE PRECIOS CONTROLLER
                        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.ProductoId == id);
                        if (producto != null)
                        {
                            producto.Costo = controlPrecio.Costo;
                            producto.PrecioVenta = controlPrecio.PrecioVenta;
                            _context.SaveChanges();
                            await _context.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ControlPrecioExists(controlPrecio.ControlPrecio1))
                        {
                            await transaction.RollbackAsync();
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                   
                return RedirectToAction(nameof(Index));
            }
            return View(controlPrecio);
        }

        // GET: ControlPrecios/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var controlPrecio = await _context.ControlPrecios
        //        .FirstOrDefaultAsync(m => m.ControlPrecio1 == id);
        //    if (controlPrecio == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(controlPrecio);
        //}

        //// POST: ControlPrecios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var controlPrecio = await _context.ControlPrecios.FindAsync(id);
        //    if (controlPrecio != null)
        //    {
        //        _context.ControlPrecios.Remove(controlPrecio);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ControlPrecioExists(int id)
        {
            return _context.ControlPrecios.Any(e => e.ControlPrecio1 == id);
        }
    }
}
