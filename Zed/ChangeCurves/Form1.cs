using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ChangeCurves
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraphs ();
		}

		/// <summary>
		/// ������� ��� ���������
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		/// <summary>
		/// ���������� ����������� �������
		/// </summary>
		private void DrawGraphs ()
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// ��� ������ ����� ��� ���� ��������
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ����� �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list1.Add (x, f (x));
				list2.Add (x, f (x * 0.5));
			}

			// ������� ��� ������, �� �� ����� ��������� ��������� �� ���
			pane.AddCurve ("", list1, Color.Blue, SymbolType.None);
			pane.AddCurve ("", list2, Color.Red, SymbolType.None);

			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}


		/// <summary>
		/// ���������� ������� �� ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeBtn_Click (object sender, EventArgs e)
		{
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������������ ������.
			// ������ � ������ �������������� �� �������� 
			// (���� ��� ������������� �������� this[] ��� ������� � ������ �� ������)
			ModifyCurve (pane.CurveList[0], 1.1);
			ModifyCurve (pane.CurveList[1], 0.9);

			// ������� ��� � ��� ������
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}


		/// <summary>
		/// ��������� ������. ���������� X �������� ����������, � ���������� Y ���������� �� k
		/// </summary>
		private static void ModifyCurve (CurveItem curve, double k)
		{
			// �������� ����� ������ ����� ��� ������
			PointPairList newlist = new PointPairList ();

			// ���������� �� ���� ������ �� ������
			for (int i = 0; i < curve.Points.Count; i++)
			{
				// �������� ����� ������ �����
				newlist.Add (curve.Points[i].X, curve.Points[i].Y * k);
			}

			// ������� ������ ����� � ������
			curve.Points = newlist;
		}
	}
}