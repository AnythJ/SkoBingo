using Microsoft.AspNetCore.Mvc;
using SkoBingo.Models;
using SkoBingo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoBingo.Controllers
{
    public class CreationController : Controller
    {
        private readonly IBingoRepository _bingoRepository;

        public CreationController(IBingoRepository _bingo)
        {
            this._bingoRepository = _bingo;
        }
        public ViewResult Index()
        {
            CreationViewModel viewModel = new();
            return View(viewModel);
        }

        [HttpPost]
        public ViewResult Create(string name, int size)
        {
            CreationViewModel viewModel = new()
            {
                Name = name,
                Size = size
            };

            return View(viewModel);
        }

        [HttpPost]
        public RedirectToActionResult BingoCreate(CreationViewModel viewModel)
        {
            Bingo bingo = new()
            {
                Name = viewModel.Name,
                Size = viewModel.Size,
                Sentence = viewModel.Sentences,
                Scoreboard = new Scoreboard()
            };
            
            bingo = _bingoRepository.Add(bingo);
            
            return RedirectToAction("Play", "Home", new { uniqueLink = bingo.UniqueLink });
        }


    }
}
