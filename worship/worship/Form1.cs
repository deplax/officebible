using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace worship
{
	public partial class BibleForm : Form
	{
		public BibleForm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}


		private void btnMakeBible_Click(object sender, EventArgs e)
		{

			worship.WorshipRibbon wr = new worship.WorshipRibbon();
			string s = wr.kuku("개역개정", txtBible.Text, Convert.ToInt16(txtChapter.Text), Convert.ToInt16(txtVerseA.Text));
			wr.testCode(s);

			//필드가 비었을 경우 조사해해서 경우에 따라 메세지 출력

			//성경이 선택되지 않았을 경우
			
			//장이 선택되지 않았을 경우


		}
	}
}
