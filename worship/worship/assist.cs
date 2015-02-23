using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assist
{

	enum Syllabic : byte { none, consonant, vowel };
	enum Phoneme : byte { none, initial, medial, final, dualMedial, dualFinal };

	public class Box
	{
		// 초성
		public static string[] initial = new string[] { "r", "R", "s", "e", "E", "f", "a", "q", "Q", "t", "T", "d", "w", "W", "c", "z", "x", "v", "g" };
		public static string initialKor = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
		// 중성
		public static string[] medial = new string[] { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
		public static string medialKor = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
		// 종성
		public static string[] final = new string[] { "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft", "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };
		public static string finalKor = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";

		public static String oldBibleString = @"창세기 출애굽기 레위기 민수기 신명기 여호수아 사사기 룻기 사무엘상 사무엘하 열왕기상 열왕하기 역대상 역대하 에스라 느헤미야 에스더 욥기 시편 잠언 전도서 아가 이사야 예레미야 예레미야애가 에스겔 다니엘 호세아 요엘 아모스 오바댜 요나 미가 나훔 하박국 스바냐 학개 스가랴 말라기";
		public static String newBibleString = @"마태복음 마가복음 누가복음 요한복음 사도행전 로마서 고린도전서 고린도후서 갈라디아서 에베소서 빌립보서 골로새세 데살로니가전서 데살로니가후서 디모데전서 디모데후서 디도서 빌레몬서 히브리서 야고보서 베드로전서 베드로후서 요한일서 요한이서 요한삼서 유다서 요한계시록";
		public static String oldBibleShortcutString = @"창 출 레 민 신 수 삿 룻 삼상 삼하 왕상 왕하 대상 대하 스 느 에 욥 시 잠 전 아 사 렘 애 겔 단 호 욜 암 옵 욘 미 나 합 습 학 슥 말";
		public static String newBibleShortcutString = @"마 막 눅 요 행 롬 고전 고후 갈 엡 빌 골 살전 살후 딤전 딤후 딛 몬 히 약 벧전 벧후 요일 요이 요삼 유 계";

		public static String[] oldBibleList = Box.oldBibleString.Split(' ');
		public static String[] newBibleList = Box.newBibleString.Split(' ');
		public static String[] oldBibleShortcutList = Box.oldBibleShortcutString.Split(' ');
		public static String[] newBibleShortcutList = Box.newBibleShortcutString.Split(' ');

		public static ArrayList BibleList()
		{
			ArrayList bibleList = new ArrayList();
			foreach (String oldbible in Box.oldBibleList)
				bibleList.Add(oldbible);
			foreach (String newbible in Box.newBibleList)
				bibleList.Add(newbible);

			foreach (String bibleShortcut in Box.oldBibleShortcutList)
				bibleList.Add(bibleShortcut);
			foreach (String bibleShortcut in Box.newBibleShortcutList)
				bibleList.Add(bibleShortcut);

			bibleList.Sort();
			return bibleList;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			string testString = "장세기";
			Console.WriteLine("입력값 : " + testString);

			EngToKor etk = new EngToKor();
			Console.WriteLine("영문 한글 변환 : " + etk.Trans(testString));

			testString = etk.Trans(testString);
			KorToPhoneme ktp = new KorToPhoneme();
			string KeyPhoneme = ktp.Trans(testString);
			Console.WriteLine("한글 자소 분리 : " + KeyPhoneme);

			BibleAutoComplete bac = new BibleAutoComplete();
			Console.WriteLine("suggest : " + bac.SuggestProcess(KeyPhoneme));
			Console.WriteLine("normal : " + bac.NormalProcess(bac.SuggestProcess(KeyPhoneme)));
			Console.WriteLine();
		}
	}

	class BibleAutoComplete
	{

		// 한글을 넣으면 가장 확률이 높은 요소를 반환
		public string NormalProcess(string keyword)
		{
			ArrayList bibleList = Box.BibleList();

			//for (int i = 0; i < bibleList.Count; i++)
			//	Console.WriteLine(i + " : " + bibleList[i]);

			bibleList.Add(keyword);
			bibleList.Sort();
			int idx = bibleList.IndexOf(keyword);

			// Bible shortcut mapping process
			if (Box.oldBibleShortcutList.Contains(bibleList[idx + 1]))
				return Box.oldBibleList[Array.IndexOf(Box.oldBibleShortcutList, bibleList[idx + 1])];
			else if (Box.newBibleShortcutList.Contains(bibleList[idx + 1]))
				return Box.newBibleList[Array.IndexOf(Box.newBibleShortcutList, bibleList[idx + 1])];
			else
				return bibleList[idx + 1].ToString();
		}

		// 한글 변환 파싱 된 스트링을 넣으면 가장 확률이 놓은 요소를 반환
		public string SuggestProcess(string keyword)
		{
			//성경 전용이 아니도록 고쳐야함.
			int topIndex = 0;
			int topValue = 0;
			KorToPhoneme ktp = new KorToPhoneme();
			for (var i = 0; i < Box.BibleList().Count; i++)
			{

				int bibleLen = ktp.Trans(Box.BibleList()[i].ToString()).Length;

				// Boundary 를 위해 커팅할 길이 체크
				int cutLen;
				if (bibleLen < keyword.Length)
					cutLen = bibleLen;
				else
					cutLen = keyword.Length;
				string tempStr = ktp.Trans(Box.BibleList()[i].ToString()).Substring(0, cutLen);
				char[] tempCh = tempStr.ToCharArray();

				ArrayList tempStrArrList = new ArrayList();
				tempStrArrList.AddRange(tempCh);

				int tempMatch = 0;

				for (var j = 0; j < cutLen; j++)
				{
					if (tempStrArrList.Contains(keyword[j]))
					{
						tempMatch++;
						tempStrArrList.Remove(keyword[j]);
					}
				}

				// 일치율이 최대인 값을 저장.
				if (topValue < tempMatch)
				{
					topValue = tempMatch;
					topIndex = i;
				}
			}
			return Box.BibleList()[topIndex].ToString();
		}
	}


	class EngToKor
	{

		public string Trans(string str)
		{
			//ㅣㄹ레
			//한 글자씩 나눈다.
			char[] arrChar = str.ToCharArray();
			InfoChar[] arrInfoChar = new InfoChar[arrChar.Length];

			for (int i = 0; i < arrChar.Length; i++)
			{
				arrInfoChar[i] = new InfoChar();
				arrInfoChar[i].ch = arrChar[i];
				arrInfoChar[i].syllabic = SetSyllabic(arrInfoChar[i].ch);
				arrInfoChar[i].phoneme = SetPhoneme(i, arrInfoChar);
			}
			return MakeChar(arrInfoChar);
			//for (int i = 0; i < arrChar.Length; i++)
			//	Console.WriteLine(arrInfoChar[i].phoneme);
		}

		public Syllabic SetSyllabic(char ch)
		{
			if (Box.initial.Contains(ch.ToString()))
				return Syllabic.consonant;
			if (Box.medial.Contains(ch.ToString()))
				return Syllabic.vowel;
			return Syllabic.none;
		}

		public Phoneme SetPhoneme(int idx, InfoChar[] arrInfoChar)
		{
			if (arrInfoChar[idx].syllabic == Syllabic.none)
				return Phoneme.none;
			if (idx == 0 || arrInfoChar[idx - 1].phoneme == Phoneme.none)
			{
				if (arrInfoChar[idx].syllabic == Syllabic.consonant)
					return arrInfoChar[idx].phoneme = Phoneme.initial;
				if (arrInfoChar[idx].syllabic == Syllabic.vowel)
					return arrInfoChar[idx].phoneme = Phoneme.medial;
			}
			else
			{
				if (arrInfoChar[idx - 1].phoneme == Phoneme.initial)						//이전이 초성
				{
					if (arrInfoChar[idx].syllabic == Syllabic.consonant)
						if (isDualFinal(arrInfoChar[idx - 1].ch, arrInfoChar[idx].ch))
						{
							arrInfoChar[idx - 1].phoneme = Phoneme.dualFinal;
							return arrInfoChar[idx].phoneme = Phoneme.dualFinal;
						}
						else
							return arrInfoChar[idx].phoneme = Phoneme.initial;
					if (arrInfoChar[idx].syllabic == Syllabic.vowel)
						return arrInfoChar[idx].phoneme = Phoneme.medial;
				}
				else if (arrInfoChar[idx - 1].phoneme == Phoneme.medial)					//이전이 중성
				{
					if (arrInfoChar[idx].syllabic == Syllabic.consonant)
						if (idx - 2 >= 0 && arrInfoChar[idx - 2].phoneme == Phoneme.initial)
							return arrInfoChar[idx].phoneme = Phoneme.final;
						else
							return arrInfoChar[idx].phoneme = Phoneme.initial;
					if (arrInfoChar[idx].syllabic == Syllabic.vowel)
						if (isDualMedial(arrInfoChar[idx - 1].ch, arrInfoChar[idx].ch))
						{
							arrInfoChar[idx - 1].phoneme = Phoneme.dualMedial;
							return arrInfoChar[idx].phoneme = Phoneme.dualMedial;
						}
						else
							return arrInfoChar[idx].phoneme = Phoneme.medial;
				}
				else if (arrInfoChar[idx - 1].phoneme == Phoneme.final)						//이전이 종성
				{
					if (arrInfoChar[idx].syllabic == Syllabic.consonant)
						if (isDualFinal(arrInfoChar[idx - 1].ch, arrInfoChar[idx].ch))
						{
							arrInfoChar[idx - 1].phoneme = Phoneme.dualFinal;
							return arrInfoChar[idx].phoneme = Phoneme.dualFinal;
						}
						else
							return arrInfoChar[idx].phoneme = Phoneme.initial;
					if (arrInfoChar[idx].syllabic == Syllabic.vowel)
					{
						arrInfoChar[idx - 1].phoneme = Phoneme.initial;
						return arrInfoChar[idx].phoneme = Phoneme.medial;
					}
				}
				else if (arrInfoChar[idx - 1].phoneme == Phoneme.dualMedial)				//이전이 이중중성
				{
					if (arrInfoChar[idx].syllabic == Syllabic.consonant)
						if (idx - 2 >= 0 && arrInfoChar[idx - 2].phoneme == Phoneme.initial)
							return arrInfoChar[idx].phoneme = Phoneme.final;
						else
							return arrInfoChar[idx].phoneme = Phoneme.initial;
					if (arrInfoChar[idx].syllabic == Syllabic.vowel)
						return arrInfoChar[idx].phoneme = Phoneme.medial;
				}
				else if (arrInfoChar[idx - 1].phoneme == Phoneme.dualFinal)					//이전이 이중종성
				{
					if (arrInfoChar[idx].syllabic == Syllabic.consonant)
						return arrInfoChar[idx].phoneme = Phoneme.initial;
					if (arrInfoChar[idx].syllabic == Syllabic.vowel)
					{
						arrInfoChar[idx - 1].phoneme = Phoneme.initial;
						return arrInfoChar[idx].phoneme = Phoneme.medial;
					}
				}
			}
			return Phoneme.none;
		}

		public Boolean isDualFinal(char a, char b)
		{
			char[] tempChar = new char[2];
			tempChar[0] = a;
			tempChar[1] = b;
			string str = new string(tempChar);
			return Box.final.Contains(str);
		}
		public Boolean isDualMedial(char a, char b)
		{
			char[] tempChar = new char[2];
			tempChar[0] = a;
			tempChar[1] = b;
			string str = new string(tempChar);
			return Box.medial.Contains(str);
		}

		public string MakeChar(InfoChar[] arrInfoChar)
		{
			string str = "";
			int startIdx = -1;
			int flag = 0;

			for (int i = 0; i < arrInfoChar.Length; i++)
			{
				if (arrInfoChar[i].phoneme == Phoneme.none)					//변환 불가의 경우
				{
					if (startIdx != -1)
					{
						str += setPos(startIdx, i - 1, ref flag, ref arrInfoChar);
						startIdx = -1;
					}
					str += arrInfoChar[i].ch;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.initial)			//초성의 경우
				{
					if (startIdx != -1)
						str += setPos(startIdx, i - 1, ref flag, ref arrInfoChar);
					startIdx = i;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.medial)			//중성의 경우
				{
					if (i - 1 >= 0 && arrInfoChar[i - 1].phoneme != Phoneme.initial)
					{
						if (startIdx != -1)
							str += setPos(startIdx, i - 1, ref flag, ref arrInfoChar);
						startIdx = i;
					}

				}
				else if (arrInfoChar[i].phoneme == Phoneme.final)			//종성의 경우
				{
					if (startIdx != -1)
					{
						str += setPos(startIdx, i, ref flag, ref arrInfoChar);
						startIdx = -1;
					}
				}
				else if (arrInfoChar[i].phoneme == Phoneme.dualMedial)		//중첩 중성의 경우
				{

					if (i - 1 >= 0 && arrInfoChar[i - 1].phoneme != Phoneme.initial)
					{
						if (startIdx != -1)
							str += setPos(startIdx, i - 1, ref flag, ref arrInfoChar);
						startIdx = i;
					}
					flag += 1;
					i++;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.dualFinal)		//중첩 종성의 경우
				{
					flag += 2;
					if (startIdx != 1)
					{
						str += setPos(startIdx, i + 1, ref flag, ref arrInfoChar);
						startIdx = -1;
					}
					else
						str += setPos(i, i + 1, ref flag, ref arrInfoChar);
					i++;
				}

				if (i == 0 && arrInfoChar[i].phoneme != Phoneme.none)
					startIdx = 0;

				if (i == arrInfoChar.Length - 1)							//마지막일 경우
				{
					if (startIdx != -1)
						str += setPos(startIdx, i, ref flag, ref arrInfoChar);
				}
			}
			return str;
		}

		public string setPos(int startIdx, int idx, ref int flag, ref InfoChar[] arrInfoChar)
		{
			//Console.WriteLine("st : " + startIdx + " | fn : " + idx + " | flag : " + flag);
			int initialIdx = -1;
			int medialIdx = -1;
			int finalIdx = -1;

			for (int i = startIdx; i < idx + 1; i++)
			{
				if (arrInfoChar[i].phoneme == Phoneme.initial)
				{
					for (int j = 0; j < Box.initial.Length; j++)
						if (Box.initial[j] == arrInfoChar[i].ch.ToString())
							initialIdx = j;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.medial)
				{
					for (int j = 0; j < Box.medial.Length; j++)
						if (Box.medial[j] == arrInfoChar[i].ch.ToString())
							medialIdx = j;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.final)
				{
					for (int j = 0; j < Box.final.Length; j++)
						if (Box.final[j] == arrInfoChar[i].ch.ToString())
							finalIdx = j;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.dualMedial)
				{
					char[] tempChar = new char[2];
					tempChar[0] = arrInfoChar[i].ch;
					tempChar[1] = arrInfoChar[i + 1].ch;
					string str = new string(tempChar);

					for (int j = 0; j < Box.medial.Length; j++)
						if (Box.medial[j] == str)
							medialIdx = j;
					i++;
				}
				else if (arrInfoChar[i].phoneme == Phoneme.dualFinal)
				{
					char[] tempChar = new char[2];
					tempChar[0] = arrInfoChar[i].ch;
					tempChar[1] = arrInfoChar[i + 1].ch;
					string str = new string(tempChar);

					for (int j = 0; j < Box.final.Length; j++)
						if (Box.final[j] == str)
							finalIdx = j;
					i++;
				}
			}
			flag = 0;
			return ConvertChar(initialIdx, medialIdx, finalIdx);
		}


		public string ConvertChar(int initialIdx, int medialIdx, int finalIdx)
		{
			if (initialIdx == -1 && medialIdx == -1 && finalIdx == -1)	//변환불가일 떄
			{
				Console.WriteLine("설마 이 구간 에러가 발생할까;;");
				return "";
			}
			else if (medialIdx == -1 && finalIdx == -1)					//초성일때
			{
				return Box.initialKor[initialIdx].ToString();
			}
			else if (initialIdx == -1 && finalIdx == -1)				//중성일때
			{
				return Box.medialKor[medialIdx].ToString();
			}
			else if (initialIdx == -1 && medialIdx == -1)				//종성일때
			{
				return Box.finalKor[finalIdx].ToString();
			}
			else if (finalIdx == -1)									//초성 중성
			{
				String result = new String(new char[] { Convert.ToChar(0xAC00 + initialIdx * 21 * 28 + medialIdx * 28) });
				return result;
			}
			else														//초성 중성 종성
			{
				String result = new String(new char[] { Convert.ToChar(0xAC00 + initialIdx * 21 * 28 + medialIdx * 28 + finalIdx + 1) });
				return result;
			}
		}


	}

	class InfoChar
	{
		public Syllabic syllabic;
		public Phoneme phoneme;
		public char ch;
	}

	class KorToPhoneme
	{

		public string Trans(string str)
		{
			string tempStr = "";
			for (int i = 0; i < str.Length; i++)
				tempStr += Trans(str[i]);
			return tempStr;
		}

		public string Trans(char ch)
		{
			string phonemeStr = "";

			int initialIdx, medialIdx, finalIdx;
			ushort temp = 0x0000;
			ushort m_UniCodeHangulBase = 0xAC00;
			ushort m_UniCodeHangulLast = 0xD79F;

			temp = Convert.ToUInt16(ch);

			// 캐릭터가 한글이 아닐 경우 처리
			if ((temp < m_UniCodeHangulBase) || (temp > m_UniCodeHangulLast))
				phonemeStr += ch.ToString();
			else
			{
				int nUniCode = temp - m_UniCodeHangulBase;
				initialIdx = nUniCode / (21 * 28);
				nUniCode = nUniCode % (21 * 28);
				medialIdx = nUniCode / 28;
				nUniCode = nUniCode % 28;
				finalIdx = nUniCode;

				phonemeStr += Box.initialKor[initialIdx].ToString();
				phonemeStr += Box.medialKor[medialIdx].ToString();
				if (finalIdx != 0)
					phonemeStr += Box.finalKor[finalIdx].ToString();
			}
			return phonemeStr;
		}
	}
}
