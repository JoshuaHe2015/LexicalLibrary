using System.Linq;
using System;

namespace Lexical
{
	/// <summary>
	/// Description of Token.
	/// </summary>
	public class Token
	{
		TokenType _type;
		public TokenType type{ get { return _type; } }
		readonly int lineNumber;
		public int LineNumber{ get { return lineNumber; } }
		protected Token(int lineNumber, TokenType type)
		{
			this.lineNumber = lineNumber;
			this._type = type;
		}
		
		public static readonly Token EOF = new Token(-1,TokenType.EOF);
		/* End of file */
	}
	public enum TokenType
	{
		Identifier,
		Number,
		String,
		EOF,
	}
}
