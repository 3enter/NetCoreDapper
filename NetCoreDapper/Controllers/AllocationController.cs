using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreDapper.Respository.Interfaces;

namespace NetCoreDapper.Controllers
{
    public class AllocationController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IStudentRepository _studentRepository;

        public AllocationController(IRoomRepository roomRepo, IStudentRepository studentRepo)
        {
            _roomRepository = roomRepo;
            _studentRepository = studentRepo;

        }

        // GET: Allocation
        public async Task<IActionResult> Index()
        {
            return View(await _roomRepository.GetAllocations());
        }

        // GET: Allocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Allocation/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Student = await _studentRepository.Get(null);
            var model = await _roomRepository.GetByID(id ?? 0);
            return View(model);
        }



        // POST: Allocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Allocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Allocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Allocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Allocation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}