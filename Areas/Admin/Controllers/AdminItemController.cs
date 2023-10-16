using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaDoces2.Context;
using LaDoces2.Models;

namespace LaDoces2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminItemController : Controller
    {
        private readonly AppDbContext _context;

        public AdminItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminItem
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Itens.Include(i => i.Categoria);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Itens == null)
            {
                return NotFound();
            }

            var item = await _context.Itens
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Admin/AdminItem/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Admin/AdminItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemPequenaUrl,ImagemUrl,Ativo,Destaque,CategoriaId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome", item.CategoriaId);
            return View(item);
        }

        // GET: Admin/AdminItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Itens == null)
            {
                return NotFound();
            }

            var item = await _context.Itens.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome", item.CategoriaId);
            return View(item);
        }

        // POST: Admin/AdminItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemPequenaUrl,ImagemUrl,Ativo,Destaque,CategoriaId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome", item.CategoriaId);
            return View(item);
        }

        // GET: Admin/AdminItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Itens == null)
            {
                return NotFound();
            }

            var item = await _context.Itens
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Admin/AdminItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Itens == null)
            {
                return Problem("Entity set 'AppDbContext.Itens'  is null.");
            }
            var item = await _context.Itens.FindAsync(id);
            if (item != null)
            {
                _context.Itens.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return _context.Itens.Any(e => e.ItemId == id);
        }
    }
}
