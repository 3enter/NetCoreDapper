using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreDapper.Models;
using NetCoreDapper.Respository.Interfaces;

namespace NetCoreDapper.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepo)
        {
            _studentRepository = studentRepo;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await _studentRepository.Get(null));
        }

        [HttpPost]
        public async Task<IActionResult> Index(Student room)
        {

            return View(await _studentRepository.Get(room));
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            var result = await _studentRepository.Create(student);
            return RedirectToAction(nameof(Index));
        }


        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetByID(id ?? 0);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _studentRepository.Update(student);
                return RedirectToAction(nameof(Index));

            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _studentRepository.GetByID(id ?? 0);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _studentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}