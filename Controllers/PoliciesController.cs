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
    public class PoliciesController : Controller
    {
        private readonly AstreeDbContext _context;

        public PoliciesController(AstreeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Policy.ToListAsync());
        }

        public async Task<IActionResult> ShowSearchResults(string searchPhrase)
        {
            if (Enum.TryParse<Policy.TypePolice>(searchPhrase, true, out var type))
            {
                var policies = await _context.Policy
                    .Where(p => p.Type == type)
                    .ToListAsync();

                return View("Index", policies);
            }

            // If searchPhrase does not match any TypePolice value, return all policies or handle accordingly
            return View("Index", await _context.Policy.ToListAsync());
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var policy = await _context.Policy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Policy policy)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                ModelState.AddModelError("", "User not logged in.");
                return View(policy);
            }
            policy.Id = Guid.NewGuid();
            policy.UserId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Log errors or view them during debugging
                foreach (var error in ModelState)
                {
                    var key = error.Key;
                    var errorMessages = error.Value.Errors.Select(e => e.ErrorMessage).ToList();
                    Console.WriteLine($"Key: {key}, Errors: {string.Join(", ", errorMessages)}");
                }
            }
            return View(policy);
        }


        // GET: Policies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policy.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Type,DateDeb,DateFin,Prime,PolicStatut")] Policy policy)
        {
            if (id != policy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.Id))
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
            return View(policy);
        }

        private bool PolicyExists(Guid id)
        {
            return _context.Policy.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var policy = await _context.Policy.FindAsync(id);
            if (policy != null)
            {
                _context.Policy.Remove(policy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> TestQuery()
        {
            try
            {
                var policies = await _context.Policy.Take(10).ToListAsync(); // Limit results to test
                return View(policies);
            }
            catch (Exception ex)
            {
                // Log detailed exception message
                return Content($"Error: {ex.Message}");
            }
        }
        public IActionResult SetSessionValue()
        {
            HttpContext.Session.SetString("TestKey", "This is a test value");
            return Content("Session value has been set.");
        }
        public IActionResult GetSessionValue()
        {
            var sessionValue = HttpContext.Session.GetString("TestKey");
            if (sessionValue == null)
            {
                return Content("Session value is not set.");
            }

            return Content($"Session value: {sessionValue}");
        }


    }
}
