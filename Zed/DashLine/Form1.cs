using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;
using System.Drawing.Drawing2D;

namespace DashLine
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

			pane.CurveList.Clear ();

			// �������� ��� ������ � ������� �������
			AddCurve1 (pane);
			AddCurve2 (pane);
			AddCurve3 (pane);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			// � ��������� ������ �� ������� ����� �������� ������ ����� �������, 
			// ������� ��������� � ��������� �� ����, ������������� �� ���������
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}

		/// <summary>
		/// ���������� ������ � ���� �����
		/// </summary>
		/// <param name="pane"></param>
		private void AddCurve1 (GraphPane pane)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (x));
			}

			LineItem myCurve = pane.AddCurve ("Curve 1", list, Color.Blue, SymbolType.None);

			// ���������� ����������������� �����, �������� ������ �������.
			// ������������� ����� ��������� � System.Drawing.Drawing2D.DashStyle.
			myCurve.Line.Style = DashStyle.Dot;

			// ������, ��� ������ ������ ���� �������, 
			// ����� ���������� ����� ����� ��������� �����������.
			// ��� ���������� ��-�� ����, ��� ��� ������������� �����������
			// ZedGraph ����� ������ ������� ����� �������� ���������� �� ��������.
			myCurve.Line.IsSmooth = true;
		}


		/// <summary>
		/// ���������� ������ � ���� ��������������� �����
		/// </summary>
		/// <param name="pane"></param>
		private void AddCurve2 (GraphPane pane)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (2 * x));
			}

			LineItem myCurve = pane.AddCurve ("Curve 2", list, Color.Black, SymbolType.None);

			// ���������� ����������������� �����, �������� ������ � ���� ��������������� �����.
			myCurve.Line.Style = DashStyle.DashDot;

			// ������, ��� ������ ������ ���� �������
			myCurve.Line.IsSmooth = true;
		}

		/// <summary>
		/// ���������� ������ ������������� ������
		/// </summary>
		/// <param name="pane"></param>
		private void AddCurve3 (GraphPane pane)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (0.1 * x));
			}

			LineItem myCurve = pane.AddCurve ("Curve 3", list, Color.Brown, SymbolType.None);

			// ��������� ���� ����� ��������� �����
			myCurve.Line.Style = DashStyle.Custom;

			// ����� �������� ����� 10 �.�. (1 �.� = 1/72 inch)
			myCurve.Line.DashOn = 10.0f;

			// ����� �������� ����� ����������
			myCurve.Line.DashOff = 3.0f;

			// ������, ��� ������ ������ ���� �������
			myCurve.Line.IsSmooth = true;
		}
	}
}