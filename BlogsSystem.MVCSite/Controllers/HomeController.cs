using BlogsSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogsSystem.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Models.UserViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager user = new UserManager();
                await user.Register(model.Email, model.Password);
                return Content("<p>注册成功</p>");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Login(Models.UserViewModels.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManage manage = new UserManager();
                Guid userId;
                if ( manage.Login(model.Email,model.Password,out userId))
                {
                    if (model.IsRemember)
                    {
                        Response.Cookies.Add(new HttpCookie("userName")
                        {
                            Value=model.Email,
                            Expires=DateTime.Now.AddDays(10)
                        });
                        Response.Cookies.Add(new HttpCookie("userId")
                        {
                            Value =userId.ToString(),
                            Expires = DateTime.Now.AddDays(10)
                        });
                    }
                    else
                    {
                        Session["userName"] = model.Email;
                        Session["userId"] = userId;
                    }

                    return RedirectToAction("main","Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "您的账号或密码有误");
                }
            }
            
            return View(model);

        }
        [Filters.BlogSystemAuthAttrebute]
        public ActionResult main()
        {
            return View("Contact");
        }
    }
}