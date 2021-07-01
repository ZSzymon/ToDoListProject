using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ToDoListProject.Data;
using ToDoListProject.Models;
using System.Security.Claims;
using ToDoListProject.Service;


namespace ToDoListProject.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _db;


        private readonly IUserService _userService;


        public ToDoController(ApplicationDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;


        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo obj)
        {
            string userId = _userService.GetUserId();
            IdentityUser currentUser = _db.Users.FirstOrDefault(x => x.Id == userId);
            obj.Created = DateTime.Now;
            obj.User = currentUser;
            _db.ToDos.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            string userId = _userService.GetUserId();
            IEnumerable<ToDo> objList = _db.ToDos.Where(x => x.User.Id == userId);
            return View(objList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ToDos.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _db.ToDos.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Details(int ? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ToDos.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        
  
        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ToDos.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ToDo toDo = _db.ToDos.Find(id);
            if (toDo == null)
            {
                return NotFound();
            }
            _db.ToDos.Remove(toDo);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
