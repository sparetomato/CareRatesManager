using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareRatesManager.Data;
using CareRatesManager.Models;
using X.PagedList;

namespace CareRatesManager.Controllers
{
    public class PostcodesController : Controller
    {
        private readonly PostcodeContext _context;

        public PostcodesController(PostcodeContext context)
        {
            _context = context;
        }

        // GET: Postcodes
        public object Index(int? page)
        {
            var postcodes = from p in _context.Postcodes
                            //join r in _context.Rates on p.RateId equals r.RateId
                            select p;

            int pageNumber = page ?? 1;
            var onePageOfPostcodes = postcodes.ToPagedList(pageNumber, 20);
            ViewBag.OnePageOfPostcodes = onePageOfPostcodes;
            return View();
        }




        // GET: Postcodes/Details/CV346GB
        public string Details(string postcode)
        {

            PostcodeModel postcodeDetails = (from p in _context.Postcodes.Include("RateDetails")
                                   //join r in _context.Rates on p.RateId equals r.RateId
                                   where p.Postcode == postcode
                                   select p)
                                  .FirstOrDefault();
                //;
            return postcodeDetails.ToJSONString();
        }

        // GET: Postcodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postcodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Zone,RuralOrUrban,Rate")] PostcodeModel postcode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postcode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postcode);
        }

        // GET: Postcodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postcode = await _context.Postcodes.FindAsync(id);
            if (postcode == null)
            {
                return NotFound();
            }
            return View(postcode);
        }

        // POST: Postcodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Zone,RuralOrUrban,Rate")] PostcodeModel postcode)
        {
            if (id != postcode.Postcode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postcode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostcodeExists(postcode.Postcode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postcode);
        }

        // GET: Postcodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postcode = await _context.Postcodes
                .FirstOrDefaultAsync(m => m.Postcode == id);
            if (postcode == null)
            {
                return NotFound();
            }

            return View(postcode);
        }

        // POST: Postcodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var postcode = await _context.Postcodes.FindAsync(id);
            _context.Postcodes.Remove(postcode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostcodeExists(string id)
        {
            return _context.Postcodes.Any(e => e.Postcode == id);
        }
    }
}
