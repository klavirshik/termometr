namespace ResetScale
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
			this.zedGraph = new ZedGraph.ZedGraphControl ();
			this.reset = new System.Windows.Forms.Button ();
			this.SuspendLayout ();
			// 
			// zedGraph
			// 
			this.zedGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraph.Location = new System.Drawing.Point (12, 3);
			this.zedGraph.Name = "zedGraph";
			this.zedGraph.ScrollGrace = 0;
			this.zedGraph.ScrollMaxX = 0;
			this.zedGraph.ScrollMaxY = 0;
			this.zedGraph.ScrollMaxY2 = 0;
			this.zedGraph.ScrollMinX = 0;
			this.zedGraph.ScrollMinY = 0;
			this.zedGraph.ScrollMinY2 = 0;
			this.zedGraph.Size = new System.Drawing.Size (568, 404);
			this.zedGraph.TabIndex = 0;
			// 
			// reset
			// 
			this.reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.reset.Location = new System.Drawing.Point (468, 436);
			this.reset.Name = "reset";
			this.reset.Size = new System.Drawing.Size (112, 23);
			this.reset.TabIndex = 1;
			this.reset.Text = "ְגעמלאסרעאב";
			this.reset.UseVisualStyleBackColor = true;
			this.reset.Click += new System.EventHandler (this.reset_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (592, 471);
			this.Controls.Add (this.reset);
			this.Controls.Add (this.zedGraph);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout (false);

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraph;
		private System.Windows.Forms.Button reset;
	}
}

