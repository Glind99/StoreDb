using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreDb.Data;
using StoreDb.Models;

namespace StoreDb.Controllers
{
    public class VacationsController : Controller
    {
        private readonly MyStoreDbContext _context;

        public VacationsController(MyStoreDbContext context)
        {
            _context = context;
        }

        // GET: Vacations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vacations.ToListAsync());
        }

        // GET: Vacations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }

        // GET: Vacations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,VacationType,StartDate,EndDate,Fk_EmployeeId")] Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }

        // GET: Vacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations.FindAsync(id);
            if (vacation == null)
            {
                return NotFound();
            }
            return View(vacation);
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,VacationType,StartDate,EndDate,Fk_EmployeeId")] Vacation vacation)
        {
            if (id != vacation.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacation.ApplicationId))
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
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", vacation.Fk_EmployeeId);
            return View(vacation);
        }

        // GET: Vacations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }

        // POST: Vacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacation = await _context.Vacations.FindAsync(id);
            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //
        // GET: Vacation, result by name
        public async Task<IActionResult> SearchForVacation()
        {
            var VacationDb = _context.Vacations.Include(v => v.Employees);
            return View(await VacationDb.ToListAsync());
        }
        public async Task<IActionResult> Showvacation(string VacationSearchFirstName, string VacationSearchLastName)
        {
            var employeeIdQuery = _context.Employees
                .Where(e => e.FirstName == VacationSearchFirstName && e.LastName == VacationSearchLastName)
                .Select(e => e.EmployeeId);

            var results = await _context.Vacations
                .Include(v => v.Employees)
                .Where(v => employeeIdQuery.Contains(v.Fk_EmployeeId))
                .ToListAsync();

            return View("Index", results);
        }

        // GET: VacationAdmin, select by month
        public async Task<IActionResult> VacationAdmin()
        {
            var VacationDb = _context.Vacations.Include(v => v.Employees);
            return View(await VacationDb.ToListAsync());
        }
        public async Task<IActionResult> ShowVacationResults(int MonthSelect)
        {
            var vacationDbContext = _context.Vacations.Include(v => v.Employees).ToList();
            var filteredVacations = vacationDbContext.Where(v => v.ApplicationDate.Month == MonthSelect);
            return View("Index", filteredVacations);
        }
        //
        private bool VacationExists(int id)
        {
            return _context.Vacations.Any(e => e.ApplicationId == id);
        }
    }
}
