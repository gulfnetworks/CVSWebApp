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
    public class ResolutionLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResolutionLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResolutionLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResolutionLog.ToListAsync());
        }

        // GET: ResolutionLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resolutionLog = await _context.ResolutionLog
                .SingleOrDefaultAsync(m => m.ResolutionLogId == id);
            if (resolutionLog == null)
            {
                return NotFound();
            }

            return View(resolutionLog);
        }

        // GET: ResolutionLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResolutionLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResolutionLogId,DateTime,ResolutionDetails,SurveyId,Status,UserId,OutletId")] ResolutionLog resolutionLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resolutionLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resolutionLog);
        }

        // GET: ResolutionLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resolutionLog = await _context.ResolutionLog.SingleOrDefaultAsync(m => m.ResolutionLogId == id);
            if (resolutionLog == null)
            {
                return NotFound();
            }
            return View(resolutionLog);
        }

        // POST: ResolutionLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResolutionLogId,DateTime,ResolutionDetails,SurveyId,Status,UserId,OutletId")] ResolutionLog resolutionLog)
        {
            if (id != resolutionLog.ResolutionLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resolutionLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResolutionLogExists(resolutionLog.ResolutionLogId))
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
            return View(resolutionLog);
        }

        // GET: ResolutionLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resolutionLog = await _context.ResolutionLog
                .SingleOrDefaultAsync(m => m.ResolutionLogId == id);
            if (resolutionLog == null)
            {
                return NotFound();
            }

            return View(resolutionLog);
        }

        // POST: ResolutionLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resolutionLog = await _context.ResolutionLog.SingleOrDefaultAsync(m => m.ResolutionLogId == id);
            _context.ResolutionLog.Remove(resolutionLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResolutionLogExists(int id)
        {
            return _context.ResolutionLog.Any(e => e.ResolutionLogId == id);
        }
    }
}
