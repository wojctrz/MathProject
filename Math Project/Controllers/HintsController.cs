using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MathProject.Models;
using Microsoft.AspNetCore.Routing;

namespace MathProject.Controllers
{
    public class HintsController : Controller
    {
        private readonly MathProjectContext _context;

        public HintsController(MathProjectContext context)
        {
            _context = context;
        }

        // GET: Hints
        [Route("/[controller]/{qid}")]
        [Route("/[controller]")]
        public async Task<IActionResult> Index(int? qid)
        {
            if(qid == null)
            {
                ViewBag.Result = "Index";
                return View(await _context.Hint.ToListAsync());
            }
            var question = await _context.Question.SingleOrDefaultAsync(q => q.ID == qid);
            ViewBag.Result = question.Content;
            return View(await _context.Hint.Where(h => h.QuestionID == qid).ToListAsync());
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
        [Route("[Controller]/[action]/{questionid}")]
        public IActionResult Create(int questionid)
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
                return RedirectToAction("Index", new { qid = questionid });
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
        //public async Task<ActionResult> ShowOneHint(int questionID, int nrOfHint)
        //{
        //    if (questionID == null)
        //    {
        //        return NotFound();
        //    }
        //    List<Hint> listOfHints = new List<Hint>();
        //    listOfHints = await _context.Hint.ToListAsync(m => m.QuestionID == questionID);
        //       // .SingleOrDefaultAsync(m => m.QuestionID == questionID).ToListAsync();
        //    foreach (var hint in listOfHints)
        //    {
        //        if (hint == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    ViewBag.hints = listOfHints;
        //    ViewBag.numberOfHints = listOfHints.Count;

        //    return View(listOfHints);
        //}
    }
}
