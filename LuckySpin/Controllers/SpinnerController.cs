using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        Random random;
        Repository repository;

        /***
         * Controller Constructor
         */
        public SpinnerController()
        {
            random = new Random();
            //TODO: Inject the Repository singleton
            repository = new Repository();
        }

        /***
         * Entry Page Action
         **/
        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }
        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (!ModelState.IsValid) { return View(); }

            // TODO: Add the Player to the Repository

            // TODO: Build a new SpinItViewModel object with data from the Player and pass it to the View

            return RedirectToAction("SpinIt", player); 
        }

        /***
         * Spin Action - Game Play
         **/  
         public IActionResult SpinIt(Player player) //TODO: replace the parameter with a ViewModel
        {
            Spin spin = new Spin
            {
                Luck = player.Luck,
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10)
            };

            spin.IsWinning = (spin.A == spin.Luck || spin.B == spin.Luck || spin.C == spin.Luck);

            //Add to Spin Repository
            repository.AddSpin(spin);

            //TODO: Clean up ViewBag using a SpinIt ViewModel instead
            ViewBag.ImgDisplay = (spin.IsWinning) ? "block" : "none";
            ViewBag.FirstName = player.FirstName;
            ViewBag.Balance = player.Balance;

            return View("SpinIt", spin);
        }

        /***
         * LuckList Action - End of Game
         **/
         public IActionResult LuckList()
        {
                return View(repository.PlayerSpins);
        }
    }
}

