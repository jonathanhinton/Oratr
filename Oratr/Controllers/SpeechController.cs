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
        // GET: Speech/Speeches
        public ActionResult Speeches()
        {
            return View(Repo.GetSpeeches());
        }

        // GET: Speech
        public ActionResult Index()
        {
            return View();
        }

        // GET: Speech/SetWPM
        public ActionResult SetWPM()
        {
            return View();
        }

        // POST: Speech/SetWPM

        // GET: Speech/Details/5
        public ActionResult Details(int id)
        {
            Speech found_speech = Repo.GetSpeech(id);
            string user_id = User.Identity.GetUserId();
            ApplicationUser user = Repo.GetUser(user_id);

            if (found_speech == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Repo.CalculateDeliveryTime(user, found_speech);
                return View(found_speech);
            }
        }

        // GET: Speech/Practice
        public ActionResult Practice()
        {
            return View();
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
                
                return RedirectToAction("Speeches");
            }
            catch
            {
                return View();
            }
        }

        // GET: Speech/Edit/5
        public ActionResult Edit(int id)
        {
            Speech found_speech = Repo.GetSpeech(id);
            if (found_speech == null)
            {

                RedirectToAction("Speeches");
            }
            return View(found_speech);
        }

        // POST: Speech/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "SpeechId,SpeechTitle,SpeechBody")]Speech speech_to_edit)
        {
            if(ModelState.IsValid)
            {
                Repo.EditSpeech(speech_to_edit);
            }
            return RedirectToAction("Speeches");
        }

        // GET: Speech/Delete/5
        public ActionResult Delete(int id)
        {
            Speech found_speech = Repo.GetSpeech(id);
            if (found_speech != null)
            {
                Repo.RemoveSpeech(id);
            }
            return RedirectToAction("Speeches");
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
