using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Acuity.Data;
using Acuity.Models;

namespace Stations.Controllers {
    public class BomsController : Controller {
        private readonly AcuityContext _context;

        public BomsController(AcuityContext context) {
            _context = context;
        }

        // GET: Boms
        public async Task<IActionResult> Index() {
            var acuityContext = _context.Bom.Include(b => b.Part).Include(b => b.Product);
            return View(await acuityContext.ToListAsync());
        }

        // GET: Boms/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bom = await _context.Bom
                .Include(b => b.Part)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.BomId == id);
            if (bom == null) {
                return NotFound();
            }

            return View(bom);
        }

        // GET: Boms/Create
        public IActionResult Create() {
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "Name");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            return View();
        }

        // POST: Boms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BomId,Quantity,PartId,ProductId")] Bom bom) {
            if (ModelState.IsValid) {
                _context.Add(bom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "Name", bom.PartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", bom.ProductId);
            return View(bom);
        }

        // GET: Boms/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bom = await _context.Bom.FindAsync(id);
            if (bom == null) {
                return NotFound();
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "Name", bom.PartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", bom.ProductId);
            return View(bom);
        }

        // POST: Boms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BomId,Quantity,PartId,ProductId")] Bom bom) {
            if (id != bom.BomId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(bom);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!BomExists(bom.BomId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "Name", bom.PartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", bom.ProductId);
            return View(bom);
        }

        // GET: Boms/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bom = await _context.Bom
                .Include(b => b.Part)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.BomId == id);
            if (bom == null) {
                return NotFound();
            }

            return View(bom);
        }

        // POST: Boms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var bom = await _context.Bom.FindAsync(id);
            _context.Bom.Remove(bom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BomExists(int id) {
            return _context.Bom.Any(e => e.BomId == id);
        }
    }
}
