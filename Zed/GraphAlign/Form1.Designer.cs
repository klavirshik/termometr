namespace GraphAlign
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container ();
			this.zedGraph1 = new ZedGraph.ZedGraphControl ();
			this.zedGraph2 = new ZedGraph.ZedGraphControl ();
			this.SuspendLayout ();
			// 
			// zedGraph1
			// 
			this.zedGraph1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraph1.Location = new System.Drawing.Point (12, 12);
			this.zedGraph1.Name = "zedGraph1";
			this.zedGraph1.ScrollGrace = 0;
			this.zedGraph1.ScrollMaxX = 0;
			this.zedGraph1.ScrollMaxY = 0;
			this.zedGraph1.ScrollMaxY2 = 0;
			this.zedGraph1.ScrollMinX = 0;
			this.zedGraph1.ScrollMinY = 0;
			this.zedGraph1.ScrollMinY2 = 0;
			this.zedGraph1.Size = new System.Drawing.Size (568, 201);
			this.zedGraph1.TabIndex = 0;
			// 
			// zedGraph2
			// 
			this.zedGraph2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraph2.Location = new System.Drawing.Point (12, 219);
			this.zedGraph2.Name = "zedGraph2";
			this.zedGraph2.ScrollGrace = 0;
			this.zedGraph2.ScrollMaxX = 0;
			this.zedGraph2.ScrollMaxY = 0;
			this.zedGraph2.ScrollMaxY2 = 0;
			this.zedGraph2.ScrollMinX = 0;
			this.zedGraph2.ScrollMinY = 0;
			this.zedGraph2.ScrollMinY2 = 0;
			this.zedGraph2.Size = new System.Drawing.Size (568, 201);
			this.zedGraph2.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (592, 471);
			this.Controls.Add (this.zedGraph2);
			this.Controls.Add (this.zedGraph1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout (false);

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraph1;
		private ZedGraph.ZedGraphControl zedGraph2;
	}
}

