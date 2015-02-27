using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Core = Microsoft.Office.Core;
using Color = System.Drawing.Color;
using ColorTranslator = System.Drawing.ColorTranslator;

namespace worship
{
	class InnerBox
	{
		public static string DBroot = FileControl.Box.DBroot;
		public static string ListRoot = FileControl.Box.ListRoot;
	}
	class SlideControl
	{
		public void makeBibleSlide(string str, int verse)
		{
			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			PowerPoint.CustomLayout customLayout = currentPT.SlideMaster.CustomLayouts[PowerPoint.PpSlideLayout.ppLayoutTitleOnly];
			//PowerPoint.Slide newSlide = currentPT.Slides.AddSlide((currentPT.Slides.Count + 1), currentPT.SlideMaster.CustomLayouts._Index(6));

			//Color myBackgroundColor = Color.Beige;
			//int oleColor = ColorTranslator.ToOle(myBackgroundColor);
			//newSlide.FollowMasterBackground = Core.MsoTriState.msoFalse;
			//newSlide.Background.Fill.ForeColor.RGB = oleColor;

			//currentPT.SlideMaster.Shapes.AddPicture(@"D:\성경송출\BackgroundDB\BooksoftheBible-1440x900.jpg", Core.MsoTriState.msoFalse, Core.MsoTriState.msoTrue, 0, 0);
			//특정 슬라이드 마스터에 그림을 추가한다.
			float slideHeight = currentPT.PageSetup.SlideHeight;
			float slidewidth = currentPT.PageSetup.SlideWidth;
			PowerPoint.CustomLayout bibleLayout = currentPT.SlideMaster.CustomLayouts._Index(6);
			bibleLayout.Shapes.AddPicture(@"D:\성경송출\BackgroundDB\wallpaper.png", Core.MsoTriState.msoFalse, Core.MsoTriState.msoTrue, 0, 0, slidewidth, slideHeight);
			PowerPoint.Slide newSlide = currentPT.Slides.AddSlide((currentPT.Slides.Count + 1), bibleLayout);

			currentPT.SlideMaster.CustomLayouts.Add(currentPT.SlideMaster.CustomLayouts.Count + 1);
			//PowerPoint.CustomLayout customLayout2 = currentPT.SlideMaster.CustomLayouts._Index(currentPT.SlideMaster.CustomLayouts.Count + 1);
			//customLayout2.Shapes[1].TextFrame.TextRange.Text = "영어english";
			//customLayout2.Shapes[1].TextFrame.TextRange.Font.Name = "Adobe 고딕 Std B";



			//성경 구절 다자인
			newSlide.Shapes[1].TextEffect.Alignment = Core.MsoTextEffectAlignment.msoTextEffectAlignmentCentered;
			newSlide.Shapes[1].TextFrame.TextRange.Font.Size = 60;
			newSlide.Shapes[1].TextFrame.TextRange.Text = verse + " " + str;
			newSlide.Shapes[1].TextFrame.TextRange.Font.Name = "Adobe 고딕 Std B";
			newSlide.Shapes[1].Top = (slideHeight / 2) - (newSlide.Shapes[1].Height / 2);

			//PowerPoint.Shape textBox = newSlide.Shapes.AddTextbox(Core.MsoTextOrientation.msoTextOrientationHorizontal, 10, 10, 200, 200);
			//textBox.TextFrame.TextRange.Text = "english한글";
			//textBox.TextFrame.TextRange.Font.Size = 20;
			//textBox.TextFrame.TextRange.Font.Name = "Adobe 고딕 Std B";

			//newSlide.Shapes[1].TextEffect.FontName = "Adobe 고딕 Std B";
			//newSlide.Shapes[1].TextFrame.DeleteText();
			//newSlide.Shapes[1].TextFrame.TextRange.InsertAfter("english.한글");
			//newSlide.Shapes[1].TextFrame.TextRange.Font.Name = "Adobe 고딕 Std B";


		}

		public void dodo()
		{
			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			currentPT.Slides[2].Shapes[1].TextFrame.TextRange.InsertAfter("한글을 추가했다.");
			currentPT.Slides[2].Shapes[1].TextFrame.TextRange.Font.Name = "나눔고딕";
		}

		public void CopySlide(string str, Boolean worship)
		{
			string filePath = "";
			if (worship)
				filePath = InnerBox.ListRoot + "\\" + "찬양집" + "\\" + str;
			else
				filePath = InnerBox.ListRoot + "\\" + "새찬송가" + "\\" + str;

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
	}
}
