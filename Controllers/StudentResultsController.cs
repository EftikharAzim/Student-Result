using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseResults.Data;
using StudentCourseResults.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseResults.Controllers
{
    public class StudentResultsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentResultsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudentResults
        public async Task<IActionResult> Index(ResultStatus? statusFilter)
        {
            var results = _context.StudentResults.AsQueryable();

            if (statusFilter.HasValue)
            {
                results = results.Where(r => r.Status == statusFilter.Value);
            }

            ViewBag.StatusFilter = statusFilter;
            return View(await results.ToListAsync());
        }

        // GET: StudentResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var studentResult = await _context.StudentResults
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studentResult == null)
                return NotFound();

            return View(studentResult);
        }

        // GET: StudentResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentResult result)
        {
            if (ModelState.IsValid)
            {
                result.Status = result.TotalMarks switch
                {
                    < 50 => ResultStatus.NeedsImprovement,
                    < 80 => ResultStatus.Good,
                    _ => ResultStatus.Excellent
                };

                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        // GET: StudentResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var studentResult = await _context.StudentResults.FindAsync(id);
            if (studentResult == null)
                return NotFound();

            return View(studentResult);
        }

        // POST: StudentResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentResult result)
        {
            if (id != result.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    result.Status = result.TotalMarks switch
                    {
                        < 50 => ResultStatus.NeedsImprovement,
                        < 80 => ResultStatus.Good,
                        _ => ResultStatus.Excellent
                    };

                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentResultExists(result.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        // GET: StudentResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var studentResult = await _context.StudentResults
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studentResult == null)
                return NotFound();

            return View(studentResult);
        }

        // POST: StudentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentResult = await _context.StudentResults.FindAsync(id);
            if (studentResult != null)
            {
                _context.StudentResults.Remove(studentResult);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StudentResultExists(int id)
        {
            return _context.StudentResults.Any(e => e.Id == id);
        }
    }
}
