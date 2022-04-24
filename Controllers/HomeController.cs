using Microsoft.AspNetCore.Mvc;
using SkoBingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoBingo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBingoRepository _bingoRepository;

        public HomeController(IBingoRepository _bingo)
        {
            this._bingoRepository = _bingo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/Play/{uniqueLink}")]
        public IActionResult Play(string uniqueLink)
        {
            Bingo bingo = _bingoRepository.GetBingo(uniqueLink);

            return View(bingo);
        }
    }
}
