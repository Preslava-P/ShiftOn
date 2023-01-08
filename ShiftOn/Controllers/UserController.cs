using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Migrations;
using ShiftOn.Models;

namespace ShiftOn.Controllers
{
    public class UserController
    {
        public IRepository<User> _repo;

        public UserController (IRepository<User> repo)
        {
            _repo = repo;
        }

        //Get: User
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAsync());
        }

        //Get: User/Create
        public IActionResult Create()
        {
            return View();
        }

        //Post: User/Create
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

        //Post: User/Edit
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
