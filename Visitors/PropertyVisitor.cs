using System;
using System.Reflection;

namespace HowToChangeTypeInLambdaExpression
{
	internal class PropertyVisitor<T> where T : class, IData
	{
		protected PropertyVisitor()
		{
			this.sourceType = typeof(T);
		}

		private Type sourceType;

		volatile static PropertyVisitor<T> visitor;
		public static PropertyVisitor<T> Default
		{
			get
			{
				if (visitor == null)
					visitor = new PropertyVisitor<T>();
				return visitor;
			}
		}

		private static readonly string Prefix = typeof(IData).FullName + ".";

		public PropertyInfo Visit(PropertyInfo property)
		{
			var sourceType = this.sourceType;
			string propNameInClass = property.Name;

			var propInInterface = sourceType.GetProperty(Prefix + propNameInClass, BindingFlags.Instance | BindingFlags.NonPublic);
			if (propInInterface == null) //means not implement explicitly
				return property;

			//for simple, say Property in Interface and Class have same name
			var propInClass = sourceType.GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public);
			if (propInClass == null)
				throw new InvalidOperationException(
					string.Format("Cannot find property \"{0}\" in type \"{0}\"", propNameInClass, sourceType));
			return propInClass;
		}
	}
}
