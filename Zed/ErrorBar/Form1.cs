using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ErrorBar
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
			return Math.Sin (x) * Math.Cos (2 * x);
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList dataList = new PointPairList ();

			// !!! �������� ������ ��������
			PointPairList errorList = new PointPairList ();

			double xmin = 0;
			double xmax = 4 * Math.PI;

			// �������� ������� ��� ���� �����
			double error = 0.1;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.3)
			{
				double curry = f (x);

				// ������� � ������ �����
				dataList.Add (x, curry);

				// !!! ������� ������ ��� ���� �� �����
				// ������ �������� - ���������� X,
				// ������ �������� - ����������� �������� ���������
				// ������ �������� - ������������ �������� ���������
				errorList.Add (x, curry - error, curry + error);
			}

			// �������� ������ � �������
			LineItem myCurve = pane.AddCurve ("Data", dataList, Color.Blue, SymbolType.Circle);
			myCurve.Symbol.Size = 5.0f;

			// !!! �������� ������, ������������ �������
			ErrorBarItem errorCurve = pane.AddErrorBar ("Error", errorList, Color.Black);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}