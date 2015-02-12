using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//프로그래밍 목적 정리
//1. 빠르고 정확하게 띄울 수 있을 것
//ㄴ 필드에 들어가면 한글로 자동 전환해준다.
//ㄴ 최소의 클릭을 사용하도록 한다.
//ㄴ 자동 완전 기능으로 오타를 보정한다.
//ㄴ 영어로 쳐도 한글로 나오도록
//ㄴ 단축 이름 호출 가능
//ㄴ 장 단위로 불러오기


namespace codeTest
{
	class Program
	{
		static String initial = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
		static String medial = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
		static String final = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
		static ushort korBase = 0xAC00;

		static void Main(string[] args)
		{
			//---------------------------------------------
			//http://inobae.blog.me/90007564511
			//완성형 한글 유니코드 시작 AC00 = 가 ~ D7AF(실제 문자가 있는 것은 D79F)
			//한글 자모 유니코드 시작 1100 = ㄱ ~ 11FF
			//한글코드 위치 = 한글 시작위치 + (초성 인덱스 * 21 + 중성 인덱스) * 28 + 종성 인덱스
			//초성길이 = 19, 중성길이 = 21, 종성길이 = 28(공백포함)
			//ex) 안 = 0xAC00 + (11 * 21 + 0) * 28 + 3
			//ex) 녕 = 0xAC00 + (2 * 21 + 6) * 28 + 21
			//---------------------------------------------


			Console.WriteLine(initial.IndexOf("ㄴ"));
			Console.WriteLine(medial.IndexOf("ㅕ"));
			Console.WriteLine(final.IndexOf("ㅇ"));
			ushort uni = 0xAC00 + (2 * 21 + 6) * 28 + 21;
			Console.WriteLine(Convert.ToChar(uni));

			if (initial[0] == 0x1100)
				Console.WriteLine("true");

			//-------------------------------------------
			String oldBibleString = @"창세기 출애굽기 레위기 민수기 신명기 여호수아 사사기 룻기 사무엘상 사무엘하 열왕기상 열왕하기 역대상 역대하 에스라 느헤미야 에스더 욥기 시편 잠언 전도서 아가 이사야 예레미야 예레미야애가 에스겔 다니엘 호세아 요엘 아모스 오바댜 요나 미가 나훔 하박국 스바냐 학개 스가랴 말라기";
			String newBibleString = @"마태복음 마가복음 누가복음 요한복음 사도행전 로마서 고린도전서 고린도후서 갈라디아서 에베소서 빌립보서 골로새세 데살로니가전서 데살로니가후서 디모데전서 디모데후서 디도서 빌레몬서 히브리서 야고보서 베드로전서 베드로후서 요한일서 요한이서 요한삼서 유다서 요한계시록";
			String bibleShortcutString = @"왕상 왕하";
			ArrayList bibleList = new ArrayList();
			String[] oldBibleList = oldBibleString.Split(' ');
			String[] newBibleList = newBibleString.Split(' ');
			String[] bibleShortcutList = bibleShortcutString.Split(' ');
			foreach (String oldbible in oldBibleList)
				bibleList.Add(oldbible);
			foreach (String newbible in newBibleList)
				bibleList.Add(newbible);
			foreach (String bibleShortcut in bibleShortcutList)
				bibleList.Add(bibleShortcut);

			bibleList.Sort();

			for (int i = 0; i < bibleList.Count; i++)
			{
				Console.WriteLine(i + " : " + bibleList[i]);
			}

			String keyword = "곤";
			bibleList.Add(keyword);
			bibleList.Sort();
			int idx = bibleList.IndexOf(keyword);

			Console.WriteLine("keyword : " + keyword);
			Console.WriteLine("result : " + bibleList[idx + 1]);

			Program pg = new Program();
			Console.WriteLine("test parse : " + pg.DevideChar('ㄱ'));
			Console.WriteLine(pg.MergeJaso(" ", "ㅏ", ""));

			//---------------------------------------------
			//입력 경우의 수
			//1.[한] 정확히 맞을 경우 - 통과
			//2.[한] 앞은 맞았으나 뒤가 틀릴경우 - 통과
			//3.[한] 중간이나 앞이 틀릴 경우 - 파서로 분석 후 일치율로 자동완성
			//4.[한] 약자를 사용하고 정확히 맞는 경우 - 약자 등록 후 입력이 약자인지 판별 후에 원문에 연결
			//5.[한] 약자를 사용하고 뒤가 틀릴 경우 - 4번 과정 후 통과
			//6.[한] 약자를 사용하고 중간이나 앞이 틀릴 경우 - 3번 과정 후 4번
			//7.[영] 정확히 맞을 경우 - 한글로 변환 후 일치율 확인
		}

		public void EngToKor()
		{
			//들어온 영어 입력을 파싱된 한글로 변환
		}

		public void SearchBible(String bible)
		{
			//들어온 값을 파서를 이용해 한글자모 파싱
			//성경이름 파싱 리스트 불러옴
			//불러온 리스트에서 파싱된 글자의 일치율을 확인
			//가장 일치율이 높은 성경을 호출
			//호출된 성경이 약자일 경우 원래 성경을 재호출
		}


		//한 글자를 자모로 나누어 반환
		public String DevideChar(char ch)
		{
			int initial, medial, final;
			ushort korStart = 0xAC00;
			ushort temp = 0x0000;
			temp = Convert.ToUInt16(ch);

			//한글이 아닐 경우에 대한 처리 해야됨.

			int code = temp - korStart;
			initial = code / (21 * 28);
			code = code % (21 * 28);
			medial = code / 28;
			code = code % 28;
			final = code;

			String result = initial + " " + medial + " " + final;
			return result;
		}

		public string MergeJaso(string choSung, string jungSung, string jongSung)
		{
			int initialPos, medialPos, finalPos;
			int nUniCode;

			initialPos = initial.IndexOf(choSung);     // 초성 위치
			Console.WriteLine("init : " + initialPos);
			medialPos = medial.IndexOf(jungSung);   // 중성 위치
			finalPos = final.IndexOf(jongSung);   // 종성 위치

			// 앞서 만들어 낸 계산식
			nUniCode = korBase + (initialPos * 21 + medialPos) * 28 + finalPos;
			// 코드값을 문자로 변환
			char temp = Convert.ToChar(nUniCode);
			return temp.ToString();
		}
	}

	class ParseChar
	{
		int initial, medial, final;

		ParseChar()
		{
			initial = 0;
			medial = 0;
			final = 0;
		}
	}
}
