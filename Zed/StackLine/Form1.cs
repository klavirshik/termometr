using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace StackLine
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private double f (double x, double h)
		{
			return Math.Sin (x) * Math.Cos (2.0 * x) + h;
		}

		private void DrawGraph ()
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// ������ ����� ��� ������������ �������
			PointPairList list_center = new PointPairList ();

			// ������ �����, ��� ��������, ������������� ������������ ������ ��������
			PointPairList list_top = new PointPairList ();
			PointPairList list_bottom = new PointPairList ();

			double xmin = 0;
			double xmax = 3 * Math.PI;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.2)
			{
				// ����� ��� ��������� (������������) �������
				list_center.Add (x, f (x, 5));

				// ����� ��� ������� �������. 
				// ��� ���������� ������������� ������������ ����������� �������
				list_bottom.Add (x, -2);

				// ����� ��� �������� �������. 
				// ��� ���������� ������������� ������������ ����������� �������,
				// �.�. ������������ �������.
				list_top.Add (x, 4);				
			}

			// ������� ������
			// ������� ���������� ������ �����, 
			// �.�. �� ���������� ������������� ����������

			// ������� ������� ����������� ������
			LineItem curve_center = pane.AddCurve ("Center Line", list_center, 
				Color.Black, SymbolType.None);

			// ������ ������ ������,
			// ���������� �������� ������������� ������������ ������������ �������
			LineItem curve_bottom = pane.AddCurve ("Bottom Line", list_bottom, 
				Color.Black, SymbolType.Circle);

			// ������� ������, 
			// ���������� �������� ������������� ������������ ������� �������
			LineItem curve_top = pane.AddCurve ("Top Line", list_top, 
				Color.Black, SymbolType.Star);

			// ��������� ������� ��� ������� ������
			curve_top.Line.Fill = new ZedGraph.Fill (Color.Yellow, Color.White, 90.0f);

			// ������, ��� �� ���������� ������������� ����������
			pane.LineType = LineType.Stack;
			
			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}