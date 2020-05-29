using BlogsSystem.DAL;
using BlogsSystem.Dto;
using BlogsSystem.IBLL;
using BlogsSystem.IDAL;
using BlogsSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.BLL
{
    public  class ArticleManager : IArticleManager
    {
        public async Task CreateArticle(string title, string content, Guid userId, Guid[] categoryIds)
        {
            using (var articleSvc=new ArticleService())
            {
                Article article = new Article()
                {
                    Title=title,
                    Content=content,
                    UserId=userId
                };
                await articleSvc.CreateAsync(article);
                Guid id = article.Id;
                using (var articleTocategory=new ArticleToCategoryService())
                {
                    foreach (var item in categoryIds)
                    {
                        await articleTocategory.CreateAsync(new ArticleToCategory(){
                        ArticleCategoryId=item,
                        ArticleId=id
                        },autoSave:false);
                    }
                    await articleTocategory.Save();
                }
            }
        }

        public async Task CreateCategory(string name, Guid userId)
        {
            using (var categorySvc=new CategoryService())
            {
                await categorySvc.CreateAsync(new BlogCategory()
                {
                    CategoryName = name,
                    UserId=userId
                });
            }
        }

        public async Task EditArticle(Guid articleId, string title, string content, Guid[] categoryId)
        {
            using (IDAL.IArticleService articleService=new  ArticleService())
            {
                var article =await articleService.GetOneByIdAsync(articleId);
                article.Title = title;
                article.Content = content;
                await articleService.EditAsync(article);
            }

            using (DAL.ArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
            {
                foreach (var item in articleToCategoryService.GetAllAsync().Where(m => m.ArticleId == articleId))
                {
                    await articleToCategoryService.RemoveAsync(item, false);
                }
                foreach (var item in categoryId)
                {
                    await articleToCategoryService.CreateAsync(new ArticleToCategory()
                    {
                        ArticleId = articleId,
                        ArticleCategoryId = item
                    }, false);
                }
                await articleToCategoryService.Save();
            }
        }

        public async Task EditCategory(Guid categoryId, string newName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsArticle(Guid articleId)
        {
            using (IArticleService articleService=new  ArticleService())
            {
                return await articleService.GetAllAsync().AnyAsync(m=>m.Id==articleId);
            }
             
        }

        public async Task<List<ArticleDto>> GetAllArticleByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticleByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticleGroupPage(Guid userId,int currentIndex,int pageSize ,bool asc=true)
        {
            using (IArticleService service=new ArticleService())
            {
                var list = await GetAllArticleByUserId(userId);
                 list = list.Skip(currentIndex*pageSize).Take(pageSize).ToList();
            return list;

            }
        }

        public async Task<List <ArticleDto>> GetAllArticleByUserId(Guid userId)
        {
            using (var service =new ArticleService() )
            {
                var list = await service.GetAllAsync().Include(m => m.User).Where(m => m.UserId == userId).Select(m => new ArticleDto()
                {
                    Id = m.Id,
                    Email = m.User.Email,
                    UserId = m.UserId,
                    CreateTime = m.CreatTime,
                    Title = m.Title,
                    ImagePath = m.User.ImagePath,
                    Content = m.Content,
                    BadCount = m.BadCount,
                    GoodCount = m.GoodCount

                }).OrderByDescending(m=>m.CreateTime).ToListAsync();
        
                using (IArticleToCategory articleToCategoryServic = new ArticleToCategoryService())
                {
                    foreach (var item in list)
                    {
                        var newList = await articleToCategoryServic.GetAllAsync().Include(m => m.BlogCategory).Where(m => m.ArticleId == item.Id).ToListAsync();
                        item.CategoryIds = newList.Select(m => m.ArticleCategoryId).ToArray();
                        item.CategoryNames = newList.Select(m => m.BlogCategory.CategoryName).ToArray();
                    }
                    return list;
                }
            }

        }
           

        public async Task<List<BlogCategoryDto>> GetAllCategory(Guid userId)
        {
            using (ICategory service=new CategoryService())
            {
                return await service.GetAllAsync().Where(m=>m.UserId==userId).Select(m => new BlogCategoryDto()
                {
                    Id=m.Id,
                    CategoryName=m.CategoryName
                }).ToListAsync();
            }
        }

        public async Task<ArticleDto> GetOneArticleById(Guid ArticleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllAsync().Include(m => m.User)
                    .Where(m => m.Id == ArticleId).Select(m => new Dto.ArticleDto()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreatTime,
                        BadCount = m.BadCount,
                        GoodCount = m.GoodCount,
                        Email = m.User.Email,
                        ImagePath = m.User.ImagePath
                    }).FirstOrDefaultAsync();

                using (IArticleToCategory service = new ArticleToCategoryService())
                {
                    var categiryList = await service.GetAllAsync().Include(m => m.BlogCategory)
                        .Where(m => m.ArticleId == list.Id).ToListAsync();
                    list.CategoryIds = categiryList.Select(m => m.BlogCategory.Id).ToArray();
                    list.CategoryNames = categiryList.Select(m => m.BlogCategory.CategoryName).ToArray();
                    return list;
                }
            }

        }

        public async Task<int> GetPageCount(Guid userId)
        {
            using (IArticleService articleService=new ArticleService())
            {
               return await articleService.GetAllAsync().CountAsync(m=>m.UserId==userId);
            }
        }

        public async Task RemoveArticle(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCategory(string name)
        {
            throw new NotImplementedException();
        }

        public async Task GoodCountsAdd(Guid ArticleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                var article = await articleService.GetOneByIdAsync(ArticleId);
                article.GoodCount++;
                await articleService.EditAsync(article);
            }
        }

        public async Task BadCountsAdd(Guid ArticleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                var article = await articleService.GetOneByIdAsync(ArticleId);
                article.BadCount++;
                await articleService.EditAsync(article);
            }
        }
        public async Task CreateComment(Guid articleId,Guid userId,string  content)
        {
            using (ICommentService commentService=new CommmentService())
            {
                await commentService.CreateAsync(new Comment() { 
                UserId=userId,
                ArticleId=articleId,
                Content=content
                });
            }
        }

        public async Task<List< Dto.CommentDto>> GetCommentByArticleId(Guid articleId)
        {
            using (ICommentService service=new CommmentService())
            {
                return await service.GetOrderAsync(false).Where(m=>m.ArticleId==articleId).Include(m => m.User).Select(m=>new CommentDto() { 
                UserId=m.UserId,
                ArticleId=m.ArticleId,
                CreateTime=m.CreatTime,
                Comment=m.Content,
                Email=m.User.Email,
                Id=m.Id
                }).ToListAsync();
            }
        }
    }
}
