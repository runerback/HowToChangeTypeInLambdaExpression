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

		protected override Expression VisitUnary(UnaryExpression node)
		{
			if (node.NodeType == ExpressionType.Convert)
			{
				var operand = node.Operand;
				if (operand.NodeType == ExpressionType.MemberAccess)
				{
					var propExp = (MemberExpression)operand;
					if (propExp.Member.MemberType == System.Reflection.MemberTypes.Property)
					{
						var accExp = propExp.Expression;
						if (accExp.NodeType == ExpressionType.Parameter)
						{
							var paramExp = (ParameterExpression)accExp;
							if (paramExp.Type == typeof(IData))
							{
								//remove the convert, because VisitMember will be the next
								return Visit(node.Operand);
							}
						}
					}
				}
			}
			return base.VisitUnary(node);
		}
	}
}
