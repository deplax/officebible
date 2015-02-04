namespace Worship
{
    partial class Worship : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Worship()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tabWorship = this.Factory.CreateRibbonTab();
			this.grpTest = this.Factory.CreateRibbonGroup();
			this.btnTest = this.Factory.CreateRibbonButton();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.dropDown1 = this.Factory.CreateRibbonDropDown();
			this.comboBox1 = this.Factory.CreateRibbonComboBox();
			this.tabWorship.SuspendLayout();
			this.grpTest.SuspendLayout();
			this.group1.SuspendLayout();
			// 
			// tabWorship
			// 
			this.tabWorship.Groups.Add(this.grpTest);
			this.tabWorship.Groups.Add(this.group1);
			this.tabWorship.Label = "Worship";
			this.tabWorship.Name = "tabWorship";
			// 
			// grpTest
			// 
			this.grpTest.Items.Add(this.btnTest);
			this.grpTest.Label = "grpTest";
			this.grpTest.Name = "grpTest";
			// 
			// btnTest
			// 
			this.btnTest.Label = "btnTest";
			this.btnTest.Name = "btnTest";
			this.btnTest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnTest_Click);
			// 
			// group1
			// 
			this.group1.Items.Add(this.dropDown1);
			this.group1.Items.Add(this.comboBox1);
			this.group1.Label = "group1";
			this.group1.Name = "group1";
			// 
			// dropDown1
			// 
			this.dropDown1.Label = "dropDown1";
			this.dropDown1.Name = "dropDown1";
			this.dropDown1.ShowLabel = false;
			// 
			// comboBox1
			// 
			this.comboBox1.Label = "comboBox1";
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.test);
			// 
			// Worship
			// 
			this.Name = "Worship";
			this.RibbonType = "Microsoft.PowerPoint.Presentation";
			this.Tabs.Add(this.tabWorship);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Worship_Load);
			this.tabWorship.ResumeLayout(false);
			this.tabWorship.PerformLayout();
			this.grpTest.ResumeLayout(false);
			this.grpTest.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();

        }

        #endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab tabWorship;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpTest;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTest;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown1;
		internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBox1;
    }

    partial class ThisRibbonCollection
    {
        internal Worship Worship
        {
            get { return this.GetRibbon<Worship>(); }
        }
    }
}
