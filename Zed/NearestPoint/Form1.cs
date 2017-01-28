using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace NearestPoint
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}


		/// <summary>
		/// ��������� ������� MouseClick - ���� �� �������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zedGraph_MouseClick (object sender, MouseEventArgs e)
		{
			// ���� ����� ��������� ������, ����� � ������� ��� ���������� ����
			CurveItem curve;

			// ���� ����� �������� ����� ����� ������, ��������� � ����� �����
			int index;

			GraphPane pane = zedGraph.GraphPane;

			// ������������ ���������� �� ����� ����� �� ������ � ��������, 
			// ��� ������� ��� ���������, ��� ���� ����� � ����������� ������.
			GraphPane.Default.NearestTol = 10;

			bool result = pane.FindNearestPoint (e.Location, out curve, out index);

			if (result)
			{
				// ����������� ���������� �� ����� ����� �� ������ �� ��������� NearestTol

				// ������� ����� �� ������, ������ ������� ��������� ����
				PointPairList point = new PointPairList ();

				point.Add (curve[index]);

				// ������, ��������� �� ����� �����. ����� ����� �������� ����� ������
				LineItem curvePount = pane.AddCurve ("",
					new double[] { curve[index].X },
					new double[] { curve[index].Y },
					Color.Blue,
					SymbolType.Circle);

				// 
				curvePount.Line.IsVisible = false;

				// ���� ���������� ����� - �������
				curvePount.Symbol.Fill.Color = Color.Blue;

				// ��� ���������� - �������� �������
				curvePount.Symbol.Fill.Type = FillType.Solid;

				// ������ �����
				curvePount.Symbol.Size = 7;
			}
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

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������ � ��������� "Sinc", 
			// ������� ����� ���������� ������� ������ (Color.Blue),
			// ������� ����� ���������� �� ����� (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// ������� ����������� �����
			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			// � ��������� ������ �� ������� ����� �������� ������ ����� �������, 
			// ������� ��������� � ��������� �� ����, ������������� �� ���������
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}