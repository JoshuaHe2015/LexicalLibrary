using System.Linq;
using System;

namespace Lexical
{
	/// <summary>
	/// Description of NumberToken.
	/// </summary>
	public class NumberToken:Token
	{
		double value;
		public double Value { get { return value; } }
		public NumberToken(int lineNumber, double value)
			: base(lineNumber, TokenType.Number)
		{
			this.value = value;
		}
		public override string ToString()
		{
			return value.ToString();
		}
	}
}
