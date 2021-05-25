using Boarsenger.API.MVCInterface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.MVCControllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ProfileEdit()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ServerManagement()
        {
            return View();
        }

        public IActionResult PremiumAccess()
        {
            return View();
        }





        public static List<ServerViewModel> ServersList { get; set; }
        // GET: Students
        public IActionResult Show()
        {
            if (ServersList == null)
            {
                ServersList = new List<ServerViewModel>();
            }
            return View(ServersList);
        }

        public IActionResult CreateSampleStudents()
        {

            int maxId = 0;
            if (ServersList.Count > 0)
            {
                maxId = ServersList.Max<ServerViewModel>(a => a.Id);
            }
            for (int i = 0; i < 5; i++)
            {
                int newId = maxId + i + 1;
                ServerViewModel s = new ServerViewModel
                {
                    Id = newId,
                    Name = "Sample firstname " + newId,
                    Users = newId,
                    Online = newId
                };
                ServersList.Add(s);
                //Wait 1 second to create students in different times to be able to see ordering in data table
                Thread.Sleep(800);
            }
            return RedirectToAction(nameof(Show));
        }

        // GET: Students/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Show));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Show));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Show));
            }
            catch
            {
                return View();
            }
        }
    }
}
