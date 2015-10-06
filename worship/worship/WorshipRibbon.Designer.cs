namespace worship
{
	partial class WorshipRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public WorshipRibbon()
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
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.box1 = this.Factory.CreateRibbonBox();
			this.editBox1 = this.Factory.CreateRibbonEditBox();
			this.editBox2 = this.Factory.CreateRibbonEditBox();
			this.box2 = this.Factory.CreateRibbonBox();
			this.editBox3 = this.Factory.CreateRibbonEditBox();
			this.editBox4 = this.Factory.CreateRibbonEditBox();
			this.button1 = this.Factory.CreateRibbonButton();
			this.button2 = this.Factory.CreateRibbonButton();
			this.button3 = this.Factory.CreateRibbonButton();
			this.group2 = this.Factory.CreateRibbonGroup();
			this.gallery1 = this.Factory.CreateRibbonGallery();
			this.tab1.SuspendLayout();
			this.group1.SuspendLayout();
			this.box1.SuspendLayout();
			this.box2.SuspendLayout();
			this.group2.SuspendLayout();
			// 
			// tab1
			// 
			this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.tab1.Groups.Add(this.group1);
			this.tab1.Groups.Add(this.group2);
			this.tab1.Label = "Worship";
			this.tab1.Name = "tab1";
			// 
			// group1
			// 
			this.group1.Items.Add(this.box1);
			this.group1.Items.Add(this.button1);
			this.group1.Items.Add(this.button2);
			this.group1.Items.Add(this.button3);
			this.group1.Label = "Bible";
			this.group1.Name = "group1";
			// 
			// box1
			// 
			this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
			this.box1.Items.Add(this.editBox1);
			this.box1.Items.Add(this.editBox2);
			this.box1.Items.Add(this.box2);
			this.box1.Name = "box1";
			// 
			// editBox1
			// 
			this.editBox1.Label = "Bible";
			this.editBox1.Name = "editBox1";
			this.editBox1.SizeString = "WWWWWWW";
			this.editBox1.SuperTip = "성경을 입력해 주세요";
			this.editBox1.Text = null;
			this.editBox1.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editBox1_TextChanged);
			// 
			// editBox2
			// 
			this.editBox2.Label = "chapter";
			this.editBox2.Name = "editBox2";
			this.editBox2.SizeString = "www";
			this.editBox2.Text = null;
			// 
			// box2
			// 
			this.box2.Items.Add(this.editBox3);
			this.box2.Items.Add(this.editBox4);
			this.box2.Name = "box2";
			// 
			// editBox3
			// 
			this.editBox3.Label = "Verse";
			this.editBox3.Name = "editBox3";
			this.editBox3.SizeString = "WW";
			this.editBox3.Text = null;
			// 
			// editBox4
			// 
			this.editBox4.Label = " ";
			this.editBox4.Name = "editBox4";
			this.editBox4.SizeString = "WW";
			this.editBox4.Text = null;
			// 
			// button1
			// 
			this.button1.Label = "button1";
			this.button1.Name = "button1";
			this.button1.ShowImage = true;
			this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Label = "button2";
			this.button2.Name = "button2";
			this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Label = "button3";
			this.button3.Name = "button3";
			// 
			// group2
			// 
			this.group2.Items.Add(this.gallery1);
			this.group2.Label = "group2";
			this.group2.Name = "group2";
			// 
			// gallery1
			// 
			this.gallery1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.gallery1.Label = "gallery1";
			this.gallery1.Name = "gallery1";
			this.gallery1.ShowImage = true;
			this.gallery1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.gallery1_Click);
			// 
			// WorshipRibbon
			// 
			this.Name = "WorshipRibbon";
			this.RibbonType = "Microsoft.PowerPoint.Presentation";
			this.Tabs.Add(this.tab1);
			this.Close += new System.EventHandler(this.WorshipRibbon_Close);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.WorshipRibbon_Load);
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.box1.ResumeLayout(false);
			this.box1.PerformLayout();
			this.box2.ResumeLayout(false);
			this.box2.PerformLayout();
			this.group2.ResumeLayout(false);
			this.group2.PerformLayout();

		}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
		internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editBox1;
		internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editBox2;
		internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editBox3;
		internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editBox4;
		internal Microsoft.Office.Tools.Ribbon.RibbonBox box2;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
		internal Microsoft.Office.Tools.Ribbon.RibbonGallery gallery1;
	}

	partial class ThisRibbonCollection
	{
		internal WorshipRibbon WorshipRibbon
		{
			get { return this.GetRibbon<WorshipRibbon>(); }
		}
	}
}
