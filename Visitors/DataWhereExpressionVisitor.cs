using System;
using System.Linq.Expressions;

namespace HowToChangeTypeInLambdaExpression
{
	internal class DataWhereExpressionVisitor<TSource> : DataExpressionVisitor<TSource>
			where TSource : class, IData
	{
		public DataWhereExpressionVisitor()
		{
			UpdateLambdaFactory<bool>();
		}

		public Expression<Func<TSource, bool>> Where(Expression<Func<IData, bool>> expression)
		{
			return (Expression<Func<TSource, bool>>)base.Visit(expression);
		}
	}
}
