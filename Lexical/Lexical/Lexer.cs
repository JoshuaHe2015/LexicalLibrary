using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.IO;
namespace Lexical
{
	/// <summary>
	/// Description of Lexer.
	/// </summary>
	public class Lexer:IDisposable
	{
		TextReader reader;
		int lineNumber;
		StringBuilder s;
		public Lexer(int lineNumber, TextReader reader)
		{
			this.lineNumber = lineNumber;
			if (reader == null)
				throw new ArgumentNullException();
			this.reader = reader;
			s = new StringBuilder();
		}
		int Peek()
		{
			return reader.Peek();
		}
		int GetChar()
		{
			return reader.Read();
		}
		public IEnumerable<Token> ReadWords()
		{
			for (;;) {
				Token word = ReadWord();
				if (word.type == TokenType.EOF)
					break;
				yield return word;
			}
		}
		Token ReadWord()
		{
			if (s.Length > 0)
				s.Clear();
			while (IsSpace(Peek()))
				GetChar();
			
			int ch = Peek();
			
			if (ch < 0)
				return Token.EOF;//End of text
			if (ch == '"' || ch == '\'') {/* start of string */
				return ReadStr((char)GetChar());
			} else if (IsDigit(ch)) {
				ReadNumber();
				
				if (Peek() == 'E' || Peek() == 'e') {/* Scientific notation */
					
					s.Append((char)GetChar());
					if (Peek() == '+' || Peek() == '-') {
						s.Append((char)GetChar());
					}
					ReadInteger();/* Read exponent */
				}
				   
				return new NumberToken(lineNumber, double.Parse(s.ToString()));
				
			} else if (IsLetterOrUnderline(ch)) {
				do {
					s.Append((char)GetChar());/* Read Id */
					ch = Peek();
				} while (IsLetterOrUnderline(ch) || IsDigit(ch));
				return new IdToken(lineNumber, s.ToString());
				
			} else if (ch == '=')
				return	ReadCompare('=');
			else if (ch == '>')
				return ReadCompare('>');
			else if (ch == '<')
				return ReadCompare('<');
			else if (arithmetic_operators.Concat(brackets).Contains((char)ch)) {
				GetChar();
				return new IdToken(lineNumber, ((char)ch).ToString());
				
			} else if (ch == ';' || ch == ',') {
				GetChar();
				return new IdToken(lineNumber, ((char)ch).ToString());
				
			} else if (ch == '\n') {
				lineNumber++;
				return new IdToken(lineNumber, "\\n");
			} else
				throw new IOException(
					string.Format("line {0}: Unrecognizable token '{1}'", lineNumber, (char)ch));
		}
		Token ReadStr(char start_ch)
		{
			while (Peek() != -1) {
				if (Peek() == start_ch) {
					GetChar();
					if (s.Length == 0)
						return new StrToken(lineNumber, string.Empty);
					else if (s[s.Length - 1] != '\\')
						return new StrToken(lineNumber, s.ToString());
					else
						s.Append(start_ch);
				} else {
					s.Append((char)GetChar());
				}
			}
			throw new IOException();
		}
		void ReadNumber()
		{
			ReadInteger();
			if (Peek() == '.') {/* Read decimal part*/
				GetChar();
				s.Append('.');
				do {
					s.Append((char)GetChar());
				} while (IsDigit(Peek()));
			}
		}
		void ReadInteger()
		{
			do {
				s.Append((char)GetChar());/* Read integer part */
			} while (IsDigit(Peek()));
		}
		Token ReadCompare(char cmp)
		{
			GetChar();
			if (Peek() == '=') {
				GetChar();
				return new IdToken(lineNumber, new string(new []{ cmp, '=' }));
			} else
				return new IdToken(lineNumber, char.ToString(cmp));
		}
		static bool IsLetterOrUnderline(int ch)
		{
			return ('A' <= ch && ch <= 'Z') || ('a' <= ch && ch <= 'z') || ch == '_';
		}
		static bool IsDigit(int ch)
		{
			return '0' <= ch && ch <= '9';
		}
		static bool IsSpace(int ch)
		{
			return ch == '\t' || ch == ' ' || ch == '\r';
		}
		static readonly char[] arithmetic_operators = "+-*/%^".ToCharArray();
		static readonly char[] brackets = "()[]{}".ToCharArray();
		#region IDisposable implementation
		public void Dispose()
		{
			if (reader != null)
				reader.Dispose();
		}
		#endregion
	}
}
