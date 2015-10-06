using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest
{
	class Bible
	{
		public string ver;
		public string name;
		public int chapter;
		public int verse;
		public string phase;

		public Bible(){}

		public Bible(string ver, string name, int chapter, int verse, string phase)
		{
			this.ver = ver;
			this.name = name;
			this.chapter = chapter;
			this.verse = verse;
			this.phase = phase;
		}

		public Bible(string ver, string name, int chapter, int verse)
		{
			this.ver = ver;
			this.name = name;
			this.chapter = chapter;
			this.verse = verse;
		}

	}
}
