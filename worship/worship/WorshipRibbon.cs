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
			RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
			item.Label = "First Name";
			item.OfficeImageId = "FormControlComboBox";

			RibbonDropDownItem item2 = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
			item2.Label = "First Name";
			item2.OfficeImageId = "FormControlComboBox";

			gallery1.Items.Add(item);
			gallery1.Items.Add(item2);
			

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

		private void button2_Click(object sender, RibbonControlEventArgs e)
		{
			SlideControl sc = new SlideControl();
			sc.dodo();
		}

		private void button3_Click(object sender, RibbonControlEventArgs e)
		{
			SlideControl sc = new SlideControl();
			sc.kuku();
		}

		private void gallery1_Click(object sender, RibbonControlEventArgs e)
		{

		}
	}

}
