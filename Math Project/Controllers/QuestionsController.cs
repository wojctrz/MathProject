using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MathProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MathProject.Controllers
{
    [Authorize(Roles = "Student")]
    public class QuestionsController : Controller
    {
        private readonly MathProjectContext _context;

        public QuestionsController(MathProjectContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {

            return View(await _context.Question.ToListAsync());


            // var exercises = _context.Question.Where(q => q.Category == GetEnum<Categories>(categ));
            //return View(await exercises.ToListAsync());
        }
        public async Task<IActionResult> Category(string categ)
        {
            var exercises = _context.Question.Where(q => q.Category == GetEnum<Categories>(categ));
            return View(await exercises.ToListAsync());
        }

        private static TEnum? GetEnum<TEnum>(string value) where TEnum : struct
        {
            TEnum result;

            return Enum.TryParse<TEnum>(value, out result) ? (TEnum?)result : null;
        }

        // GET: Questions/Details/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var question = await _context.Question
                                 .Include(q => q.Hints)
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ChooseToEdit()
        {

            return View(await _context.Question.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Category,Content,CorrectAnswer")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ChooseHowToEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Content,CorrectAnswer")] Question question)
        {
            if (id != question.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.ID))
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
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize(Roles = "Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.ID == id);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult AddHint(int id)         //Już działa !!!!!!
        {
            return RedirectToAction("Create", "Hints", new { questionid = id });
        }

        public async Task<IActionResult> Solve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                                 .Include(q => q.Hints)
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Solve(int id, Question _question)
        {
            /*_question is a question with answer submitted by user
             * corrquestion is a question with the correct answer grabbed from the database
             */
            if (_question == null)
            {
                return NotFound();
            }

            var corrquestion = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);

            if (_question.CorrectAnswer == corrquestion.CorrectAnswer)
            {
                ViewBag.Result = "Good";
            }
            else
            {
                ViewBag.Result = "Wrong";
            }
            return View(corrquestion);
        }

        public IActionResult Wrong()
        {
            return View();
        }
        public ActionResult Good()
        {

            return View();

        }

        public async Task<IActionResult> ChooseHowToSolve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }
        //public async Task<IActionResult> SolveWithHints(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Question
        //                         .Include(q => q.Hints)
        //                         .AsNoTracking()
        //                         .SingleOrDefaultAsync(m => m.ID == id);

        //    ViewBag.Task = question.Content;


        //    return View(await _context.Hint.Where(m => m.QuestionID == id).ToListAsync());
        //}

        [Route("/[controller]/[action]/{questionID}/{hintID}")]
        public IActionResult SolveWithHints(int? questionID, int hintID)
        {

            if (_context.Hint.Where(m => m.QuestionID == questionID).ToList() == null)
            {
                return NotFound();
            }
            if (questionID == null)
            {
                return NotFound();
            }

            var question = _context.Question.SingleOrDefault(m => m.ID == questionID);
            ViewBag.Task = question.Content;
            var hints = _context.Hint.Where(m => m.QuestionID == questionID).ToList();

            /*TODO
            *jak był ostatni hint dobrze odpowiedziany
            * (czyli chyba jak hintID jest większe od rozmiaru listy czy coś)
            * to przekierowuje do czego tam powinno przekierować
            */
            if (hintID == hints.Count)
            {
                return RedirectToAction(nameof(Solve), new { id = questionID });
            }
            ViewBag.Length = hints.Count;
            ViewBag.hintID = hintID;

            return View(hints[hintID]);
        }
        [HttpPost]
        [Route("/[controller]/[action]/{questionID}/{hintID}")]
        public async Task<IActionResult> SolveWithHints(int questionID, int hintID, Hint hintFromUser)
        {
            ViewBag.Task = _context.Question.SingleOrDefault(m => m.ID == questionID).Content;
            if (hintFromUser == null)
            {
                return NotFound();
            }
            var correctListOfHints = await _context.Hint.Where(m => m.QuestionID == questionID).ToListAsync(); // list of all hints for given questionID
            if (correctListOfHints.Count < 1)
            {
                return NotFound();
            }
            var correctHint = correctListOfHints[hintID];


            if (hintFromUser.CorrectAnswer == correctHint.CorrectAnswer)
            {

                ViewBag.hintID = hintID + 1;
                return RedirectToAction("SolveWithHints", new { questionID = questionID, hintID = hintID + 1 });
            }
            else
            {
                ViewBag.hintID = hintID;
                ViewBag.Result = "Wrong";
                return View(correctHint);
            }

        }
        public IActionResult PickRandomQuestionAndSolve(int questionID)
        {
            var maxID = _context.Question.OrderByDescending(u => u.ID).FirstOrDefault().ID;
            var minID = _context.Question.OrderByDescending(u => u.ID).LastOrDefault().ID;
            Random random = new Random();
            int newQuestionID;
            do
            {
                newQuestionID = random.Next(minID-1, maxID+1);
            } while (!QuestionExists(newQuestionID)||questionID==newQuestionID);


            return RedirectToAction("Solve", new { id= newQuestionID });
        }

    }
}