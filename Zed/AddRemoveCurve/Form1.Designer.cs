namespace AddRemoveCurve
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
			this.addBtn = new System.Windows.Forms.Button ();
			this.removeBtn = new System.Windows.Forms.Button ();
			this.SuspendLayout ();
			// 
			// zedGraph
			// 
			this.zedGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraph.Location = new System.Drawing.Point (12, 12);
			this.zedGraph.Name = "zedGraph";
			this.zedGraph.ScrollGrace = 0;
			this.zedGraph.ScrollMaxX = 0;
			this.zedGraph.ScrollMaxY = 0;
			this.zedGraph.ScrollMaxY2 = 0;
			this.zedGraph.ScrollMinX = 0;
			this.zedGraph.ScrollMinY = 0;
			this.zedGraph.ScrollMinY2 = 0;
			this.zedGraph.Size = new System.Drawing.Size (568, 418);
			this.zedGraph.TabIndex = 0;
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addBtn.Location = new System.Drawing.Point (12, 436);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size (75, 23);
			this.addBtn.TabIndex = 1;
			this.addBtn.Text = "Добавить";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler (this.addBtn_Click);
			// 
			// removeBtn
			// 
			this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removeBtn.Location = new System.Drawing.Point (93, 436);
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.Size = new System.Drawing.Size (75, 23);
			this.removeBtn.TabIndex = 1;
			this.removeBtn.Text = "Удалить";
			this.removeBtn.UseVisualStyleBackColor = true;
			this.removeBtn.Click += new System.EventHandler (this.removeBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (592, 471);
			this.Controls.Add (this.removeBtn);
			this.Controls.Add (this.addBtn);
			this.Controls.Add (this.zedGraph);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout (false);

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraph;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.Button removeBtn;
	}
}

