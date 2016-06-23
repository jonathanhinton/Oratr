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
            return View();
        }

        // GET: Speech/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Speech/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Speech/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string Title = collection.Get("SpeechTitle");

                string user_id = User.Identity.GetUserId();
                ApplicationUser user = Repo.GetUser(user_id);
                
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
