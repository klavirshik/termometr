using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace Selection
{
	public partial class Form1 : Form
	{
		// �������� ������ ��� ���������
		CurveItem _myCurve;

		public Form1 ()
		{
			InitializeComponent ();

			// !!!
			// �������� ����� ������
			zedGraph.IsEnableSelection = true;

			// �������� ������ ����� � ������� ����� ������ ����
			zedGraph.SelectButtons = MouseButtons.Left;

			// ��� ���� ������� �������� ������� �� ����
			zedGraph.SelectModifierKeys = Keys.None;

			// �������� ���������������, ��� ��� �� ��������� 
			// ��� ���� ���������� ����� ������ ���� ��� �������������� ������
			zedGraph.IsEnableZoom = false;

			// ���������� �� �������, ������� ���������� ��� ������ ��� ������ ������ ������
			zedGraph.Selection.SelectionChangedEvent += 
				new EventHandler (Selection_SelectionChangedEvent);			

			DrawGraph ();
		}


		/// <summary>
		/// ���������� ������� ������/������ ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Selection_SelectionChangedEvent (object sender, EventArgs e)
		{
			// � ������� �������� ������ CurveItem.IsSelected 
			// ����� ����������, ������� ������ ������ ��� ���.
			// ����� ����� ��������������� ��������� zedGraph.Selection,
			// ������� ������ ������ ��������� � ������ ������ ������			
			if (_myCurve.IsSelected)
			{
				Text = "������ �������";
			}
			else
			{
				Text = "������ �� �������";
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

			// �������� ������
			_myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}