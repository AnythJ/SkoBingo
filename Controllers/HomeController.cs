﻿using Microsoft.AspNetCore.Mvc;
using SkoBingo.Models;
using SkoBingo.ViewModels;
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
        public ViewResult Index()
        {
            return View();
        }

        [Route("Play/{uniqueLink}")]
        public IActionResult Play(string uniqueLink)
        {
            Bingo bingo = _bingoRepository.GetBingo(uniqueLink);
            if (bingo == null) return StatusCode(500);

            HomeViewModel viewModel = new()
            {
                Bingo = bingo,
                Player = new Player()
            };

            return View(viewModel);
        }

        public ViewResult Multiplayer()
        {
            return View(new Bingo());
        }

        [HttpPost]
        public RedirectToActionResult MultiPlayer(string uniqueLink)
        {
            return RedirectToAction("Play", new { uniqueLink = uniqueLink });
        }

        [HttpPost]
        public RedirectToActionResult Win(HomeViewModel viewModel)
        {
            viewModel.Player.ScoreboardId = viewModel.Bingo.Scoreboard.ScoreboardId;
            viewModel.Player.WinDate = DateTime.Now;

            _bingoRepository.AddPlayer(viewModel.Player);
            TempData["scoreboardId"] = viewModel.Player.ScoreboardId;

            return RedirectToAction("Scoreboard", "Home", new { uniqueLink = viewModel.Bingo.UniqueLink });
        }

        [Route("Scoreboard/{uniqueLink}")]
        public IActionResult Scoreboard(string uniqueLink)
        {
            
            if (TempData["scoreboardId"] != null)
            {
                int scoreboardId = (int)TempData["scoreboardId"];
                IList<Player> players = _bingoRepository.GetPlayers(scoreboardId).ToList();
                return View(players);
            }
            else
            {
                Bingo bingo = _bingoRepository.GetBingo(uniqueLink);
                if (bingo == null) return StatusCode(500);

                IList<Player> players = _bingoRepository.GetPlayers(bingo.Scoreboard.ScoreboardId).ToList();
                return View(players);
            }
        }

        public ViewResult About()
        {
            return View();
        }
    }
}
