using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Titles
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}


		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		private void DrawGraph ()
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// !!!
			// ������� ���� ������� �� ��� X
			pane.XAxis.Title.Text = "��� X";

			// ������� ��������� ������ ��� ��� X
			pane.XAxis.Title.FontSpec.IsUnderline = true;
			pane.XAxis.Title.FontSpec.IsBold = false;
			pane.XAxis.Title.FontSpec.FontColor = Color.Blue;

			// ������� ����� �� ��� Y
			pane.YAxis.Title.Text = "��� Y";

			// ������� ����� ��������� �������
			pane.Title.Text = "Sinc";

			// � ���������� ������ ������� ������� ������� ������
			pane.Title.FontSpec.Fill.Brush = new SolidBrush (Color.Red);
			pane.Title.FontSpec.Fill.IsVisible = true;

			// ������� ����� �� ����������
			pane.Title.FontSpec.IsBold = false;

			
			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			// �������� ������
			pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}