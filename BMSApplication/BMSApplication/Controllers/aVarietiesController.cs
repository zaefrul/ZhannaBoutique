using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BMSApplication.Models;

namespace BMSApplication.Controllers
{
    public class aVarietiesController : ApiController
    {
        private BMSApplicationContext db = new BMSApplicationContext();

        // GET: api/aVarieties
        public IQueryable<Variety> GetVarieties()
        {
            return db.Varieties;
        }

        // GET: api/aVarieties/5
        [ResponseType(typeof(Variety))]
        public async Task<IHttpActionResult> GetVariety(int id)
        {
            Variety variety = await db.Varieties.FindAsync(id);
            if (variety == null)
            {
                return NotFound();
            }

            return Ok(variety);
        }

        // PUT: api/aVarieties/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVariety(int id, Variety variety)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != variety.Id)
            {
                return BadRequest();
            }

            db.Entry(variety).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VarietyExists(id))
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

        // POST: api/aVarieties
        [ResponseType(typeof(Variety))]
        public async Task<IHttpActionResult> PostVariety(Variety variety)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Varieties.Add(variety);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = variety.Id }, variety);
        }

        // DELETE: api/aVarieties/5
        [ResponseType(typeof(Variety))]
        public async Task<IHttpActionResult> DeleteVariety(int id)
        {
            Variety variety = await db.Varieties.FindAsync(id);
            if (variety == null)
            {
                return NotFound();
            }

            db.Varieties.Remove(variety);
            await db.SaveChangesAsync();

            return Ok(variety);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VarietyExists(int id)
        {
            return db.Varieties.Count(e => e.Id == id) > 0;
        }
    }
}