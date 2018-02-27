
namespace HowToChangeTypeInLambdaExpression
{
	public class DataModel : IData
	{
		public int? Value1 { get; set; }
		public string Value2 { get; set; }

		TypeInEnum? IData.Value1
		{
			get
			{
				return Value1.HasValue ? (TypeInEnum?)Value1.Value : null;
			}
			set
			{
				//ignore enum validation here
				this.Value1 = value.HasValue ? (int?)value.Value : null;
			}
		}
	}
}
