using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;

namespace worship
{
	public partial class WorshipRibbon
	{
		BibleForm bf = new BibleForm();
		
		private void WorshipRibbon_Load(object sender, RibbonUIEventArgs e)
		{

		}

		private void editBox1_TextChanged(object sender, RibbonControlEventArgs e)
		{
			//MessageBox.Show("change?");
			editBox1.Text = "kuuk";
		}

		private void button1_Click(object sender, RibbonControlEventArgs e)
		{
			
			bf.Show();
		}

		private void WorshipRibbon_Close(object sender, EventArgs e)
		{
			bf.Close();
		}
	}

}
