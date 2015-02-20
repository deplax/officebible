using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace Worship
{

    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
        public PowerPoint.Presentation OpenPPTSession()
        {

            // Create a PowerPoint application object.
            PowerPoint.Application appPPT = new PowerPoint.Application();

            // Create a new PowerPoint presentation.
            PowerPoint.Presentation pptPreso = appPPT.Presentations.Add();

            return pptPreso;
        }

        public PowerPoint.Slide AddPPTSlide(PowerPoint.Presentation pptPreso)
        {

            // Define pptLayout as the "Blank" layout of the default presentation template.
            // If another template is set as default, select the first layout.
            PowerPoint.CustomLayout pptLayout = default(PowerPoint.CustomLayout);
            if ((pptPreso.SlideMaster.CustomLayouts._Index(7) == null))
            {
                pptLayout = pptPreso.SlideMaster.CustomLayouts._Index(1);
            }
            else
            {
                pptLayout = pptPreso.SlideMaster.CustomLayouts._Index(7);
            }

            // Create newSlide by using pptLayout.
            PowerPoint.Slide newSlide =
                pptPreso.Slides.AddSlide((pptPreso.Slides.Count + 1), pptLayout);

            return newSlide;
        }
    }
}
