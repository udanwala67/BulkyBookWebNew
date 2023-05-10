using BulkyBookWebsite.Data;
using BulkyBookWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace BulkyBookWebsite.Controllers
{
    public class CatagoryController : Controller
    {

        private readonly BulkyDbContext _bulkyDbContext;


        public CatagoryController(BulkyDbContext bulkyDbContext)
        {
            _bulkyDbContext = bulkyDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Catagory> ObjectCatagoryList = _bulkyDbContext.Catagories.ToList();
            return View(ObjectCatagoryList);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Catagory item)
        {
            if (item.Name == item.DispalyOrder.ToString())
            {
                ModelState.AddModelError("name", "name and order can't exactly match");
            }
            if (ModelState.IsValid)
            {
                var ExistingName = _bulkyDbContext.Catagories.Where(NameOfUser => NameOfUser.Name == item.Name).FirstOrDefault();

                if (ExistingName == null)
                {
                    item.CreatedDateTime = DateTime.Now;
                    _bulkyDbContext.Catagories.Add(item);
                    _bulkyDbContext.SaveChanges();
                    TempData["success"] = "Created successfully!";
                    return RedirectToAction("Index", "Catagory");

                }
                else
                {
                    ModelState.AddModelError("Name", "Given Name is already exist");
                }
            }
            return View(item);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == 0 && Id == null)
            {
                return NotFound();
            }
            else
            {
                var GetIDFromDb = _bulkyDbContext.Catagories.Where(Use => Use.Id == Id).FirstOrDefault();
                if (GetIDFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(GetIDFromDb);
                }
            }
        }

        [HttpPost]

        public IActionResult Edit(Catagory item)
        {
            if (item.Name == item.DispalyOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order Both can't Be same");
            }
            if (ModelState.IsValid)
            {
                item.CreatedDateTime = DateTime.Now;
                _bulkyDbContext.Catagories.Update(item);
                _bulkyDbContext.SaveChanges();
                TempData["success"] = "Edited successfully!";
                return RedirectToAction("Index", "Catagory");

            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == 0 && Id == null)
            {
                return NotFound();
            }
            else
            {
                var GetIdFromDb = _bulkyDbContext.Catagories.Where(u => u.Id == Id).FirstOrDefault();
                if (GetIdFromDb == null)
                {
                    return View();
                }
                return View(GetIdFromDb);

            }

        }

        [HttpPost]

        public IActionResult Delete(Catagory item)
        {
            _bulkyDbContext.Remove(item);
            _bulkyDbContext.SaveChanges();
            TempData["success"] = "Deleted successfully!";
            return RedirectToAction("Index", "Catagory");
        }
    }
}
