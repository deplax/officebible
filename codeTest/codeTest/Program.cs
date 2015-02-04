using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeTest
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("HelloWorld!");
			//http://inobae.blog.me/90007564511

            String chosung = "ㄱㄴㄷㄹㅁㅂㅅㅇㅈㅊㅋㅌㅍㅎ";
            Console.WriteLine(chosung.IndexOf("ㄴ"));
			ushort uni = 0xAC00;
            Console.WriteLine(Convert.ToChar(uni));

		}
	}
}
