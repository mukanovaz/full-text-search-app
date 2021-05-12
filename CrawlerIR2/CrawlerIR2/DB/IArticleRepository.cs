using CrawlerIR2.Models;
using System.Collections.Generic;

namespace CrawlerIR2.DB
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetAll();
        Article GetById(int EmployeeID);
        void Insert(Article employee);
        void Update(Article employee);
        void Delete(int EmployeeID);
        void Save();
    }
}
