using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using ZedGraph;

namespace WindowsCoord
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ���������� �� �������, ����������� ��� ������� ������ ����
			// �������� ��������, ��� ������������� �� ������� MouseDownEvent, � �� MouseDown
			zedGraph.MouseDownEvent += 
				new ZedGraphControl.ZedMouseEventHandler (zedGraph_MouseDownEvent);
			
			DrawGraph ();
		}


		/// <summary>
		/// ���������� ������� ������� �� ������ ����
		/// </summary>
		/// <returns>����� ���������� true, ���� ����� ��������� ���������� ���������� ��������� ������� (����� ������������ ����, ������ ��������� � �.�.), � false, ���� ��������� ������� ������ ���� ����������</returns>
		bool zedGraph_MouseDownEvent (ZedGraphControl sender, MouseEventArgs e)
		{
			GraphPane pane = zedGraph.GraphPane;

			// ����������, ������� �������� � �������
			Point eventPoint = new Point (e.X, e.Y);
			eventCoord.Text = string.Format ("({0}; {1})", eventPoint.X, eventPoint.Y);

			// ����������, ������������� � ������� ��������� �������
			double graphX, graphY;

			// ����������� ���������� �� ������� ���������, ��������� � ��������� zedGraph 
			// � ������� ���������, ��������� � ��������
			pane.ReverseTransform (new PointF (e.X, e.Y), out graphX, out graphY);
			graphCoord.Text = string.Format ("({0:F3}; {1:F3})", graphX, graphY);

			// ����������� � �������� ������� �� ������� ��������� �������
			// � ������� ��������� ��������. 
			// ������ �������� �� �� ��������, ��� � � eventPoint 
			// (� ��������� �� ����������� ����������)
			// ��������� �������� CoordType.AxisXYScale ����������, 
			// � ����� ������� ��������� ������ ����������, ���������� � ������ ���� ����������.
			PointF controlPoint = pane.GeneralTransform (new PointF ((float)graphX, 
				(float)graphY), 
				CoordType.AxisXYScale);

			controlCoord.Text = string.Format ("({0}; {1})", controlPoint.X, controlPoint.Y);

			return false;
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
			GraphPane pane = zedGraph.GraphPane;

			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;

			// ������� ������ ������
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

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}