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

			SetBibleVer();
			this.txtBible.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnter);

		}

		private void SetBibleVer()
		{
			FileControl.FileControl fc = new FileControl.FileControl();
			string[] verList = fc.SetBibleVer();
			for (var i = 0; i < verList.Length; i++)
			{
				cmbBibleVer.Items.Add(verList[i]);
			}
			cmbBibleVer.SelectedIndex = 0;
		}


		private void btnMakeBible_Click(object sender, EventArgs e)
		{

			worship.WorshipRibbon wr = new worship.WorshipRibbon();
			string biblerVer = cmbBibleVer.Text;
			string bible = txtBible.Text;
			int chapter = Convert.ToInt16(txtChapter.Text);
			int verseA = Convert.ToInt16(txtVerseA.Text);
			int verseB = Convert.ToInt16(txtVerseB.Text);

			string s = wr.kuku(biblerVer, bible, chapter, verseA);
			wr.testCode(s);


			//성경이 선택되지 않았을 경우

			//장이 선택되지 않았을 경우


		}

		private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				MessageBox.Show("press enter");
				this.ActiveControl = txtVerseB;
			}
		}

		private void txtChapterLeave(object sender, EventArgs e)
		{
			//숫자가 들어왔는지 확인

			//MessageBox.Show(txtChapter.Text);
		}

		private void txtBibleLeave(object sender, EventArgs e)
		{
			Assist.EngToKor etk = new Assist.EngToKor();
			string str = etk.Trans(txtBible.Text);
			Assist.KorToPhoneme ktp = new Assist.KorToPhoneme();
			Assist.BibleAutoComplete bac = new Assist.BibleAutoComplete();
			txtBible.Text = bac.NormalProcess(bac.SuggestProcess(ktp.Trans(str)));
		}

		private void btnBibleClick(object sender, EventArgs e)
		{
			try
			{
				Button btnSenter = (Button)sender;
				if (Assist.Box.oldBibleShortcutList.Contains(btnSenter.Text))
					txtBible.Text = Assist.Box.oldBibleList[Array.IndexOf(Assist.Box.oldBibleShortcutList, btnSenter.Text)];
				else if (Assist.Box.newBibleShortcutList.Contains(btnSenter.Text))
					txtBible.Text = Assist.Box.newBibleList[Array.IndexOf(Assist.Box.newBibleShortcutList, btnSenter.Text)];
				else
				{
					lblMessage.Text = "[주의] 프로그래밍중에 오타가 발생하였습니다.";
					txtBible.Text = "";
				}
			}
			catch
			{
				lblMessage.Text = "[위험] btnBibleClick에서 오류가 발생했습니다.";
			}
		}
	}


	class Checker
	{
		public void isNumber()
		{
			//입력값이 숫자인지 확인
		}
	}
}
