using System.Security.Principal;

namespace FullTextSearch.Indexer.Query
{
    // Source: https://gist.github.com/meziantou/10603804
    class ExpressionTree
    {
        public abstract class Node
        {
            public abstract bool Eval(EvaluateTerms evaluate);
        }

        public abstract class UnaryNode : Node
        {
            private readonly Node _expression;

            public Node Expression
            {
                get { return _expression; }
            }

            protected UnaryNode(Node expression)
            {
                _expression = expression;
            }
        }

        public abstract class BinaryNode : Node
        {
            private readonly Node _leftExpression;
            private readonly Node _rightExpression;

            public Node LeftExpression
            {
                get { return _leftExpression; }
            }

            public Node RightExpression
            {
                get { return _rightExpression; }
            }

            protected BinaryNode(Node leftExpression, Node rightExpression)
            {
                _leftExpression = leftExpression;
                _rightExpression = rightExpression;
            }
        }

        public class AndNode : BinaryNode
        {
            public AndNode(Node leftExpression, Node rightExpression)
                : base(leftExpression, rightExpression)
            {
            }

            public override bool Eval(EvaluateTerms evaluate)
            {
                return LeftExpression.Eval(evaluate) && RightExpression.Eval(evaluate);
            }
        }

        public class OrNode : BinaryNode
        {
            public OrNode(Node leftExpression, Node rightExpression)
                : base(leftExpression, rightExpression)
            {
            }

            public override bool Eval(EvaluateTerms evaluate)
            {
                return LeftExpression.Eval(evaluate) || RightExpression.Eval(evaluate);
            }
        }

        public class NotNode : UnaryNode
        {
            public NotNode(Node expression)
                : base(expression)
            {
            }

            public override bool Eval(EvaluateTerms evaluate)
            {
                return !Expression.Eval(evaluate);
            }
        }

        public class RoleNode : Node
        {
            private readonly string _roleName;

            public string RoleName
            {
                get { return _roleName; }
            }

            public RoleNode(string roleName)
            {
                _roleName = roleName;
            }

            public override bool Eval(EvaluateTerms evaluate)
            {
                return evaluate.Evaluate(RoleName);
            }
        }
    }
}
