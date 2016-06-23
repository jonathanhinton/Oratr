using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Oratr.DAL;
using Oratr.Models;

namespace Oratr.Controllers
{
    public class SpeechesController : ApiController
    {
        private OratrContext db = new OratrContext();
        private OratrRepository Repo = new OratrRepository();

        // GET: api/Speeches
        public IQueryable<Speech> GetSpeeches()
        {
            return db.Speeches;
        }

        // GET: api/Speeches/5
        [ResponseType(typeof(Speech))]
        public IHttpActionResult GetSpeech(int id)
        {
            Speech speech = db.Speeches.Find(id);
            if (speech == null)
            {
                return NotFound();
            }

            return Ok(speech);
        }

        // PUT: api/Speeches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpeech(int id, Speech speech)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != speech.SpeechId)
            {
                return BadRequest();
            }

            db.Entry(speech).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeechExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Speeches
        [ResponseType(typeof(Speech))]
        public IHttpActionResult PostSpeech(Speech speech)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Speeches.Add(speech);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = speech.SpeechId }, speech);
        }

        // DELETE: api/Speeches/5
        [ResponseType(typeof(Speech))]
        public IHttpActionResult DeleteSpeech(int id)
        {
            Speech speech = db.Speeches.Find(id);
            if (speech == null)
            {
                return NotFound();
            }

            db.Speeches.Remove(speech);
            db.SaveChanges();

            return Ok(speech);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpeechExists(int id)
        {
            return db.Speeches.Count(e => e.SpeechId == id) > 0;
        }
    }
}