namespace WindowsCoord
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
			this.label1 = new System.Windows.Forms.Label ();
			this.graphCoord = new System.Windows.Forms.TextBox ();
			this.label2 = new System.Windows.Forms.Label ();
			this.controlCoord = new System.Windows.Forms.TextBox ();
			this.label3 = new System.Windows.Forms.Label ();
			this.eventCoord = new System.Windows.Forms.TextBox ();
			this.SuspendLayout ();
			// 
			// zedGraph
			// 
			this.zedGraph.Location = new System.Drawing.Point (12, 12);
			this.zedGraph.Name = "zedGraph";
			this.zedGraph.ScrollGrace = 0;
			this.zedGraph.ScrollMaxX = 0;
			this.zedGraph.ScrollMaxY = 0;
			this.zedGraph.ScrollMaxY2 = 0;
			this.zedGraph.ScrollMinX = 0;
			this.zedGraph.ScrollMinY = 0;
			this.zedGraph.ScrollMinY2 = 0;
			this.zedGraph.Size = new System.Drawing.Size (568, 379);
			this.zedGraph.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point (12, 426);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size (227, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Координаты в системе координат графика";
			// 
			// graphCoord
			// 
			this.graphCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.graphCoord.Location = new System.Drawing.Point (408, 423);
			this.graphCoord.Name = "graphCoord";
			this.graphCoord.ReadOnly = true;
			this.graphCoord.Size = new System.Drawing.Size (172, 20);
			this.graphCoord.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point (12, 452);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size (281, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Координаты в системе координат контрола zedGraph";
			// 
			// controlCoord
			// 
			this.controlCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.controlCoord.Location = new System.Drawing.Point (408, 449);
			this.controlCoord.Name = "controlCoord";
			this.controlCoord.ReadOnly = true;
			this.controlCoord.Size = new System.Drawing.Size (172, 20);
			this.controlCoord.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point (12, 400);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size (226, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Координаты, посланные в MouseEventArgs";
			// 
			// eventCoord
			// 
			this.eventCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.eventCoord.Location = new System.Drawing.Point (408, 397);
			this.eventCoord.Name = "eventCoord";
			this.eventCoord.ReadOnly = true;
			this.eventCoord.Size = new System.Drawing.Size (172, 20);
			this.eventCoord.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (592, 481);
			this.Controls.Add (this.eventCoord);
			this.Controls.Add (this.label3);
			this.Controls.Add (this.controlCoord);
			this.Controls.Add (this.label2);
			this.Controls.Add (this.graphCoord);
			this.Controls.Add (this.label1);
			this.Controls.Add (this.zedGraph);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraph;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox graphCoord;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox controlCoord;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox eventCoord;
	}
}

