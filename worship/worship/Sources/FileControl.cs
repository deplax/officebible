using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FileControl
{
	class Box
	{
		public static string ListRoot = @"D:\성경송출";
		public static string DBroot = ListRoot + @"\BibleDB";
		
	}
	class FileControl
	{
		public static ArrayList entireChapter = new ArrayList();
		public static ArrayList chapterIdxList = new ArrayList();
		public static string[] worshipList = GetFileList(@"찬양집");
		public static string[] hymnList = GetFileList(@"새찬송가");
		public static string[] worshipOriginList = GetFileOriginList(@"찬양집");
		public static string[] hymnOriginList = GetFileOriginList(@"새찬송가");

		public string GetBibleVerse(string bibleVer, string bible, int chapter, int verse)
		{
			int chapterIdx = (int)chapterIdxList[chapter];
			string[] verseStr = (string[])entireChapter[chapterIdx + (verse - 1)];

			return verseStr[3];
		}

		public string[] GetBibleVerse(string bibleVer, string bible, int chapter, int verseA, int verseB)
		{
			int chapterIdx = (int)chapterIdxList[chapter];
			string[] verseStr = new string[verseB - verseA + 1];
			int cnt = 0;
			for (var i = verseA; i < verseA + verseStr.Length; i++)
			{
				string[] temp = (string[])entireChapter[chapterIdx + (i - 1)];
				verseStr[cnt] = temp[3];
				cnt++;
			}
			return verseStr;
		}

		public void ReadBible(string bibleVer, string bible)
		{
			string biblePath = MakePath(bibleVer, bible);
			entireChapter.Clear();
			int lineCounter = 0;
			string line;

			System.IO.StreamReader file = new System.IO.StreamReader(biblePath, Encoding.Default);
			chapterIdxList.Clear();
			chapterIdxList.Add(0);
			char[] charSeparators = new char[] { ',' };

			int indexCnt = 1;
			while ((line = file.ReadLine()) != null)
			{
				string[] temp = line.Split(charSeparators, 4);
				if (Convert.ToInt16(temp[1]) == indexCnt)
				{
					chapterIdxList.Add(lineCounter);
					indexCnt++;
				}
				entireChapter.Add(temp);
				lineCounter++;
			}
			file.Close();
		}

		public static string[] GetFileList(string str)
		{
			string filePath = Box.ListRoot + @"\\" + str;
			string[] filePaths = Directory.GetFiles(filePath);
			for (int i = 0; i < filePaths.Length; i++)
			{
				string[] temp = filePaths[i].Split('\\');
				filePaths[i] = temp.Last();
				filePaths[i] = filePaths[i].Replace(" ", "");
			}
			return filePaths;
		}

		public static string[] GetFileOriginList(string str)
		{
			string filePath = Box.ListRoot + @"\\" + str;
			string[] filePaths = Directory.GetFiles(filePath);
			for (int i = 0; i < filePaths.Length; i++)
			{
				string[] temp = filePaths[i].Split('\\');
				filePaths[i] = temp.Last();
			}
			return filePaths;
		}


		public string MakePath(string bibleVer, string bible)
		{
			string str = Box.DBroot;
			return str + @"\" + bibleVer + @"\" + bibleVer + @"_" + bible + @".txt";
		}
		public string[] SetBibleVer()
		{
			string[] filePaths = Directory.GetDirectories(Box.DBroot);
			string[] list = new string[filePaths.Length];
			for (var i = 0; i < filePaths.Length; i++)
			{
				string[] temp = filePaths[i].Split('\\');
				list[i] = temp.Last();
			}
			return list;
		}

	}
}
