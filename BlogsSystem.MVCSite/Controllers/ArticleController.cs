using BlogsSystem.IBLL;
using BlogsSystem.BLL;
using BlogsSystem.MVCSite.Models.ArticleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Webdiyer.WebControls;
using System.Web.UI;
using Webdiyer.WebControls.Mvc;
using BlogsSystem.Dto;

namespace BlogsSystem.MVCSite.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Filters.BlogSystemAuthAttrebute]
        public async Task < ActionResult> CreateCategory(CategoryViewModels category)
        {
            if (ModelState.IsValid)
            {
                IArticleManager articleManager = new ArticleManager();
                await articleManager.CreateCategory(category.CategoryName,Guid.Parse( Session["userId"].ToString()));
                return RedirectToAction("categoryList","Article");
            }
            ModelState.AddModelError("categoryError","您的输入有误，请重新输入");
            return View(category);
        }
        [HttpGet]
        [Filters.BlogSystemAuthAttrebute]
        public async Task< ActionResult> CategoryList()
        {
            return View(await new ArticleManager().GetAllCategory(Guid.Parse(Session["userId"].ToString())));
        }
        [HttpGet]
        [Filters.BlogSystemAuthAttrebute]
        public async Task<ActionResult>CreateArticle()
        {
            var userid = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds =await new ArticleManager().GetAllCategory(userid); 
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult>CreateArticle(CreateArticleViewModels models)
        {
            if (ModelState.IsValid)
            {
                var userId =Guid.Parse( Session["userId"].ToString());
                await new ArticleManager().CreateArticle(models.Title,models.Content,userId,models.CategoryIds);
                return RedirectToAction("ArticleList","Article");
            }
            ModelState.AddModelError("CreateArticleError","您的数据输入有误");
            return View(models);
        }
        [HttpGet]
        [Filters.BlogSystemAuthAttrebute]
        public async Task<ActionResult>ArticleList(int currentIndex=1,int pageSize=4) 
        {
            var articleManager =new ArticleManager();
            var userId = Guid.Parse(Session["userId"].ToString());
            var articles = await articleManager.GetAllArticleGroupPage(userId, currentIndex-1, pageSize);
            var count = await articleManager.GetPageCount(userId);
            //ViewBag.PageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1;
            //ViewBag.CurrentIndex = currentIndex;
            return View(new PagedList<Dto.ArticleDto>(articles,currentIndex,pageSize,count));
        }
        
        public async Task<ActionResult>ArticleDetails(Guid? id)
        {
            ArticleManager articleManager = new ArticleManager();

            if (id == null || !await articleManager.ExistsArticle(id.Value))
                return RedirectToAction(nameof(ArticleList));
            ViewBag.Comments =await articleManager.GetCommentByArticleId(id.Value);
            return View(await articleManager.GetOneArticleById(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult>EditArticle(Guid articleId)
        {
            IArticleManager articleManager = new ArticleManager();
            var editArticle = await articleManager.GetOneArticleById(articleId);
            var userid = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategory(userid);
            return View(new EditArticleViewModel() { 
            Id=editArticle.Id,
            Title=editArticle.Title,
            Content=editArticle.Content,
            CategoryIds=editArticle.CategoryIds
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(EditArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IArticleManager articleManager = new ArticleManager();
            await articleManager.EditArticle(model.Id,model.Title,model.Content,model.CategoryIds);
            return RedirectToAction("ArticleList");
        }

        [HttpPost]
        public async Task<ActionResult>GoodCounts(Guid Id )
        {
            IArticleManager articleManager = new ArticleManager();
            await articleManager.GoodCountsAdd(Id);
            return Json(new { result="OK" });
        }

        [HttpPost]
        public async Task<ActionResult> BadCounts(Guid Id)
        {
            IArticleManager articleManager = new ArticleManager();
            await articleManager.BadCountsAdd(Id);
            return Json(new { result = "OK" });
        }

        [HttpPost] 
        public async Task<ActionResult> AddComment(CreateCommentsViewModel model)
        {
            IArticleManager articleManager = new ArticleManager();
            var userId = Guid.Parse(Session["userId"].ToString());
            await articleManager.CreateComment(model.Id,userId,model.Comment);
            return Json(new { result="OK"});
        }
    }
}