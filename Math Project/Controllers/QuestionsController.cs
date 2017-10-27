﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MathProject.Models;

namespace MathProject.Controllers
{
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
        public async Task<IActionResult> Exercise(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var question = await _context.Question.Where(q => q.ID == id).SingleOrDefaultAsync(q => q.ID == id);
            return View( question);

        }
            private static TEnum? GetEnum<TEnum>(string value) where TEnum : struct
        {
            TEnum result;

            return Enum.TryParse<TEnum>(value, out result) ? (TEnum?)result : null;
        }
        // GET: Questions/Details/5
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

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Questions/Edit/5
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

        public ActionResult AddHint(int id)         //NAD TYM JESZCZE PRACUJE TO NIE DZIAŁA NA RAZIE
        {
            return RedirectToAction("Create", "Hints", id);
        }

    }
}
