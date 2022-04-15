using Microsoft.AspNetCore.Mvc;
using SkoBingo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoBingo.Controllers
{
    public class CreationController : Controller
    {
        public IActionResult Index()
        {
            CreationViewModel viewModel = new CreationViewModel();
            return View(viewModel);
        }
    }
}
