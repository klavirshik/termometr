using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace EditPoints
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();
			
			// !!!
			// ���������� ����� ����� ����� � ������� ������� ������ ����...
			zedGraph.EditButtons = MouseButtons.Middle;

			// ... � ��� ������� ������� Alt.
			zedGraph.EditModifierKeys = Keys.Alt;

			// ����� ����� ����������, ��� �� �����������,...
			zedGraph.IsEnableHEdit = true;

			// ... ��� � �� ���������.
			zedGraph.IsEnableVEdit = true;

			// ���������� �� �������, ���������� ����� ����������� �����
			zedGraph.PointEditEvent += 
				new ZedGraphControl.PointEditHandler (zedGraph_PointEditEvent);

			DrawGraph ();
		}


		/// <summary>
		/// ���������� ������� ����������� �����.
		/// ��� ����������� �����, ���������� � ��� ������������ � ��������� ����
		/// </summary>
		/// <param name="sender">��������� ZedGraph</param>
		/// <param name="pane">������ � ��������</param>
		/// <param name="curve">������, ����� ������� �����������</param>
		/// <param name="iPt">����� �����</param>
		/// <returns>����� ������ ���������� ������</returns>
		string zedGraph_PointEditEvent (ZedGraphControl sender, 
			GraphPane pane, CurveItem curve, int iPt)
		{
			string title = string.Format ("�����: {0}. ����� ����������: ({1}; {2})",
				iPt, curve[iPt].X, curve[iPt].Y);

			this.Text = title;

			return title;
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
			for (double x = xmin; x <= xmax; x += 1)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.Circle);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}