using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace GraphAlign
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraphs ();
		}

		private double f1 (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		private double f2 (double x)
		{
			double k = 1000;

			if (x == 0)
			{
				return k;
			}

			return k * Math.Sin (x) / x;
		}


		private void DrawGraphs ()
		{
			// ������� ������ ��� ���������
			GraphPane pane1 = zedGraph1.GraphPane;
			GraphPane pane2 = zedGraph2.GraphPane;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane1.CurveList.Clear ();
			pane2.CurveList.Clear ();

			// ��� ������� ����������� ������� ���, 
			// ����� ������ ����� ���� Y �� �������� ������ ����������.
			// ����� ����� ����� ��������� ����� ����� �������.
			pane1.YAxis.Scale.FontSpec.Size = 10;
			pane2.YAxis.Scale.FontSpec.Size = 35;

			// �������� ������ ����� ��� ���� ��������
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list1.Add (x, f1 (x));
				list2.Add (x, f2 (x));
			}

			// �������� ������
			LineItem myCurve1 = pane1.AddCurve ("", list1, Color.Blue, SymbolType.None);
			LineItem myCurve2 = pane2.AddCurve ("", list2, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph1.AxisChange ();
			zedGraph2.AxisChange ();

			// !!! ������ �� ����� �������� �������, ������� ���������� ����� �������.
			// Chart - ��� ��������� ������, ������� �������� �� ��� ������.
			// � ������ ������ �� ������ ������������ ������� �������������� 
			// ������ ������� �������, � ���������� ����� ������������ ��������
			// ����, ����� ������ �������� ������ ����� ��� ����� ����� ������ ��������.
			// ����� ���� ������, ��� ������� ����� ����� ������ ������� �� ������.
			pane1.Chart.Rect = new RectangleF (pane2.Chart.Rect.X,
				pane2.Chart.Rect.Y,
				pane2.Chart.Rect.Width,
				pane2.Chart.Rect.Height);

			// � ����� ������ ������������ �������� ����� �������� ����� ������:
			//pane1.Chart.Rect = pane2.Chart.Rect;
			

			// ��������� ������
			zedGraph1.Invalidate ();
			zedGraph2.Invalidate ();
		}
	}
}