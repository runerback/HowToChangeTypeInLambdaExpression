using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HowToChangeTypeInLambdaExpression
{
	public static class Enumerable1
	{
		public static IEnumerable<TKey> Select1<TSource, TKey>(this IEnumerable<TSource> source, Expression<Func<IData, TKey>> keySelector)
			where TSource : class, IData
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (keySelector == null)
				throw new ArgumentNullException("keySelector");

			return source.Select(new DataSelectExpressionVisitor<TSource, TKey>()
				.Select(keySelector)
				.Compile());
		}

		public static IEnumerable<TSource> Where1<TSource>(this IEnumerable<TSource> source, Expression<Func<IData, bool>> predicate)
			where TSource : class, IData
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (predicate == null)
				throw new ArgumentNullException("predicate");

			return source.Where(new DataWhereExpressionVisitor<TSource>()
				.Where(predicate)
				.Compile());
		}
	}
}
