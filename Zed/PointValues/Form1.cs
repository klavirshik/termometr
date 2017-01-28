using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace PointValues
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ������� ����� ����������� ��������� ��� ��������� ������� �� ������
			zedGraph.IsShowPointValues = true;

			// ����� ������������ ������� PointValueEvent, ����� �������� ������ ������������� ���������
			zedGraph.PointValueEvent += 
				new ZedGraphControl.PointValueHandler (zedGraph_PointValueEvent);

			DrawGraph ();
		}


		/// <summary>
		/// ���������� ������� PointValueEvent.
		/// ������ ������� ������, ������� ����� �������� �� ����������� ���������
		/// </summary>
		/// <param name="sender">����������� ���������</param>
		/// <param name="pane">������ ��� ���������</param>
		/// <param name="curve">������, ����� ������� ��������� ������</param>
		/// <param name="iPt">����� ����� � ������</param>
		/// <returns>����� ������� ������������ ������</returns>
		string zedGraph_PointValueEvent (ZedGraphControl sender, 
			GraphPane pane, 
			CurveItem curve, 
			int iPt)
		{
			// ������� �����, ����� ������� ���������
			PointPair point = curve[iPt];

			// ���������� ������
			string result = string.Format ("X: {0:F3}\nY: {1:F3}", point.X, point.Y);

			return result;
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

			// �������� ������ �����
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