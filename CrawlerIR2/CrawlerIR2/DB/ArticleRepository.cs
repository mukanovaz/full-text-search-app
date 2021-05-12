using CrawlerIR2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CrawlerIR2.DB
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly Context _context;

        private bool disposed = false;

        public ArticleRepository(Context context)
        {
            _context = context;
        }

        public void Delete(int articleId)
        {
            Article employee = _context.Articles.Find(articleId);
            if (employee == null) return;
            _context.Articles.Remove(employee);
        }

        public IEnumerable<Article> GetAll()
        {
            return _context.Articles;
        }

        public Article GetById(int articleId)
        {
            return _context.Articles.Find(articleId);
        }

        public void Insert(Article article)
        {
            //_context.Articles.Add(article);
            _context.Articles.AddIfNotExists<Article>(article, x => x.ArticleId == article.ArticleId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Article article)
        {
            _context.Entry(article).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
