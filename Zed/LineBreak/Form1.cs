using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace LineBreak
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

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			// ����� �������� ��� ���������� �������
			double xmin = -20;
			double xmax = 20;

			// �� ��������� (xbreak1; xbreak2) ������ ����� ����������
			double xbreak1 = -2;
			double xbreak2 = 2;

			// ��������� ������ ����� ������� �� �������
			for (double x = xmin; x <= xbreak1; x += 0.01)
			{
				list.Add (x, f (x));
			}

			// ������� ����� �������.
			// ��������� PointPair.Missing - ��� �������� Double.MaxValue,
			// �� ����� ������ ������������ ������ PointPair.Missing.
			list.Add (PointPairBase.Missing, PointPairBase.Missing);

			// ��������� ������ ����� ������� ����� �������
			for (double x = xbreak2; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			// �������� ������, � ������� ������ ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}