using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace AxisTicks
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

			// ������� ���� �������� �����, ����� ���������� �������� �����
			pane.XAxis.Scale.FontSpec.Size = 10;
			pane.YAxis.Scale.FontSpec.Size = 10; 

			// !!!
			// ������� ����� �� ��� X ����� ���� � �������� 5
			pane.XAxis.Scale.MajorStep = 5.0;

			// ������ ����� ����� ���� � �������� 1
			// ����� �������, ����� �������� ������� ����� 5 ������� ��� 4 �����
			pane.XAxis.Scale.MinorStep = 1.0;

			// ������� ����� �� ��� Y ����� ���� � �������� 0.1
			pane.YAxis.Scale.MajorStep = 0.1;

			// � ������ ����� - � �������� 0.05
			// ����� �������� ������� �� ��� Y ����� ��� ������� ��� ���� �����
			pane.YAxis.Scale.MinorStep = 0.05;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -30;
			double xmax = 30;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// ������� ������ �� ����
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}