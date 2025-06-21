using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; 
using TruyenAnime.Interfaces; 
using TruyenAnime.Models; 

namespace TruyenAnime.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _repo;

        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Index(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(model);
                _repo.Save();
                return RedirectToAction("ContactConfirmation"); 
            }
            return View(model);
        }

        public IActionResult ContactConfirmation()
        {
            return View();
        }
    }
}