using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
namespace Lexical
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.Write("Input code: ");
			var input = Console.ReadLine();
			using (var reader = new StringReader(input)) {
				using (var lex = new Lexer(1, reader)) {
					Console.WriteLine(string.Join("  ", lex.ReadWords().Select(i => '\'' + i.ToString() + '\'')));
				}
			}
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}