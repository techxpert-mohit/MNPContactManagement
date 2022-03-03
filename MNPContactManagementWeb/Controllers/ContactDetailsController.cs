using Microsoft.AspNetCore.Mvc;
using MNPContactManagementWeb.Data;
using MNPContactManagementWeb.Models;

namespace MNPContactManagementWeb.Controllers
{
    public class ContactDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContactDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ContactDetails> objContactList = _db.ContactDetails;
            return View(objContactList);
        }

        public IActionResult Create()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactDetails contactDetails)
        {
            
            if(ModelState.IsValid)
            {
                _db.ContactDetails.Add(contactDetails);
                _db.SaveChanges();
                TempData["success"] = "Contact Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var ContactDetails = _db.ContactDetails.Find(id);
            if (ContactDetails == null)
            {
                return NotFound();
            }
            return View(ContactDetails);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContactDetails contactDetails)
        {

            if (ModelState.IsValid)
            {
                _db.ContactDetails.Update(contactDetails);
                _db.SaveChanges();
                TempData["success"] = "Contact Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
