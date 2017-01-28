using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Magnitude
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		/// <summary>
		/// ������������ �������
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		private double f (double x)
		{
			double ky = 1.0e6;
			double kx = 1.0e9;

			if (x == 0)
			{
				return ky;
			}

			return ky * Math.Sin (x * kx) / (x * kx);
		}

		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			// �������� ������. �� ����������� � ��� ����� �������� ��������� ��������,
			// � �� ��������� - �������
			PointPairList list = new PointPairList ();

			// �������� ��������� ���������� X
			double xmin = 0;
			double xmax = 15e-9;

			for (double x = xmin; x <= xmax; x += 20e-12)
			{
				list.Add (x, f(x));
			}

			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// !!! ��������� ��������� ����������� ������� � ��������� ����� �� ����.

			// ��������� ��� X
			// !!! �������, ��� �� ��� X � ��� �������� ����� � ��
			pane.XAxis.Title.Text = "t, ��";

			// !!! ������ ������ ����������� ������� � ������� ��� X
			pane.XAxis.Title.IsOmitMag = true;

			// !!! ���� ��������� �����������, �� ������� ���������� �������� �� ��� X
			// !!! � ������ ������ �������� ����� ���������� �� 10^-9
			pane.XAxis.Scale.Mag = -9;

			// ��������� ��� Y
			// !!! ��������� �����������, �� ������� ���������� �������� �� ��� Y 
			// !!! � ������ ������ �������� ����� ���������� �� 10^0 = 1, �� ���� ��������� �� �����
			pane.YAxis.Scale.Mag = 0;

			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}
	}
}