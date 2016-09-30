using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Lexical
{
	/// <summary>
	/// Description of StrToken.
	/// </summary>
	public class StrToken:Token
	{
		string literal;
		public string Literal{ get { return literal; } }
		public StrToken(int lineNumber, string literal)
			: base(lineNumber, TokenType.String)
		{
			this.literal = literal;
		}
		public override string ToString()
		{
			return literal;
		}
	}
}
