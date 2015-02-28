using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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
		Boolean wallpaper = false;
		Boolean BlackWhite = false;
		public void makeBibleSlide(string str, int verse, int chapter, string bible)
		{
			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			PowerPoint.CustomLayout customLayout = currentPT.SlideMaster.CustomLayouts[PowerPoint.PpSlideLayout.ppLayoutTitleOnly];
			int fontSize = 50;
			string fontName = "Adobe 고딕 Std B";

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
			if (!wallpaper)
			{
				bibleLayout.Shapes.AddPicture(@"D:\성경송출\BackgroundDB\wallpaper.png", Core.MsoTriState.msoFalse, Core.MsoTriState.msoTrue, 0, 0, slidewidth, slideHeight);
				bibleLayout.Shapes.Title.ZOrder(Core.MsoZOrderCmd.msoBringToFront);
				wallpaper = true;

				bibleLayout.Shapes.Title.TextFrame.DeleteText();
				bibleLayout.Shapes.Title.TextFrame.TextRange.Font.Name = fontName;
				
			}

			PowerPoint.Slide newSlide = currentPT.Slides.AddSlide((currentPT.Slides.Count + 1), bibleLayout);

			

			//성경 구절 다자인
			newSlide.Shapes.Title.TextEffect.Alignment = Core.MsoTextEffectAlignment.msoTextEffectAlignmentLeft;
			newSlide.Shapes.Title.TextFrame.TextRange.Font.Size = fontSize;

			newSlide.Shapes.Title.TextFrame.TextRange.Text = str;
			newSlide.Shapes.Title.Width = newSlide.Shapes.Title.Width - 50;
			newSlide.Shapes.Title.Left = newSlide.Shapes.Title.Left + 50;

			//newSlide.Shapes.Title.TextFrame.TextRange.Text = verse + " " + str;
			newSlide.Shapes.Title.TextFrame.AutoSize = PowerPoint.PpAutoSize.ppAutoSizeShapeToFitText;
			newSlide.Shapes.Title.TextFrame.TextRange.Font.Name = fontName;
			newSlide.Shapes.Title.Top = (slideHeight / 2) - (newSlide.Shapes[1].Height / 2);

			//절 디자인
			PowerPoint.Shape verseText = newSlide.Shapes.AddTextbox(Core.MsoTextOrientation.msoTextOrientationHorizontal, 0, 0, 100, 100);
			verseText.TextEffect.Alignment = Core.MsoTextEffectAlignment.msoTextEffectAlignmentRight;
			verseText.TextFrame.TextRange.Text = verse + "";
			verseText.TextFrame.TextRange.Font.Size = fontSize;
			verseText.TextFrame.TextRange.Font.Name = fontName;
			verseText.TextFrame.AutoSize = PowerPoint.PpAutoSize.ppAutoSizeShapeToFitText;
			float verseTop = newSlide.Shapes.Title.Top;
			float verseLeft = newSlide.Shapes.Title.Left - verseText.Width - 10;
			verseText.Left = verseLeft;
			verseText.Top = verseTop;

			//성경, 장 디자인
			PowerPoint.Shape chapterText = newSlide.Shapes.AddTextbox(Core.MsoTextOrientation.msoTextOrientationHorizontal, 0, 0, 250, 100);
			chapterText.TextEffect.Alignment = Core.MsoTextEffectAlignment.msoTextEffectAlignmentCentered;
			chapterText.TextFrame.TextRange.Text = bible + " " + chapter + "장";
			chapterText.TextFrame.TextRange.Font.Size = 25;
			chapterText.TextFrame.TextRange.Font.Name = fontName;
			chapterText.TextFrame.AutoSize = PowerPoint.PpAutoSize.ppAutoSizeShapeToFitText;
			float chapterTop = 20;
			float chapterLeft = slidewidth - chapterText.Width - 20;
			chapterText.Left = chapterLeft;
			chapterText.Top = chapterTop;
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


		public void dodo()
		{
			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			PowerPoint.CustomLayout custom = currentPT.Slides[1].CustomLayout;
			PowerPoint.Slide newSlide = currentPT.Slides.AddSlide(1, custom);
			newSlide.Shapes.Title.Title = "타이틀";
			newSlide.Shapes.Title.TextFrame.AutoSize = PowerPoint.PpAutoSize.ppAutoSizeShapeToFitText;
			newSlide.Shapes.Title.TextFrame.TextRange.Text = "TITLETEXT";
			float t = newSlide.Shapes.Title.Top;
			newSlide.Shapes.Title.TextEffect.Text = @"한글이로세";
			newSlide.Shapes.Title.TextEffect.FontName = @"나눔고딕";
			newSlide.Shapes.Title.TextFrame.TextRange.Font.Name = @"나눔고딕";
			newSlide.Shapes.Title.TextFrame.TextRange.Font.Size = 100;
		}

		public void kuku()
		{
			PowerPoint.Application CurrentApplication = Globals.ThisAddIn.Application;
			PowerPoint.Presentation currentPT = CurrentApplication.ActivePresentation;
			PowerPoint.CustomLayout custom = currentPT.Slides[1].CustomLayout;
			float width = currentPT.Slides[1].Shapes[1].Width;
			float height = currentPT.Slides[1].Shapes[1].Height;
			MessageBox.Show("width : " + width + " | height : " + height);
		}
	}
}
