using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Math_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Math_Project.Models;
using Microsoft.Extensions.DependencyInjection;
using MathProject.Models;
using Math_Project.Controllers;

namespace MathProject.Controllers
{
    public class ManageAccountsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;

        public ManageAccountsController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;

            _UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        }

        // GET: ManageAccounts
        public async Task<IActionResult> Index()
        {
            var _users = _context.Users.ToList();
            foreach (var _user in _users)
            {
                _user.Roles = await _UserManager.GetRolesAsync(_user);
            }
            return View(await _context.Users.ToListAsync());
        }


        public async Task<IActionResult> GrantTeacher(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var user = await _context.ApplicationUser.SingleAsync(u => u.Id == id);
                var roleresult = await _UserManager.AddToRoleAsync(user, "Teacher");
            }
            catch
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GrantAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var user = await _context.ApplicationUser.SingleAsync(u => u.Id == id);
                var roleresult = await _UserManager.AddToRoleAsync(user, "Admin");
            }
            catch
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }



        //// GET: ManageAccounts/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ManageAccounts/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ManageAccounts/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ManageAccounts/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ManageAccounts/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ManageAccounts/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ManageAccounts/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}