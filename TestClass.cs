using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HowToChangeTypeInLambdaExpression
{
	[TestClass]
	public class TestClass
	{
		private IEnumerable<DataModel> source;

		[TestInitialize]
		public void CreateData()
		{
			this.source = Enumerable
				   .Range(0, 12)
				   .Select(item =>
				   {
					   int index = item % 3;
					   return new DataModel
					   {
						   Value1 = index + 1,
						   Value2 = ((char)(index + 65)).ToString()
					   };
				   })
				   .ToArray();
		}

		[TestMethod]
		public void WhereSameType()
		{
			var source = this.source;

			var query = source.Where(item => item.Value2 == "A");
			var query1 = source.Where1(item => item.Value2 == "A");
			Assert.IsTrue(query.SequenceEqual(query1));
		}

		[TestMethod]
		public void WhereDiffType()
		{
			var source = this.source;

			var query = source.Where(item => item.Value1 == 1);
			var query1 = source.Where1(item => item.Value1 == TypeInEnum.A);
			Assert.IsTrue(query.SequenceEqual(query1));
		}

		[TestMethod]
		public void SelectSameType()
		{
			var source = this.source;

			var query = source.Select(item => item.Value2);
			var query1 = source.Select1(item => item.Value2);
			Assert.IsTrue(query.SequenceEqual(query1));
		}

		[TestMethod]
		public void SelectDiffType()
		{
			var source = this.source;

			var query = source.Select(item => item.Value1);
			var query1 = source.Select1(item => item.Value1).Cast<int?>();

			Assert.IsTrue(query.SequenceEqual(query1));
		}
	}
}
