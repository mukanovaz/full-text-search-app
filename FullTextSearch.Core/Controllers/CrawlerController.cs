using CrawlerIR2.Crawler;
using CrawlerIR2.Models;
using FullTextSearch.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Core
{
    class CrawlerController
    {
        private const string DataFolder = @"Data\";
        private const string MotorkariData1 = @"CrawlerIR_Html.xml";
        private const string MotorkariData2 = @"CrawlerIR_Html64.xml";
        private const string MotorkariData3 = @"CrawlerIR_Html128.xml";
        private const string MotorkariData4 = @"CrawlerIR_Html192.xml";

        public List<Article> GetDataFromFiles(string db_name)
        {
            List<Article> list = new List<Article>();
            Context context = new Context(db_name);
            if (context.Articles == null || context.Articles.Count() == 0)
            {
                DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData1), ref list, db_name);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData2), ref list, db_name);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData3), ref list, db_name);
                //DataReader.Instance.ReadData(Path.Combine(Environment.CurrentDirectory, DataFolder, MotorkariData4), ref list, db_name);
            }
            else
            {
                list = context.Articles.ToList();
            }

            return list;
        }

        public List<Article> GetDataFromWeb(ICrawler crawler)
        {
            return crawler == null ? null : crawler.GetArticles();
        }

    }
}
