using guesttalktask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using guesttalktask.Helper;

namespace guesttalktask.Controllers
{
    public class UsersController: Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UsersController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult AddUserPage() { 
            return View("AddUser",new User());
        }
        public IActionResult AddUser([FromForm] User user)
        {
            XMLreader userdb = new();
            if (userdb.Load())
            {
                Models.User tempusr = new User();
                tempusr.Name = user.Name;
                tempusr.Email = user.Email;
                tempusr.Password = user.Password;
                userdb.Users.Add(tempusr);
                userdb.Save();
            }
            else
                throw new Exception("no xml file");

            return RedirectToAction("ListUserPage");
        }
        public IActionResult ListUserPage()
        {
            XMLreader userdb = new ();
            userdb.Load();
            return View("ShowUser", userdb.Users);
        }
    }
}
