using BankingWebApp.Models;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingWebApp.Controllers
{
    public class LogInController : Controller
    {

        private IUserManager userManager { get; }

        public LogInController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        // GET: LogInController
        public ActionResult Index()
        {
            return View();
        }

        // POST: LogInController/Index
        [HttpPost]
        public ActionResult Index(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.getUserByLogin(loginVM.accountNb, loginVM.password);

                if(user != null)
                {
                    HttpContext.Session.SetString("userId", user.accountNb);
                    return RedirectToAction("Index", "Accounts");
                }
            }

            ModelState.AddModelError("", "Could not find an account with the provided password.");
            return View(loginVM);
        }
    }
}
