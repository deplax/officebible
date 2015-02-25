using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Box = Assist.Box;
namespace worship
{
	public partial class BibleForm : Form
	{
		FileControl.FileControl fc = new FileControl.FileControl();
		Assist.KorToPhoneme ktp = new Assist.KorToPhoneme();
		Assist.BibleAutoComplete bac = new Assist.BibleAutoComplete();
		Assist.EngToKor etk = new Assist.EngToKor();

		private KeyButton[] smartSuggest;

		Boolean isClick = false;
		Boolean isWorship = true;

		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Right:
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
					return true;
				case Keys.Shift | Keys.Right:
				case Keys.Shift | Keys.Left:
				case Keys.Shift | Keys.Up:
				case Keys.Shift | Keys.Down:
					return true;
			}
			return base.IsInputKey(keyData);
		}

		public BibleForm()
		{
			InitializeComponent();
			SetSuggestArr();
		}

		public void SetSuggestArr()
		{
			bool colorSwitch = true;
			smartSuggest = new KeyButton[5];
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BibleForm));
			for (var i = 0; i < smartSuggest.Length; i++)
			{
				smartSuggest[i] = new KeyButton();
				if (colorSwitch == true)
				{
					smartSuggest[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(186)))), ((int)(((byte)(186)))));
					colorSwitch = false;
				}
				else
				{
					smartSuggest[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
					colorSwitch = true;
				}
				smartSuggest[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
				smartSuggest[i].FlatAppearance.BorderSize = 0;
				smartSuggest[i].UseVisualStyleBackColor = false;
				smartSuggest[i].SetBounds(253, 112 + (i * 22), 173, 22);
				smartSuggest[i].FlatStyle = FlatStyle.Flat;
				smartSuggest[i].Visible = true;
				smartSuggest[i].Tag = i;
				smartSuggest[i].TabStop = false;

				resources.ApplyResources(this.smartSuggest[i], "smartSuggest" + i);
				resources.ApplyResources(this, "$this");

				this.smartSuggest[i].KeyDown += new System.Windows.Forms.KeyEventHandler(CheckWorshipAllow);
				this.smartSuggest[i].Click += new System.EventHandler(this.worshipClick);
				this.smartSuggest[i].GotFocus += new System.EventHandler(this.SelectButton);
				this.smartSuggest[i].LostFocus += new System.EventHandler(this.DiselectButton);
				Controls.Add(this.smartSuggest[i]);
			}
		}

		private void SelectButton(object sender, EventArgs e)
		{
			Button btn = (Button) sender;
			btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
		}
		private void DiselectButton(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
		}

		private void Form1_Load(object sender, EventArgs e)
		{

			SetBibleVer();
			this.txtChapter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckBibleEnter);
			this.txtVerseA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckBibleEnter);
			this.txtVerseB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckBibleEnter);

			this.KeyPreview = true;
			this.KeyDown += new KeyEventHandler(DetectESC);
		}

		private void DetectESC(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Hide();
		}

		private void SetBibleVer()
		{
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
			int chapter;
			int verseB;
			int verseA;

			isClick = true;


			string bibleVer = cmbBibleVer.Text;
			string bible = txtBible.Text;

			if (!int.TryParse(txtVerseA.Text, out verseA))
				verseA = 0;
			if (!int.TryParse(txtVerseB.Text, out verseB))
				verseB = 0;

			//장이 입력되지 않았을 경우
			if (!int.TryParse(txtChapter.Text, out chapter))
			{
				lblMessage.Text = "[주의] 장이 입력되지 않았습니다.";
				this.ActiveControl = txtChapter;
				return;
			}
			//절만 입력되지 않은 경우
			if (verseA == 0 && verseB == 0)
			{
				verseA = 1;
				verseB = GetVerseIdx(chapter);
			}

			//오른쪽에만 입력하였을 경우 1 ~ 입력까지 출력.
			if (verseA == 0 && verseB != 0)
				verseA = 1;

			//성경이 입력되지 않았을 경우
			if (bible == "")
			{
				lblMessage.Text = "[주의] 성경이 입력되지 않았습니다.";
				this.ActiveControl = txtBible;
				return;
			}
			//장의 범위가 넘어가는 경우
			else if (GetChapterIdx() < chapter)
			{
				lblMessage.Text = "[주의] 장의 범위를 확인하여 주세요. [0-" + GetChapterIdx() + "]";
				this.ActiveControl = txtChapter;
				return;
			}
			//오른쪽 절이 더 큰 경우
			else if (verseB - verseA < 0 && txtVerseB.Text != "")
			{
				lblMessage.Text = "[주의] 절이 잘못 입력되었습니다.";
				txtVerseB.Text = "";
				this.ActiveControl = txtVerseA;
				return;
			}
			//절이 양쪽 모두 선택된 경우
			else if (verseA != 0 && verseB != 0)
			{
				if (GetVerseIdx(chapter) < verseB)
				{
					lblMessage.Text = "[주의] 절의 범위를 확인하여 주세요. [0-" + GetVerseIdx(chapter) + "]";
					this.ActiveControl = txtVerseB;
					return;
				}
				//슬라이드를 생성한다.
				string[] verses = fc.GetBibleVerse(bibleVer, bible, chapter, verseA, verseB);
				foreach (string verse in verses)
					wr.makeBibleSlide(verse);
			}
			//절이 왼쪽만 있는 경우
			else if (verseA != 0 && verseB == 0)
			{
				if (GetVerseIdx(chapter) < verseA)
				{
					lblMessage.Text = "[주의] 절의 범위를 확인하여 주세요. [0-" + GetVerseIdx(chapter) + "]";
					this.ActiveControl = txtVerseA;
					return;
				}
				//슬라이드를 생성한다.
				string verse = fc.GetBibleVerse(bibleVer, bible, chapter, verseA);
				wr.makeBibleSlide(verse);
			}
			this.Hide();
		}

		private void CheckBibleEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				btnMakeBible_Click(sender, e);
			}
		}
		private void CheckWorshipAllow(object sender, KeyEventArgs e)
		{
			Button btn = (Button)sender;

			if (e.KeyCode == Keys.Up)
			{
				if ((int)btn.Tag == 0)
				{
					this.ActiveControl = txtWorship;
					return;
				}
				this.ActiveControl = smartSuggest[(int)btn.Tag - 1];
			}
			if (e.KeyCode == Keys.Down)
			{
				if ((int)btn.Tag == 4)
				{
					this.ActiveControl = smartSuggest[(int)btn.Tag];
					return;
				}
				this.ActiveControl = smartSuggest[(int)btn.Tag + 1];
			}

		}

		private void CheckWorshipKey(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
				this.ActiveControl = smartSuggest[0];
			if (e.KeyChar == (char)Keys.Down)
			{
				this.ActiveControl = smartSuggest[0];
			}
		}

		private void SetNumber(object sender, EventArgs e)
		{
			TextBox txtbox = (TextBox)sender;
			string strTmep = Regex.Replace(txtbox.Text, @"\D", "");
			strTmep = strTmep.Replace(" ", "");
			txtbox.Text = strTmep;
		}
		private void GetVerseInfo(object sender, EventArgs e)
		{
			if (isClick)
			{
				isClick = false;
				return;
			}
			if (txtBible.Text != "" && txtChapter.Text != "" && Convert.ToInt16(txtChapter.Text) <= GetChapterIdx())
				lblMessage.Text = "[정보] " + txtBible.Text + " " + txtChapter.Text + "장은 " + GetVerseIdx(Convert.ToInt16(txtChapter.Text)) + "절까지 있습니다.";
		}

		private void txtBibleLeave(object sender, EventArgs e)
		{
			if (txtBible.Text == "")
				return;
			string str = etk.Trans(txtBible.Text);
			txtBible.Text = bac.NormalProcess(bac.SuggestProcess(ktp.Trans(str)));
			fc.ReadBible(cmbBibleVer.Text, bac.OriginToShortCut(txtBible.Text));
			lblMessage.Text = "[정보] " + txtBible.Text + "는 " + GetChapterIdx() + "장까지 있습니다.";
		}

		private int GetChapterIdx()
		{
			return FileControl.FileControl.chapterIdxList.Count - 1;
		}

		private int GetVerseIdx(int chapterIdx)
		{
			if (!(chapterIdx == GetChapterIdx()))
				return (int)FileControl.FileControl.chapterIdxList[chapterIdx + 1] - (int)FileControl.FileControl.chapterIdxList[chapterIdx];
			return FileControl.FileControl.entireChapter.Count - (int)FileControl.FileControl.chapterIdxList[chapterIdx];
		}

		private void btnBibleClick(object sender, EventArgs e)
		{
			try
			{
				string temp = "";
				Button btnSenter = (Button)sender;
				if (Box.oldBibleShortcutList.Contains(btnSenter.Text))
					temp = Box.oldBibleList[Array.IndexOf(Box.oldBibleShortcutList, btnSenter.Text)];
				else if (Box.newBibleShortcutList.Contains(btnSenter.Text))
					temp = Box.newBibleList[Array.IndexOf(Box.newBibleShortcutList, btnSenter.Text)];
				else
					lblMessage.Text = "[주의] 프로그래밍중에 오타가 발생하였습니다.";

				txtBible.Text = temp;
				fc.ReadBible(cmbBibleVer.Text, btnSenter.Text);
				lblMessage.Text = "[정보] " + temp + "는 " + GetChapterIdx() + "장까지 있습니다.";
			}
			catch
			{
				lblMessage.Text = "[위험] btnBibleClick에서 오류가 발생했습니다.";
			}
			this.ActiveControl = txtChapter;
		}

		private void worshipSerchTxtChanged(object sender, EventArgs e)
		{
			string KeyPhoneme = ktp.Trans(etk.Trans(txtWorship.Text));
			string[] ranking = bac.WorshipSuggestProcess(KeyPhoneme);
			for (var i = 0; i < smartSuggest.Length; i++)
				smartSuggest[i].Text = ranking[i];
		}

		private void worshipClick(object sender, EventArgs e)
		{
			Button btnWorship = (Button)sender;
			if (btnWorship.Text == "")
			{
				lblMessage.Text = "[주의] 찬송가 / 찬양집을 검색하지 않았습니다.";
				return;
			}

			worship.WorshipRibbon wr = new worship.WorshipRibbon();

			int idx = Array.IndexOf(FileControl.FileControl.worshipList, btnWorship.Text);
			wr.CopySlide(FileControl.FileControl.worshipOriginList[idx]);
		}

		private void BibleForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		private void txtWorship_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				this.ActiveControl = smartSuggest[0];
			if (e.KeyCode == Keys.Down)
			{
				this.ActiveControl = smartSuggest[0];
			}
		}



	}
	class KeyButton : Button
	{
 		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Right:
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
					return true;
				case Keys.Shift | Keys.Right:
				case Keys.Shift | Keys.Left:
				case Keys.Shift | Keys.Up:
				case Keys.Shift | Keys.Down:
					return true;
			}
			return base.IsInputKey(keyData);
		}
		//protected override void OnKeyDown(KeyEventArgs e)
		//{
		//	base.OnKeyDown(e);
		//	switch (e.KeyCode)
		//	{
		//		case Keys.Left:
		//		case Keys.Right:
		//		case Keys.Up:
		//		case Keys.Down:
		//			if (e.Shift)
		//			{

		//			}
		//			else
		//			{
		//			}
		//			break;
		//	}
		//}
	}
}
