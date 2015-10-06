using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Collections;
using System.Reflection;


namespace DatabaseTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve2);
			BibleDao bd = new BibleDao();
			Bible a = new Bible("GYGJ", "창세기", 1, 1);
			Bible b = new Bible("GYGJ", "창세기", 1, 10);

			Console.WriteLine(bd.GetVerseCount(a));
		}

		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DatabaseTest.System.Data.SQLite.dll"))
			{
				byte[] assemblyData = new byte[stream.Length];
				stream.Read(assemblyData, 0, assemblyData.Length);
				return Assembly.Load(assemblyData);
			}
		}

		static Assembly CurrentDomain_AssemblyResolve2(object sender, ResolveEventArgs args)
		{
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DatabaseTest.System.Data.SQLite.Linq.dll"))
			{
				byte[] assemblyData = new byte[stream.Length];
				stream.Read(assemblyData, 0, assemblyData.Length);
				return Assembly.Load(assemblyData);
			}
		}
	}
	class BibleDao
	{
		string strConn = @"Data Source=" + ControlBox.ProgramRoot + ControlBox.DatabaseDirectory + "\\" + ControlBox.DatabaseName;
		
		//성경의 종류를 Map으로 리턴 (하드코딩)
		public Dictionary<String, String> GetBibleVer()
		{
			Dictionary<String, String> bibleVer = new Dictionary<string, string>();
			bibleVer.Add("개역개정", "GYGJ");
			bibleVer.Add("개역한글", "GYHG");
			bibleVer.Add("영문NIV", "NIV");
			bibleVer.Add("쉬운성경", "SOSG");
			return bibleVer;
		}

		//하나의 Bible객체를 넣으면 phase를 채워서 리턴
		public Bible GetSinglePhase(Bible bible)
		{
			SQLiteConnection con = new SQLiteConnection(strConn);
			con.Open();
			SQLiteDataReader reader;
			using (SQLiteCommand cmd = con.CreateCommand())
			{
				cmd.CommandText = "SELECT * FROM " + bible.ver + " WHERE name = '" + bible.name + "' and chapter = " + bible.chapter + " and verse = " + bible.verse;
				reader = cmd.ExecuteReader();
			}
			while (reader.Read())
			{
				string phase = Convert.ToString(reader["phase"]);
				bible.phase = phase;
			}
			reader.Close();
			con.Close();
			return bible;
		}

		//처음절과 끝절이 들어간 Bible객체를 넣으면 해당 구간의 BibleList를 리턴
		public ArrayList GetMultiPhase(Bible bibleA, Bible bibleB)
		{
			SQLiteConnection con = new SQLiteConnection(strConn);
			con.Open();
			SQLiteDataReader reader;
			ArrayList biblePhaseList = new ArrayList();
			using (SQLiteCommand cmd = con.CreateCommand())
			{
				cmd.CommandText = "SELECT * FROM " + bibleA.ver + " WHERE name = '" + bibleA.name + "' and chapter = " + bibleA.chapter + " and verse BETWEEN " + bibleA.verse + " and " + bibleB.verse;
				reader = cmd.ExecuteReader();
			}
			while (reader.Read())
			{
				string phase = Convert.ToString(reader["phase"]);
				Bible tempBible = new Bible(bibleA.ver, bibleA.name, bibleA.chapter, bibleA.verse, phase);
				biblePhaseList.Add(tempBible);
			}
			reader.Close();
			con.Close();
			return biblePhaseList;
		}

		//name까지 채워진 Bible객체를 넣으면 총 장수를 리턴.
		public int GetChapterCount(Bible bible)
		{
			SQLiteConnection con = new SQLiteConnection(strConn);
			con.Open();
			SQLiteDataReader reader;
			int count = 0;
			using (SQLiteCommand cmd = con.CreateCommand())
			{
				cmd.CommandText = "SELECT count(DISTINCT chapter) as count FROM " + bible.ver + " WHERE name = '" + bible.name + "'";
				reader = cmd.ExecuteReader();
			}
			while (reader.Read())
				count = Convert.ToInt32(reader["count"]);
			
			reader.Close();
			con.Close();
			return count;
		}

		//chapter까지 채워진 Bible객체를 넣으면 총 절수를 리턴.
		public int GetVerseCount(Bible bible)
		{
			SQLiteConnection con = new SQLiteConnection(strConn);
			con.Open();
			SQLiteDataReader reader;
			int count = 0;
			using (SQLiteCommand cmd = con.CreateCommand())
			{
				cmd.CommandText = "SELECT count(*) as count FROM " + bible.ver + " WHERE name = '" + bible.name + "' and chapter = " + bible.chapter;
				reader = cmd.ExecuteReader();
			}
			while (reader.Read())
				count = Convert.ToInt32(reader["count"]);

			reader.Close();
			con.Close();
			return count;
		}

		//성경의 종류를 불러온다.
		public void SetBibleVer()
		{
			SQLiteConnection con = new SQLiteConnection(strConn);
			con.Open();
			SQLiteDataReader reader;
			ArrayList bibleVerList = new ArrayList();
			using (SQLiteCommand cmd = con.CreateCommand())
			{
				cmd.CommandText = "SELECT * FROM sqlite_master WHERE type='table'";
				reader = cmd.ExecuteReader();
			}
			while (reader.Read())
			{
				bibleVerList.Add(reader["tbl_name"]);
			}
			reader.Close();
			con.Close();
		}
	}

	class ControlBox
	{
		public static string DatabaseDirectory = "Database";
		public static string ProgramRoot = ".\\";
		public static string DatabaseName = "bible.db";
	}
}
