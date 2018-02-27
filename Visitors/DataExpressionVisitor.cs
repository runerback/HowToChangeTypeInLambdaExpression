using System;
using System.Linq.Expressions;

namespace HowToChangeTypeInLambdaExpression
{
	internal class DataExpressionVisitor<TSource> : ExpressionVisitor
			where TSource : class, IData
	{
		protected DataExpressionVisitor() { }

		private Func<LambdaExpression, Expression> CurrentLamdaFactory;
		private void CreateLambdaFactory<TResult>()
		{
			this.CurrentLamdaFactory = node =>
			{
				var newLambda = Expression.Lambda<TResult>(node.Body, node.Parameters);
				return base.VisitLambda(newLambda);
			};
		}

		protected void UpdateLambdaFactory<TKey>()
		{
			CreateLambdaFactory<Func<TSource, TKey>>();
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			return this.CurrentLamdaFactory(node);
		}

		private ParameterExpression parameter = Expression.Parameter(typeof(TSource), "item");

		protected override Expression VisitParameter(ParameterExpression node)
		{
			return parameter;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			if (node.Member.MemberType == System.Reflection.MemberTypes.Property)
			{
				var prop = (System.Reflection.PropertyInfo)node.Member;
				if (prop.DeclaringType == typeof(IData))
				{
					var propInClass = PropertyVisitor<TSource>.Default.Visit(prop);

					if (prop != propInClass)
						return Expression.Property(parameter, propInClass);
				}
			}
			return base.VisitMember(node);
		}
	}
}
