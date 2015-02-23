using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;

using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Core = Microsoft.Office.Core;

using System.IO;

using Color = System.Drawing.Color;
using ColorTranslator = System.Drawing.ColorTranslator;

namespace worship
{
	public partial class WorshipRibbon
	{
		BibleForm form = new BibleForm();
		private void WorshipRibbon_Load(object sender, RibbonUIEventArgs e)
		{
			
			tab1.Groups[0].Visible = true;
			//tab1.Groups[0].Items[1].Visible = false;
			//group1.Items[0].Visible = false;
			int t;
			t = group1.Items.Count();
			
		
			//MessageBox.Show(t.ToString());

		}

		private void editBox1_TextChanged(object sender, RibbonControlEventArgs e)
		{
			//MessageBox.Show("change?");
			editBox1.Text = "kuuk";
		}

		private void button1_Click(object sender, RibbonControlEventArgs e)
		{
			form.Show();

			//testCode();
			
		}

		public void testCode(string str)
		{

			// Create a PowerPoint application object.
			PowerPoint.Application appPPT = Globals.ThisAddIn.Application;

			// Create a new PowerPoint presentation.
			PowerPoint.Presentation pptPresentation = appPPT.ActivePresentation;

			PowerPoint.CustomLayout pptLayout = default(PowerPoint.CustomLayout);
			if ((pptPresentation.SlideMaster.CustomLayouts._Index(7) == null))
			{
				pptLayout = pptPresentation.SlideMaster.CustomLayouts._Index(1);
			}
			else
			{
				pptLayout = pptPresentation.SlideMaster.CustomLayouts._Index(7);
			}

			// Create newSlide by using pptLayout.
			//PowerPoint.Slide newSlide = pptPresentation.Slides.AddSlide((pptPresentation.Slides.Count + 1), pptLayout);
			PowerPoint.CustomLayout customLayout = pptPresentation.SlideMaster.CustomLayouts[PowerPoint.PpSlideLayout.ppLayoutTitleOnly];
			PowerPoint.Slide newSlide = pptPresentation.Slides.AddSlide(1, pptPresentation.SlideMaster.CustomLayouts._Index(6));

			Color myBackgroundColor = Color.Beige;
			int oleColor = ColorTranslator.ToOle(myBackgroundColor);
			newSlide.FollowMasterBackground = Core.MsoTriState.msoFalse;
			newSlide.Background.Fill.ForeColor.RGB = oleColor;
			//newSlide.Background.Fill.Visible = Core.MsoTriState.msoFalse;

			newSlide.Shapes[1].TextEffect.Alignment = Core.MsoTextEffectAlignment.msoTextEffectAlignmentCentered;
			newSlide.Shapes[1].TextFrame.TextRange.Text = str;
			newSlide.Shapes[1].Top = 200;


			//PowerPoint.Shape textBox = newSlide.Shapes.AddTextbox(Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, 500, 100);
			
			//PowerPoint.Shape textBox2 = newSlide.Shapes.AddTitle();
			//textBox2.TextFrame.TextRange.Text = "kukuku";
			//textBox2.Title = "text";


			//textBox.TextFrame.TextRange.Text = "teasdfst";
		
		}

		public string kuku(string bibleVer, string bible, int chapter, int verse)
		{
			FileControl.FileControl pg = new FileControl.FileControl();
			return pg.GetBibleVerse(bibleVer, bible, chapter, verse);
		}
	}
}
