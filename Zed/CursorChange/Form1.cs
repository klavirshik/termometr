using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace CursorChange
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ���������� �� �������, ������� ����������� ��� ��������� �������
			zedGraph.CursorChanged += new EventHandler (zedGraph_CursorChanged);

			DrawGraph ();
		}


		/// <summary>
		/// ������� ��� ��������� �������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void zedGraph_CursorChanged (object sender, EventArgs e)
		{
			// �������� ZedGraph'� �������� ������ ��� ��������� ������� �� �������, 
			// � ����� ��� ����������� �������.
			// � ����� ������� ������ ����� "��������"
			// ���� ������ �� "��������", �� ��������� ������ ������� � ���� �������.
			// ���� ����� ��������� �������� ������ � ����� ������, 
			// �� ���������� ������ ������ �������. 

			if (!zedGraph.Capture)
			{
				zedGraph.Cursor = Cursors.Arrow;
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
			GraphPane pane = zedGraph.GraphPane;

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