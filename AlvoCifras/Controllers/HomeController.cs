using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlvoCifras.Models;

namespace AlvoCifras.Controllers
{
    public class HomeController : Controller
    {

        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AllSongs()
        {
            List<Songs> songs = _context.Songs.ToList();

            return View(songs);
        }

        public IActionResult AllLyrics()
        {
            List<Lyrics> lyrics = _context.Lyrics.ToList();

            return View(lyrics);
        }

        public IActionResult Search(string keyword)
        {
            ViewBag.Keyword = keyword;
            List<Songs> songs = _context.Songs.ToList();
            List<Songs> list = songs;

            if (keyword == null || keyword == "" || keyword == "+")
            {
                list = songs;
            }
            else
            {
                list = songs.Where(x => x.Name.ToLower().Contains(keyword.ToLower()) || x.Artist.Name.ToLower().Contains(keyword.ToLower())).ToList();

                if (list.Count() == 0)
                {
                    list = null;
                }

            }

            return View("Search", list);
        }

    }
}
