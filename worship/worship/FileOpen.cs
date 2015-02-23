using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOpen
{
	class Box
	{
		public static string DBroot = @"D:\성경송출\BibleDB";
	}
	class Program
	{
		static void Main(string[] args)
		{
			Program pg = new Program();
			pg.GetBibleVerse("개역개정", "요", 3, 16);

		}

		public string GetBibleVerse(string bibleVer, string bible, int chapter, int verse)
		{
			string biblePath = MakePath(bibleVer, bible);
			ArrayList entireChapter = new ArrayList();
			int lineCounter = 0;
			string line;

			System.IO.StreamReader file = new System.IO.StreamReader(biblePath, Encoding.Default);
			ArrayList chapterIdxList = new ArrayList();
			chapterIdxList.Add(0);

			int indexCnt = 1;
			while ((line = file.ReadLine()) != null)
			{
				string[] temp = line.Split(',');
				if (Convert.ToInt16(temp[1]) == indexCnt)
				{
					chapterIdxList.Add(lineCounter);
					indexCnt++;
				}
				entireChapter.Add(temp);
				lineCounter++;
			}
			file.Close();

			int chapterIdx = (int)chapterIdxList[chapter];
			string[] verseStr = (string[])entireChapter[chapterIdx + (verse - 1)];

			return verseStr[3];
		}

		public string MakePath(string bibleVer, string bible)
		{
			string str = Box.DBroot;
			return str + @"\" + bibleVer + @"\" + bibleVer + @"_" + bible + @".txt";
		}

	}
}
