using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace SaveBitmap
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ���������� �� ������� ������
			saveAsBitmapBtn.Click += new EventHandler (saveAsBitmapBtn_Click);
			savePane1.Click += new EventHandler (savePane1_Click);
			savePane2.Click += new EventHandler (savePane2_Click);

			DrawAllGraph ();
		}

		/// <summary>
		/// ��������� �������� ������ 1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void savePane1_Click (object sender, EventArgs e)
		{
			SavePaneImage (0);
		}

		/// <summary>
		/// ��������� �������� ������ 2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void savePane2_Click (object sender, EventArgs e)
		{
			SavePaneImage (1);
		}


		/// <summary>
		/// ��������� �������� ������, ��������� � ������� �������
		/// </summary>
		/// <param name="index"></param>
		private void SavePaneImage (int index)
		{
			// ������ ������ ����� ����� ������� �������
			SaveFileDialog dlg = new SaveFileDialog ();
			dlg.Filter = "*.png|*.png|*.jpg; *.jpeg|*.jpg;*.jpeg|*.bmp|*.bmp|��� �����|*.*";

			if (dlg.ShowDialog () == DialogResult.OK)
			{
				// �������� ������ �� �� �������
				GraphPane pane = zedGraph.MasterPane.PaneList[index];

				// �������� ��������, ��������������� ������
				Bitmap bmp = pane.GetImage ();

				// ��������� �������� ���������� ������ Bitmap
				// ������ �������� ���������� ������ �� ����� ���������� �����
				if (dlg.FileName.EndsWith (".png"))
				{
					bmp.Save (dlg.FileName, ImageFormat.Png);
				}
				else if (dlg.FileName.EndsWith (".jpg") || dlg.FileName.EndsWith (".jpeg"))
				{
					bmp.Save (dlg.FileName, ImageFormat.Jpeg);
				}
				else if (dlg.FileName.EndsWith (".bmp"))
				{
					bmp.Save (dlg.FileName, ImageFormat.Bmp);
				}
				else
				{
					bmp.Save (dlg.FileName);
				}
			}
		}


		/// <summary>
		/// ���������� ������� �� ������ "��������� ��� �������"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void saveAsBitmapBtn_Click (object sender, EventArgs e)
		{
			// ��� ���������� �������� ����� ���������� ZedGraphControl 
			// ���������� ������� ����� SaveAsBitmap().
			// �������� � ����� ������� ������ ����� ����� ������� �� ���� ZedGraphControl
			zedGraph.SaveAsBitmap ();
		}


		/// <summary>
		/// ������� ��� ������� � ������� MasterPane
		/// </summary>
		private void DrawAllGraph ()
		{
			MasterPane masterPane = zedGraph.MasterPane;
			masterPane.PaneList.Clear ();

			int count = 2;

			for (int i = 0; i < count; i++)
			{
				// ������� ��������� ������ GraphPane, �������������� ����� ���� ������
				GraphPane pane = new GraphPane ();

				// �������� ������ �� ������
				DrawSingleGraph (pane);

				// ������� ������ � MasterPane
				masterPane.Add (pane);
			}

			// ����� ��������� ����������� ������� � MasterPane
			using (Graphics g = CreateGraphics ())
			{
				// ������� ����� ��������� � ��� ������, 
				masterPane.SetLayout (g, PaneLayout.SingleColumn);
			}

			// ������� ��� � ���������� ������
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}


		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		private void DrawSingleGraph (GraphPane pane)
		{
			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			pane.AddCurve ("", list, Color.Blue, SymbolType.None);
		}
	}
}