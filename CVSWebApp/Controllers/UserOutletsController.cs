using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVSWebApp.Data;
using CVSWebApp.Models;

namespace CVSWebApp.Controllers
{
    public class UserOutletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserOutletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserOutlets
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserOutlets.ToListAsync());
        }

        // GET: UserOutlets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOutlet = await _context.UserOutlets
                .SingleOrDefaultAsync(m => m.UserOutletId == id);
            if (userOutlet == null)
            {
                return NotFound();
            }

            return View(userOutlet);
        }

        // GET: UserOutlets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserOutlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserOutletId,OutletId,DefaultOutlet,UserId")] UserOutlet userOutlet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userOutlet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userOutlet);
        }

        // GET: UserOutlets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOutlet = await _context.UserOutlets.SingleOrDefaultAsync(m => m.UserOutletId == id);
            if (userOutlet == null)
            {
                return NotFound();
            }
            return View(userOutlet);
        }

        // POST: UserOutlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserOutletId,OutletId,DefaultOutlet,UserId")] UserOutlet userOutlet)
        {
            if (id != userOutlet.UserOutletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userOutlet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserOutletExists(userOutlet.UserOutletId))
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
            return View(userOutlet);
        }

        // GET: UserOutlets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOutlet = await _context.UserOutlets
                .SingleOrDefaultAsync(m => m.UserOutletId == id);
            if (userOutlet == null)
            {
                return NotFound();
            }

            return View(userOutlet);
        }

        // POST: UserOutlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userOutlet = await _context.UserOutlets.SingleOrDefaultAsync(m => m.UserOutletId == id);
            _context.UserOutlets.Remove(userOutlet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserOutletExists(int id)
        {
            return _context.UserOutlets.Any(e => e.UserOutletId == id);
        }
    }
}
