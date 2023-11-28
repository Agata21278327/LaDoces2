using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaDoces2.Context;
using LaDoces2.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LaDoces2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPedidoItemController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPedidoItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPedidoItem
        public async Task<ActionResult> Index(){
            var AppDbContext = _context.PedidoItens.Include(p => p.Item).Include(p=> p.Pedido);
            return View(await AppDbContext.ToListAsync());
        }
        
        // GET: Admin/AdminPedidoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PedidoItens == null)
            {
                return NotFound();
            }

            var pedidoItem = await _context.PedidoItens
                .Include(p => p.Item)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.PedidoItemId == id);
            if (pedidoItem == null)
            {
                return NotFound();
            }

            return View(pedidoItem);
        }

        // GET: Admin/AdminPedidoItem/Create
        public IActionResult Create(int pedidoId)
        {
            ViewData["ItemId"] = new SelectList(_context.Itens, "ItemId", "Nome");
            ViewData["PedidoId"] = pedidoId;
            return View();
        }

        // POST: Admin/AdminPedidoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoItemId,PedidoId,ItemId,Quantidade,Preco")] PedidoItem pedidoItem)
        {
            if (ModelState.IsValid)
            {
                double valorModel = _context.Itens.FirstOrDefault(m => m.ItemId == pedidoItem.ItemId).Preco;
                pedidoItem.Preco = Convert.ToDecimal(valorModel);
                _context.Add(pedidoItem);
                _context.Add(pedidoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("PedidoItens", "AdminPedido", new { id = pedidoItem.PedidoId });
            }
            ViewData["ItemId"] = new SelectList(_context.Itens, "ItemId", "DescricaoCurta", pedidoItem.ItemId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "Cep", pedidoItem.PedidoId);
            return View(pedidoItem);
        }

        // GET: Admin/AdminPedidoItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PedidoItens == null)
            {
                return NotFound();
            }

            var pedidoItem = await _context.PedidoItens.FindAsync(id);
            if (pedidoItem == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Itens, "ItemId", "DescricaoCurta", pedidoItem.ItemId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "Cep", pedidoItem.PedidoId);
            return View(pedidoItem);
        }

        // POST: Admin/AdminPedidoItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoItemId,PedidoId,ItemId,Quantidade,Preco")] PedidoItem pedidoItem)
        {
            if (id != pedidoItem.PedidoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    double valorModel = _context.Itens.FirstOrDefault(m => m.ItemId == pedidoItem.ItemId).Preco;
                    pedidoItem.Preco = Convert.ToDecimal(valorModel);
                    _context.Update(pedidoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoItemExists(pedidoItem.PedidoItemId))
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
            ViewData["ItemId"] = new SelectList(_context.Itens, "ItemId", "DescricaoCurta", pedidoItem.ItemId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "Cep", pedidoItem.PedidoId);
            return View(pedidoItem);
        }

        // GET: Admin/AdminPedidoItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PedidoItens == null)
            {
                return NotFound();
            }

            var pedidoItem = await _context.PedidoItens
                .Include(p => p.Item)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.PedidoItemId == id);
            if (pedidoItem == null)
            {
                return NotFound();
            }
            return View(pedidoItem);
        }

        // POST: Admin/AdminPedidoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PedidoItens == null)
            {
                return Problem("Entity set 'AppDbContext.PedidoItens'  is null.");
            }
            var pedidoItem = await _context.PedidoItens.FindAsync(id);
            if (pedidoItem != null)
            {
                _context.PedidoItens.Remove(pedidoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("PedidoItens", "AdminPedido", new { id = pedidoItem.PedidoId });
        }

        private bool PedidoItemExists(int id)
        {
            return _context.PedidoItens.Any(e => e.PedidoItemId == id);
        }
    }
}
