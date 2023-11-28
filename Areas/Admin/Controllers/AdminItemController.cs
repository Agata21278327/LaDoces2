using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaDoces2.Context;
using LaDoces2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;

namespace LaDoces2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminItemController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ConfiguraImagem _confImg;
        private readonly IWebHostEnvironment _hostingEnvireoment;

        public AdminItemController(AppDbContext context,
        IWebHostEnvironment hostEnvironment, IOptions<ConfiguraImagem> confImg)
        {
            _context = context;
            _confImg = confImg.Value;
            _hostingEnvireoment = hostEnvironment;
        }

        // GET: Admin/AdminItem
        public async Task<IActionResult> Index(string filtro, int pageindex = 1, string sort = "Nome")
        {
            var itenslist = _context.Itens.AsNoTracking().AsQueryable();

            if (filtro != null)
            {
                itenslist = itenslist.Where(p => p.Nome.Contains(filtro));

            }
            var model = await PagingList.CreateAsync(itenslist, 5,pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary{{"filtro", filtro

}};

            return View(model);
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
        public async Task<IActionResult> Create([Bind("ItemId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemPequenaUrl,ImagemUrl,Ativo,Destaque,CategoriaId")] Item item, IFormFile Imagem, IFormFile Imagemcurta)
        {
            if (Imagem != null)
            {
                string imagemr = await SalvarArquivo(Imagem);
                item.ImagemUrl = imagemr;
            }
            if (Imagemcurta != null)
            {
                string imagemcr = await SalvarArquivo(Imagemcurta);
                item.ImagemPequenaUrl = imagemcr;
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemPequenaUrl,ImagemUrl,Ativo,Destaque,CategoriaId")] Item item, IFormFile Imagem, IFormFile Imagemcurta)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }
            if (Imagem != null)
            {
                Deletefile(item.ImagemUrl);
                string imagemr = await SalvarArquivo(Imagem);
                item.ImagemUrl = imagemr;
            }
            if (Imagemcurta != null)
            {
                Deletefile(item.ImagemPequenaUrl);
                string imagemcr = await SalvarArquivo(Imagemcurta);
                item.ImagemPequenaUrl = imagemcr;
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
                Deletefile(item.ImagemPequenaUrl);
                Deletefile(item.ImagemUrl);
                _context.Itens.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Itens.Any(e => e.ItemId == id);
        }
        public async Task<string> SalvarArquivo(IFormFile Imagem)
        {
            var filePath = Path.Combine(_hostingEnvireoment.WebRootPath,

            _confImg.NomePastaImagemItem);

            if (Imagem.FileName.Contains(".jpg") || Imagem.FileName.Contains(".gif")

            || Imagem.FileName.Contains(".svg") || Imagem.FileName.Contains(".png"))

            {
                string novoNome =

                $"{Guid.NewGuid()}.{Path.GetExtension(Imagem.FileName)}";

                var fileNameWithPath = string.Concat(filePath, "\\", novoNome);
                using (var stream = new FileStream(fileNameWithPath,

                FileMode.Create))
                {
                    await Imagem.CopyToAsync(stream);
                }
                return "~/" + _confImg.NomePastaImagemItem + "/" + novoNome;
            }
            return null;
        }
        public void Deletefile(string fname)
        {
            if (fname != null)
            {

                int pi = fname.LastIndexOf("/") + 1;
                int pf = fname.Length - pi;
                string nomearquivo = fname.Substring(pi, pf);
                try
                {
                    string _imagemDeleta = Path.Combine(_hostingEnvireoment.WebRootPath,
                    _confImg.NomePastaImagemItem + "\\", nomearquivo);
                    if ((System.IO.File.Exists(_imagemDeleta)))
                    {
                        System.IO.File.Delete(_imagemDeleta);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
