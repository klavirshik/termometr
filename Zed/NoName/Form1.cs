using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace NoName
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		// ������������ �������
		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}

		/// <summary>
		/// ���������� ������
		/// </summary>
		/// <param name="pane"></param>
		/// <param name="k">����������� ��� �������</param>
		/// <param name="name">��� ������</param>
		/// <param name="color">���� ��� �������</param>
		private void AddCurve (GraphPane pane, double k, string name, Color color)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (k * x));
			}

			LineItem myCurve = pane.AddCurve (name, list, color, SymbolType.None);

			// ������� �����������
			myCurve.Line.IsSmooth = true;
		}

		private void DrawGraph ()
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			// �������� ��� ������ � ������� �������
			// ������ ��� ������� ����� ����������� � �������
			AddCurve (pane, 1.0, "k = 1", Color.Blue);
			AddCurve (pane, 0.5, "k = 0.5", Color.Black);

			// ���� ������ � ������� �� ����� ������������
			AddCurve (pane, 0.1, "", Color.Red);

			// ������� ��� � ������
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}
		
	}
}