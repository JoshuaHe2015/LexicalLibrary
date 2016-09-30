using System.Linq;
using System;

namespace Lexical
{
	/// <summary>
	/// Description of IdToken.
	/// </summary>
	public class IdToken:Token
	{
		string text;
		public string Text{ get { return text; } }
		public IdToken(int lineNumber, string text)
			: base(lineNumber, TokenType.Identifier)
		{
			this.text = text;
		}
		public override string ToString()
		{
			return text;
		}
	}
}
