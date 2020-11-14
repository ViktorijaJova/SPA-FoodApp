using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEDC.FoodApp.Services.Helpers
{
    internal class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression _from;
        private readonly Expression _to;
        internal SwapVisitor(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }

        public override Expression Visit(Expression node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}
