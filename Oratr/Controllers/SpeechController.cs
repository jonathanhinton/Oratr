using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oratr.DAL;
using Oratr.Models;
using Microsoft.AspNet.Identity;


namespace Oratr.Controllers
{
    public class SpeechController : Controller
    {
        private OratrRepository Repo = new OratrRepository();
        // GET: Speech
        public ActionResult Index()
        {
<<<<<<< Updated upstream
            return View(Repo.GetSpeeches());
=======
            return View();
        }

        // GET: Speech/SetWPM
        public ActionResult SetWPM()
        {
            return View();
        }

        // POST: Speech/SetWPM
        [ResponseType(typeof(void))]
        [HttpPost]
        public ActionResult SetWPM(object wpm_test)
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser user = Repo.GetUser(user_id);
            string wpm = wpm_test.ToString();
            Repo.CalculateUserWPM(user, wpm);

            return RedirectToAction("Index");
>>>>>>> Stashed changes
        }

        // GET: Speech/Details/5
        public ActionResult Details(int id)
        {
            Speech found_speech = Repo.GetSpeech(id);

            if (found_speech == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(found_speech);
            }
        }

        // GET: Speech/Create
        public ActionResult Create()
        {
            ViewBag.Error = false;
            return View();
        }

        // POST: Speech/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string Title = collection.Get("SpeechTitle");
                string Body = collection.Get("SpeechBody");

                string user_id = User.Identity.GetUserId();
                ApplicationUser user = Repo.GetUser(user_id);

                if(user != null)
                {
                    Repo.AddSpeech(Title, Body, user);
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Speech/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Speech/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Speech/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Speech/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
