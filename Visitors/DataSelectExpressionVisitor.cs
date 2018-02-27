using System;
using System.Linq.Expressions;

namespace HowToChangeTypeInLambdaExpression
{
	internal class DataSelectExpressionVisitor<TSource, TKey> : DataExpressionVisitor<TSource>
			where TSource : class, IData
	{
		public DataSelectExpressionVisitor()
		{
			UpdateLambdaFactory<TKey>();
		}

		public Expression<Func<TSource, TKey>> Select(Expression<Func<IData, TKey>> keySelector)
		{
			return (Expression<Func<TSource, TKey>>)base.Visit(keySelector);
		}
	}
}
