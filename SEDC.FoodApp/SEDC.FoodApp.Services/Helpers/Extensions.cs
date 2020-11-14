using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEDC.FoodApp.Services.Helpers
{
    public static class Extensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, bool>> and) 
        {
            if (expr == null) 
            {
                return and;
            }

            var combined = Expression.AndAlso(new SwapVisitor(expr.Parameters[0], and.Parameters[0]).Visit(expr.Body), and.Body);
            return Expression.Lambda<Func<T, bool>>(combined, and.Parameters);
        }
    }
}
