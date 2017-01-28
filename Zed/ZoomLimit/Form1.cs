using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ZoomLimit
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ���������� �� ���������, ������������ � ���, 
			// ��� ������������ �������� ������� �������
			zedGraph.ZoomEvent += new ZedGraphControl.ZoomEventHandler (zedGraph_ZoomEvent);

			DrawGraph ();
		}

		/// <summary>
		/// ���������� ������� ��� ��������� ��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="oldState"></param>
		/// <param name="newState"></param>
		void zedGraph_ZoomEvent (ZedGraphControl sender, ZoomState oldState, ZoomState newState)
		{
			GraphPane pane = sender.GraphPane;

			// ��� �������� ������� ����� ������������ ��������������� 
			// ������ � ������� ���������� ������� �������

			// �������� �������� ��� ������ ��� � 
			// ��� ������������� ������������� ���

			if (pane.XAxis.Scale.Min <= -100)
			{
				pane.XAxis.Scale.Min = -100;
			}

			if (pane.XAxis.Scale.Max >= 100)
			{
				pane.XAxis.Scale.Max = 100;
			}

			if (pane.YAxis.Scale.Min <= -1)
			{
				pane.YAxis.Scale.Min = -1;
			}

			if (pane.YAxis.Scale.Max >= 2)
			{
				pane.YAxis.Scale.Max = 2;
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

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			// � ��������� ������ �� ������� ����� �������� ������ ����� �������, 
			// ������� ��������� � ��������� �� ����, ������������� �� ���������
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}