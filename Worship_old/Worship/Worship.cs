using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Core = Microsoft.Office.Core;

using System.Windows.Forms;

using System.IO;

using Color = System.Drawing.Color;
using ColorTranslator = System.Drawing.ColorTranslator;

namespace Worship
{
    public partial class Worship
    {
        private void Worship_Load(object sender, RibbonUIEventArgs e)
        {
			testCmb();
        }

        private void btnTest_Click(object sender, RibbonControlEventArgs e)
        {
            testCode();

        }

		private void testCmb()
		{
			
			dropDown1.Items.Add(makeItem("first"));
			dropDown1.Items.Add(makeItem("second"));

			comboBox1.Items.Add(makeItem("first"));
			comboBox1.Items.Add(makeItem("second"));
	


		}

		private RibbonDropDownItem makeItem(string str)
		{
			RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
			item.Label = str;
			return item;
		}

		private void test(object sender, RibbonControlEventArgs e)
		{
			MessageBox.Show("test");
		}

        private void testCode()
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
			PowerPoint.Slide newSlide =
				pptPresentation.Slides.AddSlide((pptPresentation.Slides.Count + 1), pptLayout);

			Color myBackgroundColor = Color.Aqua;
			int oleColor = ColorTranslator.ToOle(myBackgroundColor);
			newSlide.FollowMasterBackground = Core.MsoTriState.msoFalse;
			newSlide.Background.Fill.ForeColor.RGB = oleColor;
			//newSlide.Background.Fill.Visible = Core.MsoTriState.msoFalse;


			PowerPoint.Shape textBox = newSlide.Shapes.AddTextbox(Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, 500, 100);

			textBox.TextFrame.TextRange.Text = "teasdfst";

		}
	}
}

