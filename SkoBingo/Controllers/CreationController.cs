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
            InitialCreationViewModel viewModel = new();
            return View(viewModel);
        }

        [HttpPost]
        public ViewResult Create(InitialCreationViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }
            else
            {
                CreationViewModel creationViewModel = new()
                {
                    Name = viewModel.Name,
                    Size = viewModel.Size
                };

                return View(creationViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BingoCreate(CreationViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            else
            {
                Bingo bingo = new()
                {
                    Name = viewModel.Name,
                    Size = viewModel.Size,
                    Sentences = viewModel.Sentences,
                    Scoreboard = new Scoreboard()
                };

                bingo = await _bingoRepository.AddBingo(bingo);

                return RedirectToAction("Play", "Home", new { uniqueLink = bingo.UniqueLink });
            }
        }

    }
}
