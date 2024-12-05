using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AstreeWebApp.Areas.Identity.Data;
using AstreeWebApp.Models;

namespace AstreeWebApp.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly AstreeDbContext _context;

        public ClaimsController(AstreeDbContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
            return View(await _context.Claim.ToListAsync());
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyId,DateSinistre,TypeSinistre,Description,Montant,ClaimStatut")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PolicyId,DateSinistre,TypeSinistre,Description,Montant,ClaimStatut")] Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.Id))
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
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(Guid id)
        {
            return _context.Claim.Any(e => e.Id == id);
        }
    }
}
