using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MathProject.Models;

namespace MathProject.Controllers
{
    public class HintsController : Controller
    {
        private readonly MathProjectContext _context;
        private int _qid;

        public HintsController(MathProjectContext context)
        {
            _context = context;
        }

        // GET: Hints
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hint.ToListAsync());
        }

        // GET: Hints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hint = await _context.Hint
                .SingleOrDefaultAsync(m => m.ID == id);
            if (hint == null)
            {
                return NotFound();
            }

            return View(hint);
        }

        // GET: Hints/Create
        public IActionResult Create()
        {
     
            return View();
        }

        // POST: Hints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[controller]/[action]/{questionid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,CorrectAnswer")] Hint hint, int questionid)
        {
            if (ModelState.IsValid)
            {
                hint.QuestionID = questionid;
                _context.Add(hint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hint);
        }

        // GET: Hints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hint = await _context.Hint.SingleOrDefaultAsync(m => m.ID == id);
            if (hint == null)
            {
                return NotFound();
            }
            return View(hint);
        }

        // POST: Hints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,QuestionID,Content,CorrectAnswer")] Hint hint)
        {
            if (id != hint.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HintExists(hint.ID))
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
            return View(hint);
        }

        // GET: Hints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hint = await _context.Hint
                .SingleOrDefaultAsync(m => m.ID == id);
            if (hint == null)
            {
                return NotFound();
            }

            return View(hint);
        }

        // POST: Hints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hint = await _context.Hint.SingleOrDefaultAsync(m => m.ID == id);
            _context.Hint.Remove(hint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HintExists(int id)
        {
            return _context.Hint.Any(e => e.ID == id);
        }
    }
}
