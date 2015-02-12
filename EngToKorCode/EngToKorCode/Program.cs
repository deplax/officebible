using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EngToKorCode
{
	class Program
	{
		static void Main(string[] args)
		{
			//Program pg = new Program();
			//Trans t = new Trans();
			//Console.WriteLine( t.englishToKorean("dksssud") );

			kuku k = new kuku();
			string a = "ㅁㄴㅇㄹ";
			char[] b = a.ToCharArray();
			b[0] = 'd';

			k.pc("rmfTp");
			//읽다 125312
			//일가 12312
			//아녕 12123
			//ㅈ악가 ㅣㅓㄹ 가 112312 221 12

		}
	}

	/////////////////////////////////////////////////////////////////
	//영문 타이핑을 한글로 변환
	//
	
	//ㄴ 길이 추출후 인덱스로 하나씩 자름 substring
	//가져온 문자열이 영문인지 확인한다.
	//ㄴ 유니코드 범위로 확인하고 범위를 벗어날 경우 그냥 넘긴다.
	//영문일 경우 한글 유니코드 인덱스로 변환한다.
	//종성 뒤에 모음이 오는 경우
	//이 전 글자의 종성을 0으로 하고 그 종성을 이번 글자의 초성으로 가져온다.
	//

	//input
	//dkssud 안녕
	//dksud 아녕
	//rmfTpdy? 글쎄요?
	//qork rhvmek. 배가 고프다. 
	//한글이 tjRdu 있어요. 한글이 섞여 있어요.
	class kuku
	{
		// 초성
		string initial = "rRseEfaqQtTdwWczxvg";
		// 중성
		string[] medial = new string[] { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
		// 종성
		string[] final = new string[] { "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft", "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };

		//입력 문자열을 하나씩 가져온다.
		public void DivideString(string str)
		{
			for (int i = 0; i < str.Length; i++)
				continue;
		}

		public bool isEnglish(char c)
		{
			string str = c.ToString();
			Regex engRegex = new Regex(@"[a-zA-Z]");
			Boolean isMatch = engRegex.IsMatch(str);
			return isMatch;
		}

		string caseType = "QWERTOP";
		//대문자를 소문자로 변경
		public char setCase(char c)
		{
			Boolean lowerFlag = true;
			for(int i = 0; i < caseType.Length; i++)
			{
				if (c == caseType[i])
					lowerFlag = false;
			}
			if (lowerFlag)
				char.ToLower(c);
			return c;
		}

		public bool isVowel(char c)
		{
			Boolean vowel = false;
			vowel = medial.Contains(c.ToString());
			return vowel;
		}

		public void pc(string str)
		{
			char[] charArr = str.ToCharArray();

			// 변환불가 = 0 | 초성 = 1 | 중성 = 2 | 종성 = 3 | 중결 = 4 | 종결 = 5
			int[] phoneme = new int[charArr.Length];
			for (int i = 0; i < charArr.Length; i++)
			{
				if (isEnglish(charArr[i]))
				{
					charArr[i] = setCase(charArr[i]);
					setphoneme(i, ref phoneme, isVowel(charArr[i]), ref charArr);
				}
			}

			foreach(int i in phoneme){
				Console.WriteLine(i);
			}

		}
		
		public void setphoneme(int idx, ref int[] phoneme, Boolean vowel, ref char[] charArr)
		{
			if (idx == 0 || phoneme[idx - 1] == 0)		//첫 글자이다.
			{
				if (!vowel)		//자음이다.
					phoneme[idx] = 1;	
				else			//모음이다.
					phoneme[idx] = 2;
			}
			else				//첫 글자가 아니다.
			{
				if (phoneme[idx - 1] == 1)		//이전이 초성이다.
					if(!vowel)	//자음이다.
						if (isCombinationFinal(charArr[idx - 1], charArr[idx]))//종성 결합이다.
						{
							phoneme[idx - 1] = 5;
							phoneme[idx] = 3;
						}
						else
							phoneme[idx] = 1;
					else		//모음이다.
						phoneme[idx] = 2;
				else if(phoneme[idx - 1] == 2)	//이전이 중성이다.
					if(!vowel)	//자음이다.
						if(idx - 2 >= 0 && phoneme[idx - 2] == 1) //이전의 이전이 초성이다.
							phoneme[idx] = 3;
						else
							phoneme[idx] = 1;
					else		//모음이다.
						if (isCombinationMedial(charArr[idx - 1], charArr[idx]))//중성 결합이다.
						{
							phoneme[idx - 1] = 4;
							phoneme[idx] = 2;
						}
						else
							phoneme[idx] = 2;
				else if(phoneme[idx - 1] == 3)	//이전이 종성이다.
					if (!vowel)	//자음이다.
						if (isCombinationFinal(charArr[idx - 1], charArr[idx]))//종성 결합이다.
						{
							phoneme[idx - 1] = 5;
							phoneme[idx] = 3;
						}
						else
							phoneme[idx] = 1;
					else		//모음이다.
					{
						if (idx - 2 >= 0 && phoneme[idx - 2] == 5)//이전이 종성 결합이다.
							// 종성결합 첫번째가 글자일 경우
							if (idx - 4 >= 0 && phoneme[idx - 3] == 2 && phoneme[idx - 4] == 1)
							{
								phoneme[idx - 2] = 3;
								phoneme[idx - 1] = 1;
							}
							else
							{
								phoneme[idx - 2] = 1;
								phoneme[idx - 1] = 1;
							}
						else
						{
							phoneme[idx - 1] = 1;
						}
						phoneme[idx] = 2;
					}
			}						
		}

		public Boolean isCombinationFinal(char a, char b)
		{
			char[] tempChar = new char[2];
			tempChar[0] = a;
			tempChar[1] = b;
			string str = new string(tempChar);
			return final.Contains(str);
		}

		public Boolean isCombinationMedial(char a, char b)
		{
			char[] tempChar = new char[2];
			tempChar[0] = a;
			tempChar[1] = b;
			string str = new string(tempChar);
			return medial.Contains(str);
		}

		public void writeKor(ref int[] phoneme, char[] charArr)
		{
			for (int i = 0; i < phoneme.Length; i++)
			{
				int startFlag = -1;
				string str = "";
				switch(phoneme[i]){
					case 0:
						str = str + charArr[i].ToString();
						break;
					case 1:
						if (i < phoneme.Length && phoneme[i + 1] == 2)
							startFlag = i;
						else
							call();
						break;
					case 2:
						break;
					case 3:
						break;
					case 4:	
						break;
					case 5:
						break;


				}
			}
		}

		public void call()
		{

		}

		//완성된 한글 텍스트를 돌려준다.
		//배열을 돈다.
			//0일 경우 그 문자는 그대로 출력한다.
			//1일 경우
				//다음 문자가 중성일 경우
					//스타트 마크를 찍고 패스
				//아닐 경우
					//변환 출력
			//2일 경우
				//스타트 마크가 있을 경우
					//다음 문자가 종성일 경우
						//패스
					//스타트 ~ 변환출력
				//없을 경우
					//변환 출력
			//3일 경우
				//스타트 마크가 있을 경우
					//스타트 ~ 변환출력
				//없을 경우
					//변환출력
			//4일 경우
				//중성 결합
			//5일 경우
				//종성 결합


		//Int32 getCode(string type, string val)
		//{

		//	isFind = false;

		//	if (type == "initial")
		//	{
		//		returnCode = initial.IndexOf(val) * 21 * 28;
		//		if (returnCode < 0)
		//			isFind = false;
		//		else
		//			isFind = true;
		//	}
		//	else if (type == "medial")
		//	{
		//		for (var i = 0; i < medial.Length; i++)
		//		{
		//			if (medial[i] == val)
		//			{
		//				returnCode = i * 28;
		//				isFind = true;
		//				break;
		//			}
		//		}
		//	}
		//	else if (type == "final")
		//	{
		//		for (var i = 0; i < final.Length; i++)
		//		{
		//			if (final[i] == val)
		//			{
		//				returnCode = i + 1;
		//				isFind = true;
		//				break;
		//			}
		//		}
		//	}
		//	else
		//	{
		//		//alert("잘못된 타입입니다.");
		//	}
		//	if (isFind == false) returnCode = -1; // 값을 찾지 못했을 경우 -1 리턴
		//	return returnCode;
		//}
	}

	class Trans
	{
		
		string KoreaText;
		Int32 initialCode;
		Int32 medialCode;
		Int32 finalCode;
		int textLength;

		Dictionary<string, string> no_alphabet_list;
		void Add_String(string value)
		{
			no_alphabet_list.Add(value, value);
		}

		public Trans()
		{
			//이거 스트링으로 바꿔서 for문 처리
			no_alphabet_list = new Dictionary<string, string>();
			Add_String("1");
			Add_String("2");
			Add_String("3");
			Add_String("4");
			Add_String("5");
			Add_String("6");
			Add_String("7");
			Add_String("8");
			Add_String("9");
			Add_String("0");
			Add_String("!");
			Add_String("@");
			Add_String("#");
			Add_String("$");
			Add_String("%");
			Add_String("^");
			Add_String("&");
			Add_String("*");
			Add_String("(");
			Add_String(")");
			Add_String("?");
			Add_String(" ");
			Add_String(".");
			Add_String(",");
			Add_String(";");
			Add_String("-");
			Add_String("+");
			Add_String("=");
		}
		public string englishToKorean(string text)
		{
	
			KoreaText = "";
			textLength = text.Length;
			//들어온 영문 텍스트를 한글자씩 잘라요.
			for (var idx = 0; idx < textLength; idx++)
			{
				if (space_check(text.Substring(idx, 1)))
				{
					continue;
				}
				// 초성 코드 추출
				initialCode = getCode("initial", text.Substring(idx, 1));


				/**
				  * 현재 문자와 다음 문자를 합한 문자열의 중성 코드 추출
				  * ㅞ ( np ) 또는 ㄼ ( fq ) 같은 두개의 문자가 들어가는 것을 체크하기 위함
				  */
				if (initialCode == -1)
				{
					if (space_check(text.Substring(idx, 1)))
					{
						continue;
					}
				}
				else
				{
					idx++; // 다음 문자로.
				}

				Int32 tempMedialCode = -1;
				if (idx + 2 <= textLength)
					tempMedialCode = getCode("medial", text.Substring(idx, 2));

				// 코드 값이 있을 경우
				if (tempMedialCode != -1)
				{
					// 코드 값을 저장하고 인덱스가 다다음 문자열을 가르키게 한다.
					medialCode = tempMedialCode;
					idx += 2;
				}
				else // 코드값이 없을 경우 하나의 문자에 대한 중성 코드 추출
				{
					if (idx + 1 <= textLength)
						medialCode = getCode("medial", text.Substring(idx, 1));
					if (medialCode == -1)
					{
						if (space_check(text.Substring(idx, 1)))
						{
							continue;
						}
					}

					idx++; // 다음 문자로.

				}
				// 현재 문자와 다음 문자를 합한 문자열의 종성 코드 추출
				Int32 tempFinalCode = -1;
				if (idx + 2 <= textLength)
					tempFinalCode = getCode("final", text.Substring(idx, 2));
				// 코드 값이 있을 경우
				if (tempFinalCode != -1)
				{
					// 코드 값을 저장한다.
					finalCode = tempFinalCode;
					// 그 다음의 중성 문자에 대한 코드를 추출한다.
					if (idx + 3 <= textLength)
						tempMedialCode = getCode("medial", text.Substring(idx + 2, 1));
					else
						tempMedialCode = -1;
					// 코드 값이 있을 경우
					if (tempMedialCode != -1)
					{
						// 종성 코드 값을 저장한다.
						if (idx + 1 <= textLength)
							finalCode = getCode("final", text.Substring(idx, 1));
					}
					else
					{
						idx++;
					}
				}
				else // 코드 값이 없을 경우
				{
					// 그 다음의 중성 문자에 대한 코드 추출
					if (idx + 2 <= textLength)
						tempMedialCode = getCode("medial", text.Substring(idx + 1, 1));
					else
						tempMedialCode = -1;
					// 그 다음에 중성 문자가 존재할 경우
					if (tempMedialCode != -1)
					{
						// 종성 문자는 없음.
						finalCode = 0;
						idx--;
					}
					else
					{
						// 종성 문자 추출
						if (idx + 1 <= textLength)
							finalCode = getCode("final", text.Substring(idx, 1));
						else
							finalCode = -1;
						if (finalCode == -1)
						{
							if ((idx - 1) + 1 <= textLength)
							{
								if (space_check(text.Substring(idx - 1, 1)))
								{
									continue;
								}
							}

							finalCode = 0;

						}
					}
				}
				// 추출한 초성 문자 코드, 중성 문자 코드, 종성 문자 코드를 합한 후 변환하여 출력

				String result = new String(new char[] { Convert.ToChar(0xAC00 + initialCode + medialCode + finalCode) });
				KoreaText += result;
			}
			return KoreaText;
		}

		// 메소드명이 잘못됐음.
		// 공백이나 특수문자의 경우 그대로 패스!
		bool space_check(string val)
		{
			if (no_alphabet_list.ContainsKey(val))
			{
				KoreaText += val;
				return true;
			}
			return false;
		}

		/**
		  * 해당 문자에 따른 코드를 추출한다.
		  * @param type 초성 : chosung, 중성 : jungsung, 종성 : jongsung 구분
		  * @param char 해당 문자
		  */
		Int32 returnCode;
		bool isFind; // 문자를 찾았는지 체크 변수
		// 초성
		string initial = "rRseEfaqQtTdwWczxvg";
		// 중성
		string[] medial = new string[] { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
		// 종성
		string[] final = new string[] { "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft", "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };
		Int32 getCode(string type, string val)
		{

			isFind = false;

			if (type == "initial")
			{
				returnCode = initial.IndexOf(val) * 21 * 28;
				if (returnCode < 0)
					isFind = false;
				else
					isFind = true;
			}
			else if (type == "medial")
			{
				for (var i = 0; i < medial.Length; i++)
				{
					if (medial[i] == val)
					{
						returnCode = i * 28;
						isFind = true;
						break;
					}
				}
			}
			else if (type == "final")
			{
				for (var i = 0; i < final.Length; i++)
				{
					if (final[i] == val)
					{
						returnCode = i + 1;
						isFind = true;
						break;
					}
				}
			}
			else
			{
				//alert("잘못된 타입입니다.");
			}
			if (isFind == false) returnCode = -1; // 값을 찾지 못했을 경우 -1 리턴
			return returnCode;
		}
	}
}