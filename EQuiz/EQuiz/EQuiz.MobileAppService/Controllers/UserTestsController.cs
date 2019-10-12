using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQuiz.MobileAppService.Models;

namespace EQuiz.MobileAppService.Controllers
{
    public class UserTestsController : Controller
    {
        private readonly ApplicationContext _context;

        public UserTestsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: UserTests
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.UserTests.Include(u => u.User);
            return View(await applicationContext.ToListAsync());
        }

        // GET: UserTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTest == null)
            {
                return NotFound();
            }

            return View(userTest);
        }

        // GET: UserTests/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] UserTest userTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTest.UserId);
            return View(userTest);
        }

        // GET: UserTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests.FindAsync(id);
            if (userTest == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTest.UserId);
            return View(userTest);
        }

        // POST: UserTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId")] UserTest userTest)
        {
            if (id != userTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTestExists(userTest.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTest.UserId);
            return View(userTest);
        }

        // GET: UserTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTest == null)
            {
                return NotFound();
            }

            return View(userTest);
        }

        // POST: UserTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTest = await _context.UserTests.FindAsync(id);
            _context.UserTests.Remove(userTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTestExists(int id)
        {
            return _context.UserTests.Any(e => e.Id == id);
        }
    }
}
