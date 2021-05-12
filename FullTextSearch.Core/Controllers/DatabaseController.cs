using CrawlerIR2.DB;
using CrawlerIR2.Models;
using System.Collections.Generic;

namespace FullTextSearch.Core.Controllers
{
    public class DatabaseController
    {
        private IArticleRepository _articleRepository;

        public DatabaseController(string dbname)
        {
            _articleRepository = new ArticleRepository(new Context(dbname));
        }

        public DatabaseController(IArticleRepository employeeRepository)
        {
            _articleRepository = employeeRepository;
        }

        public void UpdateArticle(Article article)
        {
            if (article == null)
            {
                return;
            }
            _articleRepository.Update(article);
            _articleRepository.Save();
        }

        public void AddArticle(Article article)
        {
            if (article == null)
            {
                return;
            }
            _articleRepository.Insert(article);
            _articleRepository.Save();
        }

        public void DeleteArticle(int articleId)
        {
            _articleRepository.Delete(articleId);
            _articleRepository.Save();
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _articleRepository.GetAll();
        }

        public Article GetArticleById(int articleId)
        {
            return _articleRepository.GetById(articleId);
        }
    }
}
