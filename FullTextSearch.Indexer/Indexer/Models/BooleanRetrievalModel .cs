using CrawlerIR2.Indexer;
using FullTextSearch.Indexer.Query;
using FullTextSearch.SimpleLogger;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FullTextSearch.Indexer.Indexer.Models
{
    public class BooleanRetrievalModel : IRetrievalModel
    {
        private const string _notKW = "NOT";
        private const string _andKW = "AND";
        private const string _orKW = "OR";
        private const string BOOL_QUERY = @"(\()|(\))|(AND)|(OR)|(NOT)";
        private readonly IPreprocessing _preprocessing;

        public BooleanRetrievalModel(IPreprocessing preprocessing)
        {
            _preprocessing = preprocessing;
        } 

        List<IResult> IRetrievalModel.EvaluateResults(string query, Index index)
        {
            List<IResult> results = new List<IResult>();
            Logger.Info("BooleanRetrievalModel: parsing query");
            var q = ParseQuery(query);
            Logger.Info("BooleanRetrievalModel: evaluating terms");
            foreach (int doc in index.IndexedDocuments)
            {
                if (q.Eval(new EvaluateTerms(index, _preprocessing, doc.ToString())))
                {
                    results.Add(new Result(doc.ToString()));
                }
            }
            Logger.Info("BooleanRetrievalModel: done");
            return results;
        }

        private string[] GetTokens(string query)
        {
            return query == null || query == "" ? null : Regex.Split(query, BOOL_QUERY);
        }

        private ExpressionTree.Node ParseQuery(string query)
        {
            int idx = 0;
            string[] tokens = GetTokens(query);
            tokens = FilterTokens(tokens);

            // TODO: Expression tree start
            return ParseExpression(tokens, ref idx); // Rule 1
        }

        private string[] FilterTokens(string[] tokens)
        {
            List<string> newTokens = new List<string>();
            foreach (string token in tokens)
            {
                if ((token == "") || (token == " "))
                {
                    continue;
                }
                newTokens.Add(token);
            }
            return newTokens.ToArray();
        }

        private ExpressionTree.Node ParseExpression(string[] tokens, ref int index)
        {
            ExpressionTree.Node leftExp = ParseSubExp(tokens, ref index);
            if (index >= tokens.Length) 
            {
                return leftExp;
            } // last token => Rule 2
                
            string token = tokens[index];
            if (token == ")") // Closure ')' - force return expression TODO:!!!!
            {
                return leftExp;
            } 
            else if (token == _andKW) // Rule 3
            {
                index++;
                ExpressionTree.Node rightExp = ParseExpression(tokens, ref index);
                return new ExpressionTree.AndNode(leftExp, rightExp);
            }
            else if (token == _orKW) // Rule 4
            {
                index++;
                ExpressionTree.Node rightExp = ParseExpression(tokens, ref index);
                return new ExpressionTree.OrNode(leftExp, rightExp);
            }
            else
            {
                throw new Exception("Expected '&&' or '||' or EOF");
            }
        }

        private ExpressionTree.Node ParseSubExp(string[] tokens, ref int index)
        {
            string token = tokens[index];
            if (token == "(")
            {
                index++;
                ExpressionTree.Node node = ParseExpression(tokens, ref index);

                if (tokens[index] != ")")
                    throw new Exception("Expected ')'");

                index++; // Skip ')'

                return node;
            }
            else if (token == _notKW)
            {
                index++;
                ExpressionTree.Node node = ParseExpression(tokens, ref index);
                return new ExpressionTree.NotNode(node);
            }
            else
            {
                index++;
                return new ExpressionTree.RoleNode(token);
            }
        }

    }
}
