using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BoundedRanges
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
			GraphPane pane = zedGraph.GraphPane;
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
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// ��������� ����� �������� ��������� �� ��� X, ����� ���������� �������� �������
			// �������� �� ��������� ������������ �������
			pane.XAxis.Scale.Min = 10;
			pane.XAxis.Scale.Max = 50;

			// �� ��� Y ��������� �������������� ������ ��������
			pane.YAxis.Scale.MinAuto = true;
			pane.YAxis.Scale.MaxAuto = true;

			// !!! ��������� �������� ��������� IsBoundedRanges ��� true.
			// !!! ��� ��������, ��� ��� �������������� ������� �������� 
			// !!! ����� ��������� ������ ������� �������� �������
			pane.IsBoundedRanges = true;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����.
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}