using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Migrations;
using ShiftOn.Models;

namespace ShiftOn.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<UserController> _repo; 

        public UserController (IRepository<UserController> repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
                return View(new User());

            var item = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = collection["FirstName"],
                LastName = collection["LastName"]
            };

            await _repo.AddAsync(item);
            await _repo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Guid id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            _repo.Delete(id);
            await _repo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
