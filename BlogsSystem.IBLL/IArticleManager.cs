using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.IBLL
{
    public interface IArticleManager
    {
        Task CreateArticle(string title, string content, Guid userId, Guid[] categoryIds);
        Task CreateCategory(string name, Guid userId);
        Task<List<Dto.BlogCategoryDto>> GetAllCategory(Guid userId);
        Task<List<Dto.ArticleDto>> GetAllArticleByEmail(string email);
        Task<List<Dto.ArticleDto>> GetAllArticleGroupPage(Guid userId, int pageSize = 10, int pageIndex = 0, bool asc = true);
       
        Task<List<Dto.ArticleDto>> GetAllArticleByCategoryId(Guid categoryId);
        Task RemoveCategory(string name);
        Task EditCategory(Guid categoryId,string newName);
        Task RemoveArticle(Guid Id);
        Task EditArticle(Guid articleId,string title,string content,Guid[] categoryId);
        Task<bool> ExistsArticle(Guid articleId);
        Task<Dto.ArticleDto> GetOneArticleById (Guid ArticleId);
        Task<int> GetPageCount(Guid uerId);
        Task GoodCountsAdd(Guid ArticleId);
        Task BadCountsAdd(Guid ArticleId);
        Task CreateComment(Guid articleId, Guid userId, string content);
        Task<List<Dto.CommentDto>> GetCommentByArticleId(Guid articleId);
    }
}
