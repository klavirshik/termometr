using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace LegendPosition
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}


		private double func1 (double x)
		{
			return -x * x + 20;
		}


		private double func2 (double x)
		{
			return -2 * x * x + 10;
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ ����� ��� ���� ������
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();

			double xmin = -5;
			double xmax = 5;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� ����� � ������
				list1.Add (x, func1 (x));
				list2.Add (x, func2 (x));
			}

			// �������� ������
			LineItem curve1 = pane.AddCurve ("-x ^ 2 + 20", list1, Color.Blue, SymbolType.None);
			LineItem curve2 = pane.AddCurve ("-2 * x ^ 2 + 10", list2, Color.Black, SymbolType.None);


			// !!!
			// ���������, ��� ������������ ������� �� ����� �������� 
			// � ���� ��������� ������ �������� ����
			pane.Legend.Position = LegendPos.Float;

			// ���������� ����� ������������� � ������� ��������� ���� �������
			pane.Legend.Location.CoordinateFrame = CoordType.ChartFraction;

			// ������ ������������, ������������ �������� �� ����� �������� ����������
			// � ������ ������ �� ����� ����������� ������� ������ �����
			pane.Legend.Location.AlignH = AlignH.Right;
			pane.Legend.Location.AlignV = AlignV.Bottom;

			// ������ ���������� ������� 
			// �������� 0.02f, ����� ��� ��������� ����� ����� ����� � ��������
			pane.Legend.Location.TopLeft = new PointF (1.0f - 0.02f, 1.0f - 0.02f);			


			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}

}