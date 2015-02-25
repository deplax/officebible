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

		public void makeBibleSlide(string str)
		{
			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			PowerPoint.CustomLayout customLayout = currentPT.SlideMaster.CustomLayouts[PowerPoint.PpSlideLayout.ppLayoutTitleOnly];
			PowerPoint.Slide newSlide = currentPT.Slides.AddSlide((currentPT.Slides.Count + 1), currentPT.SlideMaster.CustomLayouts._Index(6));

			Color myBackgroundColor = Color.Beige;
			int oleColor = ColorTranslator.ToOle(myBackgroundColor);
			newSlide.FollowMasterBackground = Core.MsoTriState.msoFalse;
			newSlide.Background.Fill.ForeColor.RGB = oleColor;

			//성경 구절 다자인
			newSlide.Shapes[1].TextEffect.Alignment = Core.MsoTextEffectAlignment.msoTextEffectAlignmentCentered;
			newSlide.Shapes[1].TextEffect.FontName = @"나눔고딕";
			newSlide.Shapes[1].TextEffect.FontSize = 40;
			newSlide.Shapes[1].TextFrame.TextRange.Text = str;
			newSlide.Shapes[1].Top = 200;
		
		}


		public void CopySlide(string str)
		{
			string filePath = "";
			filePath = InnerBox.ListRoot + "\\" + "찬양집" + "\\" + str;

			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentations currentPTs = CurrentApplication.Presentations;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			PowerPoint.Presentation copyPT = currentPTs.Open(filePath, Core.MsoTriState.msoTrue, Core.MsoTriState.msoTrue, Core.MsoTriState.msoFalse);

			for (var i = 1; i <= copyPT.Slides.Count; i++)
			{
				copyPT.Slides[i].Copy();
				currentPT.Slides.Paste(currentPT.Slides.Count + 1).Design = copyPT.Slides[i].Design;
			}
		}

		private void WorshipRibbon_Close(object sender, EventArgs e)
		{
			bf.Close();
		}
	}
	class InnerBox
	{
		public static string DBroot = @"D:\성경송출\BibleDB";
		public static string ListRoot = @"D:\성경송출";
	}
}
