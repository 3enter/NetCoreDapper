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
            return View(await _roomRepository.GetRoom(null));
        }

        [HttpPost]
        public async Task<IActionResult> Index(Room room)
        {
           
            return View( await _roomRepository.GetRoom(room));
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
