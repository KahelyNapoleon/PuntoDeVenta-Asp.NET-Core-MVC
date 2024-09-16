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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Productos.Include(p => p.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,CodigoBarra,Nombre,PesoNeto,Costo,PrecioVenta,Cantidad,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                using(var transaction = await _context.Database.BeginTransactionAsync())
                {
                    _context.Add(producto);
                    await _context.SaveChangesAsync();

                    var stock = new Stock()
                    {
                        StockId = producto.ProductoId,
                        CodigoBarra = producto.CodigoBarra,
                        Nombre = producto.Nombre,
                        Cantidad = producto.Cantidad
                    };
                    _context.Add(stock);
                    await _context.SaveChangesAsync();

                    var controlPrecio = new ControlPrecio()
                    {
                        ControlPrecio1 = producto.ProductoId,
                        CodigoBarra = producto.CodigoBarra,
                        Nombre = producto.Nombre,
                        Costo = producto.Costo,
                        PrecioVenta = producto.PrecioVenta
                    };
                    _context.Add(controlPrecio);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));

                }
                   
                
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Descripcion", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", producto.CategoriaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,CodigoBarra,Nombre,PesoNeto,Costo,PrecioVenta,Cantidad,CategoriaId")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //ELIMINA LOS DATOS DEL PRODUCTO REGISTRADOS EN STOCK Y CONTROL DE PRECIO
            // ELIMINA 1RO LOS REGISTROS DE STOCK 
            // LUEGO LOS REGISTROS DE CONTROL PRECIOS
            // FINALMENTE ELIMINA EL PRODUCTO
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.StockId == id);
            if(stock != null)
            {
                _context.Remove(stock);
            }
            await _context.SaveChangesAsync();

            var controlPrecio = await _context.ControlPrecios.FirstOrDefaultAsync(c => c.ControlPrecio1 == id);
            if (controlPrecio != null)
            {
                _context.Remove(controlPrecio);
            }
            await _context.SaveChangesAsync();

            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
