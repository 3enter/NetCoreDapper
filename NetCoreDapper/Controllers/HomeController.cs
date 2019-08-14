using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreDapper.Models;
using NetCoreDapper.Respository.Interfaces;

namespace NetCoreDapper.Controllers
{
    public class HomeController : Controller
    {

        private readonly IRoomRepository _roomRepository;

        public HomeController(IRoomRepository roomRepo)
        {
            _roomRepository = roomRepo;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _roomRepository.Get(null));
        }

        [HttpPost]
        public async Task<IActionResult> Index(Room room)
        {
           
            return View( await _roomRepository.Get(room));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            var result = await _roomRepository.Create(room);
            return RedirectToAction(nameof(Index));
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomRepository.GetByID(id??0);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _roomRepository.Update(room);
                return RedirectToAction(nameof(Index));

            }
            return View(room);
        }


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomRepository.GetByID(id ?? 0);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _roomRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
