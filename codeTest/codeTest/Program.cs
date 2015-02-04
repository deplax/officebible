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

            String initial = "ㄱㄴㄷㄹㅁㅂㅅㅇㅈㅊㅋㅌㅍㅎ";
			String medial = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
			String final = "ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
            Console.WriteLine(initial.IndexOf("ㄴ"));
			ushort uni = 0xAC00;
            Console.WriteLine(Convert.ToChar(uni));

            //초성 중성 종성 이 있는 한 글자 클래스를 만들고
			
			//문자열을 넣으면
			//들어온 문자열을 초중성 클래스 배열로 반환한다.
			//---------------------------------------------

			//반환된 배열을 기존에 있는 리스트에 넣는다.
			//리스트에서 정렬 후에 가장 근접한 단어를 반환한다.


		}
	}

	class ParseChar
	{
		int initial, medial, 
	}
}
